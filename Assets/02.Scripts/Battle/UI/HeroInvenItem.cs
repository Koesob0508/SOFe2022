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
    public void SetHeroObj(GameObject HeroObj)
    {
        HeroObject = HeroObj;
        HeroObject.GetComponent<Units>().SetItemUI(this);
    }

    public void ReturnToInven()
    {
        HeroObject.SetActive(false);
        GameManager.Battle.DeleteHeroOnBattle(HeroObject);
        HeroImage.sprite = GameManager.Instance.LoadSprite(HeroObject.GetComponent<Units>().charData.GUID);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        HeroImage.sprite = null;
        HeroObject.SetActive(true);
        Vector3 WorldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        WorldPos.z = 0;
        HeroObject.transform.position = WorldPos;

        parentPanel.StartDragging();
    }

    public void OnDrag(PointerEventData eventData)
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

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.Battle.SetHeroOnBattle(HeroObject);
        parentPanel.EndDragging();
        GameManager.Battle.SetHeroOnBattle(HeroObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isPopUpOpened = !isPopUpOpened;
        parentPanel.CloseOtherPopUp(this);
        infoPopUp.gameObject.SetActive(isPopUpOpened);
    }
    public void ClosePopUp()
    {
        isPopUpOpened = false;
        infoPopUp.gameObject.SetActive(isPopUpOpened);
    }
    void Start()
    {
        parentPanel = GetComponentInParent<HeroInvenPanel>();
        HeroImage = GetComponent<Image>();
        infoPopUp = parentPanel.GetComponentInChildren<HeroInfo_PopUp>();
        infoPopUp.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
