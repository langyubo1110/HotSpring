using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    //员工角色表
  public class EmployRole
    {
        [Key]
        public int Id { get; set; }
        public string role_name {  get; set; } //职位名
        public  int role {  get; set; } //是否为领导，0非，1是
        public decimal Salary {  get; set; } //底薪
        public decimal Labor_hours {  get; set; } //工时费
    }
}
