using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private bool isRotating;

    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
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
        Debug.Log($"rotation action on {isRotating}");
    }
    
    private void EndIsRotating()
    {
        isRotating = false;
        Debug.Log($"rotation action off {isRotating}");
    }

    // Add your game manager members here
    public bool GetIsRotating()
    {
        return isRotating;
    }

}
