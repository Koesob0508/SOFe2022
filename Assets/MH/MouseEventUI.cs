using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseEventUI : MonoBehaviour
{
    private GameObject InfoUI, HeroMbtiUI;
    private bool CanBuy = true;

    // Item 정보를 띄움
    public void LongMouseClick()
    {
        uint heroGUID;
        
        InfoUI = Resources.Load<GameObject>("Prefabs/UI/ItemInfoUI");
        Vector3 UIPosition =  new Vector3(Input.mousePosition.x + 180, Input.mousePosition.y, Input.mousePosition.z);
        InfoUI = Instantiate(InfoUI, UIPosition, Quaternion.identity);
        InfoUI.transform.SetParent(this.transform);

        GameObject Hero = this.transform.parent.parent.parent.parent.gameObject;
        GameObject HeroUIContent = this.transform.parent.parent.parent.parent.parent.gameObject;

        heroGUID = HeroUIContent.GetComponent<GetHeroInfo>().GetHeroUIOrder(Hero);
        // heroGUID = HeroUIContent.GetComponent<Hero>().GUID;
        // Debug.Log(UIItemInfo.transform.GetChild(0).GetChild(0).gameObject.name);
        InfoUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Hero.GetHero(heroGUID).Name);

    }
    
    // 해당 Hero의 MBTI 영향을 확인한다
    public void LongMouseClickHero()
    {
        InfoUI = Resources.Load<GameObject>("Prefabs/UI/MemberMbti");
        Vector3 UIPosition = new Vector3(Input.mousePosition.x + 180, Input.mousePosition.y, Input.mousePosition.z);
        HeroMbtiUI = Instantiate(InfoUI, UIPosition, Quaternion.identity);
        HeroMbtiUI.transform.SetParent(transform.parent.parent.parent.parent);

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
                    if (GameManager.Data.Money < _hero.Cost)
                        return;

                    GameManager.Hero.ShopHeroList.Remove(_hero);
                    GameManager.Data.Money -= _hero.Cost;
                    break;
                }
            }
            Image button = this.transform.parent.GetComponent<Image>();
            button.color = new Color32(102, 102, 255, 225);
            this.transform.parent.GetComponent<Button>().interactable = false;
            this.transform.parent.parent.GetChild(3).GetComponent<Button>().interactable = false;

            LeanTween.scale(Hero.transform.GetChild(0).gameObject, new Vector3(1.2f, 1.2f, 1.2f), .3f).setDelay(0.2f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(Hero.transform.GetChild(1).gameObject, new Vector3(1.2f, 0.8f, 0.8f), .3f).setDelay(0.3f).setEase(LeanTweenType.easeOutCirc);

            Hero.transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().LeanAlpha(0.3f, 0.3f);
            LeanTween.scale(Hero.transform.GetChild(0).gameObject, new Vector3(1f, 1f, 1f), .3f).setDelay(0.4f).setEase(LeanTweenType.easeOutCirc);

            Hero.transform.GetChild(1).gameObject.GetComponent<CanvasGroup>().LeanAlpha(0.3f, 0.3f);
            LeanTween.scale(Hero.transform.GetChild(1).gameObject, new Vector3(1.47f, 1f, 1f), .3f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);

            // Hero 등록 애니메이션
            InfoUI = Resources.Load<GameObject>("Prefabs/UI/HeroImage");
            InfoUI = Instantiate(InfoUI, new Vector3(0, 0, -2), Quaternion.identity);
            InfoUI.GetComponent<SpriteRenderer>().sprite = GameManager.Data.LoadSprite(heroGUID);
            LeanTween.moveLocal(InfoUI, new Vector3(0f, -4f, -2f), 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(InfoUI, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);
            Destroy(InfoUI, 3f);

            CanBuy = false;

        }
    }

    // Hotel에서 Hero List 중 하나를 선택함
    public void OnSelectButton_Hotel()
    {
        GameObject Hero = this.transform.parent.gameObject;
        GameObject HeroUIContent = Hero.transform.parent.gameObject;

        uint heroGUID = HeroUIContent.GetComponent<Town_Hotel>().GetHeroUIOrder(Hero);

        foreach (Hero _hero in GameManager.Hero.HeroList)
        {
            if (_hero.GUID == heroGUID)
            {
                GameObject.Find("Content").GetComponent<Town_Hotel>().SetTarget(_hero);
                break;
            }
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
            Hero _hero = (Hero)GameManager.Data.LoadObject(heroGUID, GameManager.ObjectType.Hero);

            TextMeshProUGUI Text = this.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();
            Text.text = _hero.Cost.ToString() + "G";
        }
    }

    public void LongMouseClickExit()
    {
        Destroy(InfoUI);
    }
}
