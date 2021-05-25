using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyringesGame : MonoBehaviour
{
    public GameObject syringe;
    public GameObject tige;
    public GameObject targetTige;
    public GameObject targetTigeEnd;
    public GameObject firstMedoc;
    public GameObject secondMedoc;

    public Text textFirstMedoc;
    public Text textSecondMedoc;
    public Text textTotal;

    public float speed;
    public float speedRemplissage;
    public float speedRemplissageSeringue;

    private int firstMedocInt;
    private int secondMedocInt;
    private int firstResult;
    private int secondResult;
    private int total;
    private float totalInt;

    private bool stepOne = true;
    private bool stepTwo = false;
    private bool syringeBack = true;
    private bool syringeBackV2 = true;
    private bool start = false;
    private bool startAction = false;
    private bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        firstResult = 0;
        secondResult = 0;
        totalInt = 0;
        firstMedocInt = Random.Range(1, 11);
        secondMedocInt = Random.Range(1, 11);
        textFirstMedoc.text = firstMedocInt.ToString();
        textSecondMedoc.text = secondMedocInt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ItemMovement();
        Remplissage();
        Verification();
        foreach (Touch touch in Input.touches)
        {
            if(startAction)
            {
                if (touch.phase == TouchPhase.Began)
                    start = true;
            
                if (touch.phase == TouchPhase.Ended)
                {
                    start = false;
                    startAction = false;
                    stepOne = false;

                    if (stepTwo)
                        stepTwo = false;
                    else
                        stepTwo = true;
                }
                    
            }
        }
    }
    public void Remplissage()
    {
        if(start && tige.transform.position.y < targetTigeEnd.transform.position.y)
        {
            totalInt += Time.deltaTime * speedRemplissage;
            tige.transform.position = new Vector3(tige.transform.position.x, tige.transform.position.y + ((speedRemplissageSeringue * Time.deltaTime)/25), tige.transform.position.z);
            total = (int) totalInt;
            textTotal.text = total.ToString();           
        }
        else
        {
            if (firstResult == 0)
                firstResult = total;
            else
                secondResult = total - firstResult;
        }
    }

    public void ItemMovement()
    {
        if(!startAction)
        {
            if(stepOne)
            {
                if(firstMedoc.transform.position.x > 1.5f)
                {
                    firstMedoc.transform.position = new Vector2(firstMedoc.transform.position.x - (speed * Time.deltaTime), firstMedoc.transform.position.y);
                }
                else if (syringe.transform.position.y > -0.5f)
                {
                    syringe.transform.position = new Vector3(syringe.transform.position.x, syringe.transform.position.y - (speed * Time.deltaTime),1);
                }
                else 
                {
                    startAction = true;
                }
            }
            else if (stepTwo)
            {
                if (syringe.transform.position.y < 1 && syringeBack)
                {
                    syringe.transform.position = new Vector3(syringe.transform.position.x, syringe.transform.position.y + (speed * Time.deltaTime),1);
                }
                else if (firstMedoc.transform.position.y > -9)
                {
                    syringeBack = false;
                    firstMedoc.transform.position = new Vector2(firstMedoc.transform.position.x, firstMedoc.transform.position.y - (speed * Time.deltaTime));
                }
                else if (secondMedoc.transform.position.x > 1.5f)
                {
                    secondMedoc.transform.position = new Vector2(secondMedoc.transform.position.x - (speed * Time.deltaTime), secondMedoc.transform.position.y);
                }
                else if (syringe.transform.position.y > -0.5f)
                {
                    syringe.transform.position = new Vector3(syringe.transform.position.x, syringe.transform.position.y - (speed * Time.deltaTime),1);
                }
                else
                {
                    startAction = true;
                }
            }
            else
            {
                if (syringe.transform.position.y < 1 && syringeBackV2)
                {
                    syringe.transform.position = new Vector3(syringe.transform.position.x, syringe.transform.position.y + (speed * Time.deltaTime), 1);
                    //Debug.Log(syringe.transform.rotation.eulerAngles.z);
                }
                else if (secondMedoc.transform.position.y > -9)
                {
                    syringeBackV2 = false;
                    secondMedoc.transform.position = new Vector2(secondMedoc.transform.position.x, secondMedoc.transform.position.y - (speed * Time.deltaTime));
                }
                else if (syringe.transform.rotation.eulerAngles.z > 315 || syringe.transform.rotation.eulerAngles.z == 0)
                {
                    syringe.transform.eulerAngles = new Vector3(syringe.transform.eulerAngles.x, syringe.transform.eulerAngles.y, syringe.transform.eulerAngles.z-(speed * Time.deltaTime*50));
                }
                else if (syringe.transform.position.x > -5.5)
                {
                    syringe.transform.position = new Vector3(syringe.transform.position.x - (speed * Time.deltaTime), syringe.transform.position.y - ((speed / 2 * Time.deltaTime)));
                }
                else
                    end = true;
            }
        }
    }

    public void Verification()
    {
        if(end)
        {
            if(tige.transform.position.x>targetTige.transform.position.x)
            {
                Vector3 dir = (tige.transform.position - targetTige.transform.position).normalized;
                tige.transform.position = tige.transform.position - ((dir * Time.deltaTime * speedRemplissageSeringue) /12.5f);
            }
            else if (firstResult == firstMedocInt && secondResult == secondMedocInt)
                EventSystem.Manager.EndGame(true);
            else
                EventSystem.Manager.EndGame(false);

        }
    }
}
