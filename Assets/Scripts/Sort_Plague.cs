using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort_Plague : MonoBehaviour
{
    // Start is called before the first frame update
    public bool sick;
    public Color ColorSick;
    public int Speed;
    public Vector3 PointToGo;
    public float TimeIdle;
    public float TimeIdleSet;
    public PlagueState State;
    public PlagueZone.PlagueCamp Camp;
    public Transform UpPoint;
    public Transform DownPoint;

    void Start()
    {
        if(sick)
        {
            GetComponent<SpriteRenderer>().color = ColorSick;
        }
        PointToGo = SelectPoint(UpPoint.position, DownPoint.position);
        transform.position = PointToGo;
    }

    // Update is called once per frame
    void Update()
    {
        if(State!=PlagueState.FINGER)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointToGo, Speed * Time.deltaTime);
            if(transform.position!=PointToGo)
            {
                State = PlagueState.WALK;
            }else
            {
                if(State==PlagueState.WALK)
                {
                    State = PlagueState.IDLE;
                    TimeIdle = TimeIdleSet;
                }
                TimeIdle -= Time.deltaTime;
                if(TimeIdle<=0)
                {
                    State = PlagueState.WALK;
                    PointToGo = SelectPoint(UpPoint.position,DownPoint.position);
                }
            }

        }
    }

    public Vector3 SelectPoint(Vector3 Up,Vector3 Down)
    {
        Vector3 pos = new Vector3(Random.Range(Up.x, Down.x), Random.Range(Up.y, Down.y), Up.z);
        return pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            Camp = collision.gameObject.GetComponent<PlagueZone>().Camp;
            if(collision.gameObject.GetComponent<PlagueZone>().Camp!=PlagueZone.PlagueCamp.RIVER)
            {
                UpPoint = collision.gameObject.GetComponent<PlagueZone>().UpPoint;
                DownPoint = collision.gameObject.GetComponent<PlagueZone>().DownPoint;
                PointToGo = SelectPoint(UpPoint.position, DownPoint.position);
            }
        }
    }


    public enum PlagueState
    {
        IDLE,
        WALK,
        FINGER,
    }
}
