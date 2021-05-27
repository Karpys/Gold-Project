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
        // enables saving game progress.
        .EnableSavedGames()
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        .RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        .RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        .RequestIdToken()
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
