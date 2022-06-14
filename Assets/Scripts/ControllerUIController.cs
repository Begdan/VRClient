using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerUIController : MonoBehaviour
{
    [SerializeField] private GameObject mainLeftUI;
    [SerializeField] private GameObject altLeftUI;
    [SerializeField] private GameObject mainRightUI;
    [SerializeField] private InputActionReference changeModeReference;

    private bool _isUIActivated;

    [HideInInspector] public bool isPrefabPlaced;

    private void Update()
    {
        if (!_isUIActivated)
        {
            if (isPrefabPlaced)
            {
                TurnUIOn();
                _isUIActivated = true;
            }
        }    
        
        if (_isUIActivated)
        {
            var isAltOn = changeModeReference.action.ReadValue<float>();
            
            if (!(isAltOn <= .5f))
            {
                mainLeftUI.SetActive(false);
                altLeftUI.SetActive(true);
            }
            else
            {
                mainLeftUI.SetActive(true);
                altLeftUI.SetActive(false);
            }
        }
    }
    
    private void TurnUIOn()
    {
        if (isPrefabPlaced)
        {
            mainLeftUI.SetActive(true);
            altLeftUI.SetActive(false);
            mainRightUI.SetActive(true);
        }
    }
}

