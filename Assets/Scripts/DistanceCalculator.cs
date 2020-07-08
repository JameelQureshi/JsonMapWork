using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Utils;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour {

    const float PIx = Mathf.PI;

    public static double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
    {
        double earthRadius = 6378137; // km
        double dLat = Radians(lat2 - lat1);
        double dLon = Radians(lon2 - lon1);
        lat1 = Radians(lat1);
        lat2 = Radians(lat2);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = earthRadius * c;

        return distance;
    }



    public static double Radians(double x)
    {
        return x * PIx / 180;
    }
}
