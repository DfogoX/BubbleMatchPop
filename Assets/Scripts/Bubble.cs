using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Bubble lastBubbleTouched;
    private Color bubbleColor;
    private bool touching;
    private bool exploding;
    private Platform _platform;
    public Action OnBubblePop;
    private Animator _anim;
    [SerializeField] private GameObject bubbleExplosion;

    private void Start()
    {
        bubbleColor = GetComponent<Renderer>().material.color;
        _platform = transform.parent.GetComponent<Platform>();
        if (_platform != null)
        {
            _platform.OnEndRotating += CheckMatch;
        }

        _anim = GetComponent<Animator>();
    }


    private void CheckMatch()
    {
        if (!touching) return;
        if (lastBubbleTouched.exploding) return;
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
        OnBubblePop?.Invoke();
        exploding = true;
        _platform.OnEndRotating -= CheckMatch;
        _anim.Play("FadeAnimation");
    }

    //event added on end of animation started in PopBubble
    private void EndBubblePopAnimation()
    {
        Destroy(gameObject);
        var explosion = Instantiate(bubbleExplosion, transform.position, Quaternion.identity);
        var main = explosion.GetComponent<ParticleSystem>().main;
        main.startColor = bubbleColor;
    }

    private Color GetBubbleMaterial()
    {
        return bubbleColor;
    }
}
