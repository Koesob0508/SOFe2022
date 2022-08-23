using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

public class BattleLog : MonoBehaviour
{

    Image CauserImg;
    TMPro.TextMeshProUGUI ContentTxt;
    Image TargetImg;

    float lifeTime = 0;
    float ancOffset = 0;

    private IObjectPool<BattleLog> pool;

    CanvasGroup UIGroup;

    // Start is called before the first frame update
    void Start()
    {
        UIGroup = GetComponent<CanvasGroup>();
        CauserImg = transform.GetChild(0).GetComponent<Image>();
        ContentTxt = transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        TargetImg = transform.GetChild(2).GetComponent<Image>();

        if (CauserImg == null || ContentTxt == null || TargetImg == null)
            throw new System.Exception("Cannot Find Right UI Component");
    }

    public void SetPool(IObjectPool<BattleLog> pool)
    {
        this.pool = pool;
    }
    public void SetLifeTime(float time)
    {
        lifeTime = time;
    }
    public void SetAnchorMoveOffset(float val)
    {
        ancOffset = val;
    }
    public void ConstructWithInfo(Tuple<Character, float, Character> log)
    {
        if(UIGroup == null)
            UIGroup = GetComponent<CanvasGroup>();
        UIGroup.alpha = 1;

        

        RectTransform logRectT = gameObject.GetComponent<RectTransform>();
        logRectT.anchorMin = new Vector2(0, 0);
        logRectT.anchorMax = new Vector2(1, 0.2f);
        float width = logRectT.rect.width;
        logRectT.offsetMin = new Vector2(width, 0); //left, btm
        logRectT.offsetMax = new Vector2(width, 0); //-right,-top

        LeanTween.value(this.gameObject, width, 0, 0.5f).setOnUpdate((float val) =>
        {
            logRectT.offsetMin = new Vector2(val, 0); //left, btm
            logRectT.offsetMax = new Vector2(val, 0); //-right,-top
        }).setEaseInOutCirc();
    }

    public void AnchorMoveTo(Vector2 anc)
    {
        RectTransform logRectT = gameObject.GetComponent<RectTransform>();

        LeanTween.value(this.gameObject, 0, anc.x, 0.5f).setOnUpdate((float val) =>
        {
            Debug.Log(gameObject.name + " Moving To AncMin.y : " + val);
            logRectT.anchorMin = new Vector2(0, val);
        }).setEaseInOutCirc().setDelay(0.5f);
        LeanTween.value(this.gameObject, 0.2f, anc.y, 0.5f).setOnUpdate((float val) =>
        {
            Debug.Log(gameObject.name + " Moving To AncMax.y : " + val);
            logRectT.anchorMax = new Vector2(1, val); 
        }).setEaseInOutCirc().setDelay(0.5f).setOnComplete(()=>
        {
            Debug.Log(gameObject.name + " AncMax.y Move Finished in" + logRectT.anchorMax.y);
            if (logRectT.anchorMax.y > 0.9f)
            {
                ReserveDelete(lifeTime);
            }
        });
    }
    public void AnchorMoveUp()
    {
        RectTransform logRectT = gameObject.GetComponent<RectTransform>();

        LeanTween.value(this.gameObject, logRectT.anchorMin.y, logRectT.anchorMin.y + ancOffset, 0.5f).setOnUpdate((float val) =>
        {
            logRectT.anchorMin = new Vector2(0, val);
        }).setEaseInOutCirc().setDelay(0.5f);
        LeanTween.value(this.gameObject, logRectT.anchorMax.y, logRectT.anchorMax.y + ancOffset, 0.5f).setOnUpdate((float val) =>
        {
            logRectT.anchorMax = new Vector2(1, val);
        }).setEaseInOutCirc().setDelay(0.5f).setOnComplete(() =>
        {
            if (logRectT.anchorMax.y >= 0.9f)
            {
                ReserveDelete(lifeTime);
            }
        });
    }

    private void ReserveDelete(float time)
    {
        LeanTween.value(this.gameObject, 1f, 0f, time).setOnUpdate((float val) =>
        {
            UIGroup.alpha = val;
        }).setOnComplete(()=>
        {
            pool.Release(this);
        });
    }
}
