using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DNS
{
    public class Managers : MonoBehaviour
    {
        static Managers s_instance;
        public static Managers Instance { get { Init(); return s_instance; } }

        ResourceManager _resource = new ResourceManager();
        SceneManagerEx _scene = new SceneManagerEx();

        public static ResourceManager Resource { get { return Instance._resource; } }
        public static SceneManagerEx Scene { get { return Instance._scene; } }
        
        void Start()
        {
            Init();
        }

        void Update()
        {

        }

        static void Init()
        {
            if (s_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");

                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();
            }
        }

        public static void Clear()
        {
            Scene.Clear();
        }
    }
}