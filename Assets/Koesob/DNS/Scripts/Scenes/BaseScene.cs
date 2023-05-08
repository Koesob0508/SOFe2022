using UnityEngine;
using UnityEngine.EventSystems;

namespace DNS
{
    public abstract class BaseScene : MonoBehaviour
    {
        Define.Scene _sceneType = Define.Scene.Unknown;

        public Define.Scene SceneType { get; protected set; }

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            Object obj = FindObjectOfType(typeof(EventSystem));
            if (obj == null)
            {
                Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
            }
        }

        public abstract void Clear();
    }

}
