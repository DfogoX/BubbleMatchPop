using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformInfo
{
    [SerializeField] private Vector3 _platformPosition;
    [SerializeField] private List<Color> _bubblesColors;
    //private Array<PlatformInfo> _neighbours;


    public Vector3 GetPlatformPosition()
    {
        return _platformPosition;
    }
    
    public List<Color> GetPlatformBubbles()
    {
        return _bubblesColors;
    }

}
