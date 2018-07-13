using System;
using UnityEngine;

public abstract class MoveObject : MonoBehaviour
{
    public Vector2 PointToRotate = Vector2.zero;

    protected Single Time => Variables.Time;

    protected abstract void SetPosition();

    public void SetRotate()
    {
        Vector2 direction = new Vector2(PointToRotate.x - transform.position.x, PointToRotate.y - transform.position.y);
        transform.up = -direction;
    }
}
