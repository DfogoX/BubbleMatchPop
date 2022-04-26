using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformInfo
{
    [SerializeField] private Vector3 _platformPosition;
    [SerializeField] private List<Color> _bubblesColors;

    public PlatformInfo(Vector3 pos)
    {
        _platformPosition = pos;
        _bubblesColors = new List<Color>();
    }

    public Vector3 GetPlatformPosition()
    {
        return _platformPosition;
    }
    
    public void SetPlatformPosition(Vector3 position)
    {
        _platformPosition = position;
    }
    
    public List<Color> GetPlatformBubbles()
    {
        return _bubblesColors;
    }
    
    public void AddPlatformBubbleColor(Color colors)
    {
        _bubblesColors.Add(colors);
    }

}
