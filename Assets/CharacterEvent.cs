using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvent : MonoBehaviour
{
    // Start is called before the first frame update
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
