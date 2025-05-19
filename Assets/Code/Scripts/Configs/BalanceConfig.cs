using UnityEngine;

namespace Code.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "Configs/BalanceConfig")]
    public class BalanceConfig : ScriptableObject
    {
        public int InitialHealth = 100;
        public float VelocityFallDamage = -5f;
        public int FallDamageValue = 35;
        public int HealValue = 50;
        
        [Space]
        public LayerMask GroundLayer;
        public float StageSize = 25f;
        
        [Space]
        public int FirstAidKitCount = 20;
    }
}