using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabirinteManager : MonoBehaviour
{
    public Spirit spirit;
    public GameObject[] labirintes;
    public GameObject winZone;
    public GameObject start;
    public Text timer;
    public float min;
    public float max;

    public bool lose = false;
    
    private bool win = false;
    private bool touchSpirit = false;
    private float timerP1;
    private int timerP2;

    // Start is called before the first frame update
    void Start()
    {
        labirintes[Random.Range(0, labirintes.Length)].SetActive(true);
        timerP1 = (int)Random.Range(min, max + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!win && timerP1 > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 zoneTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                if (touch.phase == TouchPhase.Began)
                {
                    if ((zoneTouch.y < spirit.targetTopLeft.transform.position.y && zoneTouch.y > spirit.targetBotRight.transform.position.y) && (zoneTouch.x > spirit.targetTopLeft.transform.position.x && zoneTouch.x < spirit.targetBotRight.transform.position.x))
                        touchSpirit = true;
                }

                if (touch.phase == TouchPhase.Ended)
                    touchSpirit = false;

                MoveSpirit(touch);
            }
        }
        WinLose();
        Timer();
    }

    public void MoveSpirit(Touch touch)
    {
        if(touchSpirit)
        {
            spirit.pepole.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
        }
    }

    public void WinLose ()
    {
        if (winZone.transform.position.x < spirit.pepole.transform.position.x)
        {
            StartCoroutine(EventSystem.Manager.EndGame(true));
            win = true;
        }
        else if (lose)
        {
            spirit.pepole.transform.position = start.transform.position;
            lose = false;
            touchSpirit = false;
        }
            
        else if (timerP1 < 0)
            StartCoroutine(EventSystem.Manager.EndGame(false));
        else
            timerP1 -= Time.deltaTime;
    }

    public void Timer()
    {
        timerP2 = (int)(timerP1 * 10 - ((int)timerP1) * 10);
        timer.text = ((int)timerP1).ToString() + "," + timerP2.ToString();
    }

    [System.Serializable]
    public struct Spirit
    {
        public GameObject pepole;
        public GameObject targetTopLeft;
        public GameObject targetBotRight;
    }

}
