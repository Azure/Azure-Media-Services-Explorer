using System;

namespace AMSExplorer
{

    public class EDLEntryInOut
    {
        public string AssetName { get; set; }

        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }

        public TimeSpan? Duration
        {
            get
            {
                if (End != null)
                {
                    return End - Start;
                }
                else
                {
                    return null;
                }
            }
        }

        public string Description { get; set; }
    }
}