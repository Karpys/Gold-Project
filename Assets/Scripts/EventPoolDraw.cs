using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoolDraw : MonoBehaviour
{
    public List<Pool> seasonsPool;
    public Event tri;

    public List<SeasonTemplate> templatePool;
    private SeasonTemplate currentTemplate;

// Singleton
    private static EventPoolDraw inst;
    public static EventPoolDraw Pool { get => inst; }

// Struct, Enum
    public enum DayNight { Day, Night }
    [System.Serializable] public struct DayTemplate
    {
        public DayNight state;
        public bool minigame;
    }
    [System.Serializable] public struct SeasonTemplate
    {
        public List<DayTemplate> templateList;
    }


// Functions
    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }


    public void DrawEvent()
    {
        EventSystem.Manager.eventPool.Clear();

        currentTemplate = templatePool[Random.Range(0, templatePool.Count)];
        Pool tempPool = Instantiate(GetSeasonPool());

        // Get number of nights in Season
        int nightCount = 0;
        bool spiritFound = false;

        for (int i = 0; i < currentTemplate.templateList.Count; i++)
        {
            if (currentTemplate.templateList[i].state == DayNight.Night) nightCount++;
        }

        // Pull Event from respective Pool
        for (int i = 0; i < currentTemplate.templateList.Count; i++)
        {
            SubPool subPool = (currentTemplate.templateList[i].minigame) ? tempPool.Minigame : tempPool.Text;
            Event toAdd = null;
            int n;

            switch (currentTemplate.templateList[i].state)
            {
                case DayNight.Day:
                    n = Random.Range(0, subPool.Day.Count);
                    toAdd = subPool.Day[n];
                    subPool.Day.RemoveAt(n);
                    break;

                case DayNight.Night:
                    if (!spiritFound)
                    {
                        if (Random.Range(0, nightCount) == 0)
                        {
                            n = Random.Range(0, subPool.Spirit.Count);
                            toAdd = subPool.Spirit[n];
                            subPool.Spirit.RemoveAt(n);
                            spiritFound = true;
                        }
                        else
                        {
                            n = Random.Range(0, subPool.Night.Count);
                            toAdd = subPool.Night[n];
                            subPool.Night.RemoveAt(n);
                            nightCount--;
                        }
                    }
                    else
                    {
                        n = Random.Range(0, subPool.Night.Count);
                        toAdd = subPool.Night[n];
                        subPool.Night.RemoveAt(n);
                    }
                    break;

                default:
                    break;
            }

            EventSystem.Manager.eventPool.Add(toAdd);
        }
    }

    private Pool GetSeasonPool() { return seasonsPool[(int)PlayerData.Stat.season]; }
}

public enum Season
{
    Spring,
    Summer,
    Fall,
    Winter
}
