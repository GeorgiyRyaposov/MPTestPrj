using Code.Scripts.Components;
using UnityEngine;

namespace Code.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "Configs/GameAssets")]
    public class GameAssets : ScriptableObject
    {
        public PlayerPrefabs PlayerPrefabs;
    }

    [System.Serializable]
    public class PlayerPrefabs
    {
        public ThirdPersonController Prefab;
    }
}