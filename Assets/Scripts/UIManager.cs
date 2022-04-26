using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UIInstance { get; private set; }

    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject SingleLevelCanvas;
    [SerializeField] private GameObject LevelsSelectionCanvas;
    private UIScoreSystem _uiScore;
    private GameObject MainMenu;
    private GameObject LevelsMenu;
    private GameObject PauseMenu;
    private GameObject ScoringMenu;
    private GameObject VictoryMenu;
    
    private void Awake()
    {
        if (UIInstance != null && UIInstance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            UIInstance = this;
            _uiScore = gameObject.AddComponent<UIScoreSystem>();
            SceneManager.sceneLoaded += OnSceneLoad;
            MainMenu = GameObject.FindWithTag("MainMenu");
            GameObject.FindWithTag("StartButton").GetComponentInChildren<Text>().text = GameManager.GmInstance.GetLevelIndex() > 1 ? "Continue" : "Start";
            LevelsMenu = GameObject.FindWithTag("LevelsMenu");
            PauseMenu = GameObject.FindWithTag("PauseMenu");
            ScoringMenu = GameObject.FindWithTag("ScoringMenu");
            VictoryMenu = GameObject.FindWithTag("VictoryMenu");
        }
    }

    private void Start()
    {

    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        UIValidation();
    }

    private void UIValidation()
    {
        if (GameManager.GmInstance.GetLevelIndex() > 1)
        {
            //in level
            MainMenu.SetActive(false);
            LevelsMenu.SetActive(false);
            PauseMenu.SetActive(false);
            ScoringMenu.SetActive(true);
            VictoryMenu.SetActive(false);
            _uiScore.UpdateLevelScore(GameManager.GmInstance.GetCurrentLevel());
            UpdateBubblesPoppedScore(0);
            UpdatePlatformsRotatedScore(0);
        }
        else
        {
            //in menu
            MainMenu.SetActive(true);
            LevelsMenu.SetActive(false);
            PauseMenu.SetActive(false);
            ScoringMenu.SetActive(false);
            VictoryMenu.SetActive(false);
        }

    }


    public void UpdatePlatformsRotatedScore(int platformRotationsCount)
    {
        _uiScore.UpdatePlatformRotationsScore(platformRotationsCount);
    }

    public void UpdateBubblesPoppedScore(int bubblesPoppedCount)
    {
        _uiScore.UpdateBubblesPoppedScore(bubblesPoppedCount);
    }

    public void TogglePause()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        var t = Time.timeScale;
        Time.timeScale = Math.Abs(t - 1);
        ScoringMenu.SetActive(!ScoringMenu.activeSelf);
    }

    public void ToggleMenu()
    {
        MainMenu.SetActive(!MainMenu.activeSelf);
        LevelsMenu.SetActive(!LevelsMenu.activeSelf);
    }

    public void ToggleQuit()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        GameManager.GmInstance.ResumeGame();
    }

    public void VictoryScreem()
    {
        if (PauseMenu)
        {
            PauseMenu.SetActive(false);
        }

        if (ScoringMenu)
        {
            ScoringMenu.SetActive(false);
        }

        if (VictoryMenu)
        {
            VictoryMenu.SetActive(true);
        }
    }
}
