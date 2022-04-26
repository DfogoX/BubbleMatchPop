using System;
using ScriptableObjects;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameObject PlatformPrefab;

    private void Start()
    {
        //throw new NotImplementedException();
    }


    public void BuildLevel(LevelData levelData)
    {
        var plats = levelData.GetPlatformsInfo();
        foreach (var p in plats)
        {
            var o = Instantiate(PlatformPrefab, p.GetPlatformPosition(), Quaternion.identity);
            o.transform.rotation*=Quaternion.Euler(-90,0,0);
            Debug.Log($"Created platform on: {o.transform.position} with rotation: {o.transform.rotation.eulerAngles}");
            var bubbles = o.GetComponentsInChildren <Bubble>();
            var colors = p.GetPlatformBubbles();
            for (int i = 0; i < bubbles.Length; i++)
            {
                Debug.Log($"coloring bubble: {bubbles[i]}");
                bubbles[i].SetBubbleColor(colors[i]);
            }
        }
    }
}
