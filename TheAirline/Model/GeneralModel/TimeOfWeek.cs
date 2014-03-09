using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAirline.Model.GeneralModel
{
    
    //Data structure for a specific time in a week
    public struct TimeOfWeek
    {
        private DayOfWeek _WeekDay;
        private int _Hour;
        private int _Minutes;
        private GameTimeZone _TimeZone;

        public DayOfWeek WeekDay { get { return _WeekDay; } }
        public int Hour { get { return _Hour; } }
        public int Minutes { get { return _Minutes; } }
        public GameTimeZone TimeZone { get { return _TimeZone; } }

        public TimeOfWeek(DayOfWeek Day, int hh, int mm, GameTimeZone LocalTimeZone)
        {
            _WeekDay = Day;
            _Hour = hh;
            _Minutes = mm;
            _TimeZone = LocalTimeZone;
        }

        public void addTimeSpan (TimeSpan AddTime)
        {
            this._Hour += AddTime.Hours;

            if (this._Hour >= 24)
            {
                this._WeekDay++;
                this._Hour -= 24;
            }

            this._Minutes += AddTime.Minutes;

            if(this._Minutes >= 60)
            {
                this._Hour++;
                this._Minutes -= 60;
            }

 
        }
        // Adds a given TimeSpan to the TimeofWeek

        public void subtractTimeSpan (TimeSpan SubstractTime)
        {
            this._Hour -= SubstractTime.Hours;

            if (this._Hour < 0)
            {
                this._WeekDay--;
                this._Hour += 24;
            }

            this._Minutes -= SubstractTime.Minutes;

            if (this._Minutes < 0)
            {
                this._Hour--;
                this._Minutes -= 60;
            }
        }
        // Substracts a given Time from the TimeofWeek

        public TimeOfWeek convertToLocalTime (GameTimeZone LocalTimeZone)
        {
            TimeOfWeek NewTime = this;
            TimeSpan TimeDiff;

            TimeDiff = this._TimeZone.UTCOffset + LocalTimeZone.UTCOffset;
            NewTime.addTimeSpan(TimeDiff);

            return NewTime;


        }
        //Converts a TimeOfWeek into a TimeOfWeek of a different TimeZone
    }
}
