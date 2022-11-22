using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShowHerosMbti : MonoBehaviour
{
    public GameObject Target, Members, Lines;
    public GameObject MemberUI, LineUI;

    private float radius = 250.0f;
    public float lineWidth = 10.0f;

    private uint TargetHeroGUID;
    private GameManager.MbtiType TargetMbti;

    private GameObject TargetHero, HeroUIContent;
    private Dictionary<uint, GameObject> MemberUIList = new Dictionary<uint, GameObject>();

    private int TotalChemi = 0;

    public uint GetHeroUIOrder(GameObject _hero)
    {
        uint guid = MemberUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }

    void Start()
    {
        // Set target, member의 UI 생성
        SetUI();

        // Member position 원형으로 잡는다
        SetMemberPosition();

        // MBTI Value 확인 및 Make Line
        SetMbtiValues();
    }

    public void SetUI()
    {
        // 방금 클릭한 Hero의 정보 Get
        GameObject ClickObject = EventSystem.current.currentSelectedGameObject;
        TargetHero = ClickObject.transform.parent.gameObject;
        HeroUIContent = ClickObject.transform.parent.parent.gameObject;
        TargetHeroGUID = HeroUIContent.GetComponent<SetRandomObject>().GetHeroUIOrder(TargetHero);
        foreach (Hero _target in GameManager.Hero.ShopHeroList)
        {
            if (_target.GUID == TargetHeroGUID)
                TargetMbti = _target.MBTI;
        }

        Target.transform.GetComponent<Image>().sprite = GameManager.Data.LoadSprite(TargetHeroGUID);

        foreach (Hero mem in GameManager.Hero.GetHeroList())
        {
            GameObject ObjectUI = Instantiate(MemberUI, transform.position, Quaternion.identity);
            ObjectUI.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            ObjectUI.transform.GetComponent<Image>().sprite = GameManager.Data.LoadSprite(mem.GUID);
            ObjectUI.transform.SetParent(Members.transform);
            MemberUIList.Add(mem.GUID, ObjectUI);
        }
    }

    private void SetMemberPosition()
    {
        int numOfChild = Members.transform.childCount;

        for (int i = 0; i < numOfChild; i++)
        {
            float angle = i * (Mathf.PI * 2.0f) / numOfChild;

            GameObject child = Members.transform.GetChild(i).gameObject;

            child.transform.position
                = transform.position + (new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)) * radius;
        }
    }

    public void SetMbtiValues()
    {
        foreach (GameObject mem in MemberUIList.Values)
        {
            GameManager.MbtiType memMbti = GameManager.Hero.GetHero(GetHeroUIOrder(mem)).MBTI;

            GameObject ObjectUI = Instantiate(LineUI, transform.position, Quaternion.identity);
            ObjectUI.transform.SetParent(Lines.transform);
            Vector3 pointA = Target.transform.position;
            Vector3 pointB = mem.transform.position;

            Vector3 differenceVector = pointB - pointA;
            ObjectUI.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(differenceVector.magnitude, lineWidth);
            ObjectUI.transform.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
            ObjectUI.transform.GetComponent<RectTransform>().position = pointA;
            float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
            ObjectUI.transform.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);

            sbyte Score = GameManager.Relation.OriginScore[(int)TargetMbti, (int)memMbti];
            TotalChemi += Score;
            switch (Score)
            {
                case (5):
                    {
                        ObjectUI.transform.GetComponent<Image>().color = Color.blue;
                        break;
                    }
                case (3):
                    {
                        ObjectUI.transform.GetComponent<Image>().color = Color.green;
                        break;
                    }
                case (1):
                    {
                        ObjectUI.transform.GetComponent<Image>().color = Color.white;
                        break;
                    }
                case (0):
                    {
                        ObjectUI.transform.GetComponent<Image>().color = Color.yellow;
                        break;
                    }
                case (-3):
                    {
                        ObjectUI.transform.GetComponent<Image>().color = Color.red;
                        break;
                    }
            }
        }

        Target.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(""+TotalChemi);

        if (TotalChemi < 0)
            Target.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.red;
        else
            Target.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.blue;
    }

    public void HideHeroMBTIInfo()
    {
        GameObject AllHeroInfoUI = GameObject.Find("MemberMbti(Clone)");

        if (AllHeroInfoUI != null)
        {
            Destroy(AllHeroInfoUI);
        }
    }
}
