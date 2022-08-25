using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeroInvenPanel : MonoBehaviour
{

    public GameObject Item_Prefab;
    GameObject Half_UI;

    public Color StartCol = new Color(0,0,1.0f,0.5f);
    public Color EndCol = new Color(0,0,1.0f,0.0f);
    public void Initalize(List<Hero> Heros)
    {
        foreach(Hero hero in Heros)
        {
            GameObject g = Instantiate(Item_Prefab, transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.LoadSprite(hero.GUID);
            g.GetComponentInChildren<HeroInvenItem>().SetHeroObj(GameManager.Battle.CreateHero(hero));
        }
        Half_UI = new GameObject("Half_Panel");
        RectTransform rt = Half_UI.AddComponent<RectTransform>();
        Half_UI.AddComponent<CanvasRenderer>();
        Image img = Half_UI.AddComponent<Image>();
        Half_UI.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);

        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(0.5f, 1);
        rt.offsetMin = new Vector2(0, 0); //left, btm
        rt.offsetMax = new Vector2(0, 0); //-right,-top

        img.color = StartCol;
        Half_UI.SetActive(false);
    }

    public void StartDragging()
    {
        Debug.Log("StartDragging");
        Half_UI.SetActive(true);
        ToVisible();
    }
    public void EndDragging()
    {
        Debug.Log("EndDragging");
        Half_UI.SetActive(false);
        ToInvisible();
    }
    void ToVisible()
    {
        Debug.Log("To visible");
        LeanTween.imageColor(Half_UI.GetComponent<RectTransform>(),StartCol,0.5f).setOnComplete(() => {
            if (Half_UI.activeSelf)
                ToInvisible();
        });
    }
    void ToInvisible()
    {
        LeanTween.imageColor(Half_UI.GetComponent<RectTransform>(), EndCol, 0.5f).setOnComplete(() => {
            if (Half_UI.activeSelf)
                ToVisible();
        });
    }

}
