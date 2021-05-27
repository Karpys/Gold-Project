using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    // Start is called before the first frame update
    private static FadeController inst;
    public static FadeController Fade { get => inst; }
    public Animator Anim;
    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }

}
