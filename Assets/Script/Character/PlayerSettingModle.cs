using System;
using UnityEngine;

namespace Character
{

    public enum PlayerStance
    {
        Stand,
        Crouch,
        Prone
    }
    
    [Serializable]
    public class PlayerSettingModle
    {
        [Header("View Setting")] 
        public Vector2 viewSensitivity;
        public bool viewXInverted;
        public bool viewYInverted;


        [Header("Movement")]
        public float walkingForwardSpeed;
        public float walkingBackwardSpeed;
        public float walkingStrafeSpeed;

        [Header("Jumping")]
        public float jumpingHeight;

        public float jumpingFalloff;

    }

    [System.Serializable]
    public class CharacterStance
    {
        public float CameraHeight;
        public CapsuleCollider StanceCollider;

    }
}