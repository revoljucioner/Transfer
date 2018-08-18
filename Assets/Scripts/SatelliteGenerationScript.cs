using System.Linq;
using UnityEngine;

public class SatelliteGenerationScript : MonoBehaviour
{
    public GameObject Satellite;

    private void Awake()
    {
        Spawn(0, 7);
        Spawn(3.14f, 5);
    }

    public void Spawn(float phaseChange, float orbit)
    {
        var _satellite = Instantiate(Satellite);
        var sateliteMoveComponent = _satellite.GetComponent<SateliteMoveScript>();
        sateliteMoveComponent.orbit = orbit;
        sateliteMoveComponent.phaseChange = phaseChange;
    }
}
