using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonLink : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        Debug.Log(" 3   ");
        button = GetComponent<Button>();

        button.onClick.AddListener(() => GameManager.Get.PlayGame());
    }
}
