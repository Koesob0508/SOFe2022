using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNS
{
    public class MapScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Map;
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}

