using UnityEngine;

public class SateliteReflectorRotateScript : MonoBehaviour
{
    private Vector2 PointToRotate = Vector2.zero;

    public void SetRotate()
    {
        Vector2 direction = new Vector2(PointToRotate.x - transform.position.x, PointToRotate.y - transform.position.y);
        transform.up = -direction;
    }

    void Update()
    {
        SetRotate();
    }
}