using System;

namespace Linkediner.Models
{
    [Serializable]
    public class Experience
    {
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public TimeSpan WorkDuration
        {
            get { return EndTime - StartTime; }
        }
    }
}