using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UGeoDB.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace UGeoDB
{
    public class GeoDB 
    {
        private static CityInfo[] cities = new CityInfo[0];
        private static CountryInfo[] countries = new CountryInfo[0];

        private bool isCityDbLoaded = false;
        private bool isCountryDbLoaded = false;
        private bool isLoaded = false;
        public bool IsLoaded { get => isLoaded; private set => isLoaded = value; }
        private Action onLoad;
        public Action OnLoad { get => onLoad; set => onLoad = value; }

        public GeoDB()
        {
            string countryDbPath = GetStreammingAssetsPath("countryInfo.txt");
            string citiesDbPath = GetStreammingAssetsPath("cities15000.txt");

            ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
            ThreadPool.QueueUserWorkItem(cb => ReadCountryDb(countryDbPath), false);
            ThreadPool.QueueUserWorkItem(cb => ReadCitiesDb(citiesDbPath), false);
        }

        private void UpdateOnLoadFlag()
        {
            if (isCityDbLoaded && isCountryDbLoaded)
            {
                IsLoaded = true;
                OnLoad?.Invoke();
            }
        }

        private async void ReadCountryDb(string path)
        {
            string countryDb = await ReadTextResource(path);

            var lines = GetLines(countryDb);
            countries = new CountryInfo[lines.Count];
            for (int i = 0; i < lines.Count; i++)
            {
                string[] entries = lines[i].Split('\t');
                countries[i] = new CountryInfo(entries);
            }

            isCountryDbLoaded = true;
            UpdateOnLoadFlag();
        }

        private async void ReadCitiesDb(string path)
        {
            string citiesDb = await ReadTextResource(path);

            var lines = GetLines(citiesDb);
            cities = new CityInfo[lines.Count];
            for (int i = 0; i < lines.Count; i++)
            {
                string[] entries = lines[i].Split('\t');
                cities[i] = new CityInfo(entries);
            }
            
            isCityDbLoaded = true;
            UpdateOnLoadFlag();
        }

        public async Task<string> ReadTextResource(string path)
        {
            string text = "";
            bool useUWR = false;

            if (path.Contains("://") || path.Contains(":///"))
                useUWR = true;

#if UNITY_WEBGL && !UNITY_EDITOR
            useUWR = true;
#endif
            if (useUWR)
            {
                using var www = UnityWebRequest.Get(path);
                var operation = www.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                text = www.downloadHandler.text;
            }
            else
            {
                text = File.ReadAllText(path);
            }

            return text;
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

        public CityInfo FindNearestCity(GeoCoordinate coord)
        {
            double closestDistance = double.MaxValue;
            CityInfo closestCity = null;

            foreach (CityInfo city in cities)
            {
                double distance = GeoMath.HaversineDistance(coord, city.coordinate);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCity = city;
                }
            }

            return closestCity;
        }

        public CityInfo[] FindNearestCities(GeoCoordinate coord, int range, GeoMath.DistanceUnit distanceUnit = GeoMath.DistanceUnit.Kilometers)
        {
            List<CityInfo> closestCities = new List<CityInfo>();

            foreach (CityInfo city in cities)
            {
                double distance = GeoMath.HaversineDistance(coord, city.coordinate, distanceUnit);
                if (distance <= range)
                {
                    closestCities.Add(city);
                }
            }

            return closestCities.ToArray();
        }

        public CountryInfo GetCountry(CityInfo city)
        {
            CountryInfo country = null;

            for (int i = 0; i < countries.Length; i++)
            {
                if (city.CountryCode == countries[i].ISO)
                {
                    country = countries[i];
                    break;
                }
            }

            return country;
        }
    }
}