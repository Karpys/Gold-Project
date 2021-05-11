using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
	private static EventSystem inst;
	public static EventSystem Manager { get => inst; }

	private Event current; // current Event being played

	public Event[] eventPool;

    private void Awake()
    {
		if (inst == null)
			inst = this;
    }

    public void NextEvent(Event next)
	{
		current = next;
		// Appeler changement Text
	}

	// call on Button w/ 0 ou 1
	public void Answer(int choice)
	{
		// clicked 'first' or 'second' answer
		Event.PlayerText playerText = (choice == 0) ? current.first : current.second;

		Debug.Log(playerText.answer); //////////////////////////////////

		/// Check if Mini-Jeu !!!

		// answer was 'good' or not
		Event.Impact impact = (playerText.good) ? current.yes : current.no;

		Debug.Log(impact.answer); //////////////////////////////////

		PlayerData.Stat.ImpactResources(impact.herbs, impact.people, impact.spirit);
	}
}
