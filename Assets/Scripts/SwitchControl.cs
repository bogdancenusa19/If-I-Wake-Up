using System;
using System.Collections;
using System.Collections.Generic;
using Cainos.CustomizablePixelCharacter;
using Cainos.PixelArtMonster_Dungeon;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwitchControl : MonoBehaviour
{
    [Header("AI")] 
    [SerializeField] private AIControl _aiControl;

    [Header("Worlds")] 
    [SerializeField] private GameObject realWorld;

    [SerializeField] private GameObject underworld;
    
    [Space][Header("Characters")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spirit;

    [Space] [Header("Camera")] 
    [SerializeField] private CinemachineVirtualCamera camera;

    [Space] [Header("Tomer Scripts References")] 
    [SerializeField] private PixelCharacter _pixelCharacter;

    [SerializeField] private PixelCharacterController _controller;
    [SerializeField] private PixelCharacterInputMouseAndKeyboard _inputMouseAndKeyboard;

    [Space] [Header("Spirit Scripts References")] 
    [SerializeField] private PixelMonster _pixelMonster;

    [SerializeField] private MonsterFlyingController _controllerSpirit;
    [SerializeField] private MonsterInputMouseAndKeyboard _inputMouseAndKeyboardSpirit;
    
    

    private bool isControlAtTomer = true;

    private void Start()
    {
        _pixelMonster.Alpha = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwapControl();
        }
    }

    private void SwapControl()
    {
        if (isControlAtTomer)
        {
            underworld.SetActive(true);
            realWorld.SetActive(false);
            ResetMovement(_controller);
            
            _aiControl.SetIsUnderControl(true);
            _pixelMonster.enabled = true;
            _controllerSpirit.enabled = true;
            _inputMouseAndKeyboardSpirit.enabled = true;
            
            _pixelCharacter.enabled = false;
            _controller.enabled = false;
            _inputMouseAndKeyboard.enabled = false;

            camera.Follow = spirit.transform;

            _pixelMonster.Alpha = 1f;
            _pixelCharacter.Alpha = 0.5f;
            isControlAtTomer = false;
            
        }
        else
        {
            realWorld.SetActive(true);
            underworld.SetActive(false);
            ResetMovement(_controllerSpirit);
            
            _pixelCharacter.enabled = true;
            _controller.enabled = true;
            _inputMouseAndKeyboard.enabled = true;
            
            _aiControl.SetIsUnderControl(false);
            _pixelMonster.enabled = false;
            _controllerSpirit.enabled = false;
            _inputMouseAndKeyboardSpirit.enabled = false;

            camera.Follow = player.transform;

            _pixelCharacter.Alpha = 1f;
            _pixelMonster.Alpha = 0.5f;
            isControlAtTomer = true;
            
        }
    }
    
    private void ResetMovement(PixelCharacterController controller)
    {
        if (controller != null)
        {
            controller.SetMovement(Vector2.zero); 
        }
    }
    
    private void ResetMovement(MonsterFlyingController spiritController)
    {
        if(spiritController != null)
        {
            spiritController.SetMovement(Vector2.zero);
        }
    }
}
