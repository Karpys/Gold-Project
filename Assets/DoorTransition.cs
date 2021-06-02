using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform DoorLeft;
    public RectTransform DoorRight;
    public DoorState State;
    public AnimationCurve Curve;
    public float TimeSet;
    public float TransitionTime;
    public string SceneName;
    public GameObject Menu;
    public LightManager Light;

    public float ScreenWidth;

    public void Start()
    {
        ScreenWidth = BaseResolution.Resolution.Width;
    }
    public void DoorTransi()
    {
        if(State == DoorState.IDLE)
        { 
        State = DoorState.CLOSING;
        TimeSet = 0;
        DoorLeft.gameObject.SetActive(true);
        DoorRight.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if(State == DoorState.CLOSING)
        {
            TimeSet += Time.deltaTime;
            float Pos = Mathf.Lerp(ScreenWidth + 150, 0, Curve.Evaluate(TimeSet));
            DoorLeft.SetLeft(-Pos);
            DoorLeft.SetRight(Pos);
            DoorRight.SetLeft(Pos);
            DoorRight.SetRight(-Pos);
            if(TimeSet>= TransitionTime)
            {
                State = DoorState.CLOSE;
            }

        }else if(State == DoorState.CLOSE)
        {
            StartCoroutine(ChangeScene());
            
        }else if(State == DoorState.OPENING)
        {
            TimeSet -= Time.deltaTime;
            float Pos = Mathf.Lerp(ScreenWidth + 150, 0, Curve.Evaluate(TimeSet));
            DoorLeft.SetLeft(-Pos);
            DoorLeft.SetRight(Pos);
            DoorRight.SetLeft(Pos);
            DoorRight.SetRight(-Pos);
            if (TimeSet <=0)
            {
                State = DoorState.OPEN;
            }
        }else if(State == DoorState.OPEN)
        {
            TimeSet = 0;
            DoorLeft.gameObject.SetActive(false);
            DoorRight.gameObject.SetActive(false);
            State = DoorState.IDLE;
        }
        
    }

    public IEnumerator ChangeScene()
    {
        State = DoorState.CHANGESCENE;
        Light.enabled = true;
        GameManager.Get.PlayGame();
        /*SceneManager.LoadScene(SceneName);*/
        Menu.SetActive(!Menu.activeSelf);
        yield return new WaitForSeconds(0.5f);
        State = DoorState.OPENING;

    }



    public enum DoorState
    {
        OPEN,
        CLOSING,
        CLOSE,
        CHANGESCENE,
        OPENING,
        IDLE,
    }
}
