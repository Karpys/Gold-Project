using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleRotation : MonoBehaviour
{
    public puzzle[] piece;
    public GameObject[] targetEnd;
    public Text timerText;
    public float time;
    public float speed;
    public float speedRotation;

    private bool win;
    private bool lose;
    private List<int> pieceTurn = new List<int>() ;
    private float timeEnd = 0;
    private float lerp = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0;i<piece.Length;i++)
        {
            int ramdon = Random.Range(0, 4);
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
        if(!lose)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 zoneTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                if (touch.phase == TouchPhase.Began)
                {
                    for (int i = 0; i < piece.Length; i++)
                    {
                        if ((zoneTouch.y < piece[i].targetTopLeft.transform.position.y && zoneTouch.y > piece[i].targetBotRight.transform.position.y) && (zoneTouch.x > piece[i].targetTopLeft.transform.position.x && zoneTouch.x < piece[i].targetBotRight.transform.position.x))
                            pieceTurn.Add(i);
                        if ((zoneTouch.y > piece[i].targetTopLeft.transform.position.y && zoneTouch.y < piece[i].targetBotRight.transform.position.y) && (zoneTouch.x > piece[i].targetTopLeft.transform.position.x && zoneTouch.x < piece[i].targetBotRight.transform.position.x))
                            pieceTurn.Add(i);
                        if ((zoneTouch.y > piece[i].targetTopLeft.transform.position.y && zoneTouch.y < piece[i].targetBotRight.transform.position.y) && (zoneTouch.x < piece[i].targetTopLeft.transform.position.x && zoneTouch.x > piece[i].targetBotRight.transform.position.x))
                            pieceTurn.Add(i);
                        if ((zoneTouch.y < piece[i].targetTopLeft.transform.position.y && zoneTouch.y > piece[i].targetBotRight.transform.position.y) && (zoneTouch.x < piece[i].targetTopLeft.transform.position.x && zoneTouch.x > piece[i].targetBotRight.transform.position.x))
                            pieceTurn.Add(i);

                    }
                }
            }            
        }

        Rotation();

        if(!win)
            Timer();
    }

    public void Rotation()
    {
        if(pieceTurn.Count != 0)
        {
            for(int i  = 0;i<pieceTurn.Count;i++)
            {
                switch (piece[pieceTurn[i]].etat)
                {
                    case 0:
                        if (piece[pieceTurn[i]].piece.transform.eulerAngles.z < 90)
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, piece[pieceTurn[i]].piece.transform.eulerAngles.z + (speedRotation*Time.deltaTime));
                        }
                        else 
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, 90);
                            piece[pieceTurn[i]].etat++;
                            pieceTurn.RemoveAt(i);
                        }

                
                        break;

                    case 1:
                        if (piece[pieceTurn[i]].piece.transform.eulerAngles.z < 180)
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, piece[pieceTurn[i]].piece.transform.eulerAngles.z + (speedRotation * Time.deltaTime));
                        }
                        else
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, 180);
                            piece[pieceTurn[i]].etat++;
                            pieceTurn.RemoveAt(i);
                        }

                        break;

                    case 2:
                        if (piece[pieceTurn[i]].piece.transform.eulerAngles.z < 270)
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, piece[pieceTurn[i]].piece.transform.eulerAngles.z + (speedRotation * Time.deltaTime));
                        }
                        else
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, 270);
                            piece[pieceTurn[i]].etat++;
                            pieceTurn.RemoveAt(i);
                        }

                        break;

                    case 3:
                        if (piece[pieceTurn[i]].piece.transform.eulerAngles.z > 3)
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, piece[pieceTurn[i]].piece.transform.eulerAngles.z + (speedRotation * Time.deltaTime));
                        }
                        else
                        {
                            piece[pieceTurn[i]].piece.transform.eulerAngles = new Vector3(0, 0, 0);
                            piece[pieceTurn[i]].etat = 0;
                            pieceTurn.RemoveAt(i);
                        }

                        break;
                }
            }
        }
        else
            Win();    

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
        {
            win = true;
            timeEnd += Time.deltaTime;
            if(timeEnd<1)
            {
                lerp += Time.deltaTime * speed;
                for (int i = 0; i < piece.Length; i++)
                    piece[i].piece.transform.position = Vector2.Lerp(piece[i].piece.transform.position, targetEnd[i].transform.position, lerp);
            }
            else
                StartCoroutine(EventSystem.Manager.EndGame(true));
        }
            

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
            lose = true;
        }


        if(pieceTurn.Count == 0 && lose)
        {
            Debug.Log("je suis bien la");
            timeEnd += Time.deltaTime;
            if (timeEnd < 1)
            {
                lerp += Time.deltaTime * speed;
                for (int i = 0; i < piece.Length; i++)
                    piece[i].piece.transform.position = Vector2.Lerp(piece[i].piece.transform.position, targetEnd[i].transform.position, lerp);
            }
            else
                StartCoroutine(EventSystem.Manager.EndGame(false));
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
