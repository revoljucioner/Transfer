using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{
    //private float Speed = Variables.BackgroundMoveSpeed();
    public float Speed;
    public float Phase { get; set; }
    private float RadiusOfRotate = Variables.CloudsOrbit() - Variables.EarthCenter.y;

    public bool Gone => (Phase < -Variables.CloudPhaseDestroy);

    private void Start()
    {
    }

    void Update()
    {
        if (Gone)
        {
            Phase = Variables.CloudPhaseDestroy;
            GetComponent<CloudSpriteScript>().ChangeSprite();
        }
        else
        {
            ChangePhase();
        }
        SetPositionX();
        SetPositionY();
        SetRotate();
    }

    private void SetPositionX()
    {
        var x = RadiusOfRotate * Mathf.Cos((90- Phase)* Mathf.Deg2Rad);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void SetPositionY()
    {
        var y = RadiusOfRotate * Mathf.Sin((90 - Phase) * Mathf.Deg2Rad) + Variables.EarthCenter.y;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

    }

    private void ChangePhase()
    {
        Phase = Phase - Speed;
    }

    private void SetRotate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Variables.EarthCenter - transform.position);
    }
}