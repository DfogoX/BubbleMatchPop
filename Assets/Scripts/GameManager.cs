using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool isRotating;
    
    private GameManager() {
        // initialize your game manager here. Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // because the game manager will be created before the objects
    }    
 
    public static GameManager Instance {
        get {
            if(instance==null) {
                instance = new GameManager();
            }
 
            return instance;
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
