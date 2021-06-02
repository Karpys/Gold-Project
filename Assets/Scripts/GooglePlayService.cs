using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class GooglePlayService : MonoBehaviour
{
    public bool isConnectedToGooglePlayServices;
    private static GooglePlayService _instance = null;

    public static GooglePlayService Instance
    {
        get => _instance;
    }

    public long HighScore;
    public Text text;

    private void Awake()
    {
        _instance = this;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ggplay");

/*        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }*/

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }

    public void Start()
    {
        SignInToGooglePlayServices();
    }

    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            switch (result)
            {
                case SignInStatus.Success:
                    isConnectedToGooglePlayServices = true;
                    LoadScore();
                    break;
                default:
                    isConnectedToGooglePlayServices = false;
                    break;
            }
        });
        
    }

    public void LoadScore()
    {
        if (isConnectedToGooglePlayServices)
        {
           
            PlayGamesPlatform.Instance.LoadScores(
                 "CgkIidW02PodEAIQDw",
                 LeaderboardStart.PlayerCentered,
                 1,
                 LeaderboardCollection.Public,
                 LeaderboardTimeSpan.AllTime,
             (LeaderboardScoreData data) => {
                 HighScore = data.PlayerScore.value;
                 text.text = HighScore.ToString();
             });
            /*ILeaderboard lb = Social.CreateLeaderboard();
                 lb.id = "CgkIidW02PodEAIQDw";
                 lb.LoadScores(ok =>
                     {
                         if(ok)
                         {
                             HighScore = (int)lb.localUserScore.value;
                         }
                     }

                     );*/
        }
    }
}