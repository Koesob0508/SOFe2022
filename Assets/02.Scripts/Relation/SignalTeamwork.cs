using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignalTeamwork : CustomSignal
{
    [SerializeField] private Hero preHero;
    [SerializeField] private Hero postHero;
    [SerializeField] private GameObject preCharacter;
    [SerializeField] private GameObject postCharacter;
    [SerializeField] private GameObject preChat;
    [SerializeField] private GameObject postChat;
    [SerializeField] private TMP_Text preText;
    [SerializeField] private TMP_Text postText;

    public override void Init()
    {
        signalTitle = "팀워크!";
        signalExplain = "두 용병이 하나에 적에 대해 행동";
        preHero = new Hero();
        postHero = new Hero();
        preCharacter = null;
        postCharacter = null;
    }
    protected override bool Condition(BattleLogPanel.Log _log)
    {
        if (_log.Causer is Hero && _log.Target is Enemy)
        {
            preHero = postHero.DeepCopy();
            preCharacter = postCharacter;
            postHero = (Hero)_log.Causer;
            postCharacter = _log.TargetObject;

            if (preHero.IsActive == false)
            {
                Debug.LogError("preHero 아직 없음");
                return false;
            }

            if (preHero.Name != postHero.Name &&
               preCharacter.Equals(postCharacter))
            {
                return true;
            }
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("팀워크!");

        GameManager.Relation.SetChangeRelationship(preHero, postHero, 1);
        BattleLogPanel.Log logA = new BattleLogPanel.Log(preHero, postHero, BattleLogPanel.LogType.Positive);
        GameManager.Battle.LogDelegate(logA);
        
        GameManager.Relation.SetChangeRelationship(postHero, preHero, 1);
        BattleLogPanel.Log logB = new BattleLogPanel.Log(postHero, preHero, BattleLogPanel.LogType.Positive);
        GameManager.Battle.LogDelegate(logB);

        if (GameManager.Battle.GetHeroGameObject(preHero).transform.position.y > GameManager.Battle.GetHeroGameObject(postHero).transform.position.y)
        {
            UpdateLog(0, "중요한 것은");
            UpdateLog(1, "꺾이지 않는 마음");
        }
        else
        {
            UpdateLog(1, "중요한 것은");
            UpdateLog(0, "꺾이지 않는 마음");
        }

        preHero = new Hero();
        postHero = new Hero();
        preCharacter = null;
        postCharacter = null;
    }

    private void UpdateLog(int _index, string _string)
    {
        if (_index == 0)
        {
            Vector3 position = GameManager.Battle.GetHeroGameObject(preHero).transform.position;
            preChat.transform.position = position;
            preChat.GetComponent<SpriteRenderer>().sortingOrder = -(int)(preChat.transform.position.y * 10);
            preText.gameObject.GetComponent<MeshRenderer>().sortingOrder = -(int)(preChat.transform.position.y * 10);
            preText.text = _string;
            preChat.SetActive(true);
            StartCoroutine(SetDisable(preChat));
        }
        else if (_index == 1)
        {
            Vector3 position = GameManager.Battle.GetHeroGameObject(postHero).transform.position;
            postChat.transform.position = position;
            postChat.GetComponent<SpriteRenderer>().sortingOrder = -(int)(postChat.transform.position.y * 10);
            postText.gameObject.GetComponent<MeshRenderer>().sortingOrder = -(int)(postChat.transform.position.y * 10);
            postText.text = _string;
            postChat.SetActive(true);
            StartCoroutine(SetDisable(postChat));
        }
    }

    private IEnumerator SetDisable(GameObject _gameObject)
    {
        yield return new WaitForSeconds(1.5f);

        _gameObject.SetActive(false);
    }
}
