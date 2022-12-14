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

    float lifeTime = 1;
    float ancOffset = 0;

    private IObjectPool<BattleLog> pool;
    public int CurNumber = 0;
    CanvasGroup UIGroup;

    bool moveUpReserved = false;
    bool isMoving = false;
    public void Setnum(int num)
    {
        CurNumber = num;
    }
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
    public void ConstructWithInfo(BattleLogPanel.Log log)
    {
        if(UIGroup == null)
            UIGroup = GetComponent<CanvasGroup>();
        UIGroup.alpha = 1;
        CauserImg = transform.GetChild(0).GetComponent<Image>();
        ContentTxt = transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        TargetImg = transform.GetChild(2).GetComponent<Image>();
        CauserImg.sprite = GameManager.Data.LoadSprite(log.Causer.GUID);
        TargetImg.sprite = GameManager.Data.LoadSprite(log.Target.GUID);
        string by="";
        string LastString="";
        switch (log.Type)
        {
            case BattleLogPanel.LogType.Kill:
                by = " ??(??) ";
                LastString = " óġ?Ͽ????ϴ?";
                break;
            case BattleLogPanel.LogType.Dead:
                by = " ???? ";
                LastString = " ?????Ͽ????ϴ?";
                break;
            case BattleLogPanel.LogType.Skill:
                by = " ???? ";
                LastString = " ??ų?? ?????߽??ϴ?";
                break;
            case BattleLogPanel.LogType.Buff:
                break;
            case BattleLogPanel.LogType.Event:
                Hero heroA = (Hero)log.Causer;
                Hero heroB = (Hero)log.Target;
                switch (GameManager.Relation.GetBetweenType(heroA, heroB))
                {
                    case RelationshipManager.Type.Fiance:
                        by = " ???? ";
                        LastString = " ?????? ???? ?ֽ??ϴ?";
                        break;
                    case RelationshipManager.Type.Some:
                        by = " ???? ";
                        LastString = " ???? Ÿ?? ?ֽ??ϴ?";
                        break;
                    case RelationshipManager.Type.Positive:
                        by = " ???? ";
                        LastString = " ȣ???? ???? ?ֽ??ϴ?";
                        break;
                    case RelationshipManager.Type.Normal:
                        by = " ??(??) ";
                        LastString = " ?˾ƺ??? ?ֽ??ϴ?";
                        break;
                    case RelationshipManager.Type.Negative:
                        by = " ??(??) ";
                        LastString = " ???????մϴ?";
                        break;
                    case RelationshipManager.Type.Disgust:
                        by = " ??(??) ";
                        LastString = " ?Ⱦ??մϴ?";
                        break;
                    case RelationshipManager.Type.Ignore:
                        by = " ??(??) ";
                        LastString = " ?????մϴ?";
                        break;
                }
                break;
            case BattleLogPanel.LogType.Positive:
                by = " ??(??) ???? ";
                LastString = " ȣ?????? ?ö????ϴ?";
                break;
            case BattleLogPanel.LogType.Negative:
                by = " ??(??) ???? ";
                LastString = " ȣ?????? ?????????ϴ?";
                break;
            default:
                Debug.LogWarning("?߸??? Battle Log");
                break;
        }
        ContentTxt.SetText(log.Causer.Name + " ??(??) " + log.Target.Name + by + LastString);

        RectTransform logRectT = gameObject.GetComponent<RectTransform>();
        logRectT.anchorMin = new Vector2(0, 0);
        logRectT.anchorMax = new Vector2(1, 0.2f);
        float width = logRectT.rect.width;
        logRectT.offsetMin = new Vector2(width/2, 0); //left, btm
        logRectT.offsetMax = new Vector2(width/2, 0); //-right,-top

        LeanTween.value(this.gameObject, width/2, 0, 0.5f).setOnUpdate((float val) =>
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
            isMoving = true;
            logRectT.anchorMin = new Vector2(0, val);
        }).setEaseInOutCirc().setDelay(0.5f);
        LeanTween.value(this.gameObject, 0.2f, anc.y, 0.5f).setOnUpdate((float val) =>
        {
            logRectT.anchorMax = new Vector2(1, val); 
        }).setEaseInOutCirc().setDelay(0.5f).setOnComplete(()=>
        {
            if (logRectT.anchorMax.y >= 0.9f)
            {
                ReserveDelete(lifeTime);
            }
            isMoving = false;
            if (moveUpReserved)
                AnchorMoveUp();
        });
    }
    public void AnchorMoveUp()
    {
        Debug.Log("AncherMoveUp : " + CurNumber.ToString() + " -> " + (CurNumber - 1).ToString());
        RectTransform logRectT = gameObject.GetComponent<RectTransform>();

        if (logRectT.anchorMax.y >= 0.95)
        {
            return;
        }
        if(!isMoving)
        {
            CurNumber--;

                LeanTween.value(this.gameObject, logRectT.anchorMin.y, logRectT.anchorMin.y + ancOffset, 0.5f).setOnUpdate((float val) =>
            {
                logRectT.anchorMin = new Vector2(0, val);
            }).setEaseInOutCirc();
            LeanTween.value(this.gameObject, logRectT.anchorMax.y, logRectT.anchorMax.y + ancOffset, 0.5f).setOnUpdate((float val) =>
            {
                logRectT.anchorMax = new Vector2(1, val);
            }).setEaseInOutCirc().setOnComplete(() =>
            {
                if (logRectT.anchorMax.y >= 0.9f)
                {
                    ReserveDelete(lifeTime);
                }
            });
        }
        else
        {
            moveUpReserved = true;
        }

    }

    private void ReserveDelete(float time)
    {
        LeanTween.value(this.gameObject, 1f, 0.1f, time).setOnUpdate((float val) =>
        {
            UIGroup.alpha = val;
        }).setOnComplete(()=>
        {
            pool.Release(this);
        });
    }
}
