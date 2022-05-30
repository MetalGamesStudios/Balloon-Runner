using UnityEngine;

namespace C_
{
    [CreateAssetMenu(fileName = "PlayerMovementProperties", menuName = "Scriptables", order = 0)]
    public class PlayerMovementProps : ScriptableObject
    {
        public float xSpeed, xLerp;
        public float clampValue;
        public float playerForwardSpeed;
        public float dragSensitivity;

        [Header("Rotation Properties")] public float yMaxRotation;
        public float yMaxOffset;
        public float yRotationTime;
    }
}