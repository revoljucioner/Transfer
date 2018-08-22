using Assets.Scripts;
using UnityEngine;
using static System.Math;

public class SateliteMoveArcScript : MonoBehaviour, IMove
{
    private float Speed => Variables.SateliteMoveSpeed();
    private float maxWeight = 1.2f;
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

    public Vector2 CalculatePositionAsFigure()
    {
        var x = (float)(maxWeight * Sin(Speed * Variables.Time + phaseChange));
        var y = (float)Sqrt(Pow(orbit, 2) - Pow(x, 2));
        //сейчас орбита идет по кругу в центре которого находится земная станция. закоменчено движение по орбите в центре которой центр Земли
        //var y = (float)Sqrt(Pow(orbit - EarthCenterY, 2) - Pow(x, 2)) + EarthCenterY;
        var position = new Vector2(x, y);
        return position;
    }
}