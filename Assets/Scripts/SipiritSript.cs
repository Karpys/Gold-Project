using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SipiritSript : MonoBehaviour
{
    public LabirinteManager labirinte;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            labirinte.lose = true;
        }
    }
}
