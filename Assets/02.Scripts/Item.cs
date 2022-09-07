using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : GlobalObject
{
    public uint HeroGUID; // 해당 아이템을 소유한 용병
    public int InventoryOrder; // 해당 아이템의 인벤토리에서의 위치 0-2
}
