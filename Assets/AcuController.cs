using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcuController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> ListPoint;
    public List<int> ListId;
    public List<int> IdRecreate;
    public int Max;
    public int counter;
    public SimonState State;
    void Start()
    {
        State = SimonState.IDLE;
        StartCoroutine(AffichePoint(2));
    }

    // Update is called once per frame
    void Update()
    {
       
      
    }


    /*public void AffichePoint()
    {
        Color col = ListPoint[0].GetComponent<SpriteRenderer>().color;
        col.a = 1;
        ListPoint[Random.Range(0, ListPoint.Count)].GetComponent<SpriteRenderer>().color = col;
    }*/


    IEnumerator AffichePoint(float pause)
    {
        State = SimonState.PLAYING;
        yield return new WaitForSeconds(pause);
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
        State = SimonState.COPY;
    }

    IEnumerator DisplayPointSelect(int id)
    {
        yield return new WaitForSeconds(0);
        Color col = ListPoint[0].GetComponent<SpriteRenderer>().color;
        col.a = 1;
        ListPoint[id].GetComponent<SpriteRenderer>().color = col;
        yield return new WaitForSeconds(0.5f);
        col.a = 0;
        ListPoint[id].GetComponent<SpriteRenderer>().color = col;
    }

    public void ActivePoint(int id)
    {
        if(State == SimonState.COPY)
        { 
            if(counter<ListId.Count)
            {
                counter += 1;
                IdRecreate.Add(id);
                StartCoroutine(DisplayPointSelect(id));
            }
        
            if(counter==ListId.Count)
            {
            
                if(Check(ListId,IdRecreate))
                {
                    if(counter==Max)
                    {
                        Debug.Log("YOUPI");
                    }
                    State = SimonState.PLAYING;
                    IdRecreate.Clear();
                    counter = 0;
                    StartCoroutine(AffichePoint(1));
                }else
                {
                    Debug.Log("Loose");
                }
            }
        }
    }

    public bool Check(List<int> a,List<int> b)
    {
        bool check = true;
        for (int i = 0;i<a.Count;i++)
        {
            if(a[i]!=b[i])
            {
                check = false;
            }
        }
        return check;
    }
    public enum SimonState
    {
        IDLE,
        PLAYING,
        COPY,
    }

}


