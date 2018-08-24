using System;
using UnityEngine;
using static System.Math;

public class SateliteMoveArcScript : SateliteMoveBase
{
    public override float Speed => StartSpeed + 0.5f* (float)Atan(Variables.Time);
    //public override float Speed => StartSpeed;
    private float maxWeight = 1.2f;
    
    private void Start()
    {
        StartSpeed = Variables.SateliteStartArcSpeed;
    }

    void Update()
    {
        SetPosition();
        //
        Debug.Log($"Time: {Variables.Time} {Environment.NewLine} Speed: {Speed}");
    }

    public override Vector2 CalculatePositionAsFigure()
    {
        var x = (float)(maxWeight * Sin(Speed * Variables.Time + phaseChange));
        var y = (float)Sqrt(Pow(orbit, 2) - Pow(x, 2));
        //сейчас орбита идет по кругу в центре которого находится земная станция. закоменчено движение по орбите в центре которой центр Земли
        //var y = (float)Sqrt(Pow(orbit - EarthCenterY, 2) - Pow(x, 2)) + EarthCenterY;
        var position = new Vector2(x, y);
        return position;
    }
}