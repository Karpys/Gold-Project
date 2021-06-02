using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResolution : MonoBehaviour
{
    // Start is called before the first frame update
    private static BaseResolution inst;
    public static BaseResolution Resolution { get => inst; }

    public float RatioApplied;
    private void Awake()
    {
        if (inst == null)
            inst = this;
        float Width = Screen.width;
        float Height = Screen.height;
        RatioApplied = Width / Height;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
