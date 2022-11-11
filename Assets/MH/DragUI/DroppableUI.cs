using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
	private Image image;
	private RectTransform rect;

	private void Awake()
	{
		image = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// 아이템 슬롯의 색상을 노란색으로 변경
		image.color = Color.yellow;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		// 아이템 슬롯의 색상을 하얀색으로 변경
		image.color = Color.white;
	}

	public void OnDrop(PointerEventData eventData)
	{
		// pointerDrag는 현재 드래그하고 있는 대상(=아이템)
		if (eventData.pointerDrag != null)
		{
			// 하나의 Slot에는 하나의 Item만이 속한다 
			if (transform.childCount > 0)
			{
				return;
			}

			GameObject NewOwner = transform.parent.parent.gameObject;
			GameObject Contents = transform.parent.parent.parent.gameObject;

			// 드래그한 아이템을 소유하던 Hero와, 소유할 Hero의 Item list를 Update
			uint InventoryOrder = 0;
			if (transform.name == "Inventory(1)")
			{
				InventoryOrder = 0;
			}
			else if (transform.name == "Inventory(2)")
			{
				InventoryOrder = 1;
			}
			else if (transform.name == "Inventory(3)")
			{
				InventoryOrder = 2;
			}

			GameManager.Hero.RemoveHeroItem(
				eventData.pointerDrag.GetComponent<GetItemInfo>().GetItemOwnerGuid(),
				eventData.pointerDrag.GetComponent<GetItemInfo>().GetUIItemGuid(),
				eventData.pointerDrag.GetComponent<GetItemInfo>().GetItemOrder());

			GameManager.Hero.AddHeroItem(
				Contents.GetComponent<GetHeroInfo>().GetHeroUIOrder(NewOwner),
				eventData.pointerDrag.GetComponent<GetItemInfo>().GetUIItemGuid(),
				InventoryOrder);

			// 드래그하고 있는 대상의 부모를 현재 오브젝트로 설정하고, 위치를 현재 오브젝트 위치와 동일하게 설정
			eventData.pointerDrag.transform.SetParent(transform);
			eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

			// 소유하고 있는 ItemInfo Update
			Contents.GetComponent<GetHeroInfo>().UpdateItems();
		}
	}
}

