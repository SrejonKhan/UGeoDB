using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UGeoDB
{
    public class GeoDB : MonoBehaviour
    {
        public CountryInfo[] editorCountries;
        private string countryDb;
        private string citiesDb;

        void Start()
        {
            string countryDbPath = GetStreammingAssetsPath("countryInfo.txt");
            string citiesDbPath = GetStreammingAssetsPath("cities500.txt");

            StartCoroutine(ReadCountryDb(countryDbPath));
            StartCoroutine(ReadCitiesDb(citiesDbPath));
        }

        IEnumerator ReadCountryDb(string filePath)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();
            countryDb = www.downloadHandler.text;
#else
            yield return null;
            countryDb = File.ReadAllText(filePath);
#endif
            var lines = GetLines(countryDb);
            CountryInfo[] countries = new CountryInfo[lines.Count];

            // #ISO	ISO3	ISO-Numeric	fips	Country	Capital	Area(in sq km)	
            // Population	Continent	tld	CurrencyCode	CurrencyName	Phone	
            // Postal Code Format	Postal Code Regex	Languages	geonameid	
            // neighbours	EquivalentFipsCode

            for (int i = 0; i < lines.Count; i++)
            {
                string[] entries = lines[i].Split('\t');

                CountryInfo country = new CountryInfo();
                country.ISO = entries[0];
                country.ISO3 = entries[1];
                country.ISO_Numeric = Convert.ToInt32(entries[2]);
                country.FIPS = entries[3];
                country.CountryName = entries[4];
                country.Capital = entries[5];
                country.Area = Convert.ToInt32(entries[6]);
                country.Population = Convert.ToInt32(entries[7]);
                country.Continent = entries[8];
                country.TopLevelDomain = entries[9];
                country.CurrencyCode = entries[10];
                country.CurrencyName = entries[11];
                country.Phone = entries[12];
                country.PostalCodeFormat = entries[13];
                country.PostalCodeRegex = entries[14];
                country.Languages = entries[15].Split(',');
                country.geonameid = Convert.ToInt32(entries[16]);
                country.neighbours = entries[17].Split(',');
                country.EquivalentFIPS = entries[18];

                countries[i] = country;
            }
            editorCountries = countries;
            Debug.Log(lines[lines.Count - 1]);
        }

        IEnumerator ReadCitiesDb(string filePath)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();
            citiesDb = www.downloadHandler.text;
#else
            yield return null;
            citiesDb = File.ReadAllText(filePath);
#endif
            var lines = GetLines(citiesDb);
            Debug.Log(lines.Count);
        }

        public static string GetStreammingAssetsPath(string fileName)
        {
#if UNITY_EDITOR_OSX
            return "file://" + Application.streamingAssetsPath + "/" + fileName;
#elif UNITY_EDITOR
            return Application.streamingAssetsPath + "/" + fileName;
#elif UNITY_ANDROID
            return Path.Combine("jar:file://" + Application.dataPath + "!/assets" , fileName);
#elif UNITY_IOS
            return Path.Combine(Application.dataPath + "/Raw" , fileName);
#else
            return Application.streamingAssetsPath + "/" + fileName;
#endif
        }

        public static List<string> GetLines(string original, bool ignoreEmptyLine = true)
        {
            List<string> lines = new List<string>();

            int startIndex = 0;

            bool isCommentLine = false;
            int lastLineEndIndex = -1; // in case, first line is comment, (-1 + 1) == 0, so condition would work

            for (int i = 0; i < original.Length; i++)
            {
                if (original[i] == '#' && i == lastLineEndIndex + 1)
                {
                    isCommentLine = true;
                }

                if (original[i] == '\n')
                {
                    string line = "";

                    if (!isCommentLine)
                        line = original.Substring(startIndex, (i - startIndex + 1));

                    if (!ignoreEmptyLine)
                        lines.Add(line);
                    else if (!string.IsNullOrEmpty(line.Trim()))
                        lines.Add(line);

                    startIndex = i + 1;
                    lastLineEndIndex = i;
                    isCommentLine = false;
                }
            }

            return lines;
        }
    }
}