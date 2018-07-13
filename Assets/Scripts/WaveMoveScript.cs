using UnityEngine;

public class WaveMoveScript : MoveObject
{
    private float Speed ;

    public bool IsReflect = false;

    void Start()
    {
        SetRotate();
        Speed = Variables.WaveMoveSpeed();
    }

    void Update()
    {
        SetPosition();
    }

    protected override void SetPosition()
    {
        var currentPosition = transform.position;
        transform.position = Vector3.MoveTowards(currentPosition, PointToRotate, Speed);
    }
}
