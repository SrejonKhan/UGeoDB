using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UGeoDB
{
    public class GeoDB : MonoBehaviour
    {
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
            Debug.Log(countryDb);
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
            Debug.Log(citiesDb);
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
    }
}