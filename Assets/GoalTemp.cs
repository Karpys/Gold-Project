using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTemp : MonoBehaviour
{
    // Start is called before the first frame update
    public CauldronManager Caul;
    public GameObject MinPos;
    public Vector2 MinMax;
    public Vector3 Position;
    void Start()
    {
        Position = transform.position;
        MinMax.x = MinPos.transform.position.y;
        MinMax.y = transform.position.y;
        float percent = Caul.TempGoal * 100 / Caul.TempMax;
        float ecart = Mathf.Abs(MinMax.x) + Mathf.Abs(MinMax.y);
        float position = Caul.TempGoal * ecart / 100;
        Position.y = position + MinMax.x;
        transform.position = Position;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
