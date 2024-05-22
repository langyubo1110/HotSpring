using HotSpringProject.Entity;
using HotSpringProject.hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace HotSpringProject.DependencyDB
{
    public class GetMessageDepency
    {
        private int _userId;

        public GetMessageDepency(int userId)
        {
            _userId = userId;
        }
        public List<EmployMessageVO> GetMessage()
        {

            string query = $"SELECT state,sender_id,part,link,recipients_id,send_time FROM dbo.Employ_Message where recipients_id={_userId}";
            string sql = $"SELECT m.state,sender.name AS sender_name, recipients.name AS recipients_name,m.part,m.link,m.recipients_id,m.sender_id,m.send_time" +
                $" FROM dbo.Employ_Message AS m   JOIN dbo.Employ_Emp AS sender ON m.sender_id = sender.id" +
                $"    JOIN dbo.Employ_Emp AS recipients ON m.recipients_id = recipients.id" +
                $"  WHERE m.recipients_id ={_userId}";
            using (var sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HS"].ConnectionString))
            {
                sqlconnection.Open();
                List<EmployMessageVO> queryList = new List<EmployMessageVO>();
                List<EmployMessageVO> sqlList = new List<EmployMessageVO>();
                using (SqlCommand queryCmd = new SqlCommand(query, sqlconnection))
                {
                    queryCmd.Notification = null;
                    //实例化一个数据监听依赖
                    SqlDependency dependency = new SqlDependency(queryCmd);
                    dependency.OnChange += new OnChangeEventHandler(dependency_Onchange);
                    if (sqlconnection.State == ConnectionState.Closed)
                        sqlconnection.Open();

                    SqlDataReader queryReader = queryCmd.ExecuteReader();


                    List<EmployMessageVO> list = new List<EmployMessageVO>();
                    while (queryReader.Read())
                    {
                        EmployMessageVO m = new EmployMessageVO();
                        m.send_time = (DateTime?)queryReader["send_time"];
                        m.link = Convert.ToString(queryReader["link"]);
                        m.part = (string)queryReader["part"];
                        m.sender_id = Convert.ToInt32(queryReader["sender_id"]);
                        m.recipients_id = Convert.ToInt32(queryReader["recipients_id"]);
                        m.state= Convert.ToInt32(queryReader["state"]);
                        queryList.Add(m);
                    }
                    queryReader.Close();
                }

                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlconnection))
                {

                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        EmployMessageVO m = new EmployMessageVO();
                        m.send_time = (DateTime?)sqlReader["send_time"];
                        m.link = Convert.ToString(sqlReader["link"]);
                        m.part = (string)sqlReader["part"];
                        m.sender_id = Convert.ToInt32(sqlReader["sender_id"]);
                        m.recipients_id = Convert.ToInt32(sqlReader["recipients_id"]);

                        // 获取发送者和接收者的名称，并赋值给属性
                        m.sender_name = (string)sqlReader["sender_name"];
                        m.recipients_name = (string)sqlReader["recipients_name"];
                        m.state = Convert.ToInt32(sqlReader["state"]);
                        sqlList.Add(m);
                    }

                    sqlReader.Close();
                }
                //foreach (EmployMessageVO queryItem in queryList)
                //{
                //    EmployMessageVO sqlItem = sqlList.FirstOrDefault(item => item.sender_id == queryItem.sender_id && item.recipients_id == queryItem.recipients_id);
                //    if (sqlItem != null)
                //    {
                //        sqlItem.sender_name = queryItem.sender_name;
                //        sqlItem.recipients_name = queryItem.recipients_name;
                //    }
                //}
                return sqlList;
            }

        }
        public void dependency_Onchange(object sender, SqlNotificationEventArgs e)
        {
            //监听数据改变
            if (e.Type == SqlNotificationType.Change)
            {
                var list = GetMessage();
                var list1 = GetMessage().Where(s => s.state == 0).ToList();
                //调用广播将学生总人数推送
                //调用广播将学生总人数推送
                EmpMessageHub.Show(list1.Count());
                EmpMessageHub.List(list);
            }
        }
    }
}