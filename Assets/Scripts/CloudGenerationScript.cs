using System.Linq;
using UnityEngine;

public class CloudGenerationScript : MonoBehaviour
{

    private GameObject[] ExistingClouds => GameObject.FindGameObjectsWithTag("cloud");

    void Start()
    {
        //Spawn(new Vector3(0.7f, 0.7f));
    }

    public void Update()
    {
        var goneBehindScreenClouds = ExistingClouds.Where(i => i.GetComponent<BackgroundMoveScript>().Gone);

        foreach (var goneCloud in goneBehindScreenClouds)
        {           
            SpawnCloud(1.5f * Variables.CameraWidth());
            //
            goneCloud.transform.position = new Vector3(5,5,0);

            //
            //Destroy(goneCloud);
        }
    }

    private void SpawnCloud(float position)
    {
        //Instantiate(randomCloud, new Vector2(transmiterPosition.x, transmiterPosition.y), Quaternion.identity);
    }
}
