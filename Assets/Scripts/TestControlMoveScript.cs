using UnityEngine;

public class TestControlMoveScript : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 speed = new Vector2(10,10);

    private void Start()
    {
    }

    private void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(speed.x*inputX, speed.y*inputY);
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}