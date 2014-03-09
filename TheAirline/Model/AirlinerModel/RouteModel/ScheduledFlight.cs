using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAirline.Model.AirportModel;
using TheAirline.Model.GeneralModel;
using TheAirline.Model.GeneralModel.Helpers;

namespace TheAirline.Model.AirlinerModel.RouteModel
{
    //Class for a ScheduledFlight (Needed for a more detailed passenger/demand model)
     public class ScheduledFlight
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
        public double DistanceToDestination {get; set;}
        public double FlightDistance { get; set; }

        public ScheduledFlight(string FlightNumber, Airport OriginA, Airport DestinationA)
        {
            this.FlightNum = FlightNumber;
            this._Origin = OriginA;
            this._Destination = DestinationA;
            this.DistanceToDestination = MathHelpers.GetDistance(OriginA.Profile.Coordinates.convertToGeoCoordinate(), DestinationA.Profile.Coordinates.convertToGeoCoordinate());
        }

        public bool schedule(TimeOfWeek DepartureT, FleetAirliner Aircraft)
        {
            bool SchedulingSuccessful = true;

            this._UsedAircraft = Aircraft; //maybe check if Aircraft is really avialable -> or ensure via GUI
            this._DepartureTime = DepartureT;
            this._ArrivalTime = DepartureT.addTimeSpan(MathHelpers.GetFlightTime(this._Origin, this._Destination, this._UsedAircraft.Airliner.Type));
            
            if(!SchedulingSuccessful || !AirportHelpers.tryGetRunwaySlot(this._Origin, this._UsedAircraft.Airliner.Type.MinRunwaylength, this._DepartureTime))
            {
                SchedulingSuccessful = false;
            }

            // Check Gate availabiltiy at Origin

            if(!SchedulingSuccessful || !AirportHelpers.tryGetRunwaySlot(this._Destination, this._UsedAircraft.Airliner.Type.MinRunwaylength, this._DepartureTime))
            {
                SchedulingSuccessful = false;
            }
            
            // Check Gate availabiltiy at Origin
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
