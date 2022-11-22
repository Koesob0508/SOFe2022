using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergyPanel : MonoBehaviour
{
    Dictionary<Observer_Battle.SynergyEvent,GameObject> childDict = new Dictionary<Observer_Battle.SynergyEvent,GameObject>();
    public GameObject synergyTextPrefab;
    public Transform synergyPanel;
    public RectTransform uiRectToAnimate;
    public Button btn;
    //4개이상 에러
    public void AddSynergy(Observer_Battle.SynergyEvent e)
    {
        GameObject g = Instantiate(synergyTextPrefab, synergyPanel);
        g.GetComponent<RectTransform>().anchorMax = new Vector2(1,1 - (synergyPanel.transform.childCount-1) * 0.25f);
        g.GetComponent<RectTransform>().anchorMin = new Vector2(0,1 - (synergyPanel.transform.childCount) * 0.25f);
        var titleComp = g.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0];
        var descComp = g.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];
        switch (e)
        {
            case Observer_Battle.SynergyEvent.SendMeHome:
                titleComp.text = "집에 보내 줘";
                descComp.text = "(I???의 용병의 체력이 10% 감소합니다)";
                break;
            case Observer_Battle.SynergyEvent.SilentScream:
                titleComp.text = "소리 없는 아우성";
                descComp.text = "(E???의 용병의 체력이 10% 감소합니다)";

                break;
            case Observer_Battle.SynergyEvent.TillPlanZ:
                titleComp.text = "플랜 Z 까지";
                descComp.text = "(???P 용병의 체력이 10% 감소합니다)";

                break;
            case Observer_Battle.SynergyEvent.NoPlan:
                titleComp.text = "무계획도 계획이다";
                descComp.text = "(???J 용병 체력이 10% 감소합니다)";
                break;
        }
        childDict.Add(e, g);
    }
    public void RemoveSynergy(Observer_Battle.SynergyEvent e)
    {
        Destroy(childDict[e]);
        childDict.Remove(e);
    }

    public void AnimateUp()
    {
        btn.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90);
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(AnimateDown);

        float animateVal = uiRectToAnimate.rect.height;

        LeanTween.value(this.gameObject, 0f, animateVal, 1f).setOnUpdate((float val) =>
        {
            uiRectToAnimate.offsetMax = new Vector2(0, val);
            uiRectToAnimate.offsetMin = new Vector2(0, val);
        }).setEaseInOutCirc();
    }
    public void AnimateDown()
    {
        btn.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -90);
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(AnimateUp);

        float animateVal = uiRectToAnimate.rect.height;

        LeanTween.value(this.gameObject, animateVal, 0f, 1f).setOnUpdate((float val) =>
        {
            uiRectToAnimate.offsetMax = new Vector2(0, val);
            uiRectToAnimate.offsetMin = new Vector2(0, val);
        }).setEaseInOutCirc();
    }
}
