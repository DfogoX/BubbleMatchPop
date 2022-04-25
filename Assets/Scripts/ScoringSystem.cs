
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    private int platformRotations;
    private int bubblesPopped;
    

    private void Start()
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
