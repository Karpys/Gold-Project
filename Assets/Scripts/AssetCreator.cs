using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.U2D.Animation;

public class AssetCreator : MonoBehaviour
{
    private Button button;
    private Text buttonText;

    public Sprite NewSprite;

    // Start is called before the first frame update
    void Awake()
    {
        button = FindObjectOfType<Button>();
        buttonText = button.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NewSprite == null)
        { 
            buttonText.text = "Add a New Sprite >>>";
            buttonText.color = Color.white;
            buttonText.fontStyle = FontStyle.Bold;
            button.interactable = false;
        }

        else
        {
            buttonText.text = "Create New Asset";
            buttonText.color = Color.black;
            buttonText.fontStyle = FontStyle.Normal;
            button.interactable = true;
        }
    }

    public void NewAsset()
    {
        Debug.Log("Create!!!!!");
    }
}
