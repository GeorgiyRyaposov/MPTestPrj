using Code.Scripts.Components;
using UnityEngine;

namespace Code.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "Configs/GameAssets")]
    public class GameAssets : ScriptableObject
    {
        public PlayerPrefabs PlayerPrefabs;
        public StageItems StageItems;
    }

    [System.Serializable]
    public class PlayerPrefabs
    {
        public ThirdPersonController Prefab;
        public PlayerSpawner PlayerSpawnerPrefab;
    }
    
    [System.Serializable]
    public class StageItems
    {
        public FirstAidKit FirstAidKitPrefab;
    }
}