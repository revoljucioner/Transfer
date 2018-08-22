using System;
using UnityEngine;

public class SateliteMoveTowardScript : MonoBehaviour
{
    private float Speed => Variables.SpeedOfRelocation;
    public Vector3 nextOrbitTypeStart;

    void Update()
    {
        MoveTowards();
    }

    protected void MoveTowards()
    {
        if (nextOrbitTypeStart.Equals(null))
            throw new Exception("Vector3 nextOrbitTypeStart was not set");
        transform.position = Vector3.MoveTowards(transform.position, nextOrbitTypeStart, Speed);
    }
}