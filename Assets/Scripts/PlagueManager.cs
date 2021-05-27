using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlagueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int NbrSick;
    public GameObject Prefab;
    public PlagueZone CampSafe;
    public PlagueZone CampSick;
    public int RatioSick;
    private int RatioSickSet;
    public List<GameObject> Village;
    public GameObject Parent;
    public float Timer=15.0f;
    public Text text;
    public int Dead;
    public int Herbe;
    public float DelaySpawn;
    public bool MiniGameGo;
    public bool DependOnVillage;

    void Start()
    {
        if(DependOnVillage)
        {
            NbrSick = Mathf.Clamp(PlayerData.Stat.People / 2, 10,50);
        }
        RatioSickSet = RatioSick;
        InstPlague();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Timer>=0 && MiniGameGo)
        {
            
            Timer -= Time.deltaTime;
            if(Timer<=0)
            {
                CheckNumber();
            }
        }
        
        text.text = Timer.ToString("0.00");
    }

    public void InstPlague()
    {
        bool safe = false;
        float time = 0f;
        for (int i = 0; i < NbrSick; i++)
        {
            time += DelaySpawn;
            safe = !safe;
            StartCoroutine(Spawn(safe,time));
        }
    }

    public void CheckNumber()
    {
        int numberok = 0;
        foreach(GameObject vi in Village)
        {
            if(vi.GetComponent<Sort_Plague>().sick && vi.GetComponent<Sort_Plague>().Camp==PlagueZone.PlagueCamp.SICK)
            {
                numberok += 1;
            }else if(!vi.GetComponent<Sort_Plague>().sick && vi.GetComponent<Sort_Plague>().Camp == PlagueZone.PlagueCamp.SAFE)
            {
                numberok += 1;
            }else if(!vi.GetComponent<Sort_Plague>().sick && vi.GetComponent<Sort_Plague>().Camp == PlagueZone.PlagueCamp.SICK)
            {
                Dead += 1;
            }
        }
        if(numberok==Village.Count)
        {
            Debug.Log("Sauvé");
        }else
        {
            Debug.Log("Moooort");
        }
        Event.Impact imp = new Event.Impact();
        imp.herbs = -Herbe;
        imp.people = -Dead;
        StartCoroutine(EventSystem.Manager.EndGame(true, imp));
    }

    IEnumerator Spawn(bool safe,float time)
    {
        yield return new WaitForSeconds(time);
        GameObject Char = Instantiate(Prefab, transform.position, transform.rotation,Parent.transform);
        if (RatioSick <= 0)
        {
            Char.GetComponent<Sort_Plague>().sick = true;
            Dead += 1;
            RatioSick = RatioSickSet;
        }
        else
        {
            Char.GetComponent<Sort_Plague>().sick = false;
        }
        if (safe)
        {
            Char.GetComponent<Sort_Plague>().UpPoint = CampSafe.UpPoint;
            Char.GetComponent<Sort_Plague>().DownPoint = CampSafe.DownPoint;
            safe = !safe;
        }
        else
        {
            Char.GetComponent<Sort_Plague>().UpPoint = CampSick.UpPoint;
            Char.GetComponent<Sort_Plague>().DownPoint = CampSick.DownPoint;
            safe = !safe;
        }
        Village.Add(Char);
        RatioSick -= 1;
    }
}
