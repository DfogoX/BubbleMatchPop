using System.Collections.Generic;
using System.IO;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    
    private readonly Vector3[] easyMode = { 
        new Vector3 { x = 0, y = 0, z = 0 },
        new Vector3 { x = 1.5f, y = 0, z = 0.875f},
        new Vector3 { x = 0, y = 0, z = 1.75f },
        new Vector3 { x = -1.5f, y = 0, z = 0.875f}
    };
    
    private readonly Vector3[] hardMode = { 
        new Vector3 { x = 0, y = 0, z = 0 },
        new Vector3 { x = 1.5f, y = 0, z = 0.875f},
        new Vector3 { x = 1.5f, y = 0, z = 2.625f},
        new Vector3 { x = 0, y = 0, z = 3.5f },
        new Vector3 { x = -1.5f, y = 0, z = 2.625f},
        new Vector3 { x = -1.5f, y = 0, z = 0.875f}
    };

    [SerializeField] private Color[] colors;
    [SerializeField] private bool easy;
    [SerializeField] private bool update;
    [SerializeField] private LevelData toUpdateLevel;


    private void Start()
    {
        #if UNITY_EDITOR
        if (update)
        {
            Populate(toUpdateLevel);
        }
        else
        {
            var newLevel = ScriptableObject.CreateInstance<LevelData>();
            const string folderPath = "Assets/ScriptableObjects/TestLevels/";
            var filesLength = Directory.GetFiles(folderPath).Length;
            var index = filesLength / 2 + 1;
            var path = folderPath + "LevelData.asset";
            AssetDatabase.CreateAsset(newLevel, path);
            Populate(newLevel);
            AssetDatabase.SaveAssets();
        }
        #endif
    }

    private void Populate(LevelData newLevel)
    {
        var positions = hardMode;
        if (easy)
        {
            positions = easyMode;
        }
        
        var colorQueue = new Queue<Color>();
        var plats = new List<PlatformInfo>();
        
        for (var i = 0; i < positions.Length; i++)
        {
            var p = new PlatformInfo(positions[i]);
            if (i > 0)
            {
                for (var j = 0; j < 3; j++)
                {
                    var color = colors[Random.Range(0, colors.Length)];
                    colorQueue.Enqueue(color);
                    p.AddPlatformBubbleColor(color);
                    p.AddPlatformBubbleColor(colorQueue.Dequeue());
                }
            }
            else
            {
                for (var j = 0; j < 3; j++)
                {
                    var color = colors[Random.Range(0, colors.Length)];
                    colorQueue.Enqueue(color);
                    p.AddPlatformBubbleColor(color);
                }
            }
            plats.Add(p);
        }
        for (var j = 0; j < 3; j++)
        {
            plats[0].AddPlatformBubbleColor(colorQueue.Dequeue());
        }
        
        newLevel.SetPlatformsInfo(plats);
    }
}
