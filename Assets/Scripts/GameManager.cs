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
    [Header("Defeat")]
    public GameObject defeatScreen;
    [SerializeField] private GameObject scroll;
    [SerializeField] private GameObject handle;
    public GameObject startTarget;
    public GameObject endTarget;
    public float scrollSpeed;

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

    public IEnumerator Defeat()
    {
        defeatScreen.SetActive(true);
        handle.transform.position = startTarget.transform.position;
        bool closed = true;
        float lerpValue = 0;
        Vector3 lockPos = scroll.transform.position;

        while(closed)
        {
            handle.transform.position = Vector3.LerpUnclamped(startTarget.transform.position, endTarget.transform.position, lerpValue);
            scroll.transform.position = lockPos;
            lerpValue += scrollSpeed * Time.deltaTime;

            if (lerpValue >= 1) closed = false;

            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log(lerpValue);
        }

        handle.transform.position = endTarget.transform.position;
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
