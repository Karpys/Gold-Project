using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called before the first frame update
    public float targetRatio = 16f / 9f; //The aspect ratio of your game
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.aspect = targetRatio;
    }

 
}
