
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager UIInstance { get; private set; }
    
    private UIScoreSystem _uiScore;
    
    private void Awake()
    {
        if (UIInstance != null && UIInstance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            UIInstance = this; 
        }
        _uiScore = gameObject.AddComponent<UIScoreSystem>();
    }

    private void Start()
    {
        UpdateBubblesPoppedScore(0);
        UpdatePlatformsRotatedScore(0);
    }

    public void UpdatePlatformsRotatedScore(int platformRotationsCount)
    {
        _uiScore.UpdatePlatformRotationsScore(platformRotationsCount);
    }

    public void UpdateBubblesPoppedScore(int bubblesPoppedCount)
    {
        _uiScore.UpdateBubblesPoppedScore(bubblesPoppedCount);
    }
}
