using System;

namespace SoftKill
{
    public class KillSoftlyAttribute : Attribute
    {
        public int[] DegredationDate { get; set; }
        public int DegredationSeconds { get; set; }
        public int DegredationWindowDays { get; set; }
        public int[] CondemnationDate { get; set; }

        public int? GetDelay(DateTime dateTime)
        {
            var degredationDate = ArrayToDateTime(DegredationDate);
            var condemnationDate = ArrayToDateTime(CondemnationDate);
            var timeSinceCondemnation = dateTime - degredationDate;
            var windows = Convert.ToInt32(
                Math.Floor(timeSinceCondemnation.TotalDays / DegredationWindowDays));

            if (dateTime >= condemnationDate)
            {
                return -1;
            }
            
            if (windows >= 0)
            {
                return DegredationSeconds * (windows + 1);
            }

            return null;
        }

        private DateTime ArrayToDateTime(int[] dateArray)
        {
            return new DateTime(dateArray[0], dateArray[1], dateArray[2]);
        }
    }
}
