using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healManager : MonoBehaviour
{
    public Item[] item = new Item[3];
    public Text commandeText;

    private string[] commande = new string[3];
    private bool[] freeItem = new bool[3];
    public string[] itemUsed = new string[3];
    public int itemUsedCompt = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < freeItem.Length; i++)
            freeItem[i] = true;

        for (int i = 0;i<commande.Length;i++)
        {
            int random = Random.Range(0, 3);
            while(!freeItem[random])
                random = Random.Range(0, 3);
            freeItem[random] = false;
            commande[i] = item[random].name;            
        }

        commandeText.text = commande[0] + " - " + commande[1] + " - " + commande[2];
    }

    // Update is called once per frame
    void Update()
    {
        if(itemUsedCompt < 3)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 zoneTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
                if (touch.phase == TouchPhase.Began)
                {
                    for(int i = 0;i<item.Length;i++)
                    {
                        if ((zoneTouch.y < item[i].targetTopLeft.transform.position.y && zoneTouch.y > item[i].targetBotRight.transform.position.y) && (zoneTouch.x > item[i].targetTopLeft.transform.position.x && zoneTouch.x < item[i].targetBotRight.transform.position.x))
                        {
                            itemUsed[itemUsedCompt] = item[i].name;
                            //if (itemUsedCompt < 2)
                                itemUsedCompt++;
                        }
                    }
                }
            }
        }
        Win();
    }

    public void Win()
    {
        if(itemUsedCompt == 3)
        {
            if (itemUsed[0] == commande[0] && itemUsed[1] == commande[1] && itemUsed[2] == commande[2])
                EventSystem.Manager.EndGame(true);
            else
                EventSystem.Manager.EndGame(false); 
        }
    }

    [System.Serializable]
    public struct Item
    {
        public string name;
        public GameObject targetTopLeft;
        public GameObject targetBotRight;
    }
}
