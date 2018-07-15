using System.Linq;
using UnityEngine;

public class WaveGeneration : MonoBehaviour
{

    public GameObject wave;
    private GameObject[] satReflectors => GameObject.FindGameObjectsWithTag("satReflector");

    void Start()
    {
        Spawn(new Vector3(0.7f, 0.7f));
    }

    public void Spawn(Vector2 newScale)
    {
        var transmiter = satReflectors[Random.Range(0, satReflectors.Length)];
        var satelitesWithoutTransmiter = satReflectors.Except(new[] { transmiter }).ToArray();
        var receiver = satelitesWithoutTransmiter[Random.Range(0, satelitesWithoutTransmiter.Length)];

        var transmiterPosition = transmiter.transform.position;

        var _wave = Instantiate(wave, new Vector2(transmiterPosition.x, transmiterPosition.y), Quaternion.identity);

        var waveTriggerScript = _wave.GetComponent<WaveTriggersScript>();
        waveTriggerScript.transmiter = transmiter;
        waveTriggerScript.receiver = receiver;

        _wave.transform.localScale = newScale;

        Vector2 S = _wave.GetComponent<SpriteRenderer>().sprite.bounds.size;
        //var S = _wave.GetComponent<BoxCollider2D>().size * newScale;
        _wave.GetComponent<BoxCollider2D>().size = S;
    }
}
