using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageData
{
    public int stage;
    public GameManager.MapType map;
    public int step;
    public int case_;
    public int ruid;
    public int count;
}

public struct StageEnemy
{
    public int ruid;
    public int count;
}

public class StageCase
{
    public List<StageEnemy> enemies = new List<StageEnemy>();
}

public class StageStep
{
    public List<StageCase> cases = new List<StageCase>();
}

public class StageMapType
{
    public List<StageStep> steps = new List<StageStep>();
}

public class StageLevel
{
    public List<StageMapType> mapTypes = new List<StageMapType>();
}