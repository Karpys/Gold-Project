using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	private static PlayerData inst;
	public static PlayerData Stat { get => inst; }

	public string playerName = "Shaman";

	[Header("	Base Value")]
	public int startingHerbs = 10;
	public int startingPeople = 20;
	public int startingSpirit = 10;
	[Header("	Resource")]
	public int Herbs;
	public int People;
	public int Spirit;
	[Header("	Max Value")]
	public int HerbsMax;
	public int PeopleMax;
	public int SpiritMax;

	[Header("")]
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
		ClampValue();

		RessourceUI.UIRessource.AnimateUI(hrb, ppl, spi);
	}

	public int CalculateScore()
    {
		int Score = 0;
		Score += Herbs * 10;
		Score += People * 30;
		Score += Spirit * 20;
		Score *= Mathf.Clamp((int)season,1,4);
		//(AchievementScore>Vector3)//
		/*if(Score>=AchievementScore.x)
        {
			AchievementManager.Achieve.UnlockAchievement("CgkIidW02PodEAIQBg");
			if(Score >= AchievementScore.y)
            {
				AchievementManager.Achieve.UnlockAchievement("CgkIidW02PodEAIQBA");
				if (Score >= AchievementScore.z)
				{ 
					AchievementManager.Achieve.UnlockAchievement("CgkIidW02PodEAIQBQ");
				}
			}
        }*/
		AchievementManager.Achieve.SetHighScore(Score);
		return Score;
    }

	public void ClampValue()
    {
		if(Herbs>=HerbsMax)
        {
			Herbs = Mathf.Clamp(Herbs, 0, HerbsMax);
			AchievementManager.Achieve.UnlockAchievement("CgkIidW02PodEAIQAQ");
		}

		if (People >= PeopleMax)
		{
			People = Mathf.Clamp(People, 0, PeopleMax);
			AchievementManager.Achieve.UnlockAchievement("CgkIidW02PodEAIQAw");
		}

		if (Spirit >= SpiritMax)
		{
			Spirit = Mathf.Clamp(Spirit, 0, SpiritMax);
			AchievementManager.Achieve.UnlockAchievement("CgkIidW02PodEAIQAg");
			
		}
	}

	public void NextSeason()
    {
		if(season==Season.Winter)
        {
			// End Game
			CalculateScore();
			StartCoroutine(GameManager.Get.Win());
			//season = Season.Spring;
        }else
        {
			season += 1;
        }
    }


	public bool NoRessource() 
	{ 
		if(People<=0 || Herbs<=0 || Spirit<=0)
        {
			return true;
        }else
        {
			return false;
        }
	}
}
