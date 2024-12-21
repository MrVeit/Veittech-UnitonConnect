using UnityEngine;
using UnitonConnect.Core.Common;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.Editor.Data
{
    [CreateAssetMenu(fileName = "Jetton_Name",
        menuName = ProjectStorageConsts.CREATE_PATH_JETTON_CONFIG)]
    public sealed class JettonConfig : ScriptableObject
    {
        [field: SerializeField, Space] public JettonTypes Type { get; private set; }
        [field: SerializeField, Space] public string Name { get; private set; }
        [field: SerializeField, Space] public string MasterAddress { get; private set; }
        [field: SerializeField, Space] public string IconUrl { get; private set; }

    }
}