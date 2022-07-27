using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventsCore
{
    public class MessageEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets and sets a message
        /// </summary>
        public string Message
        { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Full constructor with all the propeties of the class filled
        /// </summary>
        /// <param name="message"></param>
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
        #endregion
    }
}
