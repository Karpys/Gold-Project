using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	private static PlayerData inst;
	public static PlayerData Stat { get => inst; }

	public string playerName = "Shaman";

	public int startingHerbs = 10;
	public int startingPeople = 20;
	public int startingSpirit = 10;
	public int Herbs;
	public int People;
	public int Spirit;

	public int Score;

	public Season season = Season.Spring;

    private void Awake()
    {
		if (inst == null)
			inst = this;

		//DontDestroyOnLoad(this);

		Init();
	}

	public void Init()
    {
		season = Season.Spring;
		Herbs = startingHerbs;
		People = startingPeople;
		Spirit = startingSpirit;
		RessourceUI.UIRessource.UpdateUIRessource();
    }
   
    public void ImpactResources(int hrb, int ppl, int spi)
	{
		Herbs += hrb;
		People += ppl;
		Spirit += spi;
	}

	public void NextSeason()
    {
		if(season==Season.Winter)
        {
			// End Game
			StartCoroutine(GameManager.Get.Win());
			//season = Season.Spring;
        }else
        {
			season += 1;
        }
    }


	public bool NoPeopleLeft() { return People <= 0; }
}
