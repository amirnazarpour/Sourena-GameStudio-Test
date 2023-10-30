using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    private DefualtInput defualtInput;
    public Vector2 input_Movement;
    public Vector2 input_View;


    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("Reference")] [SerializeField] private Transform cameraHolder;

    [Header("Setting")] [SerializeField] private PlayerSettingModle playerSettings;

    [SerializeField] private Vector2 viewClampY = new Vector2(-70, 80);

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        defualtInput = new DefualtInput();

        defualtInput.Character.Movement.performed += value => input_Movement = value.ReadValue<Vector2>();
        defualtInput.Character.View.performed += value => input_View = value.ReadValue<Vector2>();

        defualtInput.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        CalculateMovement();
        CalculateView();
    }

    private void CalculateMovement()
    {
    }

    private void CalculateView()
    {
        float inputX = playerSettings.viewXInverted ? -input_View.x : input_View.x;
        
        newCharacterRotation.y += playerSettings.viewSensitivity.x * inputX * Time.deltaTime;
        transform.localRotation = quaternion.Euler(newCharacterRotation);

        float inputY = playerSettings.viewYInverted ? input_View.y : -input_View.y;
        
        newCameraRotation.x += playerSettings.viewSensitivity.y * inputY * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampY.x, viewClampY.y);

        cameraHolder.localRotation = quaternion.Euler(newCameraRotation);
    }
}