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
    public Image Piece;

    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }
    public void ChangeSeason()
    {
        Background.sprite = ListBackground[(int)PlayerData.Stat.season];
        Piece.sprite = PieceBack[(int)PlayerData.Stat.season];
    }
}
