using System.Collections.Generic;
using UnityEngine;
using UnitonConnect.Editor.Common;
using UnitonConnect.Editor.Data;

[CreateAssetMenu(fileName = "Jetton Configs",
    menuName = ProjectStorageConsts.CREATE_PATH_JETTON_CONFIG_STORAGE)]
public class JettonConfigsStorage : ScriptableObject
{
    [field: SerializeField, Space] public List<JettonConfig> Jettons { get; private set; }
}