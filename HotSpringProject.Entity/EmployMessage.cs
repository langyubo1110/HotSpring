using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Employ_Message")]
    public class EmployMessage
    {
        public int Id { get; set; }
        public DateTime? SendTime { get; set; }
        public string Part { get; set; }
        public string Link { get; set; }
        public int? SenderId { get; set; }
        public int? RecipientsId { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
