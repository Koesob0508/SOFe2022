using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryHero
{
    string Name { get; }

    Sprite Image { get; }

    void OnPickup();

    void OnDrop();
}



public class InventoryEventArgs : EventArgs
{
    
    public InventoryEventArgs(IInventoryHero hero)
    {
        Hero = hero;
        // Debug.Log("현재 인벤토리에 저장된된 용병은 " + hero.Name);
    }

    public IInventoryHero Hero;
}
