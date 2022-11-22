using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownUITween : MonoBehaviour
{
    [SerializeField]
    GameObject Cycle, Shop, Hotel, Guild;

    public void DoCounterClockwise()
    {   
        //Cycle.GetComponent<CanvasGroup>().LeanAlpha(0.5f, 1f);
        LeanTween.scale(Cycle, new Vector3(8f, 8f, 8f), .3f).setDelay(0.1f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.rotateAround(Cycle, Vector3.forward, 120, 0.5f).setDelay(0.2f).setEase(LeanTweenType.easeInQuad);
        LeanTween.scale(Cycle, new Vector3(12f, 12f, 12f), .3f).setDelay(0.3f).setEase(LeanTweenType.easeOutCirc);
        //Cycle.GetComponent<CanvasGroup>().LeanAlpha(1f, 1f);
    }

    public void DoClockwise()
    { 
        //Cycle.GetComponent<CanvasGroup>().LeanAlpha(0.5f, 1f);
        LeanTween.scale(Cycle, new Vector3(8f, 8f, 8f), .3f).setDelay(0.1f).setEase(LeanTweenType.easeOutCirc);
        LeanTween.rotateAround(Cycle, Vector3.forward, -120, 0.5f).setEase(LeanTweenType.easeInQuad);
        LeanTween.scale(Cycle, new Vector3(12f, 12f, 12f), .3f).setDelay(0.3f).setEase(LeanTweenType.easeOutCirc);
        //Cycle.GetComponent<CanvasGroup>().LeanAlpha(1f, 1f);
    }
}
