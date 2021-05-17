using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleRotation : MonoBehaviour
{
    public puzzle[] piece;
    public GameObject win;
    public GameObject lose;
    public Text timerText;
    public float time;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<piece.Length;i++)
        {
            int ramdon = Random.Range(0, 4);
            Debug.Log(ramdon);
            switch(ramdon)
            {
                case 0:
                    piece[i].etat = ramdon;
                    break;

                case 1:
                    piece[i].etat = ramdon;
                    piece[i].piece.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;

                case 2:
                    piece[i].etat = ramdon;
                    piece[i].piece.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;

                case 3:
                    piece[i].etat = ramdon;
                    piece[i].piece.transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!lose.activeSelf)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 zoneTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                if (touch.phase == TouchPhase.Began)
                {
                    for (int i = 0; i < piece.Length; i++)
                        Rotation(i, zoneTouch); ;
                }
            }
            
            if(!win.activeSelf)
                Timer();
        }
    }

    public void Rotation(int i ,Vector3 zoneTouch)
    {
        switch (piece[i].etat)
        {
            case 0:
                if ((zoneTouch.y < piece[i].targetTopLeft.transform.position.y && zoneTouch.y > piece[i].targetBotRight.transform.position.y) && (zoneTouch.x > piece[i].targetTopLeft.transform.position.x && zoneTouch.x < piece[i].targetBotRight.transform.position.x))
                {
                    piece[i].piece.transform.eulerAngles = new Vector3(0, 0, 90);
                    piece[i].etat++;
                    Win();
                }
                break;

            case 1:
                if ((zoneTouch.y > piece[i].targetTopLeft.transform.position.y && zoneTouch.y < piece[i].targetBotRight.transform.position.y) && (zoneTouch.x > piece[i].targetTopLeft.transform.position.x && zoneTouch.x < piece[i].targetBotRight.transform.position.x))
                {
                    piece[i].piece.transform.eulerAngles = new Vector3(0, 0, 180);
                    piece[i].etat++;
                    Win();
                }
                break;

            case 2:
                if ((zoneTouch.y > piece[i].targetTopLeft.transform.position.y && zoneTouch.y < piece[i].targetBotRight.transform.position.y) && (zoneTouch.x < piece[i].targetTopLeft.transform.position.x && zoneTouch.x > piece[i].targetBotRight.transform.position.x))
                {
                    piece[i].piece.transform.eulerAngles = new Vector3(0, 0, 270);
                    piece[i].etat++;
                    Win();
                }
                break;

            case 3:
                if ((zoneTouch.y < piece[i].targetTopLeft.transform.position.y && zoneTouch.y > piece[i].targetBotRight.transform.position.y) && (zoneTouch.x < piece[i].targetTopLeft.transform.position.x && zoneTouch.x > piece[i].targetBotRight.transform.position.x))
                {
                    piece[i].piece.transform.eulerAngles = new Vector3(0, 0, 0);
                    piece[i].etat = 0;
                    Win();
                }
                break;
        }
    }

    public void Win()
    {
        bool isOkay = true;
        for(int i = 0; i<piece.Length; i++)
        {
            if (piece[i].etat != 0)
                isOkay = false;
        }

        if (isOkay)
            win.SetActive(true);

    }

    public void Timer()
    {
        time -= Time.deltaTime; 
        int timerIntP1 = (int)time;
        int timerIntP2 = (int)(time * 10 - timerIntP1 * 10);
        timerText.text = timerIntP1.ToString()+","+ timerIntP2.ToString();
        if(time<0)
        {
            time = 0;
            lose.SetActive(true);
        }
    }

    [System.Serializable]
    public struct puzzle
    {
        public GameObject piece;
        public GameObject targetTopLeft;
        public GameObject targetBotRight;
        public int etat;
    }
}
