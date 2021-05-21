using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memorie : MonoBehaviour
{
    public Card[] card;
    public GameObject[] Target;
    public float speed;
    public float speedRotation;
    public int life;

    private bool start = true;
    private bool[] freeTarget = new bool [8];
    private bool distribution = true;
    private bool check = false;
    private bool win = true;
    private bool firstCard = false;
    private bool rotationCardN1 = false;
    private bool rotationCardN2 = false;
    private bool endCheck = false;
    private float time = 0;
    private float lerp = 0;
    private int saveCardN1 = -1;
    private int saveCardN2 = -1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < card.Length; i++)
        {
            card[i].zone = -1;
            freeTarget[i] = true;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
            RamdomCardStart();
        else
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 zoneTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log(endCheck);
                    if(!endCheck && !check)
                    {
                        for (int i = 0; i < card.Length; i++)
                        {
                            if((zoneTouch.y < card[i].targetTopLeft.transform.position.y && zoneTouch.y > card[i].targetBotRight.transform.position.y) && (zoneTouch.x > card[i].targetTopLeft.transform.position.x && zoneTouch.x < card[i].targetBotRight.transform.position.x))
                            {
                                //card[i].phaseItem.SetActive(true);
                                if (!firstCard)
                                {
                                    firstCard = true;
                                    saveCardN1 = i;                                
                                }
                                else
                                {
                                    time = 0;
                                    saveCardN2 = i;
                                    check = true;
                                }
                            }

                        }
                    }    
                }
            }
            RotationCardSelect();
            Check();
            RotationCardEnd();
        }
    }

    public void Win()
    {

        for (int i = 0; i < card.Length; i++)
        {
            if (win)
                win = !card[i].phaseCacher.activeSelf;
        }

        if (win)
            EventSystem.Manager.EndGame(false);
    }

    public void Lose()
    {
        if (life < 0)
            EventSystem.Manager.EndGame(false);
    }

     public void RamdomCardStart()
    {

        time += Time.deltaTime;
        Vector3 dir = (card[0].phaseCacher.transform.position - new Vector3(0, 0,0)).normalized;

        if(time>=2)
        {
            if(distribution)
            {
                //Debug.Log(card[0].phaseCacher.transform.eulerAngles.y);
                for (int i = 0; i < card.Length; i++)
                {
                    //Debug.Log("test");
                    //Debug.Log((speedRotation * Time.deltaTime));

                    if (card[i].phaseCacher.transform.eulerAngles.y>90)
                        card[i].phaseItem.SetActive(false);

                    if(card[i].phaseCacher.transform.eulerAngles.y >= 180)
                        card[i].phaseCacher.transform.eulerAngles = new Vector3(0,180, 0);
                    else
                        card[i].phaseCacher.transform.eulerAngles = new Vector3(0, card[i].phaseCacher.transform.eulerAngles.y + (speedRotation * Time.deltaTime), 0);
                }

                if(card[7].phaseCacher.transform.eulerAngles.y == 180)
                {
                    lerp += Time.deltaTime * speed / 10;
                
                    for (int i = 0; i < card.Length; i++)    
                        card[i].phaseCacher.transform.position = Vector2.Lerp(card[i].phaseCacher.transform.position, new Vector3(0, 0, 0), lerp/10);

                    if(lerp > 2)
                    {
                        if (card[7].zone == -1)
                        {
                            for (int i = 0; i < card.Length; i++)
                            {
                                int random = Random.Range(0, 8);
                                while (!freeTarget[random])
                                    random = Random.Range(0, 8);

                                card[i].zone = random;
                                freeTarget[random] = false;
                            }
                        }
                        else
                        {
                            distribution = false;
                            lerp = 0;
                        }                        
                    }
                }
            }
            else
            {
                if (lerp < 1)
                    lerp += Time.deltaTime * speed / 10;
                else
                {
                    start = false;
                    time = 0;
                }


                for (int i = 0; i < card.Length; i++)
                    card[i].phaseCacher.transform.position = Vector2.Lerp(card[i].phaseCacher.transform.position, Target[card[i].zone].transform.position, lerp/10);
            }
        }
    }

    public void Check()
    {
        time += Time.deltaTime;

        if(check)
        {
            if(time>2)
            {
                if(card[saveCardN1].item == card[saveCardN2].item)
                {
                    card[saveCardN2].phaseCacher.SetActive(false);
                    card[saveCardN1].phaseCacher.SetActive(false);
                    win = true;
                    saveCardN1 = -1;
                    saveCardN2 = -1;
                    rotationCardN1 = false;
                    rotationCardN2 = false;
                    //endCheck = false;
                    Win();
                }
                else 
                {
                    card[saveCardN2].phaseItem.SetActive(false);
                    card[saveCardN1].phaseItem.SetActive(false);
                    life--;

                    endCheck = true;
                    Lose();
                }
                firstCard = false;
                check = false;

            }
        }
    }

    public void RotationCardSelect()
    {
        if(saveCardN1 != -1)
        {
            if (card[saveCardN1].phaseCacher.transform.eulerAngles.y < 90)
                card[saveCardN1].phaseItem.SetActive(true);

            if (card[saveCardN1].phaseCacher.transform.eulerAngles.y >= 181 && !rotationCardN1)
            {
                card[saveCardN1].phaseCacher.transform.eulerAngles = new Vector3(0, 0, 0);
                rotationCardN1 = true;
            }
            else if (!rotationCardN1)
            {
                //Debug.Log("je te fais chier");

                card[saveCardN1].phaseCacher.transform.eulerAngles = new Vector3(0, card[saveCardN1].phaseCacher.transform.eulerAngles.y - (speedRotation * Time.deltaTime), 0);
            }
        }

        if (saveCardN2 != -1)
        {
            if (card[saveCardN2].phaseCacher.transform.eulerAngles.y < 90)
                card[saveCardN2].phaseItem.SetActive(true);

            if (card[saveCardN2].phaseCacher.transform.eulerAngles.y >= 181 && !rotationCardN2)
            {
                card[saveCardN2].phaseCacher.transform.eulerAngles = new Vector3(0, 0, 0);
                rotationCardN2 = true;
            }
            else if (!rotationCardN2)
            {
                //Debug.Log("je te fais chier");
                card[saveCardN2].phaseCacher.transform.eulerAngles = new Vector3(0, card[saveCardN2].phaseCacher.transform.eulerAngles.y - (speedRotation * Time.deltaTime), 0);
            }
        }
    }

    public void RotationCardEnd()
    {
        if(endCheck)
        {


            //Debug.Log((speedRotation * Time.deltaTime));
            //Debug.Log("rota " + card[saveCardN1].phaseCacher.transform.eulerAngles.y);
            if (card[saveCardN1].phaseCacher.transform.eulerAngles.y > 90)
                card[saveCardN1].phaseItem.SetActive(false);

            if (card[saveCardN1].phaseCacher.transform.eulerAngles.y > 180)
            {
                //Debug.Log("end "+card[saveCardN1].phaseCacher.transform.eulerAngles.y);
                card[saveCardN1].phaseCacher.transform.eulerAngles = new Vector3(0, 180, 0);

            }
                
            else
                card[saveCardN1].phaseCacher.transform.eulerAngles = new Vector3(0, card[saveCardN1].phaseCacher.transform.eulerAngles.y + (speedRotation * Time.deltaTime), 0);


            if (card[saveCardN2].phaseCacher.transform.eulerAngles.y > 90)
                card[saveCardN2].phaseItem.SetActive(false);

            if (card[saveCardN2].phaseCacher.transform.eulerAngles.y > 180)
            {
                card[saveCardN2].phaseCacher.transform.eulerAngles = new Vector3(0, 180, 0);
                endCheck = false;
                saveCardN1 = -1;
                saveCardN2 = -1;
                rotationCardN1 = false;
                rotationCardN2 = false;
            }
            else
                card[saveCardN2].phaseCacher.transform.eulerAngles = new Vector3(0, card[saveCardN2].phaseCacher.transform.eulerAngles.y + (speedRotation * Time.deltaTime), 0);


        }
    }

    [System.Serializable]
    public struct Card
    {
        public GameObject phaseCacher;
        public GameObject phaseItem;
        public GameObject targetTopLeft;
        public GameObject targetBotRight;
        public int item;
        public int zone;
    }
}
