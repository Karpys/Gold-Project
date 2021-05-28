using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager inst;
    public static GameManager Get { get => inst; }

///

    private List<GameObject> gameplayElements;
    public string menuScene;
    public GameObject winScreen;
    public GameObject defeatScreen;

///

    private void Awake()
    {
        if (inst == null) inst = this;
        DontDestroyOnLoad(this);
        
        Init();
    }

    private void Init()
    {
        gameplayElements = new List<GameObject>(); // UI Canvas + GameplayLoop

        gameplayElements.Add(FindObjectOfType<GameplayLoop>().gameObject);
        gameplayElements.Add(GameObject.Find("Main Canvas"));

        foreach (GameObject element in gameplayElements)
        {
            element.SetActive(false);
        }

        LoadScene(menuScene, true);
    }

    public void PlayGame()
    {
        UnloadScene(menuScene);
        foreach (GameObject element in gameplayElements)
        {
            element.SetActive(true);
        }
    }

    public void Win()
    {

    }

    public void Defeat()
    {

    }

///

    public void LoadScene(string sceneName, bool loadInAdditive)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            if (loadInAdditive)
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            else
                SceneManager.LoadScene(sceneName);
        }
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
