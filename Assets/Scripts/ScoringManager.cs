using System;
using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    public static ScoringManager SmInstance { get; private set; }
    
    private ScoringSystem _scoring;
    
    private void Awake()
    {
        if (SmInstance != null && SmInstance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            SmInstance = this; 
        }
        _scoring = gameObject.AddComponent<ScoringSystem>();
    }

    private void Start()
    {
        Debug.Log($"here");
        var plats = FindObjectsOfType<Platform>();
        foreach (var p in plats)
        {
            p.OnStartRotating += PlatformRotated;
        }
        var bubbles = FindObjectsOfType<Bubble>();
        foreach (var b in bubbles)
        {
            b.OnBubblePop += BubblePopped;
        }
        
    }

    public void BubblePopped()
    {
        _scoring.IncreaseBubblesPoppedCount();
        UIManager.UIInstance.UpdateBubblesPoppedScore(_scoring.GetBubblesPoppedCount());
    }

    public void PlatformRotated()
    {
        _scoring.IncreasePlatformRotationsCount();
        UIManager.UIInstance.UpdatePlatformsRotatedScore(_scoring.GetPlatformRotationsCount());
    }
    
}
