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
                by = " 을(를) ";
                LastString = " 처치하였습니다";
                break;
            case BattleLogPanel.LogType.Dead:
                by = " 에게 ";
                LastString = " 사망하였습니다";
                break;
            case BattleLogPanel.LogType.Skill:
                by = " 에게 ";
                LastString = " 스킬을 사용했습니다";
                break;
            case BattleLogPanel.LogType.Buff:
                break;
            case BattleLogPanel.LogType.Event:
                Hero heroA = (Hero)log.Causer;
                Hero heroB = (Hero)log.Target;
                switch (GameManager.Relation.GetBetweenType(heroA, heroB))
                {
                    case RelationshipManager.Type.Fiance:
                        by = " 에게 ";
                        LastString = " 우결을 찍고 있습니다";
                        break;
                    case RelationshipManager.Type.Some:
                        by = " 에게 ";
                        LastString = " 썸을 타고 있습니다";
                        break;
                    case RelationshipManager.Type.Positive:
                        by = " 에게 ";
                        LastString = " 호감을 갖고 있습니다";
                        break;
                    case RelationshipManager.Type.Normal:
                        by = " 을(를) ";
                        LastString = " 알아보고 있습니다";
                        break;
                    case RelationshipManager.Type.Negative:
                        by = " 을(를) ";
                        LastString = " 불편해합니다";
                        break;
                    case RelationshipManager.Type.Disgust:
                        by = " 을(를) ";
                        LastString = " 싫어합니다";
                        break;
                    case RelationshipManager.Type.Ignore:
                        by = " 을(를) ";
                        LastString = " 무시합니다";
                        break;
                }
                break;
            case BattleLogPanel.LogType.Positive:
                by = " 을(를) 향한 ";
                LastString = " 호감도가 올랐습니다";
                break;
            case BattleLogPanel.LogType.Negative:
                by = " 을(를) 향한 ";
                LastString = " 호감도가 떨어졌습니다";
                break;
            default:
                Debug.LogWarning("잘못된 Battle Log");
                break;
        }
        ContentTxt.SetText(log.Causer.Name + " 가(아) " + log.Target.Name + by + LastString);

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
