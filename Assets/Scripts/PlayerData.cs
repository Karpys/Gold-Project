using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	private static PlayerData inst;
	public static PlayerData Stat { get => inst; }

	public string playerName = "Shaman";

	public int Herbs = 10;
	public int People = 20;
	public int Spirit = 10;

	public int Score;

	public Season season = Season.Spring;

    private void Awake()
    {
		if (inst == null)
			inst = this;

		DontDestroyOnLoad(this);
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
			season = Season.Spring;
			//CALL END GAME ?//
        }else
        {
			season += 1;
        }
    }


	public bool NoPeopleLeft() { return People <= 0; }
}
