using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : BaseUnit
{
    [SerializeField] private Transform _targetPos;
    //[SerializeField] private Transform _targetTransform;

    private bool _isDone = false;
    private float progress = 1f;

    private Vector2 _targetPosition = new Vector2(0, 0);

    float DirectionX;
    float DirectionY;
    float Dist;
    float h;
    Vector3 Direction = new Vector3();

    private void Awake()
    {
        _animator.SetBool("Moved", true);
        //_targetPosition = _targetTransform.position;
        Dist = Distance(_targetPos.position, _targetPosition);


        h = (_speed * 0.01f * Time.fixedDeltaTime) / Dist;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bonk");
        if (collision.collider.tag == "Player")
        {
            Attack(collision);
            _animator.SetBool("Moved", false);
            _animator.SetBool("Delay", true);
            _isDone = true;
        }
            
    }

    private void FixedUpdate()
    {
        if (_isDone || !_isUpdated) return;
       
        progress -= h;
        DirectionX = Mathf.Lerp(_targetPosition.x, _targetPos.position.x, progress);
        DirectionY = Mathf.Lerp(_targetPosition.y, _targetPos.position.y, progress);
        _targetPos.position = new Vector3(DirectionX, DirectionY, 0f);
        if (progress <= 0) _isDone = true;
    }

    public static float Distance(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x)
                         + (p1.y - p2.y) * (p1.y - p2.y));
    }

    
}
