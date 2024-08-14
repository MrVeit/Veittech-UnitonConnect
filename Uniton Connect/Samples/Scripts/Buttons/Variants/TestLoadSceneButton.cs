using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestLoadSceneButton : TestBaseButton
    {
        [SerializeField, Space] private int _sceneId;

        public sealed override void OnClick()
        {
            SceneManager.LoadScene(_sceneId);
        }
    }
}