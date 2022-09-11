using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageManager
{
    [System.Serializable]
    public class Step
    {
        [SerializeField] public List<StageNode> stageNodes;

        public Step()
        {
            stageNodes = new List<StageNode>();
        }
    }

    [System.Serializable]
    public class StageMap
    {
        public List<Step> stageList;

        public StageMap(List<Step> _stageList)
        {
            stageList = _stageList;
        }
    }
}

public partial class StageManager : MonoBehaviour
{
    private StageNode currentStageNode;
    private GameObject canvas;
    public GameObject laneObject;

    [Header("각각의 스테이지 노드 프리팹 할당")]
    public StageNode battleNode;
    public StageNode townNode;
    public StageNode eventNode;
    [Header("스테이지 시작 갯수와 단계 갯수 설정")]
    public int startCount;
    public int stepCount;

    private List<List<Seed>> seeds;
    private List<Step> stages;
    private float screenHeight;
    private float stageNodeScale;

    // 현재는 GameManager에 StageManager가 할당되어 있어야 한다.
    public void Init()
    {
        DontDestroyOnLoad(this);

        // stageNode들을 Stage 오브젝트의 자식 오브젝트로 둘 예정
        canvas = GameObject.Find("Stage Canvas");

        if (canvas == null)
        {
            canvas = new GameObject { name = "Stage Canvas" };
        }

        canvas.transform.position = Vector3.zero;

        DontDestroyOnLoad(canvas);

        screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;

        var stageManagerPosition = this.transform.position;
        stageManagerPosition.x -= screenWidth * 3 / 8;
        stageManagerPosition.y -= screenHeight * 3 / 8;

        canvas.transform.position = stageManagerPosition;

        InitStageMap(startCount, stepCount);
    }

    public void InitStageMap(int _startCount, int _stepCount)
    {
        Debug.Log("Stage Node Instantiate");

        seeds = InitSeed(_startCount, _stepCount);
        seeds = RandomizeSeed(seeds);
        seeds = RandomizePosition(seeds, screenHeight);

        stageNodeScale = screenHeight * 0.1f;

        stages = GenerateNode(seeds, stageNodeScale);

        stages = SetPath(stages, seeds);

        InitLane(laneObject);

        canvas.SetActive(false);
    }

    private List<List<Seed>> InitSeed(int _startCount, int _stepCount)
    {
        List<List<Seed>> resultList = new List<List<Seed>>();

        for (int step = 0; step < _stepCount; step++)
        {
            List<Seed> stepList = new List<Seed>();

            for (int index = 0; index < _startCount; index++)
            {
                Seed initSeed = new Seed(index, step);
                stepList.Add(initSeed);
            }

            resultList.Add(stepList);
        }

        return resultList;
    }

    private List<List<Seed>> RandomizeSeed(List<List<Seed>> _seeds)
    {
        foreach (List<Seed> stepList in _seeds)
        {
            if (stepList[0].GetStep() != _seeds.Count - 1)
            {
                // 시작과 끝 사이에 있는 단계 처리
                if (stepList[0].GetStep() != 0)
                {
                    foreach (Seed seed in stepList)
                    {
                        seed.SetNextStage(seed.GetIndex());
                    }

                    int count = Random.Range(0, stepList.Count - 1);
                    List<int> randomIndex = GetRandomIndex(stepList.Count - 1, count);

                    foreach (int index in randomIndex)
                    {
                        int toIndex = stepList[index + 1].GetResultPointer();
                        stepList[index].Merge(stepList[toIndex]);
                    }
                }
                // 시작 단계 처리
                else
                {
                    foreach (Seed seed in stepList)
                    {
                        seed.SetNextStage(seed.GetIndex());
                    }
                }
            }
            // 마지막 단계 = 보스 스테이지 처리
            else
            {
                int count = stepList.Count - 1;
                List<int> randomIndex = GetRandomIndex(stepList.Count - 1, count);

                foreach (int index in randomIndex)
                {
                    int toIndex = stepList[index + 1].GetResultPointer();
                    stepList[index].Merge(stepList[toIndex]);
                }
            }
        }

        return _seeds;
    }

    /// <summary>
    /// 크기와 갯수를 주면, 해당 크기 내에서 갯수만큼 랜덤하게 뽑아서 줍니다.
    /// </summary>
    /// <param name="_size">배열의 크기</param>
    /// <param name="_count">해당 배열에서 뽑을 갯수</param>
    /// <returns></returns>
    private List<int> GetRandomIndex(int _size, int _count)
    {
        List<int> randomIndex = new List<int>();

        for (int i = 0; i < _count; i++)
        {
            int index = Random.Range(0, _size);
            while (randomIndex.Contains(index))
            {
                index = Random.Range(0, _size);
            }
            randomIndex.Add(index);
        }

        return randomIndex;
    }

