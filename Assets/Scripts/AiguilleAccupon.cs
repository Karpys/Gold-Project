using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiguilleAccupon : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 PosTogo;
    public float Speed;
    public SpriteRenderer Sprite;
    public Color ColorLerp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PosTogo!=transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, PosTogo, Speed * Time.deltaTime);
        }else if(Sprite.color.a>0)
        {
            Sprite.color = Color.Lerp(Sprite.color, ColorLerp, Speed*2 * Time.deltaTime);
        }else
        {
            Destroy(gameObject);
        }
    }
}
