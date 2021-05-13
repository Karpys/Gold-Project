using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLink : MonoBehaviour
{
    public void EndGame(bool win) { EventSystem.Manager.EndGame(win); }
}
