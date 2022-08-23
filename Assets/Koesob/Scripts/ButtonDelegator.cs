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
            GameObject HeroShop = null;
            GameObject[] AllObject = GameObject.FindGameObjectsWithTag("UI");

            foreach (GameObject obj in AllObject)
            {
                if (obj.name == "EnrollHeroUI(Clone)")
                {
                    HeroShop = obj;
                    break;
                }
            }

            if (HeroShop == null)
            {
                HeroShop = Resources.Load<GameObject>("EnrollHeroUI");
                Instantiate(HeroShop);
            }
            else
            {
                if (HeroShop.activeInHierarchy)
                {
                    HeroShop.SetActive(false);
                }
                else
                {
                    HeroShop.SetActive(true);
                }
            }
        }
    }
}