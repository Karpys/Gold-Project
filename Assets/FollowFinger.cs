using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFinger : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Position;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Position = Camera.main.ScreenToWorldPoint(touch.position);
        }
        transform.position = Position;
    }
}
