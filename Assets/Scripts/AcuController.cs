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
    public GameObject Aiguille;
    public GameObject Consigne;
    void Start()
    {
        State = SimonState.IDLE;
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if(Consigne.activeSelf)
            {
                Consigne.SetActive(false);
                StartCoroutine(AffichePoint(0.1f));
            }
        }
    }


    


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
        AudioSource.PlayClipAtPoint(SoundManager.Get.aiguille, new Vector3(0, 0, 0));
        yield return new WaitForSeconds(0);
        Color col = ListPoint[0].GetComponent<SpriteRenderer>().color;
        GameObject Aig = Instantiate(Aiguille, new Vector3(ListPoint[id].transform.position.x, ListPoint[id].transform.position.y + 1.5f, ListPoint[id].transform.position.z), Aiguille.transform.rotation);
        Aig.GetComponent<AiguilleAccupon>().PosTogo = ListPoint[id].transform.position;
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
                        StartCoroutine(EventSystem.Manager.EndGame(true));
                    }
                    State = SimonState.PLAYING;
                    IdRecreate.Clear();
                    counter = 0;
                    StartCoroutine(AffichePoint(1));
                }else
                {
                    StartCoroutine(EventSystem.Manager.EndGame(false));
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


