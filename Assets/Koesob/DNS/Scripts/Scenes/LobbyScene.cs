using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNS
{
    public class LobbyScene : BaseScene
    {
        protected override void Init()
        {
            base.Init();

            SceneType = Define.Scene.Lobby;
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }
    }
}

