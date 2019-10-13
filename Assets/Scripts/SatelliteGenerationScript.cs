using UnityEngine;

public class SatelliteGenerationScript : MonoBehaviour
{
    public GameObject Satellite;

    private void Awake()
    {
        //Spawn(0, OrbitType.Arc, 7);
        //Spawn(3.14f, OrbitType.Arc, 5);
        //Spawn(0, 7);
        //Spawn(3.14f, 5);
    }

    private void Update()
    {

    }

    //public GameObject Spawn(float phaseChange, OrbitType orbitType, float orbit)
    //{
    //    var _satellite = Spawn(phaseChange, orbitType);
    //    _satellite.GetComponent<SateliteMoveScript>().orbit = orbit;
    //    return _satellite;
    //}

    //public GameObject Spawn(float phaseChange, OrbitType orbitType)
    //{
    //    var _satellite = Instantiate(Satellite);
    //    _satellite.GetComponent<SateliteMoveScript>().phaseChange = phaseChange;
    //    _satellite.GetComponent<SateliteMoveScript>().orbitType = orbitType;
    //    return _satellite;
    //}

    public GameObject Spawn()
    {
        var _satellite = Instantiate(Satellite);
        return _satellite;
    }

    public GameObject Spawn(float phaseChange, float orbit)
    {
        var _satellite = Spawn(phaseChange);
        _satellite.GetComponent<SateliteMoveArcScript>().orbit = orbit;
        return _satellite;
    }

    public GameObject Spawn(float phaseChange)
    {
        var _satellite = Instantiate(Satellite);
        _satellite.GetComponent<SateliteMoveArcScript>().phaseChange = phaseChange;
        _satellite.GetComponent<SateliteMoveCircleScript>().phaseChange = phaseChange;
        return _satellite;
    }
}
