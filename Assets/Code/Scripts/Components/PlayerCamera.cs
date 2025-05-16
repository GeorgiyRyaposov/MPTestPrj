using Cinemachine;
using Code.Scripts.Data;
using UnityEngine;
using VContainer;

namespace Code.Scripts.Components
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] 
        private CinemachineVirtualCamera _virtualCamera;
        
        [SerializeField, Tooltip("How far in degrees can you move the camera up")]
        public float _topClamp = 70.0f;

        [SerializeField,Tooltip("How far in degrees can you move the camera down")]
        public float _bottomClamp = -30.0f;

        [SerializeField,Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float _cameraAngleOverride;

        [SerializeField, Tooltip("For locking the camera position on all axis")]
        private bool _lockCameraPosition;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private const float Threshold = 0.01f;

        private bool _attached;
        private Transform _cinemachineCameraTarget;
        private InputState _input;

        [Inject]
        private void Construct(InputState input)
        {
            _input = input;
        }

        public void Attach(ThirdPersonController controller)
        {
            _cinemachineCameraTarget = controller.CinemachineCameraTarget.transform;
            _virtualCamera.Follow = _cinemachineCameraTarget;
            _cinemachineTargetYaw = _cinemachineCameraTarget.rotation.eulerAngles.y;
            
            _attached = true;
        }
        
        private void LateUpdate()
        {
            if (_attached)
            {
                CameraRotation();
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.Look.sqrMagnitude >= Threshold && !_lockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                var deltaTimeMultiplier = _input.IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.Look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.Look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

            // Cinemachine will follow this target
            _cinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}