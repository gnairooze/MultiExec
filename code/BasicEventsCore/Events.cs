using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventsCore
{
    public class Events
    {
        #region Attributes
        /// <summary>
        /// Delegate for message receiption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MessageReception(object sender, MessageEventArgs e);
        /// <summary>
        /// Delegate for time message recieption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void TimeMessageReception(object sender, TimeMessageEventArgs e);
        /// <summary>
        /// Delegate for exception receiption
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ExceptionReception(object sender, ExceptionEventArgs e);

        /// <summary>
        /// Definition of event of message received
        /// </summary>
        public event MessageReception? MessageReceived;
        /// <summary>
        /// Definition of event of time message received
        /// </summary>
        public event TimeMessageReception? TimeMessageReceived;
        /// <summary>
        /// Definition of event of the exception Received
        /// </summary>
        public event ExceptionReception? ExceptionReceived;
        #endregion

        #region Methods
        /// <summary>
        /// Fire Message Received event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void FireMessageReceived(object sender, string message)
        {
            MessageReceived?.Invoke(sender, new MessageEventArgs(message));
        }
        /// <summary>
        /// Fire time message received
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="time"></param>
        public void FireTimeMessageReceived(object sender, string message, DateTime time)
        {
            TimeMessageReceived?.Invoke(sender, new TimeMessageEventArgs(message, time));
        }

        /// <summary>
        /// Fire Exception Received event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void FireExceptionReceived(object sender, Exception ex, DateTime time)
        {
            ExceptionReceived?.Invoke(sender, new ExceptionEventArgs(ex, time));
        }
        #endregion
    }
}
