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

    [Header("   Colors")]
    public Color baseColor;
    public Color maxColor;

    private Animator peopleAnim;
    private Animator herbsAnim;
    private Animator spiritAnim;

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
        peopleAnim = Village.GetComponentInParent<Animator>();
        herbsAnim = Leaf.GetComponentInParent<Animator>();
        spiritAnim = Soul.GetComponentInParent<Animator>();

        UpdateUIRessource();
    }

    public void UpdateUIRessource()
    {
        Village.text = PlayerData.Stat.People.ToString();
        Leaf.text = PlayerData.Stat.Herbs.ToString();
        Soul.text = PlayerData.Stat.Spirit.ToString();

        ChangeColor();
    }

    private void ChangeColor()
    {
        if (PlayerData.Stat.People >= PlayerData.Stat.PeopleMax)
            Village.color = maxColor;
        else if(PlayerData.Stat.People <= 0)
            Village.color = Color.red;
        else
            Village.color = baseColor;

        if (PlayerData.Stat.Herbs >= PlayerData.Stat.HerbsMax)
            Leaf.color = maxColor;
        else if (PlayerData.Stat.Herbs <= 0)
            Leaf.color = Color.red;
        else
            Leaf.color = baseColor;

        if (PlayerData.Stat.Spirit >= PlayerData.Stat.SpiritMax)
            Soul.color = maxColor;
        else if (PlayerData.Stat.Spirit <= 0)
            Soul.color = Color.red;
        else
            Soul.color = baseColor;
    }

    public void AnimateUI(int herbsImpactValue, int peopleImpactValue, int spiritImpactValue)
    {
        herbsAnim.SetInteger("Value", herbsImpactValue);
        if (herbsImpactValue != 0) herbsAnim.SetTrigger("TriggerAnim");
        peopleAnim.SetInteger("Value", peopleImpactValue);
        if(peopleImpactValue != 0) peopleAnim.SetTrigger("TriggerAnim");
        spiritAnim.SetInteger("Value", spiritImpactValue);
        if (spiritImpactValue != 0) spiritAnim.SetTrigger("TriggerAnim");
    }
}
