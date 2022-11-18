using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroUItween : MonoBehaviour
{
    [SerializeField]
    GameObject Background, Title, ScrollArea, Content;

    void Start()
    {
        //LeanTween.moveLocal(ScrollArea, new Vector3(0f, 0f, 2f), .7f).setEase(LeanTweenType.easeInCubic).setOnComplete(SetContent);
        LeanTween.moveLocal(Title, new Vector3(0f, 0f, 2f), 0.7f).setEase(LeanTweenType.easeInCubic).setOnComplete(SetContent);
    }

    void SetContent()
    {
        // LeanTween.alpha(Content.GetComponent<RectTransform>(), 1f, .5f).setDelay(0.4f);
        LeanTween.cancel(Background);
        LeanTween.moveLocal(Content, new Vector3(0f, -200f, 0f), 0.7f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);
        Background.GetComponent<CanvasGroup>().alpha = 0;
        Background.GetComponent<CanvasGroup>().LeanAlpha(0.8f, 0.5f);
    }

    public void SetRemove()
    {
        LeanTween.cancel(Background);
        LeanTween.moveLocal(Title, new Vector3(0f, +100f, 0f), 0.7f).setDelay(0.3f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.moveLocal(Title, new Vector3(0f, -1000f, 0f), 0.7f).setDelay(0.4f).setEase(LeanTweenType.easeOutCirc);
        // LeanTween.scale(ScrollArea, new Vector3(0f, 0f, 0f), .5f).setDelay(0.3f).setEase(LeanTweenType.easeInQuad);
        Background.GetComponent<CanvasGroup>().LeanAlpha(0f, 0.5f);
        
    }

}
