using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefab : MonoBehaviour
{
    [SerializeField] private GameObject anchor;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ControllerUIController uiController;
    private bool isSpawned;

    private void Awake()
    {
        isSpawned = false;
    }

    public void Spawn()
    {
        if (isSpawned) return;
        Instantiate(prefab, anchor.transform);
        isSpawned = true;
        uiController.isPrefabPlaced = true;
    }
}
