using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip bgMusic;

    public AudioClip normalMusicClip;
    public AudioClip papiezMusicClip;
    public AudioClip barkaMusicClip;

    public AudioClip levelUp;
    public AudioClip sebixPain1;
    public AudioClip sebixPain2;
    public AudioClip sebixPain3;
    public AudioClip sebixPain4;
    public AudioClip sebixPain5;
    public AudioClip sebixPain6;
    public AudioClip sebixPain7;
    public AudioClip moherPain1;
    public AudioClip moherPain2;
    public AudioClip moherPain3;
    public AudioClip moherPain4;
    public AudioClip moherPain5;
    public AudioClip moherPain6;
    public AudioClip moherPain7;
    public AudioClip klerPain1;
    public AudioClip klerPain2;
    public AudioClip klerPain3;
    public AudioClip klerPain4;
    public AudioClip klerPain5;
    public AudioClip klerPain6;
    public AudioClip klerPain7;

    public AudioClip superPartia;

    public AudioClip jestemHardkorem;
    public AudioClip malina;
    public AudioClip aleurwal;

    public AudioClip niechZstapi;

    bool canPlayZawadiaka = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = normalMusicClip;
        audioSource.Play();
    }

    public void playAudio(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.LevelUp:
                {
                    audioSource.PlayOneShot(this.levelUp, 1F);
                    break;
                }
            case AudioType.Barka:
                {
                    audioSource.Stop();
                    StartCoroutine(playBarka());
                    break;
                }
            case AudioType.NiechZstapi:
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(this.niechZstapi, 1F);
                    if(canPlayZawadiaka)
                    {
                        canPlayZawadiaka = false;
                     StartCoroutine(playPapiez());
                    }
                    break;
                }
            case AudioType.SebixPain:
                {
                    int randomNumber = Random.Range(1, 7);

                    int randomNumber2 = Random.Range(1, 50);

                    if (randomNumber2 == 1)
                    {
                        audioSource.PlayOneShot(this.jestemHardkorem, 2F);
                    }
                    if (randomNumber2 == 2)
                    {
                        audioSource.PlayOneShot(this.aleurwal, 1.5F);
                    }
                    if (randomNumber2 == 3)
                    {
                        audioSource.PlayOneShot(this.malina, 1.5F);
                    }

                    switch (randomNumber)
                    {
                        case 1:
                            audioSource.PlayOneShot(this.sebixPain1, 1F);
                            break;
                        case 2:
                            audioSource.PlayOneShot(this.sebixPain2, 1F);
                            break;
                        case 3:
                            audioSource.PlayOneShot(this.sebixPain3, 1F);
                            break;
                        case 4:
                            audioSource.PlayOneShot(this.sebixPain4, 1F);
                            break;
                        case 5:
                            audioSource.PlayOneShot(this.sebixPain5, 1F);
                            break;
                        case 6:
                            audioSource.PlayOneShot(this.sebixPain6, 1F);
                            break;
                        case 7:
                            audioSource.PlayOneShot(this.sebixPain7, 1F);
                            break;

                    }
                    break;
                }
            case AudioType.MoherPain:
                {
                    int randomNumber = Random.Range(1, 7);
                    int randomNumber2 = Random.Range(1, 20);

                    if(randomNumber2 == 1)
                    {
                        audioSource.PlayOneShot(this.superPartia, 1.5F);
                    }

                    switch (randomNumber)
                    {
                        case 1:
                            audioSource.PlayOneShot(this.moherPain1, 1F);
                            break;
                        case 2:
                            audioSource.PlayOneShot(this.moherPain2, 1F);
                            break;
                        case 3:
                            audioSource.PlayOneShot(this.moherPain3, 1F);
                            break;
                        case 4:
                            audioSource.PlayOneShot(this.moherPain4, 1F);
                            break;
                        case 5:
                            audioSource.PlayOneShot(this.moherPain5, 1F);
                            break;
                        case 6:
                            audioSource.PlayOneShot(this.moherPain6, 1F);
                            break;
                        case 7:
                            audioSource.PlayOneShot(this.moherPain7, 1F);
                            break;

                    }
                    break;
                }
            case AudioType.KlerPain:
                {
                    int randomNumber = Random.Range(1, 7);

                    switch (randomNumber)
                    {
                        case 1:
                            audioSource.PlayOneShot(this.klerPain1, 1F);
                            break;
                        case 2:
                            audioSource.PlayOneShot(this.klerPain2, 1F);
                            break;
                        case 3:
                            audioSource.PlayOneShot(this.klerPain3, 1F);
                            break;
                        case 4:
                            audioSource.PlayOneShot(this.klerPain4, 1F);
                            break;
                        case 5:
                            audioSource.PlayOneShot(this.klerPain5, 1F);
                            break;
                        case 6:
                            audioSource.PlayOneShot(this.klerPain6, 1F);
                            break;
                        case 7:
                            audioSource.PlayOneShot(this.klerPain7, 1F);
                            break;

                    }
                    break;
                }
            default:
                {

                    break;
                }
        }
    }


    IEnumerator playBarka()
    {
        yield return new WaitForSecondsRealtime(1);
        audioSource.clip = barkaMusicClip;
        audioSource.Play();
    }

    IEnumerator playPapiez()
    {
        yield return new WaitForSecondsRealtime(7);
        audioSource.clip = papiezMusicClip;
        audioSource.Play();
    }
}
