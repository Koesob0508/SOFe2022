using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Path
{
    public class Node : System.Object
    {

        public readonly Vector2 pos;
        private bool bIsObstacle = false;
        public readonly int index = 0;
        public float G = 0;
        public Node Parent;


        public Node(float x, float y, int idx = 0)
        {
            pos = new Vector2(x, y);
            index = idx;
        }
        public override bool Equals(System.Object obj)
        {
            Node n = obj as Node;
            if (n == null)
                return false;
            return (pos.x == n.pos.x) && (pos.y == n.pos.y);
        }
        public void SetObstacle()
        {
            bIsObstacle = true;
        }

        public bool GetObstacle()
        {
            return bIsObstacle;
        }

        public Vector2 GetPosition()
        {
            return pos;
        }

    }

}
