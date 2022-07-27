namespace BasicEventsCore
{
    public class ExceptionEventArgs
    {
        #region Properties
        /// <summary>
        /// Get and sets the exception
        /// </summary>
        public Exception Error
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets time
        /// </summary>
        public DateTime Time
        {
            get;
            set;
        }
        #endregion

        #region Constructors
     

        /// <summary>
        /// Full constructor with all the paarameters
        /// </summary>
        /// <param name="error"></param>
        public ExceptionEventArgs(Exception error, DateTime time)
        {
            this.Error = error;
            this.Time = time;
        }
        #endregion
    }
}