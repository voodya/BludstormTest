using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

internal class Bullet: MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    private Transform _startPosition;
    private Transform _endPosition;
    private bool _isDone = false;
    public bool _inUse = false;

    float progress = 1;
    float h;

    public void Attack(Transform end, Transform start)
    {
        _startPosition = start;
        _endPosition = end;
        _inUse = true;
        h = (_speed) / EnemyDefault.Distance(end.position, start.position);
    }

    private void FixedUpdate()
    {
        if (_isDone || _startPosition == null || _endPosition == null) return;

        progress -= h;
        float DirectionX = Mathf.Lerp(_startPosition.position.x, _endPosition.position.x, progress);
        float DirectionY = Mathf.Lerp(_startPosition.position.y, _endPosition.position.y, progress);
        transform.position = new Vector3(DirectionX, DirectionY, 0f);
        if (progress <= 0)
        {
            PlayerControl.Damaged?.Invoke();
            _inUse = false;
            _isDone = true;
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}