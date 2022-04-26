using ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GmInstance { get; private set; }
    private bool isRotating;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private LevelData[] _levels;
    private int _levelIndex = 1;
    private int _lastLevel; 

    private void Awake()
    {
        if (GmInstance != null && GmInstance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            GmInstance = this; 
        }
    }

    private void Start()
    {
        if (_levels != null)
        {
            _levelBuilder.BuildLevel(_levels[_levelIndex]);    
        }

        _lastLevel = PlayerPrefs.GetInt("lastlevel");
        
        
        var plats = FindObjectsOfType<Platform>();
        foreach (var p in plats)
        {
            p.OnStartRotating+= StartIsRotating;
            p.OnEndRotating += EndIsRotating;
        }
    }

    private void StartIsRotating()
    {
        isRotating = true;
    }
    
    private void EndIsRotating()
    {
        isRotating = false;
    }

    // Add your game manager members here
    public bool GetIsRotating()
    {
        return isRotating;
    }

    public void SetIndex(int index)
    {
        _levelIndex = index;
    }

}
