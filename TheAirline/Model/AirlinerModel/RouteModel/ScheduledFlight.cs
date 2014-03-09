using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAirline.Model.AirportModel;
using TheAirline.Model.GeneralModel;

namespace TheAirline.Model.AirlinerModel.RouteModel
{
    //Class for a ScheduledFlight (Needed for a more detailed passenger/demand model)
    class ScheduledFlight
    {
        private Airport _Origin;
        private Airport _StopOver;
        private Airport _Destination;
        private TimeOfWeek _DepartureTime;
        private TimeOfWeek _ArrivalTime;
        private FleetAirliner _UsedAircraft;
       

        public string FlightNum;
        public Airport Origin { get { return _Origin; } }
        public Airport StopOver { get { return _StopOver; } }
        public Airport Destination { get { return _Destination; } }
        public TimeOfWeek DepartureTime { get { return _DepartureTime; } }
        public TimeOfWeek ArrivalTime { get { return _ArrivalTime; } }
        public FleetAirliner UsedAircraft { get { return _UsedAircraft; } }
        public List<ScheduledFlight> ConnectingFlights;
        public int[] AvailableSeats = new int[2];
        public int[] AvailableCargoSpace = new int[2];

        public ScheduledFlight(string FlightNumber, Airport OriginA, Airport DestinationA)
        {
            this.FlightNum = FlightNumber;
            _Origin = OriginA;
            _Destination = DestinationA;
        }

        public bool schedule(TimeOfWeek DepartureT, FleetAirliner Aircraft)
        {
            bool SchedulingSuccessful = true;

            _UsedAircraft = Aircraft; //maybe check if Aircraft is really avialable -> or ensure via GUI

            //Calculate ArrivalTime with DepTime and Aircraft
            //Check Gates and Runway Slots at DepAirport and DepTime
            //Check Gates and Runway Slots at ArrivalAirport
            //Calculate AvailableSeats and Cargospace from Aircraft
            //Check for ConnectingFlights

            if (SchedulingSuccessful)
            {
                return true;
            }
            else
            {
                //Undo changed properties
                return false;
            }

        }
        // Schedules a ScheduledFlight

        public int[] bookPassengers(int[] RequestedSeats)
        {
            int[] BookedSeats = new int[2];

            foreach (int i in RequestedSeats)
            {
                BookedSeats[i] = Math.Min(RequestedSeats[i], AvailableSeats[i]);
                AvailableSeats[i] = AvailableSeats[i] - BookedSeats[i];
            }

            return BookedSeats;
        }
        // Books passengers onto flight
    }
    
}
