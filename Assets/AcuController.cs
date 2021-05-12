using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcuController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> ListPoint;
    public List<int> ListId;
    public int Max;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AffichePoint());
        }
    }

    /*public void AffichePoint()
    {
        Color col = ListPoint[0].GetComponent<SpriteRenderer>().color;
        col.a = 1;
        ListPoint[Random.Range(0, ListPoint.Count)].GetComponent<SpriteRenderer>().color = col;
    }*/


    IEnumerator AffichePoint()
    {
        for(int i = 0;i<ListId.Count;i++)
        {
            yield return new WaitForSeconds(0.5f);
            int id = ListId[i];
            Color col = ListPoint[0].GetComponent<SpriteRenderer>().color;
            col.a = 1;
            ListPoint[id].GetComponent<SpriteRenderer>().color = col;
            yield return new WaitForSeconds(0.5f);
            col.a = 0;
            ListPoint[id].GetComponent<SpriteRenderer>().color = col;
        }

        if(ListId.Count<Max)
        {
            yield return new WaitForSeconds(0.5f);
            int id = Random.Range(0, ListPoint.Count);
            Color col = ListPoint[0].GetComponent<SpriteRenderer>().color;
            col.a = 1;
            ListPoint[id].GetComponent<SpriteRenderer>().color = col;
            yield return new WaitForSeconds(0.5f);
            col.a = 0;
            ListPoint[id].GetComponent<SpriteRenderer>().color = col;
            ListId.Add(id);
        }
    }

    public enum SimonState
    {
        IDLE,
        PLAYING,
        COPY,
    }

}
