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

    private void SetPopUpData(Hero hero)
    {
        infoPopUp.SetUpData(hero);
    }
    private void SetHeroObj(GameObject HeroObj)
    {
        HeroObject = HeroObj;
        HeroObject.GetComponent<Units>().SetItemUI(this);
    }

    public void ReturnToInven()
    {
        isHeroInInven = true;
        HeroObject.SetActive(false);
        GameManager.Battle.DeleteHeroOnBattle(HeroObject);
        HeroImage.sprite = GameManager.Data.LoadSprite(HeroObject.GetComponent<Units>().charData.GUID);
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
            HeroImage.sprite = null;
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

            if (hit.collider != null)
            {
                HeroInvenItem item = hit.collider.gameObject.GetComponent<HeroInvenItem>();
                if (item == this)
                {
                    Debug.Log("here2");

                    ReturnToInven();
                }
            }
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
        HeroImage = GetComponent<Image>();
        infoPopUp = parentPanel.GetComponentInChildren<HeroInfo_PopUp>();
        infoPopUp.gameObject.SetActive(false);

        isDead = hero.isDead;

        if (isDead)
            HeroImage.color = new Color(1, 1, 1, 0.5f);

        SetPopUpData(hero);
        SetHeroObj(GameManager.Battle.CreateHero(hero));
    } 
}
