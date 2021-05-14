using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLink : MonoBehaviour
{
    public Event.Impact newImpact;

    public void EndGame(bool win) { EventSystem.Manager.EndGame(win); }
    public void EndGameImpact(bool win) {
        EventSystem.Manager.EndGame(win, newImpact);
    }
}
