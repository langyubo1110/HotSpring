using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class BackUpDataBaseService : IBackUpDataBaseService
    {
        public Task Execute()
        {
            // 获取当前时间
            BackupDatabase();
            return Task.CompletedTask;
        }
        public void BackupDatabase()
        {

            string databaseName = "HotSpring"; // 数据库名称
            string backupDirectory = @"D:\Download\HotSpringBackUP"; // 备份文件存储目录路径
            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }
            // 构建备份文件路径
            string backupFileName = $"{databaseName}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            string backupFilePath = Path.Combine(backupDirectory, backupFileName);

            // 执行数据库备份操作
            try
            {
                // 使用 SQL Server 提供的备份命令进行备份
                string connectionString = "data source=.;initial catalog=HotSpring;integrated security=False;User ID=sa;pwd=020609;MultipleActiveResultSets=True;App=EntityFramework\" providerName=\"System.Data.SqlClient"; // 数据库连接字符串
                string backupCommand = $"BACKUP DATABASE [{databaseName}] TO DISK = '{backupFilePath}'";

                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand(backupCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"数据库备份成功，备份文件路径：{backupFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"数据库备份失败：{ex.Message}");
            }
        }
    }
}
