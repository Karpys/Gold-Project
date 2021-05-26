using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "Base Character", menuName = "Events/Base Villager Event")]
[System.Serializable]
public class Event : ScriptableObject
{
	[System.Serializable] public struct Impact
	{
		public int herbs;
		public int people;
		public int spirit;

		public string answer;

		public Impact(int Herbs, int People, int Spirit, string Answer)
		{
			herbs = Herbs;
			people = People;
			spirit = Spirit;
			answer = Answer;
		}

		public Impact Add(Impact right) // add impacts
        {
			right.herbs += herbs;
			right.people += people;
			right.spirit += spirit;

			return right;
        }

		public static Impact operator + (Impact a, Impact b) { return new Impact(a.herbs + b.herbs, a.people + b.people, a.spirit + b.spirit, b.answer); }
	}

	[System.Serializable] public struct CharacterText
	{
		public string[] line;
	}

	[System.Serializable] public struct PlayerText
	{
		public bool good;
		public string answer;
	}

	[Header("   Character Spawn Scene")]
	public GameObject CharacterSpawn;

	[Header("   Interaction")]
	public CharacterText[] dialog;

	public bool isButtonEvent = true;

	[Header("	Resource Impact")]
	public Impact yes;
	public Impact no;

	[Header("	Player Choice")]
	public PlayerText first;
	public PlayerText second;

	[Header("	Mini-game Scene Name")]
	public string minigame;

	private int talkingCharacter = 0;
	private int dialogLine = 0;
	[HideInInspector] public bool endedDialog = false;



    public void NextLine()
	{
		if(endedDialog) { 
			Dialog.Manager.Close();
			GameplayLoop.Loop.StartCoroutine(GameplayLoop.Loop.EndEvent());
		}

		if (talkingCharacter < dialog.Length) // si tous les personnages ne sont pas passés
		{
			if (dialogLine < dialog[talkingCharacter].line.Length) // si le personnage a encore des lignes de dialogue
			{
				if (dialogLine == 0)
					Dialog.Manager.NextDialog(dialog[talkingCharacter].line[dialogLine], CharacterSpawn.GetComponent<CharacterEvent>().characterName);
				else
					Dialog.Manager.NextDialog(dialog[talkingCharacter].line[dialogLine]);
			}

			if (dialogLine < dialog[talkingCharacter].line.Length - 1)
				++dialogLine;
			else
			{
				dialogLine = 0;
				++talkingCharacter;

				if (talkingCharacter >= dialog.Length)
                {
					if (isButtonEvent)
						Dialog.Manager.Prompt(true);
					else
						endedDialog = true;
                }
			}
		}
	}
}