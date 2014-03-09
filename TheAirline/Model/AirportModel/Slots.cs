using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAirline.Model.GeneralModel;
using System.Collections;

namespace TheAirline.Model.AirportModel
{
    // A basic model for slots at an Airport
    class Slots
    {
        private int[][] FreeSlots = new int [6][];
        private int SlotInterval;
        private int SlotsInInterval;
        private int _FreeSlotCount; //Instead of doing a foreach query the number of free slots is saved in the obj for performance

        public int FreeSlotCount { get{return _FreeSlotCount; }}
        
        public Slots (int IntervalMin, int SlotsPerInterval)
        {
            int IntervalsPerDay;
            
            this.SlotInterval = IntervalMin;
            
            
            IntervalsPerDay = 60 / IntervalMin * 24;

            _FreeSlotCount = IntervalsPerDay *7;

            int[] iniArray = new int[IntervalsPerDay];

            foreach (int i in iniArray)
            {
                iniArray[i] = SlotsPerInterval;
            }

            for (int x = 0; x<= 6; x++)
            {
                FreeSlots[x] = iniArray;
            }                               
           
        }
        

        private int convertTimeToIndex (TimeOfWeek time) 
        {
            int value;

            value = time.Hour * 60 / SlotInterval;
            value += time.Minutes / SlotInterval;
            
            return value;
        }
        //Converts a time of a day into the index neccessary to access the array

        public bool tryBlockSlot (TimeOfWeek BlockingTime)

        {
            int DayIndex =(int)BlockingTime.WeekDay;
            int TimeIndex = convertTimeToIndex(BlockingTime);

            if (FreeSlots[DayIndex][TimeIndex] > 0)
            {
                FreeSlots[DayIndex][TimeIndex]--;
                _FreeSlotCount--;
                return true;
            }
            else { return false; }            
        }
        // Trys to block a slot at a specific time returns true if blocking was successful, false if there were no free slots 

        public void releaseSlot(TimeOfWeek ReleaseTime)
        {
            int DayIndex = (int)ReleaseTime.WeekDay;
            int TimeIndex = convertTimeToIndex(ReleaseTime);

             if (FreeSlots[DayIndex][TimeIndex] < SlotsInInterval)
             {
                 FreeSlots[DayIndex][TimeIndex]++;
                 _FreeSlotCount--;
             }
        }
        // Releases a Slot at a given time

        

    }
}
