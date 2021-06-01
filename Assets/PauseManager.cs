using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Mask")]
    public GameObject pausePanel;
    public GameObject[] target;

    [Header("Bouton")]
    public GameObject[] bouton;
    public GameObject[] targetBouton;

   [Header("AudioListener")]
    public AudioSource sound;

    [Header("Name Scene")]
    public string nameSceneRetry;

    [Header("")]
    public float speed;

    private bool open = false;
    private bool close = true;
    private float lerp = 0;

    // Update is called once per frame
    void Update()
    {
        if (pausePanel.transform.position.x < targetBouton[0].transform.position.x)
            bouton[0].SetActive(true);
        else
            bouton[0].SetActive(false);

        if (pausePanel.transform.position.x < targetBouton[1].transform.position.x)
            bouton[1].SetActive(true);
        else
            bouton[1].SetActive(false);

        if (pausePanel.transform.position.x < target[1].transform.position.x + 0.5)
            bouton[2].SetActive(true);
        else
            bouton[2].SetActive(false);

        if (open)
        {
            lerp += Time.deltaTime * speed;

            if(lerp<1)
                pausePanel.transform.position = Vector3.Lerp(pausePanel.transform.position, target[1].transform.position, lerp);
            else
                lerp = 0;

        }

        if (close)
        {
            lerp += Time.deltaTime * speed;

            if (pausePanel.transform.position.x!= target[0].transform.position.x)
                pausePanel.transform.position = Vector3.Lerp(pausePanel.transform.position, target[0].transform.position, lerp);
            else
                lerp = 0;

        }
    }

    public void OpenPause()
    {
        if(!open)
        {
            open = true;
            close = false;
            lerp = 0;
        }
        else
        {
            close = true;
            open = false;
            lerp = 0;
        }

    }

    public void BackMenu()
    {
        GameManager.loadMenu = true;
        GameManager.Get.LoadScene(nameSceneRetry, false);
    }

    public void Sound(bool onOff)
    {
        if(!onOff)
            sound.volume = 0;
        else
            sound.volume = 1;
    }

    public void Retry()
    {
        GameManager.loadMenu = false;
        GameManager.Get.LoadScene(nameSceneRetry, false);
    }
}
