using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    // Start is called before the first frame update
    public float Temperature;
    public float TempMax;
    public Vector3 LocalScale;
    public GameObject JaugeTemp;
    public float DivideJauge;
    public float Tempvaria;
    public float TempLose;
    public float TempUp;
    public float TempGoal;
    public float TempRange;
    public ValidTemp Jauge;
    void Awake()
    {
        LocalScale = JaugeTemp.transform.localScale;
        DivideJauge = TempMax / LocalScale.y;
        TempGoal = Random.Range(40, 80);
    }


    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            Tempvaria += TempUp;
        }

        if(Tempvaria>-0.6f)
        {
            Tempvaria -= TempLose*Time.deltaTime;
        }

        if(Temperature>TempGoal-TempRange && Temperature < TempGoal+TempRange)
        {
            Jauge.TimeValid += Time.deltaTime;
        }

        Temperature += Tempvaria;
        Temperature = Mathf.Clamp(Temperature, 0, TempMax);
        LocalScale.y = Temperature / DivideJauge;
        JaugeTemp.transform.localScale = LocalScale;
    }
}
