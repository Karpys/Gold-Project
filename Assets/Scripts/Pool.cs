using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System;

[Serializable]
public struct SubPool
{
	public List<Event> Day;
	public List<Event> Night;
	public List<Event> Spirit;
}

[CreateAssetMenu(fileName = "New Pool", menuName = "Pool")]
public class Pool : ScriptableObject
{
	[Header("	Minigame Events")]
	public SubPool Minigame;
	[Header("	Events without minigame")]
	public SubPool Text;
}
