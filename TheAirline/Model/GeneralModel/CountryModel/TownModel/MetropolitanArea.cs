using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAirline.Model.AirportModel;

namespace TheAirline.Model.GeneralModel.CountryModel.TownModel
{
    class MetropolitanArea
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public int Population { get; set; }
        public List<Airport> AirportsInRegion { get; set; }
                       

    }
}
