using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SatelliteOverloadingScript : MonoBehaviour
{
    private GameObject[] satellites => GameObject.FindGameObjectsWithTag("satBody");
    private OrbitType currentOrbit = OrbitType.Arc;

    private void Start()
    {
        foreach (var sat in satellites)
        {
            sat.GetComponent<SateliteMoveTowardScript>().enabled = false;

            sat.GetComponent<SateliteMoveArcScript>().enabled = true;
            sat.GetComponent<SateliteMoveCircleScript>().enabled = false;
        }
    }

    private void Update() 
    {
        if (SatelliteOverloadTimerScript.IsCurrentOrbitType(OrbitType.RelToEllipse) && currentOrbit == OrbitType.Arc)
        {
            OverloadToCircle(3);
            MoveToNextOrbitTypeStart<SateliteMoveCircleScript>();
            currentOrbit = OrbitType.RelToEllipse;
        }
        else if (SatelliteOverloadTimerScript.IsCurrentOrbitType(OrbitType.Ellipse) && currentOrbit == OrbitType.RelToEllipse)
        {
            foreach (var sat in satellites)
            {
                sat.GetComponent<SateliteMoveCircleScript>().enabled = true;
                sat.GetComponent<SateliteMoveTowardScript>().enabled = false;
            }
            currentOrbit = OrbitType.Ellipse;
        }
    }

    private void OverloadToCircle(uint newCount)
    {
        ResetPhasesAndSpawnSatellites(newCount);
        foreach (var sat in satellites)
        {
            sat.GetComponent<SateliteMoveArcScript>().enabled = false;
            sat.GetComponent<SateliteMoveCircleScript>().enabled = false;
        }
    }

    private void ResetPhasesAndSpawnSatellites(uint newCount)
    {
        if (satellites.Count() >= newCount)
            throw new Exception("satellites.Count() >= newCount");
        var satellitesOrdered = satellites.OrderBy(i => i.GetComponent<SateliteMoveCircleScript>().phaseChange).ToArray();
        var newPhases = GetPhases(newCount);

        for (var i = 0; i < satellitesOrdered.Count(); i++)
        {
            satellitesOrdered[i].GetComponent<SateliteMoveCircleScript>().phaseChange = newPhases[i];
        }

        for (var i = satellitesOrdered.Count(); i< newCount; i++)
        {
            Camera.current.GetComponent<SatelliteGenerationScript>().Spawn(newPhases[i]);
        }
    }

    private float[] GetPhases(uint count)
    {
        var phases = new List<float>();
        for (var i = 0; i < count; i++)
        {
            phases.Add(i * 360 / count);
        }
        return phases.ToArray();
    }

    protected void MoveToNextOrbitTypeStart<T>() where T: MonoBehaviour, IMove
    {
        foreach (var sat in satellites)
        {
            var currentPosition = sat.transform.position;
            var nextOrbitTypeStart = sat.GetComponent<T>().CalculatePositionAsFigure();
            var sateliteMoveTowardScript = sat.GetComponent<SateliteMoveTowardScript>();

            sateliteMoveTowardScript.nextOrbitTypeStart = nextOrbitTypeStart;
            sateliteMoveTowardScript.enabled = true;
        }
    }
}
