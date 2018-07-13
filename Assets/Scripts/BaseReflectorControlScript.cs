using UnityEngine;

public class BaseReflectorControlScript : MonoBehaviour
{
    // При введение сенсорного управления это поле удалить
    public int speedOfRotation = 1;

    public Vector3 PivotPoint;
    private readonly float minMaxAngle = 25;

    private void Start()
    {
        SetPivotPoint();
    }

    void Update()
    {
        Control();
    }

    private bool IsAngleInSector(float angle)
    {
        var isAngleGreaterThenMinAngle = angle > 360 - minMaxAngle;
        var isAngleLessThenMinAngle = angle < minMaxAngle;
        return isAngleGreaterThenMinAngle || isAngleLessThenMinAngle;
    }

    private void Control()
    {
        float inputX = Input.GetAxis("Horizontal");

        float deltaAngle = (-1) * speedOfRotation * inputX;
        float newAngleZ = transform.localEulerAngles.z + deltaAngle;

        if (IsAngleInSector(newAngleZ))
        {
            transform.RotateAround(PivotPoint, Vector3.forward, deltaAngle);
        }
    }

    private void SetPivotPoint()
    {
        var size = GetComponent<BoxCollider2D>().bounds.size;
        PivotPoint = new Vector3(
            transform.position.x,
            transform.position.y+ size.y/2,
            transform.position.z);
    }
}