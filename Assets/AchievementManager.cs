using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static AchievementManager inst;
    public static AchievementManager Achieve { get => inst; }

    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }
    // Update is called once per frame

    public void UnlockAchievement(string name)
    {
        if(GooglePlayService.Instance.isConnectedToGooglePlayServices)
        {
            if(Social.localUser.authenticated)
            {
            Social.ReportProgress(name, 100f, sucess => { });
            }
        }
    }

}
