using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResolution : MonoBehaviour
{
    // Start is called before the first frame update
    private static BaseResolution inst;
    public static BaseResolution Resolution { get => inst; }

    public float RatioApplied;
    public float Width;
    public float Height;
    private void Awake()
    {
        if (inst == null)
            inst = this;
        Width = Screen.width;
        Height = Screen.height;
        RatioApplied = Width / Height;
    }
}
