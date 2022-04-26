
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreSystem : MonoBehaviour
{
    private Text _rotationsText;
    private Text _bubblesPoppedText;
    private Text _levelText;

    private void Awake()
    {
        var rotScore = GameObject.FindWithTag("PlatformRotationsScore");
        if (rotScore == null)
        {
            Debug.LogWarning($"PlatformRotationsScore Tag not found");
        }
        else
        {
            _rotationsText = rotScore.GetComponentInChildren<Text>();    
        }

        var popScore = GameObject.FindWithTag("BubblesPoppedScore");
        if (popScore == null)
        {
            Debug.LogWarning($"BubblesPoppedScore Tag not found");
        }
        else
        {
            _bubblesPoppedText = popScore.GetComponentInChildren<Text>();
        }
        
        var levelScore = GameObject.FindWithTag("LevelScore");
        if (popScore == null)
        {
            Debug.LogWarning($"LevelScore Tag not found");
        }
        else
        {
            _levelText = levelScore.GetComponentInChildren<Text>();
        }
    }

    private void Start()
    {

    }

    public void UpdatePlatformRotationsScore(int platformsRotated)
    {
        if (_rotationsText == null) return;
        _rotationsText.text = $"Platforms Rotated: {platformsRotated}";
    }
    
    public void UpdateBubblesPoppedScore(int bubblesPopped)
    {
        if (_bubblesPoppedText == null) return;
        _bubblesPoppedText.text = $"Bubbles Popped: {bubblesPopped}";
    }
    
    public void UpdateLevelScore(int level)
    {
        if (_levelText == null) return;
        _levelText.text = $"Level {level}";
    }
    
    
}
