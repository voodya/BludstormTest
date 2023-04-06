using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Config", menuName = "Game/Config")]
[System.Serializable]
public class GameConfig : ScriptableObject
{
    public List<LevelData> _existedLevels;
    public BaseUnitData _playerData;

}

[System.Serializable]
public class LevelData
{
    public List<UnitTypeData> _levelUnits;
    public int _levelNumber;
}

[System.Serializable]
public class UnitTypeData
{
    public BaseUnitData _data;
    public BaseUnit _prefab;
    public int _count;
}

