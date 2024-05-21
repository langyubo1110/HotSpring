using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProject.Entity.VO;
using HotSpringProject.hubs;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotSpringProject.DependencyDB
{
    public class ThresholdDepency
    {
        private readonly int _page;
        private readonly int _limit;
        private readonly RepoGoodsStockFilter _filter;
        private List<string> nameList = new List<string>();

        public ThresholdDepency(int page, int limit, RepoGoodsStockFilter filter)
        {
            _page = page;
            _limit = limit;
            _filter = filter;

        }
        public List<RepoGoodsStock> SendThreshold()
        {
            string sql = $@"select id, goods_name, goods_number, threshold, factory, imgurl, create_time, update_time, goods_type 
                    from dbo.Repo_Goods_Stock 
                    where CAST(threshold AS int) > goods_number";
            string query = $@"select id, goods_name, goods_number, threshold, factory, imgurl, create_time, update_time, goods_type 
            from dbo.Repo_Goods_Stock ";
            using (var sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HS"].ConnectionString))
            {
                sqlconnection.Open();
                List<RepoGoodsStock> queryList = new List<RepoGoodsStock>();
                List<RepoGoodsStock> sqlList = new List<RepoGoodsStock>();//依赖
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlconnection))
                {
                    sqlCmd.Notification = null;
                    //实例化一个数据监听依赖
                    SqlDependency dependency = new SqlDependency(sqlCmd);
                    dependency.OnChange += new OnChangeEventHandler(dependency_Onchange);
                    if (sqlconnection.State == ConnectionState.Closed)
                        sqlconnection.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        RepoGoodsStock goodsStock = new RepoGoodsStock();
                        goodsStock.goods_name = (string)sqlReader["goods_name"];
                        goodsStock.goods_number = Convert.ToInt32(sqlReader["goods_number"]);
                        goodsStock.threshold = (string)sqlReader["threshold"];
                        goodsStock.factory = (string)sqlReader["factory"];
                        goodsStock.imgurl = (string)sqlReader["imgurl"];
                        goodsStock.create_time = (DateTime)sqlReader["create_time"];
                        goodsStock.update_time = (DateTime)sqlReader["update_time"];
                        goodsStock.goods_type = Convert.ToInt32(sqlReader["goods_type"]);
                        sqlList.Add(goodsStock);
                        nameList.Add(goodsStock.goods_name);
                    }
                    sqlReader.Close();
                }
                using (SqlCommand queryCmd = new SqlCommand(query, sqlconnection))
                {
                   SqlDataReader queryReader = queryCmd.ExecuteReader();

                    while (queryReader.Read())
                    {
                        RepoGoodsStock goodsStock = new RepoGoodsStock();
                        goodsStock.goods_name = (string)queryReader["goods_name"];
                        goodsStock.goods_number = Convert.ToInt32(queryReader["goods_number"]);
                        goodsStock.threshold = (string)queryReader["threshold"];
                        goodsStock.factory = (string)queryReader["factory"];
                        goodsStock.imgurl = (string)queryReader["imgurl"];
                        goodsStock.create_time = (DateTime)queryReader["create_time"];
                        goodsStock.update_time = (DateTime)queryReader["update_time"];
                        goodsStock.goods_type = Convert.ToInt32(queryReader["goods_type"]);
                        queryList.Add(goodsStock);
                    }
                    queryReader.Close();
                }
                return queryList;
            }

        }
        public List<RepoGoodsStock> PagerAndMakeQuery(int page, int limit, RepoGoodsStockFilter filter)
        {
            List<RepoGoodsStock> list = SendThreshold();
            if (!String.IsNullOrEmpty(filter.goods_name))
            {
                list = list.Where(x => x.goods_name.Contains(filter.goods_name)).ToList();
            }
            if (filter.goods_type != null)
            {
                list = list.Where(x => x.goods_type == filter.goods_type).ToList();
            }
            int count = list.Count();
            List<RepoGoodsStock> result = list.OrderBy(x => x.id).Skip((page - 1) * limit).Take(limit).ToList();
            return result/* != null ? ResMessage.Success(result, count) : ResMessage.Fail()*/;
        }
        public void dependency_Onchange(object sender, SqlNotificationEventArgs e)
        {
            //监听数据改变
            if (e.Type == SqlNotificationType.Change)
            {
                nameList.Clear();
                var list = PagerAndMakeQuery(_page, _limit, _filter);

                //给仓管人员发信息
                int roleId = 30;
                string query = $"select id from dbo.Employ_Emp where role_id={roleId}";
                using (var sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HS"].ConnectionString))
                {
                    List<EmployEmp> emplist = new List<EmployEmp>();//链表里放的所有仓管人员id

                    sqlconnection.Open();
                    using (SqlCommand queryCmd = new SqlCommand(query, sqlconnection))
                    {
                        SqlDataReader queryReader = queryCmd.ExecuteReader();

                        while (queryReader.Read())
                        {
                            EmployEmp m = new EmployEmp();
                            m.id = Convert.ToInt32(queryReader["id"]);
                            emplist.Add(m);
                        }
                        queryReader.Close();
                    }
                    foreach (var item in emplist)
                    {
                        int recipients_id = item.id;
                        int sender_id = 1;
                        DateTime create_time = DateTime.Now;
                        DateTime send_time = DateTime.Now;
                        string link = "abcde";
                        string allName = "";
                        foreach(var name in nameList)
                        {
                            allName += name + ",";
                        }
                        allName= allName.Substring(0,allName.Length - 1);
                        string part = $"{allName}低于阈值";
                        string sql = $@"insert into dbo.Employ_Message(send_time,part,link,sender_id,recipients_id,create_time)
                        values('{send_time}', '{part}', '{link}',{sender_id },{ recipients_id},'{create_time}') ";
                        using (SqlCommand queryCmd = new SqlCommand(sql, sqlconnection))
                        {
                            int flag = queryCmd.ExecuteNonQuery();


                        }
                    }
                    sqlconnection.Close();
                }
                



               
                //调用广播将学生总人数推送
                //EmpMessageHub.Show(list.Count());
                EmpMessageHub.StockList(list);
            }
        }
    }
}