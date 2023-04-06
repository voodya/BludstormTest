using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerControl : BaseUnit
{
    [SerializeField] private Bullet _firebal;
    [SerializeField] private GameController _controller;
    [SerializeField] private Image _hpBar;
    private List<Bullet> _bulletsPull = new List<Bullet>();
    private BaseUnit _targetUnit;
    private Bullet _targetBullet;
    private float _defaultHP;

    public BaseUnit TargetUnit { get => _targetUnit == null ? null : _targetUnit;}

    public static Action Damaged;
    
    private void Awake()
    {
        _defaultHP = _hitpoint;
        _hpBar.fillAmount = 1;
        StartCoroutine(UpdateEnemys());
        Damaged += SetDamageToTargetEnemy;
        DamageGetted += UpdateBar;
    }

    private void UpdateBar()
    {
        _hpBar.fillAmount = _hitpoint/_defaultHP;
    }

    public void ForseUpdate()
    {
        StopAllCoroutines();
        _hpBar.fillAmount = 1;
        StartCoroutine(UpdateEnemys());
    }
    

    public override void KillUnit()
    {
        base.KillUnit();
        _targetUnit = null;
        StopAllCoroutines();
        _controller.GameOver();
    }

    private void SetDamageToTargetEnemy()
    {
        _targetUnit?.GetDamage(_damage);
    }

    private IEnumerator UpdateEnemys()
    {
        while (true)
        {
            int targettBulletIndex = 0;
            yield return new WaitForSeconds(0.5f);
            List<BaseUnit> units = FindObjectInRadius("Enemy", _radius);
            foreach (BaseUnit unit in units)
            {
                if (!unit._isTarget && _targetUnit == null)
                {
                    unit._isTarget = true;
                    _targetUnit = unit;
                    break;
                }  
            }
            if (_targetUnit == null) continue;
            //if (_bulletsPull.Count < 10)
            //{
                Bullet _newBullet = Instantiate(_firebal);
                _bulletsPull.Add(_newBullet);
              
                _newBullet.gameObject.SetActive(true);
                _newBullet.Attack(transform, _targetUnit.transform);
            //}
            //else
            //{
            //    foreach(Bullet a in _bulletsPull)
            //    {
            //        if (a._inUse)
            //            continue;
            //        else
            //        {
            //            a.transform.position = transform.position;
            //            a.Attack(transform, _targetUnit.transform);
            //            a.gameObject.SetActive(true);
            //            break;
            //        }
            //            
            //    }
            //}
            
            
            
        }
    }

}
