using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Villager", menuName = "Events/Base Villager Event")]
[System.Serializable]
public class Event : ScriptableObject
{
	[System.Serializable]
	public struct Impact
	{
		public int herbs;
		public int people;
		public int spirit;

		public string answer;
	}

	[System.Serializable]
	public struct CharacterText
	{
		public Character character; // Character ou GameObject si besoin de prefab
		public string[] line;
	}

	[System.Serializable]
	public struct PlayerText
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
}
