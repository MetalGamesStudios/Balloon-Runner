using System;
using MetalGamesSDK;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sounds[] sounds;
    public AudioSource musicSource;
    public AudioSource source;
    public AudioClip[] popSounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
    }

    void PlayConfetti()
    {
        Play("shurli");
    }

    public void CheckMuteState()
    {
        if (IsMusicMuted)
            musicSource.enabled = false;
        else
        {
            musicSource.enabled = true;
        }
    }

    bool IsMusicMuted
    {
        get { return !StorageManager.Instance.SettingsPanelVariables.Music; }
    }


    void PlayAlertWhistle() => Play("alert");

    public void Play(string sound)
    {
        Sounds s = Array.Find(sounds, item => item.name == sound);

        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.Play();
    }

    public void PlayRandomPopSound()
    {
        int ran = Random.Range(0, popSounds.Length);

        musicSource.pitch = Random.Range(0.9f, 1.4f);
        musicSource.PlayOneShot(popSounds[ran]);
    }
}