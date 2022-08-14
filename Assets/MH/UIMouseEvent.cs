using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMouseEvent : MonoBehaviour
{
    public GameObject HeroInfoUI;
    private GameObject UIItemInfo;

    public void ButtonClicked()
    {
        if (HeroInfoUI.activeInHierarchy == true)
        {
            HeroInfoUI.SetActive(false);
        }
        else
        {
            HeroInfoUI.SetActive(true);
        }
    }

    public void OnMouseEnter()
    {
        uint heroGUID;
        Debug.Log(this.gameObject.name);
        UIItemInfo = Resources.Load<GameObject>("Canvas");
        Instantiate(UIItemInfo, Input.mousePosition, Quaternion.identity);
        
        //GameObject HeroUIContent = Hero.transform.parent.gameObject;
        //heroGUID = HeroUIContent.GetComponent<GetHeroInfo>().GetHeroUIOrder(Hero);

        UIItemInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("아이템 정보");
        

        Debug.Log("Mouse Enter");

    }

    public void OnClick()
    {

    }

    public void OnMouseExit()
    {
        Debug.Log("Mouse Exit");
        Destroy(UIItemInfo);
    }
}
