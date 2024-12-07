using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureTypeChanger : EditorWindow {
  // Поле для выбора папки с ассетами
  [SerializeField] private DefaultAsset folder;

  [MenuItem("Tools/Texture Type Changer")]
  public static void ShowWindow() {
    GetWindow<TextureTypeChanger>("Texture Type Changer");
  }

  private void OnGUI() {
    GUILayout.Label("Change Texture Type to Sprite (2D and UI) with Single Mode", EditorStyles.boldLabel);

    folder = (DefaultAsset)EditorGUILayout.ObjectField("Folder", folder, typeof(DefaultAsset), false);

    if (GUILayout.Button("Change Texture Type")) {
      if (folder == null) {
        Debug.LogError("Please select a folder.");
        return;
      }

      string folderPath = AssetDatabase.GetAssetPath(folder);
      ChangeTextureTypeInFolder(folderPath);
      AssetDatabase.Refresh();
    }
  }

  private void ChangeTextureTypeInFolder(string folderPath) {
    string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.AllDirectories);

    foreach (string file in files) {
      string relativePath = file.Replace(Application.dataPath, "Assets");
      TextureImporter importer = AssetImporter.GetAtPath(relativePath) as TextureImporter;

      if (importer != null) {
        bool isModified = false;

        // Изменяем Texture Type на Sprite, если он отличается
        if (importer.textureType != TextureImporterType.Sprite) {
          importer.textureType = TextureImporterType.Sprite;
          isModified = true;
        }

        // Устанавливаем Sprite Mode на Single, если он отличается
        if (importer.spriteImportMode != SpriteImportMode.Single) {
          importer.spriteImportMode = SpriteImportMode.Single;
          isModified = true;
        }

        // Сохраняем изменения, если были модификации
        if (isModified) {
          importer.SaveAndReimport();
          Debug.Log($"Changed texture type and mode for: {relativePath}");
        }
      }
    }
  }
}
