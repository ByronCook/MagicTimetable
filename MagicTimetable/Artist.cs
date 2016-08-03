using System;

namespace MagicTimetable
{
    public class Artist
    {
        public string Name { get; set; }
        public DateTime SetStartTime { get; set; }
        public DateTime SetEndTime { get; set; }
        public string Stage { get; set; }
        public int Day { get; set; }
        public string EventName { get; set; }
    }
}