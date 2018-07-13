using System.Collections;
using System.Linq;
using UnityEngine;

public class WaveGeneration : MonoBehaviour {

    public GameObject wave;
    private GameObject[] satReflectors => GameObject.FindGameObjectsWithTag("satReflector");

    void Start ()
    {
        //StartCoroutine(Spawn());	
        Spawn(1);
	}
	
    //IEnumerator Spawn()
    //{
    //    while(true)
    //    {
    //        var satReflectorPosition = satRef.transform.position;

    //        Instantiate(wave, new Vector2(satReflectorPosition.x, satReflectorPosition.y), Quaternion.identity);
    //        yield return new WaitForSeconds(3);
    //    }
    //}

    public void Spawn(float scale)
    {
        var transmiter = satReflectors[Random.Range(0, satReflectors.Length)];
        var satelitesWithoutTransmiter = satReflectors.Except(new[] { transmiter }).ToArray();
        var receiver = satelitesWithoutTransmiter[Random.Range(0, satelitesWithoutTransmiter.Length)];

        var waveTriggerScript = wave.GetComponent<WaveTriggersScript>();
        waveTriggerScript.transmiter = transmiter;
        waveTriggerScript.receiver = receiver;

        var transmiterPosition = transmiter.transform.position;
        //
        wave.GetComponent<BoxCollider2D>().size = new Vector3(scale, scale, scale);
        wave.transform.localScale = new Vector3(scale, scale, scale);
        //
        Instantiate(wave, new Vector2(transmiterPosition.x, transmiterPosition.y), Quaternion.identity);
    }
}
