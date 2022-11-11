using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HeroInvenItem : MonoBehaviour, IPointerClickHandler ,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image HeroImage;
    GameObject HeroObject;

    HeroInvenPanel parentPanel;
    HeroInfo_PopUp infoPopUp;

    Vector3 HalfPos;

    bool isPopUpOpened = false;
    bool isHeroInInven = true;
    bool isDead = true;

    /// <summary>
    /// Info Update 발생 시 호출
    /// </summary>
    public void UpdateInfo()
    {
        infoPopUp.UpdateInfo();
    }
    /// <summary>
    /// Initalize Function for PopupData
    /// </summary>
    /// <param name="hero"></param>
    private void SetPopUpData(Hero hero)
    {
        infoPopUp.SetUpData(hero);
    }
    private void SetHeroObj(GameObject HeroObj)
    {
        HeroObject = HeroObj;
        //HeroObject.GetComponent<Units>().SetItemUI(this);
    }
    public bool GetIsHeroInInven()
    {
        return isHeroInInven;
    }
    public void ReturnToInven(GameObject Hero)
    {
        isHeroInInven = true;
        HeroImage.gameObject.SetActive(true);
        Hero.SetActive(false);
        GameManager.Battle.DeleteHeroOnBattle(Hero);
        HeroImage.sprite = GameManager.Data.LoadSprite(Hero.GetComponent<Units>().charData.GUID);
        HeroObject = Hero;
        SetPopUpData((Hero)HeroObject.GetComponent<Units>().charData);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!isDead)
        {
            if(isPopUpOpened)
            {
                isPopUpOpened = !isPopUpOpened;
                infoPopUp.gameObject.SetActive(isPopUpOpened);
            }
            isHeroInInven = false;
            HeroImage.gameObject.SetActive(false);
            HeroObject.SetActive(true);
            Vector3 WorldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            WorldPos.z = 0;
            HeroObject.transform.position = WorldPos;

            parentPanel.StartDragging();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isDead)
        {
            Vector2 screenPos = eventData.position;
            if(screenPos.x > Screen.width / 2)
            {
                screenPos.x = Screen.width / 2;
            }
            Vector3 WorldPos = Camera.main.ScreenToWorldPoint(screenPos);
            WorldPos.z = 0;
            HeroObject.transform.position = WorldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!isDead)
        {
            parentPanel.EndDragging();
            GameManager.Battle.SetHeroOnBattle(HeroObject);
            int layerMask = ~(1 << LayerMask.NameToLayer("Units"));  // Unit 레이어만 충돌 체크함
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero, Mathf.Infinity, layerMask);
            Vector2 screenPos = eventData.position;

            if (hit.collider != null)
            {
                HeroInvenItem item = hit.collider.gameObject.GetComponent<HeroInvenItem>();
                if (item == this)
                {
                    ReturnToInven(HeroObject);
                }
                else
                {
                    if (screenPos.x > Screen.width / 2)
                    {
                        screenPos.x = Screen.width / 2;
                    }
                    if (screenPos.x < Screen.width * 0.1)
                    {
                        screenPos.x = Screen.width * 0.1f;
                    }
                    if (screenPos.y > Screen.height / 2)
                    {
                        screenPos.y = Screen.height / 2;
                    }
                    if (screenPos.y < 0)
                    {
                        screenPos.y = 0;
                    }
                }
            }
            else
            {
                if (screenPos.x > Screen.width / 2)
                {
                    screenPos.x = Screen.width / 2;
                }
                if (screenPos.x < Screen.width * 0.1)
                {
                    screenPos.x = Screen.width * 0.1f;
                }
                if (screenPos.y > Screen.height / 2)
                {
                    screenPos.y = Screen.height / 2;
                }
                if (screenPos.y < 0)
                {
                    screenPos.y = 0;
                }
            }
            Vector3 WorldPos = Camera.main.ScreenToWorldPoint(screenPos);
            WorldPos.z = 0;
            HeroObject.transform.position = WorldPos;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(isHeroInInven)
        {
            isPopUpOpened = !isPopUpOpened;
            parentPanel.CloseOtherPopUp(this);
            infoPopUp.gameObject.SetActive(isPopUpOpened);
        }
    }
    public void ClosePopUp()
    {
        isPopUpOpened = false;
        infoPopUp.gameObject.SetActive(isPopUpOpened);
    }

    public void Initalize(Hero hero)
    {
        parentPanel = GetComponentInParent<HeroInvenPanel>();
        HeroImage = GetComponentsInChildren<Image>()[1];
        infoPopUp = parentPanel.GetComponentInChildren<HeroInfo_PopUp>();
        infoPopUp.gameObject.SetActive(false);

        isDead = hero.isDead;

        if (isDead)
            HeroImage.color = new Color(1, 1, 1, 0.5f);

        SetPopUpData(hero);
        SetHeroObj(GameManager.Battle.CreateHero(hero));
    } 
}
