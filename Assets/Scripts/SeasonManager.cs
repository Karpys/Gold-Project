using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SeasonManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static SeasonManager inst;
    public static SeasonManager Season { get => inst; }
    public List<Sprite> ListBackground;
    public List<Sprite> PieceBack;
    public Image Background;
    public Image BackgroundNext;
    public Image Piece;
    public float Timeset;
    public AnimationCurve Curve;
    public SEASONSTATE State;

    private void Awake()
    {
        if (inst == null)
            inst = this;

        //DontDestroyOnLoad(this);
    }

    public void Update()
    {
        if(State == SEASONSTATE.SETNEW)
        {
            BackgroundNext.sprite = ListBackground[(int)PlayerData.Stat.season];
            BackgroundNext.sprite = ListBackground[(int)PlayerData.Stat.season];
            State = SEASONSTATE.FADEODL;
        }else if(State == SEASONSTATE.FADEODL)
        {
            Timeset += Time.deltaTime;
            Background.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, Curve.Evaluate(Timeset)));
        }else if(State == SEASONSTATE.REPLACEOLD)
        {
            Background.sprite = ListBackground[(int)PlayerData.Stat.season];
            Background.color = Color.white;
            State = SEASONSTATE.IDLE;
            Timeset = 0;
        }
    }
    public IEnumerator ChangeSeason()
    {
        State = SEASONSTATE.SETNEW;
        yield return new WaitForSeconds(1.1f);
        State = SEASONSTATE.REPLACEOLD;
        Piece.sprite = PieceBack[(int)PlayerData.Stat.season];
    }

    public enum SEASONSTATE
    {
        SETNEW,
        FADEODL,
        REPLACEOLD,
        IDLE,
    }
}
