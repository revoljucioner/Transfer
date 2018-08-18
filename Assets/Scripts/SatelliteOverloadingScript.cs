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
            sat.GetComponent<SateliteMoveArcScript>().enabled = true;
            sat.GetComponent<SateliteMoveCircleScript>().enabled = false;
        }
    }

    private void Update()
    {
        Console.WriteLine(Variables.Time);
        if (Variables.Time >= Variables.Tarc && currentOrbit == OrbitType.Arc)
        {
            OverloadToCircle(3);
        }
    }

    private void OverloadToCircle(uint newCount)
    {
        ResetPhasesAndSpawnSatellites(newCount);
        foreach (var sat in satellites)
        {
            sat.GetComponent<SateliteMoveArcScript>().enabled = false;
            sat.GetComponent<SateliteMoveCircleScript>().enabled = true;
        }
        currentOrbit = OrbitType.Ellipse;
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

    public enum OrbitType
    {
        Arc,
        Ellipse,
        Lemniscate,
        None
    }

    //protected override void SetPosition()
    //{
    //    var currentPosition = transform.position;
    //    transform.position = Vector3.MoveTowards(currentPosition, PointToRotate, Speed);
    //}
}
