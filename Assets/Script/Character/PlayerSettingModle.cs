using System;
using UnityEngine;

namespace Character
{
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
}