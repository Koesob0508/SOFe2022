using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoItemSynthesis : MonoBehaviour
{
    public Item[] TargetItems = new Item[2];
    public uint TargetNum = 0;

    public GameObject ItemUIObject;
    public GameObject Result;
    public GameObject Target;

    private bool DoSynthesis = false;

    public void PushDoButton()
    {
        if (DoSynthesis)
        {
            ItemSynthesis();
        }
    }

    public void AddTargetItem(uint ItemGUID, uint order)
    {
        if (TargetNum < 2)
        {
            foreach (GlobalObject g in GameManager.Data.ObjectCodex.Values)
            {
                Item item = g as Item;

                if (item != null && item.GUID == ItemGUID)
                {
                    TargetItems[order] = item;
                    TargetNum += 1;
                }
            }
        }
    }

    public void RemoveTargetItem(uint ItemGUID, uint order)
    {

    }

    public Item ItemSynthesis()
    {
        Item NewItem = null;

        if (TargetItems.Length == 2 && TargetItems[0].GUID == TargetItems[0].GUID)
        {
            if (TargetItems[0].Star <= 4)
            {
                uint ItemGUID = TargetItems[0].GUID;
                foreach (GlobalObject g in GameManager.Data.ObjectCodex.Values)
                {
                    NewItem = g as Item;

                    if (NewItem != null && NewItem.GUID == ItemGUID + 1)
                    {
                        GameObject ItemUI = Instantiate(ItemUIObject, transform.position, Quaternion.identity);
                        ItemUI.transform.SetParent(Result.transform, false);
                        ItemUI.transform.localPosition = new Vector3(0, 0, 0);
                        //ItemUI.transform.localScale = new Vector3(30, 30, 30);

                        Image Item = ItemUI.GetComponent<Image>();
                        Item.sprite = GameManager.Data.LoadSprite(NewItem.GUID);
                        Item.GetComponent<GetItemInfo>().SetInfo(NewItem);

                        Debug.Log("아이템 합성");

                        // 합성 이후 재료 Item 삭제
                        DoSynthesis = false;
                        TargetItems = new Item[2];
                        for (int i = 0; i < 2; i++)
                        {
                            Destroy(Target.transform.GetChild(i).GetChild(0).gameObject);
                        }
                    }
                }
            }
        }

        return NewItem;
    }

    // Start is called before the first frame update
    void Start()
    {
        TargetItems = new Item[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetNum == 2)
        {
            DoSynthesis = true;
        }
    }
}
