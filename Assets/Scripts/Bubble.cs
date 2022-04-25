using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Bubble lastBubbleTouched;
    private Color bubbleColor;
    private bool touching;
    private Platform _platform;

    private void Start()
    {
        bubbleColor = GetComponent<Renderer>().material.color;
        _platform = transform.parent.GetComponent<Platform>();
        if (_platform != null)
        {
            _platform.OnEndRotating += CheckMatch;
        }
    }


    private void CheckMatch()
    {
        if (!touching) return;
        if (lastBubbleTouched.GetBubbleMaterial() != bubbleColor) return;
        Debug.Log($"Pop the Bubbles!!");
        lastBubbleTouched.PopBubble();
        PopBubble();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        touching = true;
        var otherBubble = other.gameObject.GetComponent<Bubble>();
        if (otherBubble != null)
        {
            lastBubbleTouched = otherBubble;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        touching = false;
    }

    private void PopBubble()
    {
        _platform.OnEndRotating -= CheckMatch;
        Destroy(gameObject);
    }

    private Color GetBubbleMaterial()
    {
        return bubbleColor;
    }
}
