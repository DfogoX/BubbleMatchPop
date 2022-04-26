using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelsSelection : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnLoad;
        RefreshLevels();

    }

    private void OnLoad(Scene arg0, LoadSceneMode arg1)
    {
        RefreshLevels();
    }
    
    public void RefreshLevels()
    {
        var _currentLevel = GameManager.GmInstance.GetLevelIndex();
        Debug.Log($"refreshing for {_currentLevel} levels");

        var i = 0;
        foreach (Transform t in transform)
        {
            var b = t.GetComponent<Button>();
            if (b == null) continue;
            b.GetComponentInChildren<Text>().text = (i+1).ToString();
            if (i > _currentLevel-1)
            {
                var image = b.gameObject.GetComponent<Image>();
                var c = image.color;
                c.a = 0.5f;
                image.color = c;
                b.enabled = false;
            }
            i++;
        }
    }
}
