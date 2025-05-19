using Code.Scripts.Configs;
using UnityEngine;

namespace Code.Scripts.Services
{
    public class StageService
    {
        private readonly BalanceConfig _balanceConfig;

        public StageService(BalanceConfig balanceConfig)
        {
            _balanceConfig = balanceConfig;
        }

        public Vector3 GetRandomPosition()
        {
            const float offset = 100f;
            var random = Random.insideUnitSphere * _balanceConfig.StageSize;
            random.y = offset;
            var ray = new Ray(random, Vector3.down);
            var hit = Physics.Raycast(ray, out var hitInfo, offset + 1, _balanceConfig.GroundLayer, QueryTriggerInteraction.Ignore);
            
            return hit 
                ? hitInfo.point 
                : Vector3.zero;
        }
    }
}