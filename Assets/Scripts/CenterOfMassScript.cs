using UnityEngine;

public class CenterOfMassScript : MonoBehaviour
{
    public Vector3 CenterOfMass;

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = CenterOfMass;
    }  
}