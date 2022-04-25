using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GmInstance { get; private set; }
    private bool isRotating;

    private void Awake()
    {
        if (GmInstance != null && GmInstance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            GmInstance = this; 
        }
    }

    private void Start()
    {
        var plats = FindObjectsOfType<Platform>();
        foreach (var p in plats)
        {
            p.OnStartRotating+= StartIsRotating;
            p.OnEndRotating += EndIsRotating;
        }
    }

    private void StartIsRotating()
    {
        isRotating = true;
    }
    
    private void EndIsRotating()
    {
        isRotating = false;
    }

    // Add your game manager members here
    public bool GetIsRotating()
    {
        return isRotating;
    }

}
