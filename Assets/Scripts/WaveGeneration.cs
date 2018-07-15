using System.Linq;
using UnityEngine;

public class WaveGeneration : MonoBehaviour
{

    public GameObject wave;
    private GameObject[] satReflectors => GameObject.FindGameObjectsWithTag("satReflector");

    void Start()
    {
        Spawn(1);
    }

    public void Spawn(float scale)
    {
        var transmiter = satReflectors[Random.Range(0, satReflectors.Length)];
        var satelitesWithoutTransmiter = satReflectors.Except(new[] { transmiter }).ToArray();
        var receiver = satelitesWithoutTransmiter[Random.Range(0, satelitesWithoutTransmiter.Length)];

        var transmiterPosition = transmiter.transform.position;

        var _wave = Instantiate(wave, new Vector2(transmiterPosition.x, transmiterPosition.y), Quaternion.identity);

        var waveTriggerScript = _wave.GetComponent<WaveTriggersScript>();
        waveTriggerScript.transmiter = transmiter;
        waveTriggerScript.receiver = receiver;

        _wave.transform.localScale = scale * _wave.transform.localScale;

        Vector2 S = _wave.GetComponent<SpriteRenderer>().sprite.bounds.size;
        _wave.GetComponent<BoxCollider2D>().size = S;

    }
}
