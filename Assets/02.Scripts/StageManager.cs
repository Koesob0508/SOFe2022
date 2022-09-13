using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public partial class StageManager
{
    [System.Serializable]
    public struct SerializedNode
    {
        public StageNode.StageType type;
        public int step;
        public int index;
        public Vector3 poistion;
        public bool isMerged;
        public bool isCompleted;
        public bool isPassPoint;
        public List<Vector2Int> nextStages;
        public List<Enemy> enemies;
    }

    [System.Serializable]
    public class Step : ISerializationCallbackReceiver
    {
        public List<SerializedNode> serializeNodes;
        private List<StageNode> stageNodes;

        public Step()
        {
            stageNodes = new List<StageNode>();
            serializeNodes = new List<SerializedNode>();
        }

        public void AddStageNode(StageNode _stageNode)
        {
            stageNodes.Add(_stageNode);
        }

        public StageNode GetStageNode(int _index)
        {
            return stageNodes[_index];
        }

        public List<StageNode> GetStageNodes()
        {
            return stageNodes;
        }

        public void OnAfterDeserialize()
        {
            // ���� stageNode���� ���ο� struct�� �޾�����Ѵ�.
            Debug.Log("�����ϼ���"); 
        }

        public void OnBeforeSerialize()
        {
            serializeNodes.Clear();

            foreach(StageNode stage in stageNodes)
            {
                SerializedNode node;

                node.type = stage.Type;
                node.step = stage.Step;
                node.index = stage.Index;
                node.poistion = stage.transform.position;
                node.isMerged = stage.IsMerged;
                node.isCompleted = stage.IsCompleted;
                node.isPassPoint = stage.IsPassPoint;
                node.nextStages = stage.NextStages;
                node.enemies = stage.Enemies;

                serializeNodes.Add(node);
            }
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

    [Header("������ �������� ��� ������ �Ҵ�")]
    public StageNode battleNode;
    public StageNode townNode;
    public StageNode eventNode;
    [Header("�������� ���� ������ �ܰ� ���� ����")]
    public int startCount;
    public int stepCount;

    private List<List<Seed>> seeds;
    private List<Step> stages;
    [SerializeField] private StageMap stageMap;
    private float screenHeight;
    private float stageNodeScale;

    // ����� GameManager�� StageManager�� �Ҵ�Ǿ� �־�� �Ѵ�.
    public void Init()
    {
        DontDestroyOnLoad(this);

        // stageNode���� Stage ������Ʈ�� �ڽ� ������Ʈ�� �� ����
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

        InitLane(laneObject, stages);

        canvas.SetActive(false);

        stageMap = new StageMap(stages);

        //string stageMapToString = SerializeStageMap();
        //SaveStageMap(stageMapToString);
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
                // ���۰� �� ���̿� �ִ� �ܰ� ó��
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
                // ���� �ܰ� ó��
                else
                {
                    foreach (Seed seed in stepList)
                    {
                        seed.SetNextStage(seed.GetIndex());
                    }
                }
            }
            // ������ �ܰ� = ���� �������� ó��
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
    /// ũ��� ������ �ָ�, �ش� ũ�� ������ ������ŭ �����ϰ� �̾Ƽ� �ݴϴ�.
    /// </summary>
    /// <param name="_size">�迭�� ũ��</param>
    /// <param name="_count">�ش� �迭���� ���� ����</param>
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
                // ���⼭ ���� ó���� �ʿ���
                // ���ƿͼ� �ϸ� �� ����
                // yPosition�� screenHeight / startCount
                // xPosition�� �װͿ� 1.5��? ����
                float yStep = _screenHeight / (startCount + 1);
                float xStep = yStep * 1.5f;

                // ������ �� yPosition�� ����
                float xRange = yStep / 2;
                float yRange = yStep / 2;

                float xPosition = seed.GetStep() * xStep;
                float yPosition = seed.GetIndex() * yStep;

                // seed�� ���� ���� ������ ����������
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

                StageNode stageNode = Instantiate(battleNode, position, Quaternion.identity, canvas.transform);
                // Seed�κ��� StageNode ���� �ҷ�����
                stageNode.name = string.Format("Step : {0} Index : {1}", step, index);
                stageNode.transform.localScale = new Vector3(_nodeScale, _nodeScale, 1f);

                stageNode.Init(battleNode.Type, step, index);
                stageNode.RegistStageNode += UpdateCurrentNode;

                stageSteps.AddStageNode(stageNode);

                if(seed.isMerged())
                {
                    stageNode.SetIsMerged(seed.isMerged());
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

                        stages[currentStep].GetStageNode(currentIndex).AddNextStage(_stages[nextStep].GetStageNode(resultIndex));
                    }
                }
                else
                {
                    int currentStep = seed.GetStep();
                    int currentIndex = seed.GetIndex();
                    stages[currentStep].GetStageNode(currentIndex).gameObject.SetActive(false);
                }
            }
        }

        return _stages;
    }

    private void InitLane(GameObject _lanePrefab, List<Step> _stages)
    {
        foreach(Step step in stages)
        {
            foreach (StageNode node in step.GetStageNodes())
            {
                if(!node.IsMerged)
                {
                    node.GenerateLane(_lanePrefab, _stages);
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
        if (currentStageNode.Type == StageNode.StageType.Battle)
        {
            return currentStageNode.Enemies;
        }

        Debug.Log("This Stage is not Battle Stage");
        return null;
    }

    public void CompleteStage()
    {
        foreach (Step step in stages)
        {
            foreach (StageNode node in step.GetStageNodes())
            {
                node.button.interactable = false;
            }
        }
        currentStageNode.Complete(stages);
    }

    private void UpdateCurrentNode(StageNode _currentNode)
    {
        currentStageNode = _currentNode;
    }

    private void UnloadStages()
    {
        foreach (Step step in stages)
        {
            foreach (StageNode node in step.GetStageNodes())
            {
                node.RegistStageNode -= UpdateCurrentNode;
            }
        }
    }

    public string SerializeStageMap()
    {
        return JsonUtility.ToJson(stageMap);
    }

    private void SaveStageMap(string _serializedStageMap)
    {
        FileStream fileStream = new FileStream("Assets/Koesob/Save.json", FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(_serializedStageMap);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }
}
