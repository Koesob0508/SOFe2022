using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroInfo : MonoBehaviour
{
    [SerializeField]
    private Image heroImage;

    [SerializeField]
    private TMP_Text title;

    [SerializeField]
    private TMP_Text description;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.heroImage.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = "";
    }

    public void SetDescription(Sprite sprite, string heroName, string heroDescription)
    {
        this.heroImage.gameObject.SetActive(true);
        this.heroImage.sprite = sprite;
        this.title.text = heroName;
        this.description.text = heroDescription;
    }
}
