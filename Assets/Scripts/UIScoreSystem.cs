
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreSystem : MonoBehaviour
{
    private Text _rotationsText;
    private Text _bubblesPoppedText;

    private void Awake()
    {
        _rotationsText = GameObject.FindWithTag("PlatformRotationsScore").GetComponentInChildren<Text>();
        if (_rotationsText == null)
        {
            Debug.LogWarning($"PlatformRotationsScore Tag not found");
        }
        _bubblesPoppedText = GameObject.FindWithTag("BubblesPoppedScore").GetComponentInChildren<Text>();
        if (_bubblesPoppedText == null)
        {
            Debug.LogWarning($"BubblesPoppedScore Tag not found");
        }
    }

    private void Start()
    {

    }

    public void UpdatePlatformRotationsScore(int platformsRotated)
    {
        _rotationsText.text = $"Platforms Rotated: {platformsRotated}";
    }
    
    public void UpdateBubblesPoppedScore(int bubblesPopped)
    {
        _bubblesPoppedText.text = $"Bubbles Popped: {bubblesPopped}";
    }
}
