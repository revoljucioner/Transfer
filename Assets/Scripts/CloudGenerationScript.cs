using System;
using System.Linq;
using UnityEngine;

public class CloudGenerationScript : MonoBehaviour
{
    private GameObject[] ExistingClouds => GameObject.FindGameObjectsWithTag("cloud");
    public GameObject cloud;
    public Sprite[] sprites;
    private float SpeedDistantClouds = Variables.BackgroundDistantMoveSpeed();
    private float SpeedNearClouds = Variables.BackgroundNearMoveSpeed();
    private float SizeDistantClouds = Variables.DistantCloudsSize();
    private float SizeNearClouds = Variables.NearCloudsSize();

    void Start()
    {
        SpawnClouds("NearClouds", 4, SpeedNearClouds, SizeNearClouds);
        SpawnClouds("Background", 5, SpeedDistantClouds, SizeDistantClouds);
    }

    public void Update()
    {
        var goneBehindScreenClouds = ExistingClouds.Where(i => i.GetComponent<BackgroundMoveScript>().Gone);

        foreach (var goneCloud in goneBehindScreenClouds)
        {
            //SpawnCloud();
            MoveCloud(goneCloud, Variables.CloudPhaseDestroy);
            ChangeSprite(goneCloud);
        }
    }

    private void SpawnCloud(string sortingLayerName, float phase, float speed, float size)
    {
        var newCloud = Instantiate(cloud);
        var backgroundMoveScriptComponent = newCloud.GetComponent<BackgroundMoveScript>();
        backgroundMoveScriptComponent.Phase = phase;
        backgroundMoveScriptComponent.Speed = speed;
        newCloud.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
        ChangeSprite(newCloud, size);
    }

    private void SpawnClouds(string sortingLayerName, int count, float speed, float size)
    {
        for (var i = 0; i < count; i++)
        {
            var phase = -Variables.CloudPhaseDestroy + 2*i* Variables.CloudPhaseDestroy/(count);
            SpawnCloud(sortingLayerName, phase, speed, size);
        }
    }

    private void MoveCloud(GameObject cloud, float phase)
    {
        cloud.GetComponent<BackgroundMoveScript>().Phase = phase;
    }

    private void ChangeSprite(GameObject cloud)
    {
        var sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        cloud.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void ChangeSprite(GameObject cloud, float size)
    {
        ChangeSprite(cloud);
        cloud.transform.localScale = new Vector3(size, size, size);
    }
}
