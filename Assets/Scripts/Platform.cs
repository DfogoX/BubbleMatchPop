using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Platform : MonoBehaviour
{
    
    [SerializeField] private float rotationDuration = 1.0f;
    public Action OnStartRotating;
    public Action OnEndRotating;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RotatePlatform()
    {
        if (GameManager.GmInstance.GetIsRotating()) return;
        StartCoroutine(InteractionRotate());
    }

    private IEnumerator InteractionRotate()
    {
        OnStartRotating?.Invoke();
        var startRot = transform.rotation;
        var startEuler = startRot.eulerAngles;
        var endRot = Quaternion.Euler(startEuler.x, startEuler.y+60, startEuler.z);

        for (float t = 0; t < rotationDuration; t+= Time.fixedDeltaTime) 
        {
            transform.rotation = Quaternion.Lerp(startRot, endRot, t/rotationDuration);
            yield return new WaitForFixedUpdate();    
        }

        transform.rotation = endRot;
        OnEndRotating?.Invoke();
    }
}
