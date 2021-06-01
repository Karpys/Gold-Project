using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float BaseRatio = 16f / 9f;
    private void Awake()
    {

        //DontDestroyOnLoad(this);
    }
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = cam.orthographicSize * BaseRatio / BaseResolution.Resolution.RatioApplied;
    }

 
}

