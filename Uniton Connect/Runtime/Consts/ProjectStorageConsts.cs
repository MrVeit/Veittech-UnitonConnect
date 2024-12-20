using UnityEngine;
using UnitonConnect.Runtime.Data;

namespace UnitonConnect.Editor.Common
{
    public sealed class ProjectStorageConsts
    {
        public const string CREATE_PATH_JETTON_CONFIG = "Uniton Connect/Jetton";
        public const string CREATE_PATH_JETTON_CONFIG_STORAGE = "Uniton Connect/Jetton Storage";

        public const string EDITOR_FILE_NAME = "dAppsData.asset";
        public const string EDITOR_STORAGE = "Assets/Uniton Connect/Editor/Internal Resources";

        public const string RUNTIME_FILE_NAME_WITOUT_FORMAT = "dAppsRuntimeData";
        public const string RUNTIME_FILE_NAME = RUNTIME_FILE_NAME_WITOUT_FORMAT + ".asset";

        public const string RUNTIME_STORAGE = "Assets/Resources/Uniton Connect";
        public const string RUNTIME_FOLDER_IN_RESOURCES = "Uniton Connect";

        public const string APP_ICON_FILE_NAME = "icon.png";
        public const string APP_DATA_FILE_NAME = "dAppData.json";

        public const string TEST_APP_URL = "https://mrveit.github.io/Veittech-UnitonConnect";
        public const string TEST_APP_NAME = "Uniton Connect";

        public static string GetTestAppManifest()
        {
            return GetAppManifest(TEST_APP_URL, APP_DATA_FILE_NAME);
        }

        public static string GetAppManifest(string url, string manifestFileName)
        {
            return $"{url}/{manifestFileName}";
        }

        public static DAppConfig GetRuntimeAppStorage()
        {
            return Resources.Load<DAppConfig>($"{RUNTIME_FOLDER_IN_RESOURCES}/{RUNTIME_FILE_NAME_WITOUT_FORMAT}");
        }
    }
}