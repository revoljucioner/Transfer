using System;
using System.Linq;
using UnityEngine;

public class CloudGenerationScript : MonoBehaviour
{
    private GameObject[] ExistingClouds => GameObject.FindGameObjectsWithTag("cloud");
    public GameObject cloud;
    public Sprite[] sprites;

    void Start()
    {
        //Spawn(new Vector3(0.7f, 0.7f));
    }

    public void Update()
    {
        var goneBehindScreenClouds = ExistingClouds.Where(i => i.GetComponent<BackgroundMoveScript>().Gone);

        foreach (var goneCloud in goneBehindScreenClouds)
        {
            //SpawnCloud();
            MoveCloud(goneCloud);
            ChangeSprite(goneCloud);
        }
    }

    //TODO: Instantiate random cloud
    //private void SpawnCloud()
    //{
    //    ChangeSprite(cloud);
    //    Instantiate(cloud, GetCloudPositionByX(positionToDestroy), Quaternion.identity);
    //}

    private void MoveCloud(GameObject cloud)
    {
        cloud.GetComponent<BackgroundMoveScript>().Phase = Variables.CloudPhaseDestroy;
    }

    private void ChangeSprite(GameObject cloud)
    {
        var sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        cloud.GetComponent<SpriteRenderer>().sprite = sprite;
        cloud.transform.localScale = new Vector3(-0.1f, -0.1f, -0.1f);
    }
}
