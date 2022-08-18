using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseEventItemInfo : MonoBehaviour
{

    private GameObject UIItemInfo;

    public void OnMouseEnter()
    {
        uint heroGUID;
        Debug.Log(this.gameObject.name);
        UIItemInfo = Resources.Load<GameObject>("Canvas");
        UIItemInfo = Instantiate(UIItemInfo, Input.mousePosition, Quaternion.identity);
        UIItemInfo.transform.parent = this.transform;
        GameObject Hero = this.transform.parent.parent.parent.parent.gameObject;

        // Debug.Log(this.transform.parent.parent.parent.parent.parent.gameObject.name);
        GameObject HeroUIContent = this.transform.parent.parent.parent.parent.parent.gameObject;
        heroGUID = HeroUIContent.GetComponent<GetHeroInfo>().GetHeroUIOrder(Hero);
        // heroGUID = HeroUIContent.GetComponent<Hero>().GUID;
        Debug.Log(UIItemInfo.transform.GetChild(0).GetChild(0).gameObject.name);
        UIItemInfo.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Hero.GetHero(heroGUID).Name);

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
