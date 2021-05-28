using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class GGPlayManager : MonoBehaviour
{
    private static GGPlayManager inst;
    public static GGPlayManager GGPlay { get => inst; }

    public bool isConnected;

    private void Awake()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public void Start()
    {
        SignInToGooglePlay();
    }

    public void SignInToGooglePlay()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {

            switch (result)
            {
                case SignInStatus.Success:
                    isConnected = true;
                    break;
                default:
                    isConnected = false;
                    break;
            }


        });
    }
}
