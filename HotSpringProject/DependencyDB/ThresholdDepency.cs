using HotSpringProject.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotSpringProject.DependencyDB
{
    //public class ThresholdDepency
    //{

    //    public List<RepoGoodsStock> SendThreshold()
    //    {

    //        string query = $@"select id, goods_name, goods_number, threshold, factory, imgurl, create_time, update_time, goods_type 
    //        from dbo.Repo_Goods_Stock ";
    //        using (var sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HS"].ConnectionString))
    //        {
    //            sqlconnection.Open();
    //            List<RepoGoodsStock> queryList = new List<RepoGoodsStock>();
    //            using (SqlCommand queryCmd = new SqlCommand(query, sqlconnection))
    //            {
    //                queryCmd.Notification = null;
    //                //实例化一个数据监听依赖
    //                SqlDependency dependency = new SqlDependency(queryCmd);
    //                dependency.OnChange += new OnChangeEventHandler(dependency_Onchange1);
    //                if (sqlconnection.State == ConnectionState.Closed)
    //                    sqlconnection.Open();
    //                SqlDataReader queryReader = queryCmd.ExecuteReader();
    //                List<RepoGoodsStock> list = new List<RepoGoodsStock>();
    //                while (queryReader.Read())
    //                {
    //                    RepoGoodsStock goodsStock = new RepoGoodsStock();
    //                    goodsStock.goods_name = (string);
    //                    goodsStock.goods_number = (string)queryReader["link"];
    //                    goodsStock.threshold = (string)queryReader["part"];
    //                    goodsStock.factory = Convert.ToInt32(queryReader["sender_id"]);
    //                    goodsStock.imgurl = Convert.ToInt32(queryReader["recipients_id"]);
    //                    goodsStock.create_time = (DateTime)queryReader["send_time"];
    //                    goodsStock.update_time = (DateTime)queryReader["send_time"];
    //                    goodsStock.goods_type = Convert.ToInt32(queryReader["recipients_id"]);

    //                    queryList.Add(m);
    //                }
    //                queryReader.Close();
    //            }

    //            using (SqlCommand sqlCmd = new SqlCommand(sql, sqlconnection))
    //            {

    //                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

    //                while (sqlReader.Read())
    //                {
    //                    EmployMessageVO m = new EmployMessageVO();
    //                    m.send_time = (DateTime?)sqlReader["send_time"];
    //                    m.link = (string)sqlReader["link"];
    //                    m.part = (string)sqlReader["part"];
    //                    m.sender_id = Convert.ToInt32(sqlReader["sender_id"]);
    //                    m.recipients_id = Convert.ToInt32(sqlReader["recipients_id"]);

    //                    // 获取发送者和接收者的名称，并赋值给属性
    //                    m.sender_name = (string)sqlReader["sender_name"];
    //                    m.recipients_name = (string)sqlReader["recipients_name"];

    //                    sqlList.Add(m);
    //                }

    //                sqlReader.Close();
    //            }
              
    //            return sqlList;
    //        }

    //    }
    //    public void dependency_Onchange1(object sender, SqlNotificationEventArgs e)
    //    {
    //        //监听数据改变
    //        if (e.Type == SqlNotificationType.Change)
    //        {
    //            var list = SendMessage();
    //            //调用广播将学生总人数推送
    //            EmpMessageHub.Show(list.Count());
    //            EmpMessageHub.List(list);
    //        }
    //    }
    //}
}