using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGooglePlay : MonoBehaviour
{
    // Start is called before the first frame update
    public GooglePlayService Gg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Gg.isConnectedToGooglePlayServices)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
