using MultiExec.Models;
using System.Diagnostics;

namespace MultiExec.Business
{
    public class Manager
    {
        #region attributes
        protected readonly Configurations _Configurations;
        protected readonly BasicEventsCore.Events _events;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the basic events instance
        /// </summary>
        public BasicEventsCore.Events Events
        {
            get
            {
                return _events;
            }
        }
        #endregion

        public Manager(Configurations config)
        {
            _events = new();
            _Configurations = config;
        }

        /// <summary>
        /// Run Specific operation, specifc step
        /// </summary>
        /// <param name="opName"></param>
        public void Run()
        {
            List<Operation> orderedOperations = _Configurations.Operations.Where(o => o.Enabled == true).OrderBy(o => o.Order).ToList();

            foreach (Operation op in orderedOperations)
            {
                _events.FireTimeMessageReceived(this, $"operation {op.Id} - {op.Name} started", DateTime.Now);

                try
                {
                    List<Step> orderedSteps = op.Steps.Where(s => s.Enabled == true).OrderBy(s => s.Order).ToList();

                    if (_Configurations.Setting.Parallel)
                    {
                        RunParallelSteps(orderedSteps);
                    }
                    else
                    {
                        RunSerialSteps(orderedSteps);
                    }

                    _events.FireTimeMessageReceived(this, $"operation {op.Id} - {op.Name} completed", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _events.FireTimeMessageReceived(this, $"operation {op.Id} - {op.Name} aborted", DateTime.Now);

                    _events.FireExceptionReceived(this, ex, DateTime.Now);
                }

                _events.FireMessageReceived(this, "---------------------------------------------------------");
            }
        }

        private void RunSerialSteps(List<Step> steps)
        {
            foreach (var step in steps)
            {
                _events.FireTimeMessageReceived(this, $"operation Id {step.OperationId} - step {step.Id} - {step.Name} - order {step.Order} started", DateTime.Now);

                try
                {
                    RunCommand(step.FileName, Path.GetFullPath(step.Path));

                    _events.FireTimeMessageReceived(this, $"operation Id {step.OperationId} - step {step.Id} - {step.Name} completed", DateTime.Now);
                }
                catch
                {
                    _events.FireTimeMessageReceived(this, $"operation Id {step.OperationId} - step {step.Id} - {step.Name} - order {step.Order} aborted", DateTime.Now);

                    throw;
                }
            }
        }

        private void RunParallelSteps(List<Step> steps)
        {
            Parallel.ForEach(steps, step =>
            {
                // Peek behind the scenes to see how work is parallelized. 
                // But be aware: Thread contention for the Console slows down parallel loops!!!
                _events.FireTimeMessageReceived(this, $"Processing {step.Name} on thread {Environment.CurrentManagedThreadId}", DateTime.Now);

                #region run step
                _events.FireTimeMessageReceived(this, $"operation Id {step.OperationId} - step {step.Id} - {step.Name} - order {step.Order} started", DateTime.Now);

                try
                {
                    RunCommand(step.FileName, Path.GetFullPath(step.Path));

                    _events.FireTimeMessageReceived(this, $"operation Id {step.OperationId} - step {step.Id} - {step.Name} - order {step.Order} completeded", DateTime.Now);
                }
                catch
                {
                    _events.FireTimeMessageReceived(this, $"operation Id {step.OperationId} - step {step.Id} - {step.Name} - order {step.Order} aborted", DateTime.Now);

                    throw;
                }
                #endregion
            });
        }

        protected void RunCommand(string cmd, string path)
        {
            // Start the child process.
            Process p = new ();

            //check whether to open the new EXEs in a new window or not
            if (!_Configurations.Setting.NewWindow)
            {
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
            }
            p.StartInfo.WorkingDirectory = path;
            p.StartInfo.FileName = Path.Combine(path, cmd);
            
            p.Start();

            string output = string.Empty;

            //If the application in the same window, read the output stream
            if (!_Configurations.Setting.NewWindow)
            {
                // Read the output stream first and then wait.
                output = p.StandardOutput.ReadToEnd();
            }

            _events.FireTimeMessageReceived(this, output, DateTime.Now);

            p.WaitForExit();
        }
    }
}