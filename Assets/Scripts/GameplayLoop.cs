using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoop : MonoBehaviour
{
    private static GameplayLoop inst;
    public static GameplayLoop Loop { get => inst; }
    // Start is called before the first frame update
    public GameState State;
    public Transform CharacterSpawn;
    public GameObject CharacterSpawned;
    public int IdPool;
    // Update is called once per frame

    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(State==GameState.IDLE)
            {
            LaunchEvent();
            }
        }
    }



    public void LaunchEvent()
    {
        GameObject obj = Instantiate(EventSystem.Manager.eventPool[IdPool].CharacterSpawn, CharacterSpawn);
        CharacterSpawned = obj;
        State = GameState.CHARACTERWALKING;
        
    }

    public void LaunchDialog()
    {
        State = GameState.CHARACTERDIALOG;
        EventSystem.Manager.NextEvent(EventSystem.Manager.eventPool[IdPool]);
    }

    

    public IEnumerator EndEvent()
    {

        //StartCoroutine(GameplayLoop.Loop.EndEvent());//
        CharacterSpawned.GetComponent<CharacterEvent>().Anim.SetBool("Destroy", true);
        yield return new WaitForSeconds(1.0f);
        IdPool += 1;
        Destroy(CharacterSpawned);
        CharacterSpawned = null;
        State = GameState.IDLE;
    }

    public void ResetPool()
    {
        IdPool = 0;
    }

    public enum GameState
    {
        IDLE,
        CHARACTERSPAWN,
        CHARACTERWALKING,
        CHARACTERDIALOG,
    }
}
