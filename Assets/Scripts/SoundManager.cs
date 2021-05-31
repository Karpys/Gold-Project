using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager inst;
    public static SoundManager Get { get => inst; }

    [Header("Sound")]
    public AudioClip aiguille;
    public AudioClip remplicageSeringue;
    public AudioClip feuCrepitant;
    public AudioClip desinfectant;
    public AudioClip creme;
    public AudioClip pansement;
    public AudioClip remousDEau;
    public AudioClip mouvementPiece;
    public AudioClip mixCard;
    public AudioClip turnCard;

    private void Awake()
    {
        if (inst == null)
            inst = this;

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
