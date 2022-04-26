using System;
using DitzeGames.Effects;
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

    public int GetLevelIndex()
    {
        var a = PlayerPrefs.GetInt("lastlevel", 1);
        Debug.Log($"i found max level to be: {a}");
        return a;
    }

    private void SetLevelIndex(int index)
    {
        PlayerPrefs.SetInt("lastlevel",index);
        Debug.Log($"max level is now: {index}");
    }

    public int GetCurrentLevel()
    {
        return _lastLevel;
    }

    public void LoadNewLevel(int levelIndex)
    {
        _lastLevel = levelIndex;
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
        CameraEffects.ShakeOnce(1f, 20);
        UIManager.UIInstance.VictoryScreem();
    }

    public void GoToNextLevel()
    {
        if (_lastLevel >= _levels.Length - 1)
        {
            var lvl = GameObject.FindObjectOfType<UILevelsSelection>();
            if (lvl != null)
            {
                lvl.RefreshLevels();
            }
            LoadNewLevel(0);
        }
        else
        {
            _lastLevel++;
            var curBest = GetLevelIndex();
            Debug.Log($"Actual {_lastLevel} and Best {curBest}");
            var max = Math.Max(_lastLevel, curBest);
            Debug.Log($"max is {max}");
            SetLevelIndex(max);
            LoadNewLevel(_lastLevel);
        }

    }

    public void SetInputManager(bool state)
    {
        GetComponent<InputManager>().enabled = state;
    }
}
