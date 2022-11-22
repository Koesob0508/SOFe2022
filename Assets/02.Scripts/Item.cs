using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : GlobalObject
{
    public uint OwnHeroGUID; // �ش� �������� ������ �뺴, �⺻���� ���� �뺴 null
    public uint InventoryOrder = 0; // �ش� �������� �κ��丮������ ��ġ 0-2
    public uint Cost = 100;

    public uint Star;
    public GameManager.ItemType BasicType; // �⺻ ȿ�� ����
    public float BasicNum; // �⺻ ȿ�� ��ġ
    public float SpeicalNum; // Ư�� ȿ�� ��ġ
    public string Info; // ������ ����

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
