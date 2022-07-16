using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public bool soundOn;
    public AudioSource sfxAuidoSource;
    public GameObject bgMusicIngame;
    public List<AudioClip> collisionSFX;
    public AudioClip splashSFX;
    public AudioClip buttonTapSFX;
    //public AudioClip collisionSFX;
    public AudioClip tapStartSFX;
    public AudioClip coinSFX;
    public AudioClip bounceSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip logPickSFX;
    public AudioClip logPlaceSFX;
    public AudioClip jumpSFX;
    public AudioClip landSFX;
    public AudioClip beepSFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        soundOn = true;
        sfxAuidoSource = GetComponent<AudioSource>();
        //bgMusicIngame.SetActive(false);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        //if (PlayerPrefs.GetInt("isSound") == 1)
        //{
        if (soundOn)
        {
            sfxAuidoSource.PlayOneShot(audioClip);
        }

        //}
    }

    public AudioClip GetRandomHitSFX()
    {
        return collisionSFX[Random.Range(0, collisionSFX.Count)];
    }
}
