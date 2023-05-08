using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNS
{
    public class BattleScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Battle;
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}

