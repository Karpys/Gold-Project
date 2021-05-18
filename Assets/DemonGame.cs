using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DemonGame : MonoBehaviour
{
    public Text timer;
    public GameObject winText;
    public GameObject loseText;

    public int min;
    public int max;

    private float timeP1;
    private int timeP2;
    private bool lose = false;

    // Start is called before the first frame update
    void Start()
    {
        timeP1 = (int)Random.Range(min,max+1);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeP1 > 0)
        {
            if (!lose)
                timeP1 -= Time.deltaTime;
            else
                loseText.SetActive(true);

            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    lose = true;
                }
            }
        }
        else
            winText.SetActive(true);

        timeP2 = (int)(timeP1 * 10 - ((int)timeP1) * 10);
        timer.text = ((int)timeP1).ToString() + "," + timeP2.ToString();

    }
}
