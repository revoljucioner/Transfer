using Assets.Scripts;
using UnityEngine;

public abstract class SateliteMoveBase : MonoBehaviour, IMove
{
    //protected float Speed;
    public float orbit;
    public float phaseChange;
    protected float StartSpeed;
    public bool mobile = true;

    public abstract float Speed { get; }

    void Update()
    {
        SetPosition();
    }

    protected void SetPosition()
    {
        if (mobile)
        {
            transform.position = CalculatePositionAsFigure();
        }
    }

    public abstract Vector2 CalculatePositionAsFigure();
}