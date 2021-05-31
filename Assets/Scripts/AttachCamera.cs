using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;

public class AttachCamera : MonoBehaviour
{
    public Canvas canvas;

    private void Start()
    {
        GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(canvas.name);

        if (canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
        }
    }
}
