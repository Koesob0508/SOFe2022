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
    }
}