using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixeliseScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private static PixeliseScreen inst;
    public static PixeliseScreen PixelScreen { get => inst; }
    public float Width;
    public float Height;
    public float Divide;
    public float MaxDivide;
    public ScreenPixel State;
    public float TimeSet;
    public float TimeDivide;
    public AnimationCurve Curve;
    

    void Start()
    {
        Width = Screen.width;
        Height = Screen.height;
    }

    private void Awake()
    {
        if (inst == null)
            inst = this;

        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(State == ScreenPixel.DOWN)
        {
            TimeSet += Time.deltaTime;
            Divide = Mathf.Lerp(1, MaxDivide, Curve.Evaluate(TimeSet));
            Screen.SetResolution((int)(Mathf.Clamp(Width / Divide, 4, 9999)), (int)(Mathf.Clamp(Height / Divide, 2, 9999)), true);
            if(Divide>=MaxDivide)
            {
                State = ScreenPixel.UP;
                TimeSet = 0;
            }
        }else if(State == ScreenPixel.UP)
        {
            TimeSet += Time.deltaTime;
            Divide = Mathf.Lerp(MaxDivide,1, Curve.Evaluate(TimeSet));
            Screen.SetResolution((int)(Mathf.Clamp(Width / Divide, 4, 9999)), (int)(Mathf.Clamp(Height / Divide, 2, 9999)), true);
            if (Divide <= 1)
            {
                State = ScreenPixel.IDLE;
                TimeSet = 0;
            }
        }

        
    }

   
    public void Pixelise()
    {
        State = ScreenPixel.DOWN;
    }

    public enum ScreenPixel
    {
        IDLE,
        DOWN,
        UP,
    }
}
