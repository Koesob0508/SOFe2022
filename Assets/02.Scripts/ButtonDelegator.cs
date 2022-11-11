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
            GameManager.Scene.ToTownScene();
        }

        public void ToEvent()
        {
            GameManager.Scene.ToEventScene();
        }
        public void ToItem()
        {
            // Stage Select Scene���� Item �ռ� ������ â�� ���
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

        // Event scene���� Guildâ
        public void ShowHeroShop()
        {
            GameObject HeroShop = GameObject.Find("GuildUI(Clone)");

            if (HeroShop != null)
            {
                Destroy(HeroShop);
            }
            else
            {
                HeroShop = Resources.Load<GameObject>("Prefabs/UI/GuildUI");
                Instantiate(HeroShop);
            }
        }

        // Event scene���� Hotelâ
        public void ShowHotel()
        {
            GameObject Hotel = GameObject.Find("HotelUI(Clone)");

            if (Hotel != null)
            {
                Destroy(Hotel);
            }
            else
            {
                Hotel = Resources.Load<GameObject>("Prefabs/UI/HotelUI");
                Instantiate(Hotel);
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