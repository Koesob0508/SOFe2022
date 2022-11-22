using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public partial class StageManager : MonoBehaviour
{
    private StageNode currentStageNode;
    public GameObject laneObject;
    private bool isDebugMode = false;

    [Header("각각의 스테이지 노드 프리팹 할당")]
    public StageNode jungleNode;
    public StageNode caveNode;
    public StageNode bossNode;
    public StageNode townNode;
    public StageNode eventNode;

    [Header("스테이지 시작 갯수와 단계 갯수 설정")]
    public int startCount;
    public int stepCount;
    public int townCount;

    private List<List<Seed>> seeds;
    private List<Step> stages;
    public List<int> townIndices;
    [SerializeField] private StageMap stageMap;

    private List<StageData> stageData;
    private List<StageLevel> loadedStageData;

    private string saveData;

    public bool isNextStage = false;
    private int stageLevel;

    #region Sub Manager
    private Viewer viewer;
    #endregion

    // 현재는 GameManager에 StageManager가 할당되어 있어야 한다.
    public void Init()
    {
        DontDestroyOnLoad(this);

        viewer = new Viewer(this);
        viewer.Init();

        if (saveData != null)
        {
            Debug.Log("Save data is found, Load save data");
            LoadStageMap(saveData);
        }
        else
        {
            townIndices = new List<int>();
            int step = stepCount / (townCount + 1);
            for (int index = step; index < stepCount - 1; index += step)
            {
                townIndices.Add(index);
            }
            Debug.Log("Save data did not found, Init save data");
            loadedStageData = new List<StageLevel>();
            LoadStageData(stageData);
            InitStageMap(startCount, stepCount, townIndices, stageLevel);
        }
    }

    public void Clear()
    {
        saveData = null;

        viewer.Clear();
    }

    private void LoadStageData(List<StageData> _stageData)
    {
        foreach (StageData stageData in _stageData)
        {
            StageEnemy stageEnemy = new StageEnemy();
            stageEnemy.ruid = stageData.ruid;
            stageEnemy.count = stageData.count;

            // 리팩토링 필수...
            if (loadedStageData.Count
                < stageData.stage + 1)
            {
                StageLevel stageLevel = new StageLevel();
                loadedStageData.Add(stageLevel);
            }

            if (loadedStageData[stageData.stage].mapTypes.Count
                < (int)stageData.map + 1)
            {
                StageMapType stageMap = new StageMapType();
                loadedStageData[stageData.stage].mapTypes.Add(stageMap);
            }

            if (loadedStageData[stageData.stage].mapTypes[(int)stageData.map].steps.Count
                < stageData.step + 1)
            {
                StageStep stageStep = new StageStep();
                loadedStageData[stageData.stage].mapTypes[(int)stageData.map].steps.Add(stageStep);
            }

            if (loadedStageData[stageData.stage].mapTypes[(int)stageData.map].steps[stageData.step].cases.Count
                < stageData.case_ + 1)
            {
                StageCase stageCase = new StageCase();
                loadedStageData[stageData.stage].mapTypes[(int)stageData.map].steps[stageData.step].cases.Add(stageCase);
            }

            loadedStageData[stageData.stage].mapTypes[(int)stageData.map].steps[stageData.step].cases[stageData.case_].enemies.Add(stageEnemy);
        }

        string test = "";
        int level = 0;
        foreach (StageLevel stagelevel in loadedStageData)
        {
            test += string.Format("Stage {0}", level);
            int mapLevel = 0;
            foreach (StageMapType map in loadedStageData[level].mapTypes)
            {
                test += string.Format(" Map {0}", mapLevel);
                int stepLevel = 0;
                foreach (StageStep step in loadedStageData[level].mapTypes[mapLevel].steps)
                {
                    test += string.Format(" Step {0}", stepLevel);
                    int caseLevel = 0;
                    foreach (StageCase cases in loadedStageData[level].mapTypes[mapLevel].steps[stepLevel].cases)
                    {
                        test += string.Format(" Case {0}", caseLevel);
                        foreach (StageEnemy eneimies in loadedStageData[level].mapTypes[mapLevel].steps[stepLevel].cases[caseLevel].enemies)
                        {
                            test += string.Format(" RUID {0} Count {1}", eneimies.ruid, eneimies.count);
                        }
                        test += ";";
                        caseLevel++;
                    }
                    stepLevel++;
                }
                mapLevel++;
            }
            level++;
        }
        // Debug.Log(test);
    }

    public List<StageEnemy> GetStageEnemy(int _stage, int _map, int _step)
    {
        if (loadedStageData.Count < _stage + 1)
        {
            Debug.LogError(string.Format("Stage Over, Stage {0} Map {1} Step{2}", _stage, _map, _step));
            return null;
        }
        else if (loadedStageData[_stage].mapTypes.Count < _map + 1)
        {
            Debug.LogError(string.Format("Map Over, Stage {0} Map {1} Step{2}", _stage, _map, _step));
            return null;
        }
        else if (loadedStageData[_stage].mapTypes[_map].steps.Count < _step + 1)
        {
            Debug.LogError(string.Format("Step Over, Stage {0} Map {1} Step{2}", _stage, _map, _step));
            return null;
        }

        int randomCase = Random.Range(0, loadedStageData[_stage].mapTypes[_map].steps[_step].cases.Count);

        if (loadedStageData[_stage].mapTypes[_map].steps[_step].cases.Count < randomCase + 1)
        {
            Debug.LogError("Case Count : " + loadedStageData[_stage].mapTypes[_map].steps[_step].cases.Count);
        }

        return loadedStageData[_stage].mapTypes[_map].steps[_step].cases[randomCase].enemies;
    }

    private void InitStageMap(int _startCount, int _stepCount, List<int> _townIndices, int _stageLevel)
    {
        Debug.Log("Stage Node Instantiate");

        seeds = InitSeed(_startCount, _stepCount);
        seeds = RandomizeSeed(seeds, _townIndices, _stageLevel);
        seeds = RandomizePosition(seeds);

        stages = viewer.GenerateNode(seeds);

        stages = SetPath(stages, seeds);

        InitLane(laneObject, stages);

        stageMap = new StageMap(stages);
    }

    public void SetStageData(List<StageData> _stageData)
    {
        stageData = _stageData;

        Debug.Log("Set StageData");
    }

    private void LoadStageMap(string _saveData)
    {
        stageMap = DeserializeStageMap(_saveData);
        viewer.ReconstructStageNodes(stageMap);
        ReconstructLane(laneObject, stageMap);
    }

    private List<List<Seed>> InitSeed(int _startCount, int _stepCount)
    {
        List<List<Seed>> resultList = new List<List<Seed>>();

        for (int step = 0; step < _stepCount; step++)
        {
            List<Seed> stepList = new List<Seed>();

            for (int index = 0; index < _startCount; index++)
            {
                Seed initSeed = new(index, step);
                stepList.Add(initSeed);
            }

            resultList.Add(stepList);
        }

        return resultList;
    }

    private List<List<Seed>> RandomizeSeed(List<List<Seed>> _seeds, List<int> _townIndices, int _stageLevel)
    {
        int stageStep = 0;

        foreach (List<Seed> stepList in _seeds)
        {
            // 보스 스테이지가 아니라면
            if (stepList[0].Step != _seeds.Count - 1)
            {
                if (_townIndices.Contains(stepList[0].Step))
                {
                    stageStep++;

                    foreach (Seed seed in stepList)
                    {
                        seed.StageLevel = _stageLevel;
                        seed.StageStep = stageStep;
                    }
                }
                else
                {
                    foreach (Seed seed in stepList)
                    {
                        seed.StageLevel = _stageLevel;
                        seed.StageStep = stageStep;
                    }
                }

                int townIndex = Random.Range(0, stepList.Count - 1);

                // 시작 단계가 아니라면
                if (stepList[0].Step != 0)
                {
                    foreach (Seed seed in stepList)
                    {
                        seed.SetNextStage(seed.Index);

                        if (_townIndices.Contains(seed.Step))
                        {
                            if (seed.Index == townIndex)
                            {
                                seed.Type = StageType.Town;
                            }
                            else
                            {
                                //int toIndex = stepList[townIndex].GetResultPointer();
                                seed.Merge(stepList[townIndex]);
                            }
                        }
                        else
                        {
                            int eventDice = Random.Range(0, 100);

                            if(eventDice > 15)
                            {
                                seed.Type = StageType.Battle;

                                int dice = Random.Range(0, 100);

                                if (dice > 15)
                                {
                                    seed.StageMapType = GameManager.MapType.Jungle;
                                }
                                else
                                {
                                    seed.StageMapType = GameManager.MapType.Dessert;
                                }
                            }
                            else
                            {
                                seed.Type = StageType.Event;
                            }
                        }
                    }


                    int count = Random.Range(1, stepList.Count - 1);
                    List<int> randomIndex = GetRandomIndex(stepList.Count - 1, count);

                    if (!_townIndices.Contains(stepList[0].Step))
                    {
                        foreach (int fromIndex in randomIndex)
                        {
                            int toIndex = 0;
                            int indexDirection = Random.Range(0, 1);

                            if (indexDirection == 0)
                            {
                                toIndex = fromIndex + 1;
                                toIndex = stepList[toIndex].GetResultPointer();
                                stepList[fromIndex].Merge(stepList[toIndex]);
                            }
                            else
                            {
                                toIndex = fromIndex - 1;
                                toIndex = stepList[toIndex].GetResultPointer();
                                stepList[fromIndex].Merge(stepList[toIndex]);
                            }
                        }
                    }
                }
                // 시작 단계 처리
                else
                {
                    int startIndex = Random.Range(0, stepList.Count - 1);

                    foreach (Seed seed in stepList)
                    {
                        seed.SetNextStage(seed.Index);

                        if (seed.Index == startIndex)
                        {
                            seed.Type = StageType.Battle;
                        }
                        else
                        {
                            seed.Merge(stepList[startIndex]);
                        }
                    }
                }
            }
            // 마지막 단계 = 보스 스테이지 처리
            else
            {
                int startIndex = Random.Range(0, stepList.Count - 1);

                foreach (Seed seed in stepList)
                {
                    if (seed.Index == startIndex)
                    {
                        seed.Type = StageType.Battle;
                        seed.StageMapType = GameManager.MapType.Boss;
                        seed.StageStep = 0;
                    }
                    else
                    {
                        seed.Merge(stepList[startIndex]);
                    }
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

    private List<List<Seed>> RandomizePosition(List<List<Seed>> _seeds)
    {
        foreach (List<Seed> stepList in _seeds)
        {
            foreach (Seed seed in stepList)
            {
                // 여기서 사전 처리가 필요함
                // 돌아와서 하면 될 내용
                // yPosition은 screenHeight / startCount
                // xPosition은 그것에 1.5배? 정도
                //float yStep = _screenHeight / (startCount + 1);
                float yStep = 1f;
                float xStep = yStep * 1.5f;

                // 범위는 딱 yPosition의 절반
                float xRange = yStep / 2;
                float yRange = yStep / 2;

                float xPosition = seed.Step * xStep;
                float yPosition = seed.Index * yStep;

                // seed의 값에 따라 적당한 포지션으로
                seed.SetPosition(xPosition, yPosition);
                seed.RandomizePosition(xRange, yRange);
            }
        }

        return _seeds;
    }

    private List<Step> SetPath(List<Step> _stages, List<List<Seed>> _seeds)
    {
        foreach (List<Seed> steps in _seeds)
        {
            foreach (Seed seed in steps)
            {
                if (!seed.IsMerged)
                {
                    int currentStep = seed.Step;
                    int currentIndex = seed.Index;
                    int nextStep = currentStep + 1;

                    foreach (int nextIndex in seed.NextStages)
                    {
                        int resultIndex = seeds[nextStep][nextIndex].GetResultPointer();

                        stages[currentStep].GetStageNode(currentIndex).AddNextStage(_stages[nextStep].GetStageNode(resultIndex));
                    }
                }
                else
                {
                    int currentStep = seed.Step;
                    int currentIndex = seed.Index;
                    stages[currentStep].GetStageNode(currentIndex).gameObject.SetActive(false);
                }
            }
        }

        return _stages;
    }

    private void InitLane(GameObject _lanePrefab, List<Step> _stages)
    {
        foreach (Step step in stages)
        {
            foreach (StageNode node in step.GetStageNodes())
            {
                if (!node.IsMerged)
                {
                    node.GenerateLane(_lanePrefab, _stages);
                }
            }
        }
    }

    public void ShowStageMap()
    {
        viewer.On();
    }

    public void HideStageMap()
    {
        viewer.Off();
    }

    public List<Enemy> GetEnemies()
    {
        if (currentStageNode.Type == StageManager.StageType.Battle)
        {
            return currentStageNode.Enemies;
        }

        Debug.Log("This Stage is not Battle Stage");
        return null;
    }

    public GameManager.MapType GetMapType()
    {
        return currentStageNode.StageMapType;
    }

    public void CompleteStage()
    {
        foreach (Step step in stageMap.stages)
        {
            foreach (StageNode node in step.GetStageNodes())
            {
                node.IsInteractable = false;
                node.button.interactable = false;
                node.ChangeNodeAlpha(false);
            }
        }
        currentStageNode.Complete(stageMap.stages);
    }

    private void UpdateCurrentNode(StageNode _currentNode)
    {
        currentStageNode = _currentNode;
        viewer.UpdateCanvas(_currentNode.transform.position.x);
    }

    private void UnloadStages()
    {
        foreach (Step step in stageMap.stages)
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

    public StageMap DeserializeStageMap(string _jsonData)
    {
        return JsonUtility.FromJson<StageMap>(_jsonData);
    }

    public void LoadStageSaveData(string _saveData)
    {
        saveData = _saveData;
    }
    /// <summary>
    /// 혹시 몰라 쓰진 않았는데 Serialized StageMap을 줘야합니다.
    /// </summary>
    /// <param name="_serializedStageMap"></param>
    private void GenerateSaveFile(string _serializedStageMap)
    {
        FileStream fileStream = new FileStream("Assets/Koesob/Save.json", FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(_serializedStageMap);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    private void ReconstructLane(GameObject _lanePrefab, StageMap _stageMap)
    {
        foreach (Step step in _stageMap.stages)
        {
            foreach (StageNode node in step.GetStageNodes())
            {
                if (!node.IsMerged)
                {
                    node.GenerateLane(_lanePrefab, _stageMap.stages);
                }
            }
        }
    }

    public void OnDebugMode()
    {
        isDebugMode = true;
        foreach (Step step in stageMap.stages)
        {
            step.OnDebugMode();
        }
    }

    public void OffDebugMode()
    {
        isDebugMode = false;
        foreach (Step step in stageMap.stages)
        {
            step.OffDebugMode();
        }
    }

    public void MoveCanvasLeft()
    {
        if(isDebugMode)
        {
            viewer.MoveLeftCanvas(2);
        }
    }

    public void MoveCanvasRight()
    {
        if(isDebugMode)
        {
            viewer.MoveRightCanvas(2);
        }
    }

    public string GetLastStage()
    {
        return currentStageNode.Step.ToString();
    }

    public bool IsToNextStage()
    {
        return isNextStage;
    }

    public void ToNextStage()
    {
        stageLevel++;
        viewer.Clear();
        viewer.Init();

        InitStageMap(startCount, stepCount, townIndices, stageLevel);

        isNextStage = false;
    }
}
