using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPlague : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Drag;
    public bool Draging;
    public PlagueManager Manage;
    public Vector2 Position;
    private bool sound = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        foreach (Touch touch in Input.touches)
        {
            Manage.MiniGameGo = true;
            Position = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Moved)
            {
                if (Drag)
                    Attach(Drag);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (Drag)
                {
                    Release(Drag);
                    Drag = null;
                }
                
            }
        }
        transform.position = Position;
       /* if (Input.GetMouseButtonDown(0))
        {
            if(Drag)
            Attach(Drag);
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (Drag)
            Release(Drag);
        }*/
        if(Draging)
        {
            Drag.transform.position = new Vector3(transform.position.x, transform.position.y, Drag.transform.position.z);
        }
    }


    public void Attach(GameObject Drag)
    {
        Drag.GetComponent<Sort_Plague>().State = Sort_Plague.PlagueState.FINGER;
        Draging = true;
        if(sound)
        {
            if(Random.Range(0,2)==0)
                AudioSource.PlayClipAtPoint(SoundManager.Get.takePeople_01, new Vector3(0, 0, 0));
            else
                AudioSource.PlayClipAtPoint(SoundManager.Get.takePeople_02, new Vector3(0, 0, 0));
            sound = false;
        }

    }

    public void Release(GameObject Drag)
    {
        Drag.GetComponent<Sort_Plague>().State = Sort_Plague.PlagueState.IDLE;
        Draging = false;
        if(Drag.GetComponent<Sort_Plague>().Camp==PlagueZone.PlagueCamp.RIVER)
        {
            if(Drag.GetComponent<Sort_Plague>().sick)
            {
                Manage.Herbe += 1;
            }else
            {
                Manage.Dead += 1;
            }
            Destroy(Drag);
            Manage.Village.Remove(Drag);
        }
        if (Random.Range(0, 2) == 0)
            AudioSource.PlayClipAtPoint(SoundManager.Get.dropePeople_01, new Vector3(0, 0, 0));
        else
            AudioSource.PlayClipAtPoint(SoundManager.Get.dropePeople_02, new Vector3(0, 0, 0));
        sound = true;
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
