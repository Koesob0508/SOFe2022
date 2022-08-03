using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    public Inventory _Inventory;
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel = transform as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            
            IInventoryHero hero = eventData.pointerDrag.gameObject.GetComponent<ItemDragHandler>().Hero;
            // Debug.Log(hero.Name);

            if (hero != null)
            { 
                hero.OnDrop();
                if (gameObject.activeSelf == true)
                {
                    _Inventory.RemoveItem(hero);
                    // Debug.Log("Drop Item");
                }
            }
        }
    }
}