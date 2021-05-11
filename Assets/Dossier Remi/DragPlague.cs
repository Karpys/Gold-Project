using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlague : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Drag;
    public bool Draging;
    public PlagueManager Manage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        
        /*foreach (Touch touch in Input.touches)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                if (Drag)
                    Attach(Drag);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (Drag)
                    Release(Drag);
            }
        }*/
        transform.position = worldPosition;
        if (Input.GetMouseButtonDown(0))
        {
            if(Drag)
            Attach(Drag);
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (Drag)
            Release(Drag);
        }
        if(Draging)
        {
            Drag.transform.position = new Vector3(transform.position.x, transform.position.y, Drag.transform.position.z);
        }
    }


    public void Attach(GameObject Drag)
    {
        Drag.GetComponent<Sort_Plague>().State = Sort_Plague.PlagueState.FINGER;
        Draging = true;
    }

    public void Release(GameObject Drag)
    {
        Drag.GetComponent<Sort_Plague>().State = Sort_Plague.PlagueState.IDLE;
        Draging = false;
        if(Drag.GetComponent<Sort_Plague>().Camp==PlagueZone.PlagueCamp.RIVER)
        {
            Manage.Dead += 1;
            Destroy(Drag);
            Manage.Village.Remove(Drag);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Draging)
        {
            if(collision.GetComponent<Sort_Plague>())
            {
                Drag = collision.gameObject;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == Drag)
        {
            Drag = null;
        }
    }
}
