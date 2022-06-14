using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] private InputActionReference moveReference;
    [SerializeField] private InputActionReference scaleReference;
    [SerializeField] private InputActionReference changeModeReference;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private int minScaleInPercents = 20;
    [SerializeField] private int maxScaleInPercents = 150;

    private float _minScale;
    private float _maxScale;

    public void Awake()
    {
        _minScale = gameObject.transform.localScale.x * (minScaleInPercents / 100);
        _maxScale = gameObject.transform.localScale.x * (maxScaleInPercents / 100);
    }

    private void Update()
    {
        var moveValue = moveReference.action.ReadValue<Vector2>();
        var scaleValue = scaleReference.action.ReadValue<Vector2>();
        var changeModeValue = changeModeReference.action.ReadValue<float>();
        
        if (changeModeValue <= .5f)
        {
            Move(moveValue);
            Scale(scaleValue);
        }
        else Rotate(moveValue);
    }
    
    private void Move(Vector2 vec)
    {
        gameObject.transform.position += 
            new Vector3(vec.x * movementSpeed * Time.deltaTime, 0, vec.y * movementSpeed* Time.deltaTime);
    }

    private void Scale(Vector2 vec)
    {
        if (vec.y >= .5f || vec.y <= -.5f)
            gameObject.transform.localScale += new Vector3(vec.y * scaleSpeed * Time.deltaTime, 
                vec.y * scaleSpeed * Time.deltaTime, vec.y * scaleSpeed * Time.deltaTime);  
    }

    private void Rotate(Vector2 vec)
    {
        if (vec.y >= .5f || vec.y <= -.5f)
        {
            gameObject.transform.position += 
                new Vector3(0, vec.y * movementSpeed * Time.deltaTime, 0);    
        }
        else
            gameObject.transform.Rotate(new Vector3(0, vec.x * rotationSpeed * Time.deltaTime, 0));
    }   
}
