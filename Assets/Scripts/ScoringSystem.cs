
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoringSystem : MonoBehaviour
{
    private int platformRotations;
    private int bubblesPopped;
    

    private void Start()
    {
        ResetScore();
        SceneManager.sceneLoaded += OnLoad;
    }

    private void OnLoad(Scene arg0, LoadSceneMode arg1)
    {
        ResetScore();
    }

    public void ResetScore()
    {
        platformRotations = 0;
        bubblesPopped = 0;
    }

    public void IncreasePlatformRotationsCount()
    {
        platformRotations++;
    }
    
    public int GetPlatformRotationsCount()
    {
        return platformRotations;
    }


    public void IncreaseBubblesPoppedCount()
    {
        bubblesPopped++;
    }

    public int GetBubblesPoppedCount()
    {
        return bubblesPopped;
    }

}
