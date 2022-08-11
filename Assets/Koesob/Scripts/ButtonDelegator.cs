using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Koesob
{
    public class ButtonDelegator : MonoBehaviour
    {
        public void ToInit()
        {
            GameManager.Instance.ToInitGameScene();
        }

        public void ToStageSelect()
        {
            GameManager.Instance.ToStageSelectScene();
        }

        public void ToBattle()
        {
            GameManager.Instance.ToBattleScene();
        }
    }
}