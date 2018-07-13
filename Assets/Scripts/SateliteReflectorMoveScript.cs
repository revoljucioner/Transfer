using UnityEngine;
using static System.Math;

public class SateliteReflectorMoveScript : MoveObject
{
    private float Speed => Variables.SateliteMoveSpeed();
    private float maxWeight = 1.2f;
    public float maxHeight = 7f;
    public float phaseChange;

    void Update()
    {
        SetPosition();
        SetRotate();
    }

    protected override void SetPosition()
    {
        transform.position = CalculatePositionAsArc();
    }

    private Vector2 CalculatePositionAsArc()
    {
        var x = (float)(maxWeight * Sin(Speed * Time + phaseChange));
        var y = (float)Sqrt(Pow(maxHeight, 2) - Pow(x, 2));
        var position = new Vector2(x, y);
        return position;
    }

}