using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ThunderSpell : BaseSpell
{
    [SerializeField] private int _jumpCount;
    [SerializeField] private float _damageBase;
    [SerializeField] private float _damageReduct;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private GameObject _thunder;

    private BaseUnit _unit;
    
    public override async void Cast()
    {
        if (_inCD) return;
        StartCoroutine(DelayCalc());
        base.Cast();
        _thunder.gameObject.SetActive(true);
        _unit = _playerControl.TargetUnit;
        for (int i = 0; i < _jumpCount; i++)
        {
            try
            {
                if (i == 0)
                {
                    _unit.GetDamage(_damageBase);
                    _unit._isDamagetToSpell = true;
                    _unit = _unit.FindMinDistantionObjectInRadius(_playerControl._radius);
                }

                else
                {
                    _unit = _unit.FindMinDistantionObjectInRadius(_playerControl._radius);
                    if (_unit == null)
                    {
                        _thunder.gameObject.SetActive(false);
                        return;
                    }
                    _unit.GetDamage(_damageBase * Mathf.Pow((1f - _damageReduct), i));
                    _unit._isDamagetToSpell = true;
                }
                if (_unit != null)
                    _thunder.transform.position = _unit.transform.position;
                if (Application.isPlaying) await Task.Delay(100);
            }
            catch
            {
                Debug.Log("No enemys");
                break;
            }
        }
        _thunder.gameObject.SetActive(false);
    }

    private void Awake()
    {
        Button Btn = GetComponent<Button>();
        Btn.onClick.AddListener(Cast);
    }
}
