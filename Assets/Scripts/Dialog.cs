using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private static Dialog inst;
    public static Dialog Manager { get => inst; }

    [HideInInspector] public GameObject Box;
    public GameObject characterName;
    private Text name_txt;
    public GameObject dialogBox;
    public GameObject playerAnswer1;
    public GameObject playerAnswer2;
    private Text dialogBox_txt;
    private Text playerAnswer1_txt;
    private Text playerAnswer2_txt;

    private void Awake()
    {
        if (inst == null)
            inst = this;

        name_txt = characterName.GetComponentInChildren<Text>();
        Box = dialogBox.transform.parent.gameObject;
        dialogBox_txt = dialogBox.GetComponentInChildren<Text>();
        playerAnswer1_txt = playerAnswer1.GetComponentInChildren<Text>();
        playerAnswer2_txt = playerAnswer2.GetComponentInChildren<Text>();

        Close();
    }

    public void Open()
    {
        dialogBox.SetActive(true);
        characterName.SetActive(true);
        Prompt(false);
    }

    public void Close()
    {
        dialogBox.SetActive(false);
        characterName.SetActive(false);
        Prompt(false);
    }

    public void NextDialog(string next)
    {
        dialogBox_txt.text = next;
    }
    public void NextDialog(string next, string name)
    {
        dialogBox_txt.text = next;
        name_txt.text = name;
    }

    public void Prompt(bool on)
    {
        playerAnswer1.SetActive(on);
        playerAnswer2.SetActive(on);

        if (on)
        {
            playerAnswer1_txt.text = EventSystem.Manager.current.first.answer;
            playerAnswer2_txt.text = EventSystem.Manager.current.second.answer;
        }
    }
}
