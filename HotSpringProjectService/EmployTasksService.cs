using DotNet.Utilities;
using HotSpringProject.Entity.VO;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace HotSpringProjectService
{
    public class EmployTasksService : IEmployTasksService
    {
        private readonly IEquUpkeepTaskRepository _dbUp;

        public EmployTasksService(IEquUpkeepTaskRepository upkeepTaskRepository)
        {
            _dbUp = upkeepTaskRepository;
        }
        public IEnumerable<EquUpkeepTaskVO> GetList(int reportID)
        {
            IEnumerable<EquUpkeepTaskVO> list = _dbUp.QueryBySql<EquUpkeepTaskVO>($"SELECT Equ_UpKeep_Task.upkeep_time,Equ_UpKeep_Task.status,Equ_Equipment.name AS name,Equ_UpKeep_Plan.id AS equ_plan_id,Equ_UpKeep_Task.distribute_time,Equ_UpKeep_Task.QRimg,Employ_Emp.name AS people_name,Equ_UpKeep_Plan.*FROM Equ_UpKeep_Task JOIN Employ_Emp ON Equ_UpKeep_Task.exec_id = Employ_Emp.id  JOIN Equ_UpKeep_Plan ON Equ_UpKeep_Task.equ_plan_id = Equ_UpKeep_Plan.id JOIN  Equ_Equipment ON Equ_UpKeep_Task.equ_id = Equ_Equipment.id WHERE Equ_UpKeep_Task.exec_id = {reportID};");
            return list;
        }
        public ResMessage UpdateTask(int id,string data)
        {
            _dbUp.execBySql($"update Equ_Upkeep_Task set upkeep_feedback_info='{data}' , status = 1 where equ_plan_id={id}");
            return ResMessage.Success("反馈成功");
        }
    }
}
