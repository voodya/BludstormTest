using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSpell : MonoBehaviour
{
    public float _delay;
    public string _spellName;
    public bool _inCD = false;
    [SerializeField] private Image _filledImage;

    public virtual void Cast()
    {
        Debug.Log($"Cast {_spellName}");
    }

    protected virtual IEnumerator DelayCalc()
    {
        _inCD = true;
        _filledImage.fillAmount = 0;
        _filledImage.gameObject.SetActive(true);
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(_delay / 60);
            _filledImage.fillAmount += 1f/60f;
        }
        _filledImage.gameObject.SetActive(false);
        _inCD = false;
    }
}
