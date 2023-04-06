using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] public Animator _animator;
    [SerializeField] protected float _hitpoint = 10f;
    [SerializeField] protected float _damage = 2f;
    [SerializeField] protected int _delay = 1000;
    [SerializeField] public float _radius = 2f;
    [SerializeField] protected float _speed = 0.5f;
    [SerializeField] public bool _isDamagetToSpell;
    [SerializeField] public bool _isEnemy;
    [SerializeField] public bool _isTarget = false;
    [SerializeField] protected bool _isUpdated = false;

    private BaseUnit _unitTemp;
    protected Action DamageGetted;

    public BaseUnit(float HP, float DMG, int Delay, bool isEnemy, float rad)
    {
        _hitpoint = HP;
        _damage = DMG;
        _delay = Delay;
        _radius = rad;
        _isEnemy = isEnemy;
    }

    public BaseUnit(BaseUnitData data)
    {
        _hitpoint = data._hitpoint;
        _damage = data._damage;
        _delay = data._delay;
        _radius = data._radius;
        _isEnemy = data._isEnemy;
        _speed = data._speed;
        _isTarget = data._isTarget;
        
    }

    public BaseUnit()
    {

    }

    public void UpdateUnitData(BaseUnitData data)
    {
        _hitpoint = data._hitpoint;
        _damage = data._damage;
        _delay = data._delay;
        _radius = data._radius;
        _isEnemy = data._isEnemy;
        _speed = data._speed * 0.01f;
        _isTarget = data._isTarget;
        _isUpdated = true;
    }

    protected async void Attack(Collision2D player)
    {
        if (gameObject == null || player == null) return;
        _animator?.SetTrigger("Attack");
        Debug.Log("Attacked");
        Debug.Log(player.gameObject.name);
        if(_unitTemp == null) _unitTemp = player.gameObject.GetComponent<BaseUnit>();
        _unitTemp.GetDamage(_damage);

        await Task.Delay(_delay); 
        if(Application.isPlaying && gameObject != null && player.gameObject.activeSelf) Attack(player);
    }

    public void GetDamage(float Damage)
    {
        _hitpoint -= Damage;
        DamageGetted?.Invoke();
        //Debug.Log($"Target {gameObject.name} hitpoint is {_hitpoint}");
        if (_hitpoint < 0) KillUnit();
    }

    public virtual void KillUnit()
    {
       if(_isEnemy && gameObject != null) Destroy(gameObject);
       else gameObject.SetActive(false);
    }

    public BaseUnit FindObjectInRadius()
    {
        Collider2D[] objs =  Physics2D.OverlapCircleAll(transform.position, 3f);
        Debug.Log(objs.Length);
        foreach(var obj in objs)
        {
            if (obj.tag == "Player")
            {
                return obj.GetComponent<BaseUnit>();
            }
            else
                return null;
        }
        return null;
    }

    public BaseUnit FindMinDistantionObjectInRadius(float radius)
    {
        if(transform == null) return null;
        Collider2D[] objs = Physics2D.OverlapCircleAll(transform.position, radius);
        List<Transform> contains = new List<Transform>();
        foreach (var obj in objs)
        {
            if (obj.tag == "Enemy")
            {
                contains.Add(obj.transform);
            }
        }
        Transform MinDist = null;
        float maxDist = 100000;
        foreach (var obj in contains)
        {
            float TargetDist = EnemyDefault.Distance(transform.position, obj.position);
            if (TargetDist < maxDist && !obj.GetComponent<BaseUnit>()._isDamagetToSpell)
            {
                maxDist = TargetDist;
                MinDist = obj;
            }   
        }

        return MinDist?.GetComponent<BaseUnit>();
    }

    public List<BaseUnit> FindObjectInRadius(string Tag, float rad)
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(transform.position, rad);
        List<BaseUnit> units = new List<BaseUnit>();
        foreach (var obj in objs)
        {
            if (obj.tag == Tag)
            {
                units.Add(obj.GetComponent<BaseUnit>());
            }
        }
        return units;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

[System.Serializable]
public class BaseUnitData
{
    public string _name;
    public float _hitpoint = 10f;
    public float _damage = 2f;
    public int _delay = 1000;
    public float _radius = 4f;
    public float _speed = 0.5f;
    public bool _isAlive;
    public bool _isEnemy;
    public bool _isTarget = false;
}

