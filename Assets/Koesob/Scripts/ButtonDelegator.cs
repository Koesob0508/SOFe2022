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

        public void ShowHeroInfo()
        {
            GameObject AllHeroInfoUI = GameObject.Find("AllHeroUI(Clone)");

            if (AllHeroInfoUI != null)
            {
                Destroy(AllHeroInfoUI);
            }
            else
            {
                AllHeroInfoUI = Resources.Load<GameObject>("AllHeroUI");
                Instantiate(AllHeroInfoUI);
            }
        }

        public void ShowHeroShop()
        {
            GameObject HeroShop = GameObject.Find("GuildUI(Clone)");

            if (HeroShop != null)
            {
                Destroy(HeroShop);
            }
            else
            {
                HeroShop = Resources.Load<GameObject>("GuildUI");
                Instantiate(HeroShop);
            }

            //GameObject HeroShop = GameObject.Find("HeroShopUI");
            //GameObject UI = null;

            //if (HeroShop.transform.childCount == 0)
            //{
            //    UI = Instantiate(Resources.Load<GameObject>("GuildUI"));
            //    UI.transform.SetParent(HeroShop.transform);
            //}
            //else
            //{
            //    if (HeroShop.transform.GetChild(0).gameObject.activeSelf)
            //    {
            //        HeroShop.transform.GetChild(0).gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        HeroShop.transform.GetChild(0).gameObject.SetActive(true);
            //    }
            //}
        }
    }
}