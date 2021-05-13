using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public string[] namesPool;
     public string characterName;

    private void Awake()
    {
        characterName = namesPool[Random.Range(0, namesPool.Length)];
    }

    // Behaviour

}
