using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
	private static EventSystem inst;
	public static EventSystem Manager { get => inst; }

	 public Event current; // current Event being played

	public List<Event> eventPool;

	private string loadedGameScene;

    private void Awake()
    {
		if (inst == null)
			inst = this;

		DontDestroyOnLoad(this);
    }

    public void NextEvent(Event next)
	{
		current = Instantiate(next);

		Dialog.Manager.Open(); ///////////
		NextDialogLine();
	}

/// Buttons
/// 
	// call on Dialog Box Button
	public void NextDialogLine() { current.NextLine(); }

	// call on Button w/ 0 ou 1
	public void Answer(int choice)
	{
		// clicked 'first' or 'second' answer
		Event.PlayerText playerText = (choice == 0) ? current.first : current.second;

		Dialog.Manager.Prompt(false);

		// answer was 'good' or not
		Event.Impact impact;
		if (playerText.good)
		{
			if (current.minigame != "") // Check if Mini-Jeu !!!
			{
				StartCoroutine(LoadGame(current.minigame));
				return;
			}
			else
			{
				impact = current.yes;
			}
		}
		else
			impact = current.no;

		ApplyImpact(impact);
	}

	public void ApplyImpact(Event.Impact impact)
	{
		Dialog.Manager.NextDialog(impact.answer);

		PlayerData.Stat.ImpactResources(impact.herbs, impact.people, impact.spirit);

		current.endedDialog = true;

		RessourceUI.UIRessource.UpdateUIRessource();
		
	}


/// Scene Manager - Minigames
/// 
	public IEnumerator LoadGame(string sceneName)
    {
		FadeController.Fade.Anim.Play("FadeScreenAnim");
		yield return new WaitForSeconds(1.0f);
		Dialog.Manager.Box.SetActive(false);

		if (Application.CanStreamedLevelBeLoaded(sceneName))
		{
			loadedGameScene = sceneName;
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			RessourceUI.UIRessource.gameObject.SetActive(false);
		}
		else Debug.LogError(" No scene \"" + sceneName + "\" could be found.");
    }


	public IEnumerator EndGame(bool win)
	{
		FadeController.Fade.Anim.Play("FadeScreenAnim");
		yield return new WaitForSeconds(1.0f);
		Dialog.Manager.Box.SetActive(true);
		RessourceUI.UIRessource.gameObject.SetActive(true);
		SceneManager.UnloadSceneAsync(loadedGameScene);

		Event.Impact impact = (win) ? current.yes : current.no;
		ApplyImpact(impact);
    }

	public IEnumerator EndGame(bool win, Event.Impact newImpact) // use when additional resources impact in minigame
	{
		FadeController.Fade.Anim.Play("FadeScreenAnim");
		yield return new WaitForSeconds(1.0f);
		Dialog.Manager.Box.SetActive(true);
		RessourceUI.UIRessource.gameObject.SetActive(true);
		SceneManager.UnloadSceneAsync(loadedGameScene);

		newImpact += (win) ? current.yes : current.no;
		ApplyImpact(newImpact);
	}
}
