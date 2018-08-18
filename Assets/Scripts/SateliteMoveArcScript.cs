using System;
using UnityEngine;
using static System.Math;

public class SateliteMoveArcScript : MonoBehaviour
{
    private float Speed => Variables.SateliteMoveSpeed();
    private float maxWeight = 1.2f;
    public float orbit;
    public float phaseChange;
    //public OrbitType orbitType;
    public bool mobile = true;

    void Update()
    {
        SetPosition();
    }

    protected void SetPosition()
    {
        if (mobile)
        {
                transform.position = CalculatePositionAsArc();
            //else if (orbitType == OrbitType.Ellipse)
                //transform.position = CalculatePositionAsArc();
            //else if (orbitType == OrbitType.Lemniscate)
                //transform.position = CalculatePositionAsArc();
            //else
            //    throw new Exception("Wrong value of OrbitType enum");
        }
    }

    private Vector2 CalculatePositionAsArc()
    {
        var x = (float)(maxWeight * Sin(Speed * Variables.Time + phaseChange));
        var y = (float)Sqrt(Pow(orbit, 2) - Pow(x, 2));
        //сейчас орбита идет по кругу в центре которого находится земная станция. закоменчено движение по орбите в центре которой центр Земли
        //var y = (float)Sqrt(Pow(orbit - EarthCenterY, 2) - Pow(x, 2)) + EarthCenterY;
        var position = new Vector2(x, y);
        return position;
    }

    //public enum OrbitType
    //{
    //    Arc,
    //    Ellipse,
    //    Lemniscate,
    //    None
    //}
}