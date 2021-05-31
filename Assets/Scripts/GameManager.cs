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
    //public string menuScene;
    [Header("Menu")]
    public GameObject menuScreen;
    [Header("Win")]
    public GameObject winScreen;
    [SerializeField] private GameObject winScroll;
    [SerializeField] private GameObject winHandle;
    public GameObject winStartTarget;
    public GameObject winEndTarget;
    public float winScrollSpeed;
    [Header("Defeat")]
    public GameObject defeatScreen;
    [SerializeField] private GameObject dfScroll;
    [SerializeField] private GameObject dfHandle;
    public GameObject dfStartTarget;
    public GameObject dfEndTarget;
    public float defeatScrollSpeed;

    public static bool loadMenu = true;

///

    private void Awake()
    {
        if (inst == null)
            inst = this;
        //DontDestroyOnLoad(this);

        gameplayElements = new List<GameObject>(); // UI Canvas + GameplayLoop

        gameplayElements.Add(FindObjectOfType<GameplayLoop>().gameObject);
        gameplayElements.Add(GameObject.Find("Main Canvas"));
    }

    private void Start()
    {
        if (loadMenu) Menu();
        else PlayGame();
    }

    private void Init()
    {
        PlayerData.Stat.Init();

        foreach (GameObject element in gameplayElements)
        {
            element.SetActive(false);
        }
    }

    public void PlayGame()
    {
        // Unload Menu //change > UnloadScene(menuScene);
        menuScreen.SetActive(false);

        foreach (GameObject element in gameplayElements)
        {
            element.SetActive(true);
        }

        GameplayLoop.Loop.ResetPool();
        GameplayLoop.Loop.lockTouch = false;
    }

    public void Menu()
    {
        Init();

        menuScreen.SetActive(true);
    }

    public IEnumerator Win()
    {
        GameplayLoop.Loop.lockTouch = true;
        winScreen.SetActive(true);
        winHandle.transform.position = winStartTarget.transform.position;
        bool closed = true;
        float lerpValue = 0;
        Vector3 lockPos = winScroll.transform.position;

        while (closed)
        {
            winHandle.transform.position = Vector3.LerpUnclamped(winStartTarget.transform.position, winEndTarget.transform.position, lerpValue);
            winScroll.transform.position = lockPos;
            lerpValue += winScrollSpeed * Time.deltaTime;

            if (lerpValue >= 1) closed = false;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        winHandle.transform.position = winEndTarget.transform.position;
    }

    public IEnumerator Defeat()
    {
        GameplayLoop.Loop.lockTouch = true;
        defeatScreen.SetActive(true);
        dfHandle.transform.position = dfStartTarget.transform.position;
        bool closed = true;
        float lerpValue = 0;
        Vector3 lockPos = dfScroll.transform.position;

        while(closed)
        {
            dfHandle.transform.position = Vector3.LerpUnclamped(dfStartTarget.transform.position, dfEndTarget.transform.position, lerpValue);
            dfScroll.transform.position = lockPos;
            lerpValue += defeatScrollSpeed * Time.deltaTime;

            if (lerpValue >= 1) closed = false;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        dfHandle.transform.position = dfEndTarget.transform.position;
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
