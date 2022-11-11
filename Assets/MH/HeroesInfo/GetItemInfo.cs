using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GetItemInfo : MonoBehaviour
{
    public Item item;

    public void SetInfo(Item _item)
    {
        item = _item;
    }
    public uint GetUIItemGuid()
    {
        return item.GUID;
    }
    public uint GetItemOwnerGuid()
    {
        return item.OwnHeroGUID;
    }
    public uint GetItemOrder()
    {
        return item.InventoryOrder;
    }
}


