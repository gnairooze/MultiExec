using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiExec.Models
{
    public class Configurations
    {
        public Settings Setting { get; set; }
        public List<Operation> Operations { get; set; }

        public Configurations()
        {
            Setting = new();
            Operations = new();
        }
    }
}
