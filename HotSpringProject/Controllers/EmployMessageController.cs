using HotSpringProject.Entity;
using HotSpringProject.hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotSpringProject.hubs;

namespace HotSpringProject.Controllers
{
    public class EmployMessageController : Controller
    {
        private int userId=0;
        // GET: Chat 站内信
        public ActionResult MesIndex()
        {
            return View();
        }
        public ActionResult MesWrite()
        {
            return View();
        }
        public List<EmployMessage> GetMessage()
        {
            
            string query = $"SELECT sender_id,part,link,recipients_id,send_time FROM dbo.Employ_Message where recipients_id={userId}";
            using (var sqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HS"].ConnectionString))
            {
                sqlconnection.Open();
                using (SqlCommand cmd = new SqlCommand(query, sqlconnection))
                {
                    cmd.Notification = null;
                    //实例化一个数据监听依赖
                    SqlDependency dependency = new SqlDependency(cmd);
                    dependency.OnChange += new OnChangeEventHandler(dependency_Onchange);
                    if (sqlconnection.State == ConnectionState.Closed)
                        sqlconnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<EmployMessage> list = new List<EmployMessage>();
                    while (reader.Read())
                    {
                        EmployMessage m = new EmployMessage();
                        //m.id = Convert.ToInt32(reader["id"]);
                        m.send_time = (DateTime?)reader["send_time"];
                        m.link = (string)reader["link"];
                        m.part = (string)reader["part"];
                        m.sender_id = Convert.ToInt32(reader["sender_id"]);
                        m.recipients_id = Convert.ToInt32(reader["recipients_id"]);
                        list.Add(m);
                    }
                    return list;
                }

            }

        }
        public void dependency_Onchange(object sender, SqlNotificationEventArgs e)
        {
            //监听数据改变
            if (e.Type == SqlNotificationType.Change)
            {
                var list = GetMessage();
                //调用广播将学生总人数推送
                EmpMessageHub.Show(list.Count());
                EmpMessageHub.List(list);
            }
        }
        public ActionResult GetNumber()
        {
           
            return Content(GetMessage().Count().ToString());
        }
        public JsonResult GetList()
        {
            EmployEmp user = (EmployEmp)HttpContext.Session["User"];
            userId = user.id;
            return Json(GetMessage(), JsonRequestBehavior.AllowGet);
        }
    }
}