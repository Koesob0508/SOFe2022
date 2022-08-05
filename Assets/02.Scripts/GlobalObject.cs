using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    Hero,
    Enemy,
    Item
}
public class GlobalObject
{
    public uint GUID;
    public string Name;
    public ObjectType Type;
}
