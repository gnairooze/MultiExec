using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicEventsCore
{
    public class TimeMessageEventArgs : MessageEventArgs
    {
        #region Properties
        /// <summary>
        /// Gets and sets datetime
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
        #endregion

        #region Constructors
          /// <summary>
        /// constructor with message parameter
        /// </summary>
        /// <param name="msg"></param>
        public TimeMessageEventArgs(string msg) : base(msg)
        {
        }
        /// <summary>
        /// full constructor with all paramters
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="time"></param>
        public TimeMessageEventArgs(string msg, DateTime time) : this(msg)
        {
            this.Time = time;
        }
        #endregion
    }
}
