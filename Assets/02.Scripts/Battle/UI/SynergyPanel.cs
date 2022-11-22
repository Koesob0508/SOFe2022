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
    //4���̻� ����
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
                titleComp.text = "���� ���� ��";
                descComp.text = "(I???�� �뺴�� ü���� 10% �����մϴ�)";
                break;
            case Observer_Battle.SynergyEvent.SilentScream:
                titleComp.text = "�Ҹ� ���� �ƿ켺";
                descComp.text = "(E???�� �뺴�� ü���� 10% �����մϴ�)";

                break;
            case Observer_Battle.SynergyEvent.TillPlanZ:
                titleComp.text = "�÷� Z ����";
                descComp.text = "(???P �뺴�� ü���� 10% �����մϴ�)";

                break;
            case Observer_Battle.SynergyEvent.NoPlan:
                titleComp.text = "����ȹ�� ��ȹ�̴�";
                descComp.text = "(???J �뺴 ü���� 10% �����մϴ�)";
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