    private List<List<Seed>> RandomizePosition(List<List<Seed>> _seeds, float _screenHeight)
    {
        foreach (List<Seed> stepList in _seeds)
        {
            foreach (Seed seed in stepList)
            {
                // 여기서 사전 처리가 필요함
                // 돌아와서 하면 될 내용
                // yPosition은 screenHeight / startCount
                // xPosition은 그것에 1.5배? 정도
                float yStep = _screenHeight / (startCount + 1);
                float xStep = yStep * 1.5f;

                // 범위는 딱 yPosition의 절반
                float xRange = yStep / 2;
                float yRange = yStep / 2;

                float xPosition = seed.GetStep() * xStep;
                float yPosition = seed.GetIndex() * yStep;

                // seed의 값에 따라 적당한 포지션으로
                seed.SetPosition(xPosition, yPosition);
                seed.RandomizePosition(xRange, yRange);
            }
        }

        return _seeds;
    }

    private List<Step> GenerateNode(List<List<Seed>> _seeds, float _nodeScale)
    {
        List<Step> resultList = new List<Step>();

        foreach(List<Seed> steps in _seeds)
        {
            Step stageSteps = new Step();

            foreach(Seed seed in steps)
            {
                Vector2 position = seed.GetPosition() + new Vector2(canvas.transform.position.x, canvas.transform.position.y);
                int step = seed.GetStep();
                int index = seed.GetIndex();

                StageNode stageNode = Instantiate<StageNode>(battleNode, position, Quaternion.identity, canvas.transform);
                // Seed로부터 StageNode 정보 불러오기
                stageNode.name = string.Format("Step : {0} Index : {1}", step, index);
                stageNode.transform.localScale = new Vector3(_nodeScale, _nodeScale, 1f);

                stageNode.Init(battleNode.type, step);
                stageNode.RegistStageNode += UpdateCurrentNode;

                stageSteps.stageNodes.Add(stageNode);

                if(seed.isMerged())
                {
                    stageNode.isMerged = seed.isMerged();
                }
            }

            resultList.Add(stageSteps);
        }

        return resultList;
    }

    private List<Step> SetPath(List<Step> _stages, List<List<Seed>> _seeds)
    {
        foreach(List<Seed> steps in _seeds)
        {
            foreach(Seed seed in steps)
            {
                if(!seed.isMerged())
                {
                    int currentStep = seed.GetStep();
                    int currentIndex = seed.GetIndex();
                    int nextStep = currentStep + 1;

                    foreach(int targetIndex in seed.GetNextStage())
                    {
                        int resultIndex = seeds[nextStep][targetIndex].GetResultPointer();

                        stages[currentStep].stageNodes[currentIndex].AddNextStage(_stages[nextStep].stageNodes[resultIndex]);
                    }
                }
                else
                {
                    int currentStep = seed.GetStep();
                    int currentIndex = seed.GetIndex();
                    stages[currentStep].stageNodes[currentIndex].gameObject.SetActive(false);
                }
            }
        }

        return _stages;
    }

    private void InitLane(GameObject _lanePrefab)
    {
        foreach(Step step in stages)
        {
            foreach (StageNode node in step.stageNodes)
            {
                if(!node.isMerged)
                {
                    node.GenerateLane(_lanePrefab);
                }
            }

        }
    }

    public void ShowStageMap()
    {
        //foreach (Step stages in stages)
        //{
        //    foreach(StageNode stage in stages.stageNodes)
        //    {
        //        if(!stage.isMerged)
        //        {
        //            stage.gameObject.SetActive(true);
        //        }
        //    }
        //}

        canvas.SetActive(true);
    }

    public void HideStageMap()
    {
        //foreach (Step stages in stages)
        //{
        //    foreach (StageNode stage in stages.stageNodes)
        //    {
        //        stage.gameObject.SetActive(false);
        //    }
        //}

        canvas.SetActive(false);
    }

    public List<Enemy> GetEnemies()
    {
        if (currentStageNode.type == StageNode.StageType.Battle)
        {
            return currentStageNode.enemies;
        }

        Debug.Log("This Stage is not Battle Stage");
        return null;
    }

    public void Test_SetBattleStage()
    {
        currentStageNode = stages[0].stageNodes[0];

        Debug.Log("Current Stage is Battle Stage");
    }

    public void CompleteStage()
    {
        foreach (Step step in stages)
        {
            foreach (StageNode node in step.stageNodes)
            {
                node.button.interactable = false;
            }
        }
        currentStageNode.Complete();
    }

    private void UpdateCurrentNode(StageNode _currentNode)
    {
        currentStageNode = _currentNode;
    }

    private void UnloadStages()
    {
        foreach (Step step in stages)
        {
            foreach (StageNode node in step.stageNodes)
            {
                node.RegistStageNode -= UpdateCurrentNode;
            }
        }
    }
}
