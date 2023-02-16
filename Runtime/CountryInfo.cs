using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CountryInfo
{
    // #ISO	ISO3	ISO-Numeric	fips	Country	Capital	Area(in sq km)	
    // Population	Continent	tld	CurrencyCode	CurrencyName	Phone	
    // Postal Code Format	Postal Code Regex	Languages	geonameid	
    // neighbours	EquivalentFipsCode   

    public string ISO;
    public string ISO3;
    public int ISONumeric;
    public string FIPS;
    public string CountryName;
    public string Capital;
    public int Area;
    public int Population;
    public string Continent;
    public string TopLevelDomain;
    public string CurrencyCode;
    public string CurrencyName;
    public string Phone;
    public string PostalCodeFormat;
    public string PostalCodeRegex;
    public string[] Languages;
    public int GeoNameID;
    public string[] Neighbours;
    public string EquivalentFIPS;

    public CountryInfo(string[] entries)
    {
        // #ISO	ISO3	ISO-Numeric	fips	Country	Capital	Area(in sq km)	
        // Population	Continent	tld	CurrencyCode	CurrencyName	Phone	
        // Postal Code Format	Postal Code Regex	Languages	geonameid	
        // neighbours	EquivalentFipsCode

        this.ISO = entries[0];
        this.ISO3 = entries[1];
        this.ISONumeric = Convert.ToInt32(entries[2]);
        this.FIPS = entries[3];
        this.CountryName = entries[4];
        this.Capital = entries[5];
        Int32.TryParse(entries[6], out this.Area);
        Int32.TryParse(entries[7], out this.Population);
        this.Continent = entries[8];
        this.TopLevelDomain = entries[9];
        this.CurrencyCode = entries[10];
        this.CurrencyName = entries[11];
        this.Phone = entries[12];
        this.PostalCodeFormat = entries[13];
        this.PostalCodeRegex = entries[14];
        this.Languages = entries[15].Split(',');
        Int32.TryParse(entries[16], out this.GeoNameID);
        this.Neighbours = entries[17].Split(',');
        this.EquivalentFIPS = entries[18];
    }
}
