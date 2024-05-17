using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HotSpringProjectService
{
    public class EmployEmpService : IEmployEmpService
    {
        private readonly IEmployEmpRepository _EmployEmpRepository;

        public EmployEmpService(IEmployEmpRepository employemprepository )
        {
            _EmployEmpRepository = employemprepository;
        }
        public ResMessage Add(EmployEmp employemp)
        {
            //工号自动生成
            //查询数据库中最大id数据对象，将id+1，为基础号码
            //再用000000加上基础号码拼成工号
            //employemp.identity_card = "FX" ;
            int baseNumber = GenerateRandomBaseNumber();
            // 生成员工的工号
            string employeeNumber = GenerateEmployeeNumber(baseNumber);
            employemp.job_number = employeeNumber;
            employemp.onboarding_time = DateTime.Now;
            employemp.create_time = DateTime.Now;
            employemp.account_status = 1;
            employemp.last_log_time = DateTime.Now;
            int pwd = 123;
            employemp.password = GetMD5Hash(pwd.ToString());
            int flag = _EmployEmpRepository.Add(employemp);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }
        public static string GetMD5Hash(string input)
        {
            // 创建一个 MD5 实例
            using (MD5 md5 = MD5.Create())
            {
                // 将输入字符串转换为字节数组
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // 计算哈希值
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // 将字节数组转换为字符串表示
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private int GenerateRandomBaseNumber()
        {
            // 这里假设你要生成一个 6 位数的随机数作为基础号码
            Random random = new Random();
            int baseNumber = random.Next(100000, 999999); // 生成 100000 到 999999 之间的随机数

            return baseNumber;
        }

        // 生成员工工号的方法
        private string GenerateEmployeeNumber(int baseNumber)
        {
            // 根据实际情况设置工号的格式，这里以 "FX" 开头，后面跟着六位数字
            string employeeNumber = "FX" + baseNumber.ToString();

            return employeeNumber;
        }
        public ResMessage Delete(int Id)
        {
            bool flag = _EmployEmpRepository.Delete(Id);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public IEnumerable<EmployEmp> GetList()
        {
            return _EmployEmpRepository.GetList();
        }
        //public IEnumerable<EmployEmp> GetListByID(int id)
        //{
        //    return _EmployEmpRepository.GetListByID(id);
        //}
        /// <summary>
        /// 带分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ResMessage GetListByPager(EmployEmpFilter filter)
        {
            IEnumerable<EmployEmp> list = _EmployEmpRepository.GetList();
            int count = 0;
            list = MakeQuery(list, filter, out count);
            return ResMessage.Success(list, count);
        }
        public IEnumerable<EmployEmp> MakeQuery(IEnumerable<EmployEmp> list, EmployEmpFilter filter, out int count)
        {
            if (!string.IsNullOrEmpty(filter.name))
            {
                list = list.Where(x => x.name.Contains(filter.name));
            }
            if (filter.role_id != 0)
            {
                list = list.Where(x => x.role_id == filter.role_id);
            }
            count = list.Count();
            //开启分页
            if (filter.page != 0 && filter.limit != 0)
            {
                list = list.OrderBy(x => x.create_time).Skip((filter.page - 1) * filter.limit).Take(filter.limit);
            }
            return list.ToList();
        }

        public ResMessage getModel(int id)
        {
            EmployEmp model = _EmployEmpRepository.GetModel(id);
            return model != null ? ResMessage.Success(model) : ResMessage.Fail();
        }

        public ResMessage Update(EmployEmp employemp, bool isLoginRequest = false)
        {
            if (isLoginRequest==true)
            {
                DateTime currentDateTime = DateTime.Now;
                int log_count = int.Parse(employemp.log_count) + 1;
                employemp.log_count = log_count.ToString();
                employemp.last_log_time = currentDateTime;
            }
            else
            {
                employemp.onboarding_time = DateTime.Now;
                employemp.create_time = DateTime.Now;
                employemp.account_status = 1;
                employemp.last_log_time = DateTime.Now;
            }
            bool flag = _EmployEmpRepository.Update(employemp);
            return flag ? ResMessage.Success() : ResMessage.Fail();
        }

        public bool Verify(string number, string password)
        {
            string pwd = GetMD5Hash(password.ToString());
            return _EmployEmpRepository.Varfy(number, pwd);
        }

        public ResMessage Update(EmployEmp movies)
        {
            throw new NotImplementedException();
        }
    }
}
