using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGeoDB.Utils
{
    public class GeoMath
    {
        public enum DistanceUnit { Miles, Kilometers };

        public static double HaversineDistance(GeoCoordinate coord1, GeoCoordinate coord2, DistanceUnit unit = DistanceUnit.Kilometers)
        {
            double r = (unit == DistanceUnit.Miles) ? 3958.8 : 6371;
            double lat1 = ToRadians(coord1.latitude);
            double lat2 = ToRadians(coord2.latitude);
            double distanceLatitude = ToRadians(coord2.latitude - coord1.latitude);
            double distanceLongitude = ToRadians(coord2.longitude - coord1.longitude);

            double cosLat1Lat2 = Math.Cos(lat1) * Math.Cos(lat2);
            double a = ToSquare(Math.Sin(distanceLatitude / 2)) + 
                (ToSquare(Math.Sin(distanceLongitude / 2)) * cosLat1Lat2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            double d = r * c;

            return d;
        }

        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }

        public static double ToSquare(double val)
        {
            return Math.Pow(val, 2);
        }
    }

    public class GeoCoordinate 
    {
        public double latitude;
        public double longitude;

        public GeoCoordinate(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
