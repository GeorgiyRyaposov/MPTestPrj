using UnityEngine;

namespace Code.Scripts.Data
{
    public class InputState
    {
        public Vector2 Move;
        public Vector2 Look;
        public bool Sprint;
        public bool Jump;
        
        public bool IsCurrentDeviceMouse = true;
        public bool AnalogMovement;
    }
}