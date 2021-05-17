using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameState State;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            LaunchEvent();
        }
    }



    public void LaunchEvent()
    {
        EventSystem.Manager.NextEvent(EventSystem.Manager.eventPool[0]);
    }


    public enum GameState
    {
        IDLE,
        CHARACTERSPAWN,
        CHARACTERWALKING,
        CHARACTERDIALOG,
        INMINIGAME,

    }
}
