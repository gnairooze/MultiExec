using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiExec.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreateDate { get; set; }

        public List<Step> Steps { get; }

        public Operation()
        {
            this.Steps = new();
        }
    }
}
