using System;
using UnityEngine;

[Serializable]
public class PlayerSettingModle
{
    [Header("View Setting")] 
    public Vector2 viewSensitivity;
    public bool viewXInverted;
    public bool viewYInverted;
}