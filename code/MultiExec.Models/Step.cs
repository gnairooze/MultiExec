using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiExec.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int OperationId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
