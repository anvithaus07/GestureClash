using GestureClash.UI;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GestureClash.EditorTools
{
    public static class ScreenIdGenerator
    {

        private const string ScreenIdFilePath = "Assets/GestureClash/Script/GameScreens/Managers/ScreenId.cs";


        [MenuItem("GestureClash/Generate ScreenId Enum")]
        public static void GenerateScreenIdEnum()
        {
            ScreenManager screenManager = UnityEngine.Object.FindFirstObjectByType<ScreenManager>();

            if (screenManager == null)
            {
                Debug.LogError("ScreenManager not found in the scene!");
                return;
            }

            var screenPrefabs = screenManager.Screens;

            var screenNames = screenPrefabs.Where(fetchedScreen => fetchedScreen != null).Select(screen => screen.name).Distinct().OrderBy(name => name).ToList();

            string enumContent = "public enum ScreenId\n{\n";
            enumContent += "    Unknown,\n";
            foreach (var name in screenNames)
            {
                enumContent += $"    {name},\n";
            }
            enumContent += "}";

            File.WriteAllText(ScreenIdFilePath, enumContent);
            AssetDatabase.Refresh();
            Debug.Log("ScreenId enum updated successfully!");
        }
    }
}