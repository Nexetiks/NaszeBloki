using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneManager : MonoBehaviour
{

    bool canLoad = true;

    void Start()
    {
        this.canLoad = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && this.canLoad)
        {
            this.canLoad = false;
            SceneManager.LoadScene("Level_Game");
        }

        if (Input.GetKeyDown("escape") && this.canLoad)
        {
            this.canLoad = false;
            SceneManager.LoadScene("MainMenu");

        }
    }
}
