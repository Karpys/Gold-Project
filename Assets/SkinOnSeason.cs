using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
public class SkinOnSeason : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteResolver[] SkinsParts;
    void Start()
    {
        SkinsParts = GetComponentsInChildren<SpriteResolver>();
        foreach(SpriteResolver Parts in SkinsParts)
        {
            Parts.SetCategoryAndLabel(Parts.GetCategory(), PlayerData.Stat.season.ToString());
        }
    }
    
}
