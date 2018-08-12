using UnityEngine;

public class CloudGenerationScript : MonoBehaviour
{
    public GameObject cloud;
    private float SpeedDistantClouds = Variables.BackgroundDistantMoveSpeed();
    private float SpeedNearClouds = Variables.BackgroundNearMoveSpeed();
    private float SizeDistantClouds = Variables.DistantCloudsSize();
    private float SizeNearClouds = Variables.NearCloudsSize();

    void Start()
    {
        SpawnClouds("NearClouds", 8, SpeedNearClouds, SizeNearClouds);
        SpawnClouds("Background", 5, SpeedDistantClouds, SizeDistantClouds);
    }

    public void Update()
    {
    }

    private void SpawnCloud(string sortingLayerName, float phase, float speed, float size)
    {
        var newCloud = Instantiate(cloud);
        var backgroundMoveScriptComponent = newCloud.GetComponent<BackgroundMoveScript>();
        backgroundMoveScriptComponent.Phase = phase;
        backgroundMoveScriptComponent.Speed = speed;
        newCloud.GetComponent<SpriteRenderer>().sortingLayerName = sortingLayerName;
        newCloud.GetComponent<CloudSpriteScript>().ChangeSprite(size);
    }

    private void SpawnClouds(string sortingLayerName, int count, float speed, float size)
    {
        for (var i = 0; i < count; i++)
        {
            var phase = -Variables.CloudPhaseDestroy + 2*i* Variables.CloudPhaseDestroy/(count);
            SpawnCloud(sortingLayerName, phase, speed, size);
        }
    }
}
