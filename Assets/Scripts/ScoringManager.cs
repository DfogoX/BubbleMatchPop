using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoringManager : MonoBehaviour
{
    public static ScoringManager SmInstance { get; private set; }
    
    private ScoringSystem _scoring;
    private int totalBubbles;
    
    private void Awake()
    {
        if (SmInstance != null && SmInstance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            SmInstance = this;
            _scoring = gameObject.AddComponent<ScoringSystem>();
        }
        
    }

    
    public void ScoreSetUp()
    {
        totalBubbles = 0; 
        var plats = FindObjectsOfType<Platform>();
        foreach (var p in plats)
        {
            p.OnStartRotating += PlatformRotated;
        }

        var bubbles = FindObjectsOfType<Bubble>();
        foreach (var b in bubbles)
        {
            b.OnBubblePop += BubblePopped;
            totalBubbles++;
        }
        _scoring.ResetScore();
    }

    public void BubblePopped()
    {
        _scoring.IncreaseBubblesPoppedCount();
        UIManager.UIInstance.UpdateBubblesPoppedScore(_scoring.GetBubblesPoppedCount());
        Debug.Log($"Popped {_scoring.GetBubblesPoppedCount()} out of {totalBubbles}");
        if (_scoring.GetBubblesPoppedCount() == totalBubbles)
        {
            GameManager.GmInstance.LevelCompleted();
        }
    }

    public void PlatformRotated()
    {
        _scoring.IncreasePlatformRotationsCount();
        UIManager.UIInstance.UpdatePlatformsRotatedScore(_scoring.GetPlatformRotationsCount());
    }

}
