using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_EventControl : MonoBehaviour
{
    public Event testEvent;

    public bool waitingInput = false;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Manager.NextEvent(testEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            EventSystem.Manager.NextEvent(testEvent);
            for (int i = 0; i < testEvent.dialog[0].line.Length; i++)
            {
                //Debug.Log(testEvent.dialog[0].line[i]);
            }

            //Debug.Log("(Y) " + testEvent.first.answer);
            //Debug.Log("(N) " + testEvent.second.answer);

            //waitingInput = true;
        }

        if(waitingInput)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                EventSystem.Manager.Answer(0);

                waitingInput = false;
            }

            else if (Input.GetKeyDown(KeyCode.N))
            {
                EventSystem.Manager.Answer(1);

                waitingInput = false;
            }
        }
    }
}
