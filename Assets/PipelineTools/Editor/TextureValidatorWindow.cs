using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class TextureValidatorWindow : EditorWindow
{
    private static List<string> validationResults = new List<string>();
    private const int MaxMobileTextureSize = 1024;
    private Vector2 scrollPosition;

    [MenuItem("Assets/Pipeline Tools/Validate Textures")]
    private static void ValidateTextures()
    {
        validationResults.Clear();

        string[] selectedPaths = Selection.assetGUIDs.Length > 0
            ? System.Array.ConvertAll(Selection.assetGUIDs, AssetDatabase.GUIDToAssetPath)
            : new string[0];

        List<string> texturePaths = new List<string>();

        foreach (string path in selectedPaths)
        {
            if (AssetDatabase.IsValidFolder(path))
            {
                string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { path });

                foreach (string guid in guids)
                {
                    texturePaths.Add(AssetDatabase.GUIDToAssetPath(guid));
                }
            }
            else
            {
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

                if (importer != null)
                {
                    texturePaths.Add(path);
                }
            }
        }

        if (texturePaths.Count == 0)
        {
            validationResults.Add("No textures found in selection.");
        }

        foreach (string texturePath in texturePaths)
        {
            ValidateTexture(texturePath);
        }

        ShowWindow();
    }

    private static void ValidateTexture(string assetPath)
    {
        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

        if (importer == null || texture == null)
        {
            return;
        }

        bool hasIssue = false;

        if (texture.width > MaxMobileTextureSize || texture.height > MaxMobileTextureSize)
        {
            validationResults.Add($"⚠ {texture.name} is too large: {texture.width}x{texture.height}");
            hasIssue = true;
        }

        if (importer.textureCompression == TextureImporterCompression.Uncompressed)
        {
            validationResults.Add($"⚠ {texture.name} uses no compression.");
            hasIssue = true;
        }

        if (!hasIssue)
        {
            validationResults.Add($"✅ {texture.name} passed validation.");
        }
    }

    private static void ShowWindow()
    {
        TextureValidatorWindow window = GetWindow<TextureValidatorWindow>();
        window.titleContent = new GUIContent("Texture Validation");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Texture Validation Results", EditorStyles.boldLabel);
        GUILayout.Space(10);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (string result in validationResults)
        {
            GUILayout.Label(result);
        }

        GUILayout.EndScrollView();
    }
}