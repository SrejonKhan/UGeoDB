using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

public class CopyStaticEditor : EditorWindow
{
    private GUIStyle titleLabelStyle;
    private GUIStyle subtitleLabelStyle;

    private Dictionary<string, bool> staticFiles = new Dictionary<string, bool>();
    private List<string> copiedFiles = new List<string>();
    private bool isInitialized = false;

    [MenuItem("Window/UGeoDB/CopyStaticFiles")]
    public static void ShowWindow()
    {
        GetWindow<CopyStaticEditor>("Copy Static Files - UGeoDB");
    }

    private void InitializeTitleLabelStyle()
    {
        titleLabelStyle = new GUIStyle(GUI.skin.label);
        titleLabelStyle.fontSize = 26;
        titleLabelStyle.fontStyle = FontStyle.Bold;
        titleLabelStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void InitializeSubtitleLabelStyle()
    {
        subtitleLabelStyle = new GUIStyle(GUI.skin.label);
        subtitleLabelStyle.fontSize = 12;
        subtitleLabelStyle.fontStyle = FontStyle.Normal;
        subtitleLabelStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void DrawTitleLabel(string title)
    {
        GUILayout.Label(title, titleLabelStyle, GUILayout.ExpandWidth(true), GUILayout.Height(30));
    }

    private void DrawSubtitleLabel(string subtitle)
    {
        GUILayout.Label(subtitle, subtitleLabelStyle, GUILayout.ExpandWidth(true));
    }

    private void OnGUI()
    {
        if (!isInitialized)
        {
            GetStaticFiles();
            GetCopiedFiles();
            InitializeTitleLabelStyle();
            InitializeSubtitleLabelStyle();
            isInitialized = true;
            this.Repaint();
        }

        DrawTitleLabel("UGeoDb");
        DrawSubtitleLabel("Copy Static Files to Streaming Assets");

        GUILayout.Label("Static Files:", EditorStyles.boldLabel);
        foreach (var file in staticFiles.Keys.ToList())
        {
            bool fileExist = copiedFiles.Contains(file);

            EditorGUI.BeginDisabledGroup(fileExist);
            staticFiles[file] = EditorGUILayout.Toggle(file, staticFiles[file]);
            EditorGUI.EndDisabledGroup();
        }

        if (GUILayout.Button("Copy Selected Files"))
        {
            CopyStaticFiles();
            isInitialized = false;
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Refresh"))
        {
            isInitialized = false;
        }
    }

    private void CopyStaticFiles()
    {
        foreach (var file in staticFiles)
        {
            if (!file.Value || File.Exists(file.Key))
                continue;

            CopyFileToStreamingAssets(file.Key);
            copiedFiles.Add(file.Key);
        }
        AssetDatabase.Refresh();
    }

    private void CopyFileToStreamingAssets(string fileName)
    {
        string sourceFilePath = Path.Combine(GetPackageDirectory(), "Static", fileName);
        string destinationFilePath = Path.Combine(Application.streamingAssetsPath, "StaticUGeoDB", fileName);

        if (!Directory.Exists(Path.GetDirectoryName(destinationFilePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath));
        }

        FileUtil.CopyFileOrDirectory(sourceFilePath, destinationFilePath);
    }

    private string GetPackageDirectory()
    {
        MonoScript script = MonoScript.FromScriptableObject(this);
        string assetPath = AssetDatabase.GetAssetPath(script);
        var packageInfo = UnityEditor.PackageManager.PackageInfo.FindForAssetPath(assetPath);
        string finalPath = packageInfo.resolvedPath;
        return finalPath;
    }

    private void GetStaticFiles()
    {
        string staticFolderPath = Path.Combine(GetPackageDirectory(), "Static");

        if (!Directory.Exists(staticFolderPath))
        {
            Debug.LogError("Static Folder doesn't exist.");
            return;
        }

        staticFiles.Clear();

        string[] files = Directory.GetFiles(staticFolderPath);
        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            if (Path.GetExtension(file) == ".meta") continue; // Skip .meta files

            string fileName = Path.GetFileName(file);
            if (!staticFiles.ContainsKey(fileName))
            {
                staticFiles.Add(fileName, false);
            }
        }
    }

    private void GetCopiedFiles()
    {
        string streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, "StaticUGeoDB");

        if (!Directory.Exists(streamingAssetsPath))
            return;

        copiedFiles.Clear();

        string[] files = Directory.GetFiles(streamingAssetsPath);
        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            if (Path.GetExtension(file) == ".meta") continue; // Skip .meta files

            string fileName = Path.GetFileName(file);
            if (!copiedFiles.Contains(fileName))
            {
                copiedFiles.Add(fileName);
            }
        }
    }
}