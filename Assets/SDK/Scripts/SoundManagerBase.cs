using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetalGamesSDK;
using Sirenix.OdinInspector;
public class SoundManagerBase : Singleton<SoundManager>
{
    [Title("AudioSources")]
    [ShowInInspector, ReadOnly] public AudioSource m_SfxSourse;
    [ShowInInspector, ReadOnly] public AudioSource m_MusicSourse;

    [Title("AudioClip")]
    [ShowInInspector, PropertyOrder(1)] public AudioClip m_MusicClip;


    protected void PlaySfx(AudioClip i_clip)
    {
        if (!IsMusicMuted)
            m_MusicSourse.PlayOneShot(i_clip);
    }

    protected void PlayMusic()
    {
        if (!IsMusicMuted)
            m_MusicSourse.clip = m_MusicClip;
        m_MusicSourse.Play();
    }



    [Title("Use Storage Manager To Toggle These")]
    [PropertyOrder(100)]
    [ShowInInspector]
    public bool IsMusicMuted
    {
        get { return !StorageManager.Instance.SettingsPanelVariables.Music; }
    }

    [PropertyOrder(100)]
    [ShowInInspector]

    public bool IsSFXMuted
    {
        get { return !StorageManager.Instance.SettingsPanelVariables.SFX; }
    }

    [Button, PropertyOrder(0)]
    public void GetSources()
    {
        m_MusicSourse = transform.FindDeepChild<AudioSource>("musicsource");
        m_SfxSourse = transform.FindDeepChild<AudioSource>("sfxsource");
    }


}
