using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{
    private float Speed => Variables.BackgroundMoveSpeed();
    private Vector3 PivotPoint = Variables.EarthCenter;

    public bool Gone => (transform.position.x < -1.5*Variables.CameraWidth());

    void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        float deltaAngle = Speed ;
        transform.RotateAround(PivotPoint, Vector3.forward, deltaAngle);
    }
}