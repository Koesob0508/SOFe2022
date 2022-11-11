using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : GlobalObject
{
    public uint OwnHeroGUID; // �ش� �������� ������ �뺴, �⺻���� ���� �뺴 null
    public uint InventoryOrder = 0; // �ش� �������� �κ��丮������ ��ġ 0-2

    public uint Star;
    public GameManager.ItemType BasicType; // �⺻ ȿ�� ����
    public float BasicNum; // �⺻ ȿ�� ��ġ
    public float SpeicalNum; // Ư�� ȿ�� ��ġ
    public string Info; // ������ ����

}
