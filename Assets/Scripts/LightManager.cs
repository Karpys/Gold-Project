using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightManager : MonoBehaviour
{
    public Light2D sun;
    public Color sunColor;
    public GameObject[] nightRoom;
    public GameObject nightCharacter;
    public GameObject dayCharacter;
    //public Light2D dayCharacterLight;
    public float speedLerp;
    private float lerp = 0;
    private int backUp;
    private bool switchSun;

//#003382
    // Start is called before the first frame update
    void Start()
    {

        backUp = GameplayLoop.Loop.IdPool;
        /*if (EventPoolDraw.Pool.currentTemplate.templateList.Count != null && EventPoolDraw.Pool.currentTemplate.templateList[GameplayLoop.Loop.IdPool].state == EventPoolDraw.DayNight.Night)
        {
            for (int i = 0; i < nightRoom.Length; i++)
                nightRoom[i].SetActive(true);

            nightCharacter.SetActive(true);
            dayCharacter.SetActive(false);
            sun.color = Color.blue;
            switchSun = true;
            
        }
        else
        {
            for (int i = 0; i < nightRoom.Length; i++)
                nightRoom[i].SetActive(false);

            nightCharacter.SetActive(false);
            dayCharacter.SetActive(true);
            sun.color = Color.white;
            switchSun = false;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if(GameplayLoop.Loop.IdPool >= 6)
        {
            SetDay();
        }else
        {

        if (backUp != GameplayLoop.Loop.IdPool)
        {
            lerp = 0;
            backUp = GameplayLoop.Loop.IdPool;
            if (EventPoolDraw.Pool.currentTemplate.templateList[GameplayLoop.Loop.IdPool].state == EventPoolDraw.DayNight.Night)
                switchSun = true;
            else
                switchSun = false;
        }


        if( EventPoolDraw.Pool.currentTemplate.templateList[GameplayLoop.Loop.IdPool].state == EventPoolDraw.DayNight.Day && !switchSun)
        {
            SetDay();
        }
        else if (switchSun )
        {
            SetNight();
        }
        }
    }


    void SetNight()
    {
        for (int i = 0; i < nightRoom.Length; i++)
            nightRoom[i].SetActive(true);

        nightCharacter.SetActive(true);
        dayCharacter.SetActive(false);
        lerp += Time.deltaTime * speedLerp;
        if (sun.color != sunColor)
            sun.color = Color.Lerp(Color.white, sunColor, lerp);
    }

    void SetDay()
    {
        for (int i = 0; i < nightRoom.Length; i++)
            nightRoom[i].SetActive(false);

        nightCharacter.SetActive(false);
        dayCharacter.SetActive(true);
        lerp += Time.deltaTime * speedLerp;
        if (sun.color != Color.white)
            sun.color = Color.Lerp(sunColor, Color.white, lerp);
    }
}
