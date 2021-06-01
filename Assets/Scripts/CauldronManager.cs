using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float Temperature;
    public float TempMax;
    public Vector3 LocalScale;
    public GameObject JaugeTemp;
    public float DivideJauge;
    public float Tempvaria;
    public float TempLose;
    public float TempUp;
    public float TempGoal;
    public float TempRange;
    public ValidTemp Jauge;

    [Header("Audio Source")]
    public AudioSource liquide;
    public AudioSource feu;
    void Awake()
    {
        liquide.clip = SoundManager.Get.remousDEau;
        liquide.Play();
        liquide.loop = true;

        feu.clip = SoundManager.Get.feuCrepitant;
        feu.Play();
        feu.loop = true;
        feu.volume = 0;

        LocalScale = JaugeTemp.transform.localScale;
        DivideJauge = TempMax / LocalScale.y;
        TempGoal = Random.Range(40, 80);
    }


    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Tempvaria += TempUp * Time.deltaTime;
            }
        }

        if (Tempvaria > -0.6f)
        {
            Tempvaria -= TempLose * Time.deltaTime;
        }

        if (Temperature > TempGoal - TempRange && Temperature < TempGoal + TempRange)
        {
            Jauge.TimeValid += Time.deltaTime;
        }

        Temperature += Mathf.Clamp(Tempvaria,-0.6f,1.8f);
        Temperature = Mathf.Clamp(Temperature, 0, TempMax);
        LocalScale.y = Temperature / DivideJauge;
        JaugeTemp.transform.localScale = LocalScale;
        feu.volume = Jauge.TimeValid/Jauge.TimeGoal;
    }
}
