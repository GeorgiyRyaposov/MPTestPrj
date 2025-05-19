using UnityEngine;

namespace Code.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "Configs/BalanceConfig")]
    public class BalanceConfig : ScriptableObject
    {
        public int InitialHealth = 100;
        public float VelocityFallDamage = -5f;
        public int FallDamageValue = 35;
    }
}