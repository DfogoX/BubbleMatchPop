
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void Update()
    {
        transform.LookAt(target.transform);
        transform.Translate(Vector3.right * Time.deltaTime);
    }

}
