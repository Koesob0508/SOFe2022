using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignalEI : CustomSignal
{
    [SerializeField] private Hero preHero;
    [SerializeField] private Hero postHero;
    [SerializeField] private Character preCharacter;
    [SerializeField] private Character postCharacter;
    [SerializeField] private GameObject preChat;
    [SerializeField] private GameObject postChat;
    [SerializeField] private TMP_Text preText;
    [SerializeField] private TMP_Text postText;

    public override void Init()
    {
        signalTitle = "둘이서 한 마음?!";
        signalExplain = "두 용병이 한 대상에 대해 행동";
        preHero = new Hero();
        postHero = new Hero();
        preCharacter = new Character();
        postCharacter = new Character();
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        // 일단 subject가 Hero여야 실행
        if (_log.Causer is Hero)
        {
            preHero = postHero.DeepCopy();
            preCharacter = postCharacter.DeepCopy();
            postHero = (Hero)_log.Causer;
            postCharacter = _log.Target;
            
            if(preHero.IsActive == false)
            {
                Debug.LogWarning("PreHero가 아직 없음");
                return false;
            }

            if (preHero.Name != postHero.Name && 
                GameManager.Relation.GetBetweenScore(preHero, postHero) < 0 &&
                GameManager.Relation.GetBetweenScore(postHero, preHero) < 0 &&
                preCharacter.Equals(postCharacter))
            {
                return true;
            }
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("E와 I 간 Event 발생");

        if (GameManager.Relation.IsI(preHero))
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", preHero, -10, 10);
            UpdateLog(0, "아직... 어사인데...");
        }
        else // I가 아니면 E이기 때문에...
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", preHero, 10, 10);
            UpdateLog(0, "이것도 천생연분?!");
        }

        if (GameManager.Relation.IsI(postHero))
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", postHero, -10, 10);
            UpdateLog(1, "아직... 어사인데...");
        }
        else // I가 아니면 E이기 때문에...
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", postHero, 10, 10);
            UpdateLog(1, "이것도 천생연분?!");
        }
    }

    private void UpdateLog(int _index, string _string)
    {
        if (_index == 0)
        {
            Vector3 position = GameManager.Battle.GetHeroGameObject(preHero).transform.position;
            preChat.transform.position = position;
            preChat.GetComponent<SpriteRenderer>().sortingOrder = -(int)(preChat.transform.position.y * 10);
            preText.gameObject.GetComponent<MeshRenderer>().sortingOrder = -(int)(preChat.transform.position.y * 10);
            preChat.SetActive(true);
            StartCoroutine(SetDisable(preChat));
        }
        else if (_index == 1)
        {
            Vector3 position = GameManager.Battle.GetHeroGameObject(postHero).transform.position;
            postChat.transform.position = position;
            postChat.GetComponent<SpriteRenderer>().sortingOrder = -(int)(postChat.transform.position.y * 10);
            postText.gameObject.GetComponent<MeshRenderer>().sortingOrder = -(int)(postChat.transform.position.y * 10);
            postChat.SetActive(true);
            StartCoroutine(SetDisable(postChat));
        }
    }

    private IEnumerator SetDisable(GameObject _gameObject)
    {
        yield return new WaitForSeconds(1f);

        _gameObject.SetActive(false);
    }
}
