using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNS
{
    public class TownScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Town;
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}

