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
    public int ISO_Numeric;

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
    public int geonameid;
    public string[] neighbours;
    public string EquivalentFIPS;
}
