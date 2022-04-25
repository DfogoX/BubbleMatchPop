using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    [SerializeField] float duration = 1.0f;
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
        Debug.Log("Starting Rotation");
        OnStartRotating?.Invoke();
        var startRot = transform.rotation;
        var startEuler = startRot.eulerAngles;
        var endRot = Quaternion.Euler(startEuler.x, startEuler.y+60, startEuler.z);

        for (float t = 0; t < duration; t+= Time.fixedDeltaTime) 
        {
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return new WaitForFixedUpdate();    
        }

        transform.rotation = endRot;
        Debug.Log("Ending Rotation");
        OnEndRotating?.Invoke();
    }
}
