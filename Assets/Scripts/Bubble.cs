using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Bubble _lastBubbleTouched;
    private Renderer _bubbleRenderer;
    private bool _touching;
    private bool _exploding;
    private Platform _platform;
    public Action OnBubblePop;
    private Animator _anim;
    [SerializeField] private GameObject bubbleExplosion;

    private void Start()
    {
        _bubbleRenderer = GetComponent<Renderer>();
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
        if (_lastBubbleTouched.GetBubbleColor() != _bubbleRenderer.material.color) return;
        _lastBubbleTouched.PopBubble();
        PopBubble();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var otherBubble = other.gameObject.GetComponent<Bubble>();
        if (otherBubble != null)
        {
            _touching = true;
            //Debug.Log($"{transform.name} is touching {other.name}");
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
        var ps = explosion.GetComponent<ParticleSystem>();
        var main = ps.main;
        var trail = ps.trails;
        main.startColor = new ParticleSystem.MinMaxGradient(_bubbleRenderer.material.color);
        trail.colorOverLifetime = new ParticleSystem.MinMaxGradient(_bubbleRenderer.material.color);
        ps.Play();

    }

    public void SetBubbleColor(Color c)
    {
        if (_bubbleRenderer == null)
        {
            _bubbleRenderer = GetComponent<Renderer>();
        }
        _bubbleRenderer.material.color = c;
    }

    private Color GetBubbleColor()
    {
        return _bubbleRenderer.material.color;
    }

    public void MenuPop()
    {
        PopBubble();
    }
}
