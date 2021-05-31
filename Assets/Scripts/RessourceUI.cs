using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RessourceUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text Village;
    public Text Leaf;
    public Text Soul;

    private static RessourceUI inst;
    public static RessourceUI UIRessource { get => inst; }
    private void Awake()
    {
        if (inst == null)
            inst = this;

        //DontDestroyOnLoad(this);
    }

    private void Start()
    {
        UpdateUIRessource();
    }

    public void UpdateUIRessource()
    {
        Village.text = PlayerData.Stat.People.ToString();
        Leaf.text = PlayerData.Stat.Herbs.ToString();
        Soul.text = PlayerData.Stat.Spirit.ToString();
    }
}
