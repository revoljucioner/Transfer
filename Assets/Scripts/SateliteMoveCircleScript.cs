using UnityEngine;

public class SateliteMoveCircleScript : SateliteMoveBase
{
    private float Speed => Variables.SateliteMoveSpeed();
    private float RadiusOfRotate = 1.8f;

    private void Start()
    {
        orbit = 5.8f;
    }
    void Update()
    {
        SetPosition();
        ChangePhase();
    }

    public override Vector2 CalculatePositionAsFigure()
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