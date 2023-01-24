using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public Dictionary<string,AudioClip> sounds = new Dictionary<string, AudioClip>();
    AudioSource[] audioSource = new AudioSource[9];

    private bool isMuted = false;

   
    public void Init()
    {
        for(int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i] = gameObject.AddComponent<AudioSource>();
        }
        DontDestroyOnLoad(this);
        sounds.Add("Boss_Battle", Resources.Load<AudioClip>("Sounds/Battle_Boss"));
        sounds.Add("Battle", Resources.Load<AudioClip>("Sounds/Battle"));
        sounds.Add("Button", Resources.Load<AudioClip>("Sounds/10_UI_Menu_SFX/Confirm"));
        sounds.Add("Fire_Effect", Resources.Load<AudioClip>("Sounds/8_Atk_Magic_SFX/Fire_Effect"));
        sounds.Add("Ice_Effect", Resources.Load<AudioClip>("Sounds/8_Atk_Magic_SFX/Ice_Effect"));
        sounds.Add("Water_Effect", Resources.Load<AudioClip>("Sounds/8_Atk_Magic_SFX/Water_Effect"));
        sounds.Add("Earth_Effect", Resources.Load<AudioClip>("Sounds/8_Atk_Magic_SFX/Earth_Effect"));
        sounds.Add("Wind_Effect", Resources.Load<AudioClip>("Sounds/8_Atk_Magic_SFX/Wind_Effect"));
        sounds.Add("Arrow_Effect", Resources.Load<AudioClip>("Sounds/Arrow_Effect"));
    }
    public void Mute()
    {
        foreach (var sound in audioSource)
        {
            if (sound.isPlaying)
                sound.Stop();
        }
        isMuted = true;
    }
    public void UnMute()
    {
        isMuted = false;
    }
    public void PlayButtonSound()
    {
        if (isMuted)
            return;
        audioSource[0].clip = sounds["Button"];
        audioSource[0].Play();
    }
    public void PlayFireEffect()
    {
        if (isMuted)
            return;
        audioSource[1].clip = sounds["Fire_Effect"];
        audioSource[1].Play();
    }
    public void PlayIceEffect()
    {
        if (isMuted)
            return;
        audioSource[2].clip = sounds["Ice_Effect"];
        audioSource[2].Play();
    }
    public void PlayWaterEffect()
    {
        if (isMuted)
            return;
        audioSource[3].clip = sounds["Water_Effect"];
        audioSource[3].Play();
    }
    public void PlayEarthEffect()
    {
        if (isMuted)
            return; 
        audioSource[4].clip = sounds["Earth_Effect"];
        audioSource[4].Play();
    }
    public void PlayArrowEffect()
    {
        if (isMuted)
            return; 
        audioSource[5].clip = sounds["Arrow_Effect"];
        audioSource[5].Play();
    }

    public void PlayWindEffect()
    {
        if (isMuted)
            return; 
        audioSource[6].clip = sounds["Wind_Effect"];
        audioSource[6].Play();
    }

    public void PlayBattleBGM()
    {
        if (isMuted)
            return;
        audioSource[7].clip = sounds["Battle"];
        audioSource[7].loop = true;
        audioSource[7].Play();
    }

    public void PlayBattleBossBGM()
    {
        if (isMuted)
            return; 
        audioSource[8].clip = sounds["Battle_Boss"];
        audioSource[8].loop = true;
        audioSource[8].Play();
    }
}
