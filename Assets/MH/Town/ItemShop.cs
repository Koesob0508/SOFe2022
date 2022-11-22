using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemShop : MonoBehaviour
{
    public GameObject Items, ItemInfo, HeroUI, TargetHero, SetHeroButton;

    public Dictionary<uint, GameObject> ObjectUIList = new Dictionary<uint, GameObject>();
    private Item buy;
    private GameObject EnrollUI;
    private uint BuyHeroGUID;
    private Hero BuyHero;
    private bool ReadyBuy = false;

    private void Start()
    {
        Items.SetActive(true);
        ItemInfo.SetActive(false);
        TargetHero.SetActive(false);
    }

    public void ClickItem()
    {
        // 방금 클릭한 Item
        GameObject ClickObject = EventSystem.current.currentSelectedGameObject;
        // Item 정보
        buy = (Item)GameManager.Data.LoadObject(ClickObject.transform.GetChild(0).GetComponent<GetItemInfo>().item.GUID, GameManager.ObjectType.Item);
        buy.Cost = ClickObject.transform.GetChild(0).GetComponent<GetItemInfo>().item.Cost;
        ItemInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(buy.Name + "\n" + buy.Info);
        ItemInfo.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(buy.Cost + "G");
        ItemInfo.SetActive(true);
    }

    public void SetTargetHero()
    {
        // 인벤토리가 비어있어 구매한 아이템을 저장할 수 있는 Hero들
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("구매한 아이템을 줄 용병을 선택하세요");
        foreach (Hero hero in GameManager.Hero.GetHeroList())
        {
            if (hero.ItemNum < 3)
            {
                EnrollUI = Instantiate(HeroUI, transform.position, Quaternion.identity);
                EnrollUI.transform.SetParent(TargetHero.transform.GetChild(0).GetChild(0));
                EnrollUI.GetComponent<Image>().sprite = GameManager.Data.LoadSprite(hero.GUID);
                ObjectUIList.Add(hero.GUID, EnrollUI);
            }
        }

        ReadyBuy = true;
    }

    public void TargetHeroButton()
    {
        ReadyBuy = false;
        GameManager.Hero.AddHeroItem(BuyHero.GUID, buy.GUID, BuyHero.ItemNum);
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(BuyHero.Name + "에게\n" + buy.Name + " 을 저장하였습니다");
    }

    public uint GetHeroInUI(GameObject _hero)
    {
        uint guid = ObjectUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }

    public void DecideToBuy()
    {
        if (buy != null && GameManager.Data.Money > buy.Cost)
        {
            GameManager.Data.Money -= buy.Cost;
            GameManager.Hero.ShopItemList.Remove(buy);

            Items.SetActive(false);
            ItemInfo.SetActive(false);
            TargetHero.SetActive(true);

            SetTargetHero();
        }
        else
        {
            LeanTween.color(ItemInfo, new Color(255 / 255, 140 / 255, 140 / 255), 0.5f);
            LeanTween.moveLocalX(ItemInfo, 10f, .1f).setEase(LeanTweenType.easeOutBounce);
            LeanTween.moveLocalX(ItemInfo, -20f, .2f).setDelay(0.1f).setEase(LeanTweenType.easeInQuart);
            LeanTween.moveLocalX(ItemInfo, 10f, .1f).setDelay(0.3f).setEase(LeanTweenType.easeOutBounce);
            LeanTween.color(ItemInfo, new Color(1, 1, 1), 0.5f);
        }

    }

    private void Update()
    {
        if (ReadyBuy)
        {
            GameObject ClickObject = EventSystem.current.currentSelectedGameObject;
            if (ClickObject != null)
            {
                BuyHeroGUID = GetHeroInUI(ClickObject);
                foreach (Hero _hero in GameManager.Hero.GetHeroList())
                {
                    if (_hero.GUID == BuyHeroGUID && _hero.ItemNum < 3)
                    {
                        BuyHero = _hero;
                        break;
                    }
                }
                if (BuyHero != null)
                    TargetHero.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(BuyHero.Name);
            }
        }
    }
}

