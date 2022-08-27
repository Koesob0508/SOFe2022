using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseEventUI : MonoBehaviour
{

    private GameObject UIItemInfo;
    private bool CanBuy = true;

    public void LongMouseClick()
    {
        uint heroGUID;
        
        UIItemInfo = Resources.Load<GameObject>("ItemInfoUI");
        Vector3 UIPosition =  new Vector3(Input.mousePosition.x + 180, Input.mousePosition.y, Input.mousePosition.z);
        UIItemInfo = Instantiate(UIItemInfo, UIPosition, Quaternion.identity);
        UIItemInfo.transform.parent = this.transform;

        GameObject Hero = this.transform.parent.parent.parent.parent.gameObject;
        GameObject HeroUIContent = this.transform.parent.parent.parent.parent.parent.gameObject;

        heroGUID = HeroUIContent.GetComponent<GetHeroInfo>().GetHeroUIOrder(Hero);
        // heroGUID = HeroUIContent.GetComponent<Hero>().GUID;
        Debug.Log(UIItemInfo.transform.GetChild(0).GetChild(0).gameObject.name);
        UIItemInfo.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Hero.GetHero(heroGUID).Name);

    }

    // Shop에서 Hero를 등록한다.
    public void OnButtonClick()
    {
        if (CanBuy)
        {
            GameObject Hero = this.transform.parent.parent.gameObject;
            GameObject HeroUIContent = Hero.transform.parent.gameObject;

            uint heroGUID = HeroUIContent.GetComponent<SetRandomObject>().GetHeroUIOrder(Hero);
            GameManager.Hero.EnrollHero(heroGUID);

            foreach (Hero _hero in GameManager.Hero.ShopHeroList)
            {
                if (_hero.GUID == heroGUID)
                {
                    GameManager.Hero.ShopHeroList.Remove(_hero);
                    break;
                }
            }
            Image button = this.transform.parent.GetComponent<Image>();
            button.color = new Color32(102, 102, 255, 225);
            this.transform.parent.GetComponent<Button>().interactable = false;

            CanBuy = false;

        }
    }

    public void OnMouseOver()
    {
        if (CanBuy)
        {
            TextMeshProUGUI Text = this.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();
            Text.text = "Recruit";
        }
    }
    public void OnMouseExit()
    {
        if (CanBuy)
        {
            GameObject Hero = this.transform.parent.parent.gameObject;
            GameObject HeroUIContent = Hero.transform.parent.gameObject;

            uint heroGUID = HeroUIContent.GetComponent<SetRandomObject>().GetHeroUIOrder(Hero);
            Hero _hero = (Hero)GameManager.Instance.LoadObject(heroGUID, GameManager.ObjectType.Hero);

            TextMeshProUGUI Text = this.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();
            Text.text = _hero.Cost.ToString() + "G";
        }
    }

    public void LongMouseClickExit()
    {
        Destroy(UIItemInfo);
    }
}
