using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidTemp : MonoBehaviour
{
    // Start is called before the first frame update
    public float Divi;
    public Vector3 LocalScale;
    public float TimeGoal;
    public float TimeValid;
    void Start()
    {
        LocalScale = transform.localScale;
        Divi = TimeGoal / LocalScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        TimeValid = Mathf.Clamp(TimeValid, 0,TimeGoal);
        LocalScale.x = TimeValid / Divi;
        transform.localScale = LocalScale;
        if(TimeValid>=TimeGoal)
        {
            Debug.Log("End");
        }
    }
}
