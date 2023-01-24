using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlButtonPanel : MonoBehaviour
{
    bool isMuted = false;
    bool isFast = false;
    bool isPaused = false;

    [SerializeField]
    Sprite ActiveSprite;
    [SerializeField]
    Sprite PauseSprite;


    [SerializeField]
    Sprite SoundSprite;
    [SerializeField]
    Sprite MuteSprite;
    public void ToggleMute(Image img)
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            img.sprite = MuteSprite;
            GameManager.Sound.Mute();
        }
        else
        {
            img.sprite = SoundSprite;
            GameManager.Sound.UnMute();
        }
    }
    public void TogglePause(Image img)
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            img.sprite = PauseSprite;
        }
        else
        {
            img.sprite = ActiveSprite;
        }    
    }
    public void ToggleFast(Image img)
    {
        isFast = !isFast;
        if (isFast)
        {
            img.color = Color.red;
            Time.timeScale = 2.0f;
        }
        else
        {
            img.color = Color.white;
            img.color = new Color(1, 1, 1, 0.58f);
            Time.timeScale = 1.0f;
        }
    }
}
