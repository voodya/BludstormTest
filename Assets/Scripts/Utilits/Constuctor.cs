using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constuctor : MonoBehaviour
{
    public Transform _topOut;
    public Transform _bothOut;
    public Transform _bothIn;
    public Transform _topIn;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_topOut.position, new(_bothOut.position.x, _topOut.position.y));
        Gizmos.DrawLine(_topOut.position, new(_topOut.position.x, _bothOut.position.y));
        Gizmos.DrawLine(_bothOut.position, new(_bothOut.position.x, _topOut.position.y));
        Gizmos.DrawLine(_bothOut.position, new(_topOut.position.x, _bothOut.position.y));

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_topIn.position, new(_bothIn.position.x, _topIn.position.y));
        Gizmos.DrawLine(_topIn.position, new(_topIn.position.x, _bothIn.position.y));
        Gizmos.DrawLine(_bothIn.position, new(_bothIn.position.x, _topIn.position.y));
        Gizmos.DrawLine(_bothIn.position, new(_topIn.position.x, _bothIn.position.y));
    }
}
