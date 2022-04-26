using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GmInstance { get; private set; }
    private bool isRotating;
    [SerializeField] private LevelBuilder _levelBuilder;
    [SerializeField] private LevelData[] _levels;
    private int _lastLevel;

    private void Awake()
    {
        if (GmInstance != null && GmInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GmInstance = this;
            SceneManager.sceneLoaded += OnSceneLoad;
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {
        BuildLevel();
    }

    private void StartIsRotating()
    {
        Debug.Log("spinning");
        isRotating = true;
    }
    
    private void EndIsRotating()
    {
        Debug.Log("stop spin");
        isRotating = false;
    }

    // Add your game manager members here
    public bool GetIsRotating()
    {
        return isRotating;
    }

    public int GetLevelIndex()
    {
        return PlayerPrefs.GetInt("lastlevel", 1);
    }

    private void SetLevelIndex(int index)
    {
        PlayerPrefs.SetInt("lastlevel",_lastLevel);
    }

    public int GetCurrentLevel()
    {
        return _lastLevel;
    }

    public void LoadNewLevel(int levelIndex)
    {
        _lastLevel = levelIndex;
        Debug.Log($"LoadNewLevel");
        if (levelIndex == 0)
        {
            UIManager.UIInstance.InLevelState(false);
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        else
        {
            const string lv = "Level 1";
            //var lv = $"Level {levelIndex}";
            UIManager.UIInstance.InLevelState(true);
            try
            {
                SceneManager.LoadScene(lv, LoadSceneMode.Single);
            }
            catch (Exception e)
            {
                UIManager.UIInstance.InLevelState(false);
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
        }
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        BuildLevel();
    }

    private void BuildLevel()
    {
        if (_lastLevel <= 0) return;
        Debug.Log($"building level {_lastLevel}");
        _levelBuilder.BuildLevel(_levels[_lastLevel-1]);
        ScoringManager.SmInstance.ScoreSetUp();
        var plats = FindObjectsOfType<Platform>();
        foreach (var p in plats)
        {
            p.OnStartRotating+= StartIsRotating;
            p.OnEndRotating += EndIsRotating;
        }
    }

    public void ResumeGame()
    {
        LoadNewLevel(PlayerPrefs.GetInt("lastlevel", 1));
    }

    public void LevelCompleted()
    {
        UIManager.UIInstance.VictoryScreem();
    }

    public void GoToNextLevel()
    {
        if (_lastLevel >= _levels.Length - 1)
        {
            LoadNewLevel(0);
        }
        else
        {
            _lastLevel++;
            var curBest = GetLevelIndex();
            Debug.Log($"Actual {_lastLevel} and Best {curBest}");
            SetLevelIndex(Math.Max(_lastLevel, curBest));
            LoadNewLevel(_lastLevel);
        }

    }

    public void SetInputManager(bool state)
    {
        GetComponent<InputManager>().enabled = state;
    }
}
