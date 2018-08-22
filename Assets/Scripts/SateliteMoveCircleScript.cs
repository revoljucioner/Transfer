using Assets.Scripts;
using UnityEngine;

public class SateliteMoveCircleScript : MonoBehaviour, IMove
{
    private float Speed => Variables.SateliteMoveSpeed();
    private float RadiusOfRotate = 1.8f;
    private float orbit = 5.8f;
    public float phaseChange;
    public bool mobile = true;

    void Update()
    {
        SetPosition();
        ChangePhase();
    }

    protected void SetPosition()
    {
        if (mobile)
        {
            transform.position = CalculatePositionAsFigure();
        }
    }

    public Vector2 CalculatePositionAsFigure()
    {
        var x = SetPositionX();
        var y = SetPositionY();
        return new Vector2(x, y);
    }

    private float SetPositionX()
    {
        return RadiusOfRotate * Mathf.Cos((90 - phaseChange) * Mathf.Deg2Rad);
    }

    private float SetPositionY()
    {
        return 0.7f * RadiusOfRotate * Mathf.Sin((90 - phaseChange) * Mathf.Deg2Rad) + orbit;
    }

    private void ChangePhase()
    {
        phaseChange = phaseChange - Speed;
    }
}