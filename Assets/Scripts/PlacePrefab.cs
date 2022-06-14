using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacePrefab : MonoBehaviour
{
    [SerializeField] private InputActionReference moveReference;
    [SerializeField] private InputActionReference scaleReference;
    [SerializeField] private InputActionReference changeModeReference;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private float rotationSpeed = 2f;
    
    [SerializeField] private GameObject anchor;
    [SerializeField] private ControllerUIController uiController;
    private GameObject model;
    private GameObject instantiatedObject;
    private bool isSpawned;
    
    private void Awake()
    {
        isSpawned = false;
        model = AssetBundle.LoadFromFile(Path.Combine(Application.dataPath, "Resources", "abandonedhouse")).LoadAsset<GameObject>("AbandonedHouse");
    }

    private void Update()
    {
        var moveValue = moveReference.action.ReadValue<Vector2>();
        var scaleValue = scaleReference.action.ReadValue<Vector2>();
        var changeModeValue = changeModeReference.action.ReadValue<float>();

        if (instantiatedObject != null)
        {
            if (changeModeValue <= .5f)
            {
                Move(moveValue);
                Scale(scaleValue);
            }
            else Rotate(moveValue);
        }
    }
    
    public void Spawn()
    {
        if (isSpawned) return;
        instantiatedObject = Instantiate(model, anchor.transform);
        isSpawned = true;
        uiController.isPrefabPlaced = true;
    }
    
    private void Move(Vector2 vec)
    {
        instantiatedObject.gameObject.transform.position += 
            new Vector3(vec.x * movementSpeed * Time.deltaTime, 0, vec.y * movementSpeed* Time.deltaTime);
    }

    private void Scale(Vector2 vec)
    {
        if (vec.y >= .5f || vec.y <= -.5f)
            instantiatedObject.gameObject.transform.localScale += new Vector3(vec.y * scaleSpeed * Time.deltaTime, 
                vec.y * scaleSpeed * Time.deltaTime, vec.y * scaleSpeed * Time.deltaTime);  
    }

    private void Rotate(Vector2 vec)
    {
        if (vec.y >= .5f || vec.y <= -.5f)
        {
            instantiatedObject.gameObject.transform.position += 
                new Vector3(0, vec.y * movementSpeed * Time.deltaTime, 0);    
        }
        else
            instantiatedObject.gameObject.transform.Rotate(new Vector3(0, vec.x * rotationSpeed * Time.deltaTime, 0));
    }   
}
