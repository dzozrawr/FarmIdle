using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance { get => instance; }

    public static AudioClip audioClip;
    public static AudioSource audioSrc;

    public static AudioClip backgroundMusic;

    private static float backgroundMusicStartTime = 0f;

    //public AudioToggle switchForAudio;


    private float pitchStep = 0.01f;
    public float PitchStep { get => pitchStep; set => pitchStep = value; }
    //private float pitchStep = 0.00f;
    private float resetDelayTime = 0.5f;
    float defaultPitch;

    Coroutine coroutine, cooldownCoroutine;


    private bool isAudioSourceEnabled;
    public bool IsAudioSourceEnabled { get => isAudioSourceEnabled; }

    //private bool stackingSoundPlayedInThisFrame = false;
    //private float delayBetweenStartingOfStackingSounds = 0.033f, timeToResumePlayingTheStackSound=0f;
    //private float stackingSoundWCooldownVolume = 0.5f;


    public AudioSource defaultAudioSrc;
    //public AudioSource backgroundMusicAudioSrc;

    private AudioClip plantingSound, harvestingSound, sellingSound, coinClaimSound;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        audioSrc = defaultAudioSrc;
        defaultPitch = audioSrc.pitch;
        isAudioSourceEnabled = audioSrc.enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        plantingSound = Resources.Load<AudioClip>("plantingSound");
        harvestingSound = Resources.Load<AudioClip>("harvestingSound");
        sellingSound = Resources.Load<AudioClip>("sellingSound");
        coinClaimSound = Resources.Load<AudioClip>("coinClaim");
    }



    /*     private void Update()
        {
            if (stackingSoundPlayedInThisFrame)
            {
                if(Time.time> timeToResumePlayingTheStackSound) stackingSoundPlayedInThisFrame = false;
            }

          //  Debug.LogError(backgroundMusicAudioSrc.time);
        } */




    public void PlaySound(string clip, float volume = 1f)
    {
        if (!audioSrc.enabled) return;
        switch (clip)
        {
            case "plantingSound":

                audioSrc.PlayOneShot(plantingSound);
                break;
            case "harvestingSound":

                audioSrc.PlayOneShot(harvestingSound);
                break;
            case "sellingSound":

                audioSrc.PlayOneShot(sellingSound);
                break;
            case "coinClaim":

                audioSrc.PlayOneShot(coinClaimSound);
                break;
        }
    }

    public void PlaySoundWPitchChange(string clip, float volume = 1f)
    {
        PlaySound(clip, volume);

        audioSrc.pitch += PitchStep;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ResetPitchCoroutine());
    }

    public void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        audioSrc.PlayOneShot(audioClip, volume);
    }


    public void PlaySoundWPitchChange(AudioClip audioClip, float volume = 1f)
    {
        audioSrc.PlayOneShot(audioClip, volume);

        audioSrc.pitch += PitchStep;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ResetPitchCoroutine());
    }

    public void ResetPitchToDefault()
    {
        audioSrc.pitch = defaultPitch;
    }

    IEnumerator ResetPitchCoroutine()
    {
        yield return new WaitForSeconds(resetDelayTime);
        audioSrc.pitch = defaultPitch;
    }

    IEnumerator PlayOneShotWCooldown(AudioSource src, AudioClip clip, float volume = 1f, float coolDown = 0.1f)
    {
        src.PlayOneShot(clip, volume);
        yield return new WaitForSeconds(coolDown);
        cooldownCoroutine = null;
    }

    void Reset()
    {
        audioSrc = GetComponent<AudioSource>();
        if (audioSrc)
        {
            audioSrc.playOnAwake = false;
        }
    }
}
