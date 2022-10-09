using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockController : MonoBehaviour
{

    public static bool sebixRangedUnlocked = false;
    public static bool sebixSpecialUnlocked = false;

    public GameObject sebixRangedModal;
    public GameObject sebixSpecialModal;

    void Start()
    {
        StartCoroutine(unlockSebixRanged());
        StartCoroutine(resume(18f));
        StartCoroutine(unlockSebixSpecial());
        StartCoroutine(resume(35f));

        this.sebixRangedModal.SetActive(false);
        this.sebixSpecialModal.SetActive(false);

    }

    IEnumerator unlockSebixRanged()
    {
        yield return new WaitForSecondsRealtime(15f);
        UnlockController.sebixRangedUnlocked = true;
        this.sebixRangedModal.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator unlockSebixSpecial()
    {
        yield return new WaitForSecondsRealtime(32f);
        UnlockController.sebixSpecialUnlocked = true;
        this.sebixSpecialModal.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator resume(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        this.sebixRangedModal.SetActive(false);
        this.sebixSpecialModal.SetActive(false);
        Time.timeScale = 1;
    }
}
