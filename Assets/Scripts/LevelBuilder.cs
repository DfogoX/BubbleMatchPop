using ScriptableObjects;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private GameObject PlatformPrefab;
    private int _totalBubbles;

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
            o.transform.rotation*=Quaternion.Euler(-90,30,0);
            var bubbles = o.GetComponentsInChildren <Bubble>();
            _totalBubbles += bubbles.Length;
            var colors = p.GetPlatformBubbles();
            for (int i = 0; i < bubbles.Length; i++)
            {
                bubbles[i].SetBubbleColor(colors[i]);
            }
        }
    }
}
