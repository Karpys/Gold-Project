using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called before the first frame update
    public float BaseRatio = 16f / 9f;
    public float RatioApply;
    public float Width;
    public float Height;
    
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        Width = Screen.width;
        Height = Screen.height;
        RatioApply = Width / Height;
        cam.orthographicSize = cam.orthographicSize * BaseRatio / RatioApply;
    }

 
}
