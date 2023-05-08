using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNS
{
    public class ResourceManager
    {
        public T Load<T>(string path) where T : Object
        {
            if (typeof(T) == typeof(GameObject))
            {
                string name = path;
                int index = name.LastIndexOf('/');
                if(index >= 0)
                {
                    name = name.Substring(index + 1);
                }
            }

            return Resources.Load<T>(path);
        }

        public GameObject Instantiate(string path, Transform parent = null)
        {
            GameObject original = Load<GameObject>($"DNS/Prefabs/{path}");

            if(original == null)
            {
                Debug.LogWarning($"Failed to load preefab : {path}");
                return null;
            }

            GameObject go = Object.Instantiate(original, parent);

            int index = go.name.IndexOf("(Clone)");
            if(index > 0)
            {
                go.name = go.name.Substring(0, index);
            }

            return go;
        }
    }
}
