using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public float TimeDestroy;

    // Update is called once per frame
    void Update()
    {
        TimeDestroy -= Time.deltaTime;
        if(TimeDestroy<=0)
        {
            Destroy(gameObject);
        }
    }
}
