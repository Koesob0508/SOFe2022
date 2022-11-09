using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GetItemInfo : MonoBehaviour
{
    public uint GUID;
    public uint OwnerGUID; // 해당 아이템을 소유한 용병
    public int InventoryOrder; // 해당 아이템의 인벤토리에서의 위치 0-2

    public void SetItem(uint heroGuid, uint guid)
    {
        GUID = guid;
        OwnerGUID = heroGuid;

        if (transform.parent.name == "Inventory(1)")
        {
            InventoryOrder = 0;
        }
        else if (transform.parent.name == "Inventory(2)")
        {
            InventoryOrder = 1;
        }
        else if (transform.parent.name == "Inventory(2)")
        {
            InventoryOrder = 2;
        }
    }
    public uint GetUIItemGuid()
    {
        return GUID;
    }
    public uint GetItemOwnerGuid()
    {
        return OwnerGUID;
    }
    public void CleanSlot()
    {
        GUID = 0;
        //OwnerGUID=0;
        //InventoryOrder=0;
    }
}


