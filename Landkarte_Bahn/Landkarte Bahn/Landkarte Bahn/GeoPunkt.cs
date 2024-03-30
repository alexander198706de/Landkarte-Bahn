using System;
using System.Collections.Generic;
using System.Text;

namespace Landkarte_Bahn
{
    class GeoPunkt
    {
         double lon;

        public double Lon
        { get { return lon; } }

        double lat;

        public double Lat
        { get { return lat; } }

        public GeoPunkt(double lon, double lat)
        {
            this.lon = lon;
            this.lat = lat;
        }
    }
}
