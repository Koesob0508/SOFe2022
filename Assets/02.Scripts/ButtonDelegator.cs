using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koesob
{
    public class ButtonDelegator : MonoBehaviour
    {
        public void ToInit()
        {
            GameManager.Scene.ToInitGameScene();
        }

        public void ToStageSelect()
        {
            GameManager.Scene.ToStageSelectScene();
        }

        public void ToBattle()
        {
            GameManager.Scene.ToBattleScene();
        }

        public void ToTown()
        {
            Debug.Log("Assets/01.Scenes Town으로 갑니다");
            GameManager.Scene.ToTownScene();
        }

        public void ToEvent()
        {
            GameManager.Scene.ToEventScene();
        }
        public void ToItem()
        {
            // Stage Select Scene에서 Item 합성 가능한 창을 띄움
            GameObject ItemUI = GameObject.Find("ItemSynthesis(Clone)");

            if (ItemUI != null)
            {
                Destroy(ItemUI, 1f);
            }
            else
            {
                ItemUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/ItemSynthesis"));
            }
        }

        public void ShowHeroInfo()
        {
            GameObject AllHeroInfoUI = GameObject.Find("AllHeroUI(Clone)");

            if (AllHeroInfoUI != null)
            {
                AllHeroInfoUI.transform.GetChild(0).GetComponent<HeroUItween>().SetRemove();
                Destroy(AllHeroInfoUI, 1f);
            }
            else
            {
                AllHeroInfoUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/AllHeroUI"));
                //AllHeroInfoUI.transform.SetParent(GameObject.Find("Main Canvas").transform);
            }
        }

        // Town scene에서 Guild창
        public void ShowHeroShop()
        {
            GameObject Swipe = GameObject.Find("MouseSwipe");
            GameObject HeroShop = GameObject.Find("GuildUI(Clone)");

            if (HeroShop != null)
            {
                Destroy(HeroShop);
                Swipe.GetComponent<Swipe>().DoSwipe = true;
            }
            else
            {
                HeroShop = Resources.Load<GameObject>("Prefabs/UI/GuildUI");
                Instantiate(HeroShop);
                Swipe.GetComponent<Swipe>().DoSwipe = false;
            }
        }

        // Town scene에서 Hotel창
        public void ShowHotel()
        {
            GameObject Hotel = GameObject.Find("HotelUI(Clone)");
            GameObject Swipe = GameObject.Find("MouseSwipe");
            if (Hotel != null)
            {
                Destroy(Hotel);
                Swipe.GetComponent<Swipe>().DoSwipe = true;
            }
            else
            {
                Hotel = Resources.Load<GameObject>("Prefabs/UI/HotelUI");
                Instantiate(Hotel);
                Swipe.GetComponent<Swipe>().DoSwipe = false;
            }
        }

        // Town scene에서 Shop창
        public void ShowItemShop()
        {
            GameObject ItemShop = GameObject.Find("ItemShopUI(Clone)");
            GameObject Swipe = GameObject.Find("MouseSwipe");
            if (ItemShop != null)
            {
                Destroy(ItemShop);
                Swipe.GetComponent<Swipe>().DoSwipe = true;
            }
            else
            {
                ItemShop = Resources.Load<GameObject>("Prefabs/UI/ItemShopUI");
                Instantiate(ItemShop);
                Swipe.GetComponent<Swipe>().DoSwipe = false;
            }
        }


        public void StageCompleted()
        {
            GameManager.Stage.CompleteStage();
            GameManager.Data.Save();
            GameManager.Scene.ToStageSelectScene();
        }

        public void NewGame()
        {
            GameManager.Instance.NewGame();
        }
    }
}