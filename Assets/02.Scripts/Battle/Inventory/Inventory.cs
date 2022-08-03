using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private const int SLOTS = 17;

    public List<IInventoryHero> mHeros = new List<IInventoryHero>();

    // 이벤트 정의
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public void AddItem(IInventoryHero hero)
    {
        // Debug.Log(hero.Name + " AddItem 중 - Inventory");

        if (mHeros.Count < SLOTS)
        {
            mHeros.Add(hero);
            hero.OnPickup();

            // 이벤트 핸들러 호출
            if (this.ItemAdded != null)
            {
                // Debug.Log(hero.Name + " ItemAdded 중 - Inventory");
                // 실행 메서드 : 이벤트 발생
                ItemAdded(this, new InventoryEventArgs(hero));
            }
        }
    }

    public void RemoveItem(IInventoryHero hero)
    {
        if (mHeros.Contains(hero))
        {
            mHeros.Remove(hero);
            //hero.OnDrop();
            Collider collider = (hero as MonoBehaviour).GetComponent<Collider>();

            if (collider != null)
            {
                collider.enabled = true;
            }

            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(hero));
            }
        }
    }
}
