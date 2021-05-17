using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memorie : MonoBehaviour
{
    public Card[] card;
    public GameObject[] Target;
    public GameObject winText;
    public GameObject LoseText;
    public float speed;
    public int life;

    private bool start = true;
    private bool[] freeTarget = new bool [8];
    private bool distribution = true;
    private bool check = false;
    private bool win = true;
    public float time = 0;
    private float lerp = 0;
    private bool firstCard = false;
    private int saveCardN1;
    private int saveCardN2;

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
                    for (int i = 0; i < card.Length; i++)
                    {
                        if((zoneTouch.y < card[i].targetTopLeft.transform.position.y && zoneTouch.y > card[i].targetBotRight.transform.position.y) && (zoneTouch.x > card[i].targetTopLeft.transform.position.x && zoneTouch.x < card[i].targetBotRight.transform.position.x))
                        {
                            card[i].phaseItem.SetActive(true);
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
            Check();
        }
    }

    public void Win()
    {
        if(life == 0)
            LoseText.SetActive(true);
        
        for(int i = 0;i<card.Length;i++)
        {
            if (win)
                win = !card[i].phaseCacher.activeSelf;
        }

        if (win)
            winText.SetActive(true);
    }

    public void Lose()
    {
        if (life < 0)
            LoseText.SetActive(true);
    }

     public void RamdomCardStart()
    {

        time += Time.deltaTime;
        Vector3 dir = (card[0].phaseCacher.transform.position - new Vector3(0, 0,0)).normalized;

        if(time>=2)
        {
            if(distribution)
            {
                for(int i = 0; i < card.Length; i++)
                    card[i].phaseItem.SetActive(false);
                
                lerp += Time.deltaTime * speed / 10;
                
                for (int i = 0; i < card.Length; i++)    
                    card[i].phaseCacher.transform.position = Vector2.Lerp(card[i].phaseCacher.transform.position, new Vector3(0, 0, 0), lerp/10);


                if(lerp > 1)
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
                    Win();
                }
                else 
                {
                    card[saveCardN2].phaseItem.SetActive(false);
                    card[saveCardN1].phaseItem.SetActive(false);
                    life--;
                    Lose();
                }
                firstCard = false;
                check = false;
            }
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
