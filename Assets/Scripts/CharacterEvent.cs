using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : MonoBehaviour
{
    public string characterName;

    public bool Dialog;
    public Animator Anim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Dialog)
        {
            if(GameplayLoop.Loop.State==GameplayLoop.GameState.CHARACTERWALKING)
            {
            GameplayLoop.Loop.LaunchDialog();
            }
        }
    }
}
