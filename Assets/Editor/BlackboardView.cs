using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class BlackboardView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BlackboardView, UxmlTraits> { }

    Blackboard bBoard;
    VisualElement keycontainer;
    EnumField keykind;
    TextField keyName;
    Button AddButton;
    Action<string> delclicked;
    public BlackboardView()
    {
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BlackboardEditor.uss");
        styleSheets.Add(styleSheet);
        delclicked += DeleteElem;
    }

    private void DeleteElem(string keyname)
    {
        bBoard.DeleteKey(keyname);
        Populateboard(bBoard);
    }
    public void BindElement()
    {
        keycontainer = this.Query<VisualElement>("KeyContainer");
        keycontainer.Clear();
        keykind = this.Q<EnumField>();
        keyName = this.Q<TextField>();
        AddButton = this.Q<Button>();
        if(keycontainer == null || keykind == null || keyName == null || AddButton == null)
        {
            Debug.Log("bind cannot");
        }
        AddButton.clicked += OnAddButtonClicked;
    }
    private void OnAddButtonClicked()
    {
        BT_Key.KeyType type = (BT_Key.KeyType)keykind.value;
        if(bBoard.AddKeyValue(keyName.value.ToString(), type))
        {
            CreateKeyView(keyName.value.ToString(), type);
        }
    }
    public void Populateboard(Blackboard board)
    {
        bBoard = board;
        BindElement();
        foreach(var key in bBoard.bb_keys)
        {
            BT_Key.KeyType obj = key.Type;
            if (obj == BT_Key.KeyType.E_bool)
            {
                CreateKeyView(key.Name, BT_Key.KeyType.E_bool);
            }
            else if (obj == BT_Key.KeyType.E_gameobject)
            {
                CreateKeyView(key.Name, BT_Key.KeyType.E_gameobject);
            }
            else if (obj == BT_Key.KeyType.E_vector2)
            {
                CreateKeyView(key.Name, BT_Key.KeyType.E_vector2);
            }
            else if (obj == BT_Key.KeyType.E_int)
            {
                CreateKeyView(key.Name, BT_Key.KeyType.E_int);
            }
            else if (obj == BT_Key.KeyType.E_float)
            {
                CreateKeyView(key.Name, BT_Key.KeyType.E_float);
            }
            else
            {
                Debug.Log(key.GetType().Name);
            }
        }
    }
    public void CreateKeyView(string name, BT_Key.KeyType type)
    {
        switch (type)
        {
            case BT_Key.KeyType.E_bool:
                {
                    BlackboardKeyView keyView = new BlackboardKeyView();
                    keyView.GenerateKeyView("Bool", name, delclicked);
                    keycontainer.Add(keyView);
                }
                break;
            case BT_Key.KeyType.E_int:
                {
                    BlackboardKeyView keyView = new BlackboardKeyView();
                    keyView.GenerateKeyView("Int", name, delclicked);
                    keycontainer.Add(keyView);
                }
                break;
            case BT_Key.KeyType.E_float:
                {
                    BlackboardKeyView keyView = new BlackboardKeyView();
                    keyView.GenerateKeyView("Float", name, delclicked);
                    keycontainer.Add(keyView);
                }
                break;
            case BT_Key.KeyType.E_vector2:
                {
                    BlackboardKeyView keyView = new BlackboardKeyView();
                    keyView.GenerateKeyView("Vec2", name, delclicked);
                    keycontainer.Add(keyView);
                }
                break;
            case BT_Key.KeyType.E_gameobject:
                {
                    BlackboardKeyView keyView = new BlackboardKeyView();
                    keyView.GenerateKeyView("Object", name, delclicked);
                    keycontainer.Add(keyView);
                }
                break;
        }
    }
   
}
