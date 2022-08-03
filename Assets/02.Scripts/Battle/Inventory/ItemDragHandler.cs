using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public IInventoryHero Hero { get; set; }

    public void OnDrag(PointerEventData eventData)
    {

        // Hero는 중앙 선 기준 오른쪽에만 배치
        if (transform.position.x <= Screen.width / 2)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
    }

    
}
