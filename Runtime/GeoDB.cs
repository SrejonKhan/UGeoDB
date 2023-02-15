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
            var lines = GetLines(countryDb);
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
                if(original[i] == '#' && i == lastLineEndIndex + 1) 
                {
                    isCommentLine = true;
                }

                if (original[i] == '\n')
                {
                    string line = "";
                    
                    if(!isCommentLine)
                        line = original.Substring(startIndex, (i - startIndex + 1));
                    
                    if(!ignoreEmptyLine)
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