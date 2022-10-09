using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    bool continueEnabled = false;

    bool canLoad = true;


    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;


    public GameObject other1;
    public GameObject other2;
    public GameObject other3;
    public GameObject other4;
    public GameObject other5;
    public GameObject other6;
    public GameObject other7;
    public GameObject other8;
    public GameObject other9;

    void Start()
    {

        this.canLoad = true;
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);


        other1.SetActive(false);
        other2.SetActive(false);
        other3.SetActive(false);
        other4.SetActive(false);
        other5.SetActive(false);
        other6.SetActive(false);
        other7.SetActive(false);
        other8.SetActive(false);
        other9.SetActive(false);

        StartCoroutine(setVisible(this.text1, 1f));
        StartCoroutine(setVisible(this.text2, 2.5f));
        StartCoroutine(setVisible(this.text3, 4f));
        StartCoroutine(setVisible(this.text4, 5.5f));
        StartCoroutine(setVisible(this.text5, 8f));
        StartCoroutine(disableAll(11f));
        StartCoroutine(enableContinue());
    }


    IEnumerator setVisible(GameObject obj, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        obj.SetActive(true);
    }

    IEnumerator disableAll(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        text5.SetActive(false);


        other1.SetActive(true);
        other2.SetActive(true);
        other3.SetActive(true);
        other4.SetActive(true);
        other5.SetActive(true);
        other6.SetActive(true);
        other7.SetActive(true);
        other8.SetActive(true);
        other9.SetActive(true);


    }

    IEnumerator enableContinue()
    {
        yield return new WaitForSeconds(13f);

        this.continueEnabled = true;

    }

    void Update()
    {
        if(this.continueEnabled)
        {

            if (Input.GetKeyDown("space") && this.canLoad)
            {
                this.canLoad = false;
                SceneManager.LoadScene("Level_Game");
            }
            }
    }

}
