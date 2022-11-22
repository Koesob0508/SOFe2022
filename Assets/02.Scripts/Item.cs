using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : GlobalObject
{
    public uint OwnHeroGUID; // 해당 아이템을 소유한 용병, 기본값은 소유 용병 null
    public uint InventoryOrder = 0; // 해당 아이템의 인벤토리에서의 위치 0-2
    public uint Cost = 100;

    public uint Star;
    public GameManager.ItemType BasicType; // 기본 효과 유형
    public float BasicNum; // 기본 효과 수치
    public float SpeicalNum; // 특수 효과 수치
    public string Info; // 아이템 설명

    public object ShallowCopy()
    {
        return this.MemberwiseClone();
    }

    public Item DeepCopy()
    {
        Item tmp = new Item();

        tmp.OwnHeroGUID = OwnHeroGUID;
        tmp.InventoryOrder = InventoryOrder;
        tmp.Cost = Cost;
        tmp.Star = Star;
        tmp.BasicType = BasicType;
        tmp.BasicNum = BasicNum;
        tmp.SpeicalNum = SpeicalNum;
        tmp.Info = Info;
        tmp.GUID = GUID;
        tmp.Name = Name;
        tmp.Type = Type;
        return tmp;
    }
}
