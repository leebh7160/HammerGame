using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    //===============================Reference
    private GameManager gameManager;
    private TimingManager timingManager;
    //===============================Reference^^

    [SerializeField] private GameObject LeftEffect;
    [SerializeField] private GameObject RightEffect;

    [SerializeField] private GameObject Crystar_LeftEffect;
    [SerializeField] private GameObject Crystar_RightEffect;



    void Start()
    {
        gameManager     = GameObject.Find("GameManager").GetComponent<GameManager>();
        timingManager   = FindObjectOfType<TimingManager>();
    }

    void Update()
    {
        KeyEffect();
        CrystarEffect();
    }

    private void KeyEffect()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            LeftEffect.SetActive(true);
            RightEffect.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            timingManager.LeftCheckTiming();
            LeftAnimation();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            LeftEffect.SetActive(false);
            RightEffect.SetActive(true);
            timingManager.RightCheckTiming_Short();
            RightAnimaiton();
        }
    }

    private void LeftAnimation()
    {
        //Debug.Log("L?????");
    }

    private void RightAnimaiton()
    {
        //Debug.Log("R?????");
    }

    private void CrystarEffect()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Crystar_RightEffect.SetActive(true);
            timingManager.RightCheckTiming_Long();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
            Crystar_RightEffect.SetActive(false);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Crystar_LeftEffect.SetActive(true);
            timingManager.LeftCheckTiming_Long();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            Crystar_LeftEffect.SetActive(false);
    }
}
