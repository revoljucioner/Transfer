using System;
using UnityEngine;
using static System.Math;

public class SateliteMoveCircleScript : MonoBehaviour
{
    private float Speed => Variables.SateliteMoveSpeed();
    private float maxWeight = 1.2f;
    private float RadiusOfRotate = 1.8f;
    private float orbit = 5.8f;
    public float phaseChange;
    //public OrbitType orbitType;
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
                transform.position = CalculatePositionAsCircle();
            //else if (orbitType == OrbitType.Ellipse)
                //transform.position = CalculatePositionAsArc();
            //else if (orbitType == OrbitType.Lemniscate)
                //transform.position = CalculatePositionAsArc();
            //else
            //    throw new Exception("Wrong value of OrbitType enum");
        }
    }

    public Vector2 CalculatePositionAsCircle()
    {
        var x = SetPositionX();
        var y = SetPositionY();
        return new Vector2(x,y);
    }

    //public enum OrbitType
    //{
    //    Arc,
    //    Ellipse,
    //    Lemniscate,
    //    None
    //}

    private float SetPositionX()
    {
        return RadiusOfRotate * Mathf.Cos((90 - phaseChange) * Mathf.Deg2Rad);
    }

    private float SetPositionY()
    {
        return 0.7f*RadiusOfRotate * Mathf.Sin((90 - phaseChange) * Mathf.Deg2Rad) + orbit;
    }

    private void ChangePhase()
    {
        phaseChange = phaseChange - Speed;
    }
}