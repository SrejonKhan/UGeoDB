using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityInfo 
{
    public int geonameid { get; set; }
    public string name { get; set; }
    public string asciiname { get; set; }
    public string alternatenames { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string feature_class { get; set; }
    public string feature_code { get; set; }
    public string country_code { get; set; }
    public string cc2 { get; set; }
    public string admin1_code { get; set; }
    public string admin2_code { get; set; }
    public string admin3_code { get; set; }
    public string admin4_code { get; set; }
    public int population { get; set; }
    public int elevation { get; set; }
    public int dem { get; set; }
    public string timezone { get; set; }
    public string modification_date { get; set; }
}
