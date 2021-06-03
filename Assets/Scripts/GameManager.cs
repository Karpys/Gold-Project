using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;
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
    public float winScrollSpeed;
    [Header("Defeat")]
    public GameObject defeatScreen;
    [SerializeField] private GameObject dfScroll;
    public float defeatScrollSpeed;
    [Header("")]
    public GameObject startTarget;
    public GameObject endTarget;
    public AnimationCurve anim;

    [Header("Score")]
    [Range(300, 600)] public float increaseSpeed;
    private GameObject scorePanel;
    private GameObject menuButton;
    private Text scoreText;

    [Header("")]
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
        menuButton = winScroll.transform.Find("MainMenu").gameObject;
        scorePanel = winScroll.transform.Find("ScorePanel").gameObject;
        scoreText = scorePanel.transform.Find("ScoreText").GetComponent<Text>();
        menuButton.SetActive(false);
        scorePanel.SetActive(false);

        winScroll.transform.position = startTarget.transform.position;

        float timer = 1.5f;
        float animTime = 0;

        while (timer > 0)
        {
            winScroll.transform.position = Vector3.Lerp(startTarget.transform.position, endTarget.transform.position, anim.Evaluate(animTime));
            animTime += Time.deltaTime;

            timer -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        StartCoroutine(ShowScore());
    }

    public IEnumerator Defeat()
    {
        GameplayLoop.Loop.lockTouch = true;
        defeatScreen.SetActive(true);
        menuButton = dfScroll.transform.Find("MainMenu").gameObject;
        scorePanel = dfScroll.transform.Find("ScorePanel").gameObject;
        scoreText = scorePanel.transform.Find("ScoreText").GetComponent<Text>();
        menuButton.SetActive(false);
        scorePanel.SetActive(false);

        dfScroll.transform.position = startTarget.transform.position;

        float timer = 1.5f;
        float animTime = 0;

        while (timer > 0)
        {
            dfScroll.transform.position = Vector3.Lerp(startTarget.transform.position, endTarget.transform.position, anim.Evaluate(animTime));
            animTime += Time.deltaTime;

            timer -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        StartCoroutine(ShowScore());
    }

    private IEnumerator ShowScore()
    {
        scorePanel.SetActive(true);
        menuButton.SetActive(true);
        scoreText.text = "0";
        
        float scoreShowing = 0;
        int scoreGoal = PlayerData.Stat.CalculateScore();

        while(scoreShowing < scoreGoal)
        {
            scoreShowing += Time.deltaTime * increaseSpeed;
            scoreShowing = Mathf.Min(scoreShowing, scoreGoal);  //Clamp
            scoreText.text = Mathf.RoundToInt(scoreShowing).ToString();

            yield return new WaitForSeconds(Time.deltaTime);
        }
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
