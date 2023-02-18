using System;

[System.Serializable]
public class CityInfo
{
    public int GeoNameID;
    public string Name;
    public string AsciiName;
    public string[] AlternateNames;
    public double Latitude;
    public double Longitude;
    public char FeatureClass;
    public string FeatureCode;
    public string CountryCode;
    public string[] AlternateCountryCode;
    public string Admin1Code;
    public string Admin2Code;
    public string Admin3Code;
    public string Admin4Code;
    public ulong Population;
    public int Elevation;
    public string DigitalElevationModel;
    public string Timezone;
    public DateTime ModificationDate;

    public CityInfo(string[] entries)
    {
        /*
         *
            geonameid         : integer id of record in geonames database
            name              : name of geographical point (utf8) varchar(200)
            asciiname         : name of geographical point in plain ascii characters, varchar(200)
            alternatenames    : alternatenames, comma separated, ascii names automatically transliterated, convenience attribute from alternatename table, varchar(10000)
            latitude          : latitude in decimal degrees (wgs84)
            longitude         : longitude in decimal degrees (wgs84)
            feature class     : see http://www.geonames.org/export/codes.html, char(1)
            feature code      : see http://www.geonames.org/export/codes.html, varchar(10)
            country code      : ISO-3166 2-letter country code, 2 characters
            cc2               : alternate country codes, comma separated, ISO-3166 2-letter country code, 200 characters
            admin1 code       : fipscode (subject to change to iso code), see exceptions below, see file admin1Codes.txt for display names of this code; varchar(20)
            admin2 code       : code for the second administrative division, a county in the US, see file admin2Codes.txt; varchar(80) 
            admin3 code       : code for third level administrative division, varchar(20)
            admin4 code       : code for fourth level administrative division, varchar(20)
            population        : bigint (8 byte int) 
            elevation         : in meters, integer
            dem               : digital elevation model, srtm3 or gtopo30, average elevation of 3''x3'' (ca 90mx90m) or 30''x30'' (ca 900mx900m) area in meters, integer. srtm processed by cgiar/ciat.
            timezone          : the iana timezone id (see file timeZone.txt) varchar(40)
            modification date : date of last modification in yyyy-MM-dd format  
         *
         */

        Int32.TryParse(entries[0], out this.GeoNameID);
        this.Name = entries[1];
        this.AsciiName = entries[2];
        this.AlternateNames = entries[3].Split(',');
        Double.TryParse(entries[4], out this.Latitude);
        Double.TryParse(entries[5], out this.Longitude);
        Char.TryParse(entries[6], out this.FeatureClass);
        this.FeatureCode = entries[7];
        this.CountryCode = entries[8];
        this.AlternateCountryCode = entries[9].Split(',');
        this.Admin1Code = entries[10];
        this.Admin2Code = entries[11];
        this.Admin3Code = entries[12];
        this.Admin4Code = entries[13];
        ulong.TryParse(entries[14], out this.Population);
        int.TryParse(entries[15], out this.Elevation);
        this.DigitalElevationModel = entries[16];
        this.Timezone = entries[17];
        DateTime.TryParseExact(entries[18], "yyyy-MM-dd\n", null, System.Globalization.DateTimeStyles.None, out this.ModificationDate);
    }
}
