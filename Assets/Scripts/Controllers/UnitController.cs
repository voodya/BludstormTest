using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Constuctor _spawnFieldData;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _spawnDelay = 300;

    private List<BaseUnit> _targetUnits;

    public async void PrepareEnemys(LevelData levelData)
    {
        foreach(UnitTypeData enemyType in levelData._levelUnits)
        {
            for(int i = 0; i < enemyType._count; i++)
            {
                SpawnEnemy(enemyType);
                if (Application.isPlaying) await Task.Delay(_spawnDelay);
                else return;
            }
        }
    }

    private void SpawnEnemy(UnitTypeData data)
    {
        
        float RandomYPos1 = Random.Range(_spawnFieldData._bothOut.position.y, _spawnFieldData._bothIn.position.y);
        float RandomYPos2 = Random.Range(_spawnFieldData._topIn.position.y, _spawnFieldData._topOut.position.y);

        float targetYPosition = Random.Range(0,2) == 1 ? RandomYPos1 : RandomYPos2;

        float RandomXPos1 = Random.Range(_spawnFieldData._bothOut.position.x, _spawnFieldData._bothIn.position.x);
        float RandomXPos2 = Random.Range(_spawnFieldData._topIn.position.x, _spawnFieldData._topOut.position.x);

        float targetXPosition = Random.Range(0, 2) == 1 ? RandomXPos1 : RandomXPos2;

        BaseUnit unit = Instantiate(data._prefab, new(targetXPosition, targetYPosition), Quaternion.identity, _parent);

        unit.UpdateUnitData(data._data);
        _targetUnits.Add(unit);

    }

    public void ClearAllUnits()
    {
        if (_parent.childCount != 0)
            for (int i = 0; i < _parent.childCount; i++)
                Destroy(_parent.GetChild(i).gameObject);

        _targetUnits?.Clear();
        _targetUnits = new List<BaseUnit>();
    }
}
