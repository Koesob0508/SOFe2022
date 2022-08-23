using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MouseEventUI : MonoBehaviour
{

    private GameObject UIItemInfo;

    public void LongMouseClick()
    {
        uint heroGUID;
        
        UIItemInfo = Resources.Load<GameObject>("ItemInfoUI");
        Vector3 UIPosition =  new Vector3(Input.mousePosition.x + 90, Input.mousePosition.y, Input.mousePosition.z);
        UIItemInfo = Instantiate(UIItemInfo, UIPosition, Quaternion.identity);
        UIItemInfo.transform.parent = this.transform;

        GameObject Hero = this.transform.parent.parent.parent.parent.gameObject;
        GameObject HeroUIContent = this.transform.parent.parent.parent.parent.parent.gameObject;

        heroGUID = HeroUIContent.GetComponent<GetHeroInfo>().GetHeroUIOrder(Hero);
        // heroGUID = HeroUIContent.GetComponent<Hero>().GUID;
        Debug.Log(UIItemInfo.transform.GetChild(0).GetChild(0).gameObject.name);
        UIItemInfo.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Hero.GetHero(heroGUID).Name);

    }

    public void OnButtonClick()
    {
        uint heroGUID;

        GameObject Hero = this.transform.parent.gameObject;
        GameObject HeroUIContent = this.transform.parent.parent.gameObject;

        heroGUID = HeroUIContent.GetComponent<SetRandomObject>().GetHeroUIOrder(Hero);

        GameManager.Hero.EnrollHero(heroGUID);
    }

    public void LongMouseClickExit()
    {
        Destroy(UIItemInfo);
    }
}
