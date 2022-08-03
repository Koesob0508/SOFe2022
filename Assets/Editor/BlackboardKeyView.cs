using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
public class BlackboardKeyView : VisualElement
{

    Label val_text;
    Label name_text;
    Button delete_btn;
    Action<string> OnDelClicked;
    public new class UxmlFactory : UxmlFactory<BlackboardKeyView, UxmlTraits> { }



    public void Init()
    {
        VisualTreeAsset uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BlackboardKeyView.uxml");
        TemplateContainer ui = uiAsset.CloneTree();

        Add(ui);

        val_text = this.Query<Label>("type");
        name_text = this.Query<Label>("name");
        delete_btn = this.Query<Button>();
        delete_btn.clicked += Delete_btn_clicked;

    }

    public void Delete_btn_clicked()
    {
        OnDelClicked(name_text.text);
    }

    public BlackboardKeyView()
    {
    }
    public void GenerateKeyView(string val_type, string name, Action<string> Onclk)
    {
        Init();

        val_text.text = val_type;
        name_text.text = name;
        OnDelClicked = Onclk;
    }
}
