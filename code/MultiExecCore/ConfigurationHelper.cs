using Microsoft.Extensions.Configuration;
using MultiExec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiExecCore
{
    internal class ConfigurationHelper
    {
        private readonly IConfigurationRoot _Config;
        public Configurations Configuration { get; private set; }

        public ConfigurationHelper()
        {
            _Config = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddXmlFile("appsettings.xml")
                           .Build();

            Configuration = ReadSettingsFromConfig();
        }

        private Configurations ReadSettingsFromConfig()
        {
            Configuration = new ();
            var operationConfigs = _Config.GetSection("operation").GetChildren();

            Configuration.Setting.NewWindow = bool.Parse(_Config.GetSection("settings")["newWindow"]);
            Configuration.Setting.Parallel = bool.Parse(_Config.GetSection("settings")["parallel"]);

            foreach (var operationConfig in operationConfigs)
            {
                Operation operation = new()
                {
                    Id = int.Parse(operationConfig["id"]),
                    Name = operationConfig["name"],
                    Order = int.Parse(operationConfig["order"]),
                    Enabled = bool.Parse(operationConfig["enabled"]),
                    CreateDate = DateTime.Parse(operationConfig["createDate"])
                };

                var stepsConfigs = operationConfig.GetSection("step").GetChildren();

                foreach (var stepsConfig in stepsConfigs)
                {
                    Step step = new()
                    {
                        Id = int.Parse(stepsConfig["id"]),
                        Name = stepsConfig["name"],
                        OperationId = operation.Id,
                        Order = int.Parse(stepsConfig["order"]),
                        Enabled = bool.Parse(stepsConfig["enabled"]),
                        CreateDate = DateTime.Parse(stepsConfig["createDate"]),
                        FileName = stepsConfig["fileName"],
                        Path = stepsConfig["Path"]
                    };

                    operation.Steps.Add(step);
                }

                Configuration.Operations.Add(operation);
            }

            return Configuration;
        }
    }
}
