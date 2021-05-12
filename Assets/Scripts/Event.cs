using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Villager", menuName = "Events/Base Villager Event")]
[System.Serializable]
public class Event : ScriptableObject
{
	[System.Serializable] public struct Impact
	{
		public int herbs;
		public int people;
		public int spirit;

		public string answer;
	}

	[System.Serializable] public struct CharacterText
	{
		public Character character; // Character ou GameObject si besoin de prefab
		public string[] line;
	}

	[System.Serializable] public struct PlayerText
	{
		public bool good;
		public string answer;
	}

	public CharacterText[] dialog;

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
		if(endedDialog) { Dialog.Manager.Close(); }

		if (talkingCharacter < dialog.Length) // si tous les personnages ne sont pas passés
		{
			Debug.Log(" still characters left ");
			if (dialogLine < dialog[talkingCharacter].line.Length) // si le personnage a encore des lignes de dialogue
			{
				if (dialogLine == 0)
					Dialog.Manager.NextDialog(dialog[talkingCharacter].line[dialogLine], "Name 1");//dialog[talkingCharacter].character.name);
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
					Dialog.Manager.Prompt(true);
			}
		}
	}
}
