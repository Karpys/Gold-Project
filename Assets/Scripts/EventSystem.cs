using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
	private static EventSystem inst;
	public static EventSystem Manager { get => inst; }

	 public Event current; // current Event being played

	public List<Event> eventPool;
	private string loadedGameScene;

	[Header("	UI Object")]
	public GameObject pauseUI;


    private void Awake()
    {
		if (inst == null)
			inst = this;

		//DontDestroyOnLoad(this);
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
		/*FadeController.Fade.Anim.Play("FadeScreenAnim");*/
		/*PixeliseScreen.PixelScreen.Pixelise();*/
		DoorTransition.Transition.DoorTransi(false);
		yield return new WaitForSeconds(1.3f);

		if (Application.CanStreamedLevelBeLoaded(sceneName))
		{
			loadedGameScene = sceneName;
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			//
			Dialog.Manager.Box.SetActive(false);
			RessourceUI.UIRessource.gameObject.SetActive(false);
			pauseUI.SetActive(false);
		}
		else Debug.LogError(" No scene \"" + sceneName + "\" could be found.");
    }


	public IEnumerator EndGame(bool win)
	{
		if (win)
			AudioSource.PlayClipAtPoint(SoundManager.Get.win, new Vector3(0, 0, 0));
		else
			AudioSource.PlayClipAtPoint(SoundManager.Get.lose, new Vector3(0, 0, 0));
		/*FadeController.Fade.Anim.Play("FadeScreenAnim");*/
		/*PixeliseScreen.PixelScreen.Pixelise();*/
		DoorTransition.Transition.DoorTransi(false);
		yield return new WaitForSeconds(1.3f);
		//
		Dialog.Manager.Box.SetActive(true);
		RessourceUI.UIRessource.gameObject.SetActive(true);
		pauseUI.SetActive(true);
		//
		SceneManager.UnloadSceneAsync(loadedGameScene);

		Event.Impact impact = (win) ? current.yes : current.no;
		ApplyImpact(impact);
    }

	public IEnumerator EndGame(bool win, Event.Impact newImpact) // use when additional resources impact in minigame
	{
		if (win)
			AudioSource.PlayClipAtPoint(SoundManager.Get.win, new Vector3(0, 0, 0));
		else
			AudioSource.PlayClipAtPoint(SoundManager.Get.lose, new Vector3(0, 0, 0));

		//FadeController.Fade.Anim.Play("FadeScreenAnim");
		/*PixeliseScreen.PixelScreen.Pixelise();*/
		DoorTransition.Transition.DoorTransi(false);
		yield return new WaitForSeconds(1.3f);
		//
		Dialog.Manager.Box.SetActive(true);
		RessourceUI.UIRessource.gameObject.SetActive(true);
		pauseUI.SetActive(true);
		//
		SceneManager.UnloadSceneAsync(loadedGameScene);

		newImpact += (win) ? current.yes : current.no;
		ApplyImpact(newImpact);
	}
}
