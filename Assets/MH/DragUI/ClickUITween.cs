using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickUITween : MonoBehaviour
{
    [SerializeField]
    GameObject Info, AbilityInfo;

    // Start is called before the first frame update
    public void OnClick()
    {
        LeanTween.scale(Info, new Vector3(1.3f, 1.3f, 1.3f), .3f).setDelay(0.2f).setEase(LeanTweenType.easeInQuad);
        LeanTween.scale(AbilityInfo, new Vector3(1.3f, 1.3f, 1.3f), .3f).setDelay(0.3f).setEase(LeanTweenType.easeInQuad);

        LeanTween.scale(Info, new Vector3(1f, 1f, 1f), .3f).setDelay(0.2f).setEase(LeanTweenType.easeInQuad);
        LeanTween.scale(AbilityInfo, new Vector3(1f, 1f, 1f), .3f).setDelay(0.3f).setEase(LeanTweenType.easeInQuad);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
