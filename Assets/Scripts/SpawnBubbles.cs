using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnBubbles : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private float SpawnTimer = 2.0f;
    [SerializeField] private float SpawnRadius = 5.0f;
    [SerializeField] private float SpawnedBubbleDuration = 3.0f;
    private float _currentTimer;

    private void Update()
    {
        SpawnBubble();
    }

    private void SpawnBubble()
    {
        _currentTimer += Time.deltaTime;
        if (!(_currentTimer >= SpawnTimer)) return;
        var p = new GameObject();
        var position = transform.position;
        var x = Random.Range(position.x - SpawnRadius, position.x + SpawnRadius);
        var z = Random.Range(position.z - SpawnRadius, position.z + SpawnRadius);
        p.transform.position = new Vector3(x, position.y, z);
        p.AddComponent<Rigidbody>();
        var b = Instantiate(bubble, p.transform.position, Quaternion.identity);
        b.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        b.transform.parent = p.transform;
        p.transform.localScale = new Vector3(100.0f,100.0f,100.0f);
        _currentTimer = 0.0f;
        StartCoroutine(PopBubble(SpawnedBubbleDuration, b));

    }

    private IEnumerator PopBubble(float spawnedBubbleDuration, GameObject o)
    {
        yield return new WaitForSeconds(spawnedBubbleDuration);
        o.GetComponent<Bubble>().MenuPop();
        var p = o.transform.parent;
        o.transform.parent = null;
        Destroy(p.gameObject);
    }
}
