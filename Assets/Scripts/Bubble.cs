using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Bubble _lastBubbleTouched;
    private Color _bubbleColor;
    private bool _touching;
    private bool _exploding;
    private Platform _platform;
    public Action OnBubblePop;
    private Animator _anim;
    [SerializeField] private GameObject bubbleExplosion;

    private void Start()
    {
        _bubbleColor = GetComponent<Renderer>().material.color;
        var p = transform.parent;
        if (p != null)
        {
            _platform = transform.parent.GetComponent<Platform>();
            if (_platform != null)
            {
                _platform.OnEndRotating += CheckMatch;
            }    
        }

        _anim = GetComponent<Animator>();
    }


    private void CheckMatch()
    {
        if (!_touching) return;
        if (_lastBubbleTouched._exploding) return;
        if (_lastBubbleTouched.GetBubbleMaterial() != _bubbleColor) return;
        Debug.Log($"Pop the Bubbles!!");
        _lastBubbleTouched.PopBubble();
        PopBubble();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _touching = true;
        var otherBubble = other.gameObject.GetComponent<Bubble>();
        if (otherBubble != null)
        {
            _lastBubbleTouched = otherBubble;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _touching = false;
    }

    private void PopBubble()
    {
        OnBubblePop?.Invoke();
        _exploding = true;
        _anim.Play("FadeAnimation");
        if (_platform != null) _platform.OnEndRotating -= CheckMatch;
    }

    //event added on end of animation started in PopBubble
    private void EndBubblePopAnimation()
    {
        Destroy(gameObject);
        var explosion = Instantiate(bubbleExplosion, transform.position, Quaternion.identity);
        var main = explosion.GetComponent<ParticleSystem>().main;
        main.startColor = GetComponent<Renderer>().material.color;
    }

    private Color GetBubbleMaterial()
    {
        return _bubbleColor;
    }

    public void MenuPop()
    {
        PopBubble();
        Debug.Log($"gonna disappear : {transform.localScale}");
    }
}
