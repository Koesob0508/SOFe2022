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
        signalTitle = "���̼� �� ����?!";
        signalExplain = "�� �뺴�� �� ��� ���� �ൿ";
        preHero = new Hero();
        postHero = new Hero();
        preCharacter = new Character();
        postCharacter = new Character();
    }

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        // �ϴ� subject�� Hero���� ����
        if (_log.Causer is Hero)
        {
            preHero = postHero.DeepCopy();
            preCharacter = postCharacter.DeepCopy();
            postHero = (Hero)_log.Causer;
            postCharacter = _log.Target;
            
            if(preHero.IsActive == false)
            {
                Debug.LogWarning("PreHero�� ���� ����");
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
        Debug.Log("E�� I �� Event �߻�");

        if (GameManager.Relation.IsI(preHero))
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", preHero, -10, 10);
            UpdateLog(0, "����... ����ε�...");
        }
        else // I�� �ƴϸ� E�̱� ������...
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", preHero, 10, 10);
            UpdateLog(0, "�̰͵� õ������?!");
        }

        if (GameManager.Relation.IsI(postHero))
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", postHero, -10, 10);
            UpdateLog(1, "����... ����ε�...");
        }
        else // I�� �ƴϸ� E�̱� ������...
        {
            GameManager.Battle.ApplyBuff("AttackSpeed", postHero, 10, 10);
            UpdateLog(1, "�̰͵� õ������?!");
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
