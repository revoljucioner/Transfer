using Assets.Scripts;
using UnityEngine;

public abstract class SateliteMoveBase : MonoBehaviour, IMove
{
    private float Speed;
    public float orbit;
    public float phaseChange;
    public bool mobile = true;

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