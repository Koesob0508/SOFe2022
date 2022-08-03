using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public abstract class CompositeNode : Node
    {
       [HideInInspector]
        public List<Node> Childs = new List<Node>();
        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.Childs = Childs.ConvertAll(n => n.Clone());
            return node;
        }
    } 

}
