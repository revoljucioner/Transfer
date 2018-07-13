using System;
using UnityEngine;

public class WaveTriggersScript : MonoBehaviour
{
    private bool IsReflect = false;
    private WaveMoveScript WaveMoveScript => GetComponent<WaveMoveScript>();
    private GameObject MainCamera => GameObject.FindGameObjectWithTag("MainCamera");
    private ScoreScript ScoreScript => MainCamera.GetComponent<ScoreScript>();
    private WaveGeneration WaveGenerationScript => MainCamera.GetComponent<WaveGeneration>();


    public GameObject transmiter;
    public GameObject receiver;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("baseReflector"))
        {
            OnTriggerBase(collider.gameObject);
        }
        else if (collider.gameObject == receiver)
        {
            OnTriggerSatelite(collider.gameObject);
        }
    }

    private void OnTriggerBase(GameObject baseReflectorObject)
    {
        if (!IsReflect)
        {
            var baseEulerAngleZ = baseReflectorObject.transform.eulerAngles.z;

            var waveEulerAngle = transform.eulerAngles.z;
            var newAngleZ = 90 + 2 * baseEulerAngleZ - waveEulerAngle;
            var newAngleZRad = Helper.DegreesToRad(newAngleZ);
            var newXtoMove = Screen.height / Math.Tan(newAngleZRad);


            this.transform.position = baseReflectorObject.GetComponent<BaseReflectorControlScript>().PivotPoint;

            WaveMoveScript.PointToRotate = new Vector3((float)newXtoMove, Screen.height, 0);
            WaveMoveScript.SetRotate();
        }
        IsReflect = true;
    }

    private void OnTriggerSatelite(GameObject satReflectorObject)
    {
        if (IsReflect)
        {
            ScoreScript.AddScorePoint();
            Destroy(gameObject);
            var nextWaveWidht = GetNextWaveWidht(satReflectorObject);
            //
            Debug.Log($"nextWaveWidht = {nextWaveWidht}");
            //
            WaveGenerationScript.Spawn(nextWaveWidht);     
        }
    }

    private float GetNextWaveWidht(GameObject satReflectorObject)
    {
        var satColider = satReflectorObject.GetComponent<BoxCollider2D>();
        var waveColider = GetComponent<BoxCollider2D>();

        var widthWave = waveColider.size.x*transform.localScale.x;
        var centerXWave = transform.position.x;
        var leftBorderWave = centerXWave - widthWave / 2;
        var rightBorderWave = centerXWave + widthWave / 2;

        var widthSat = satColider.size.x * satReflectorObject.transform.localScale.x;
        var centerXSat = satReflectorObject.transform.position.x;
        var leftBorderSat = centerXSat - widthSat / 2;
        var rightBorderSat = centerXSat + widthSat / 2;
        //
        Debug.Log($"-------");
        Debug.Log($"widthWave :{widthWave}");
        Debug.Log($"centerXWave :{centerXWave}");
        Debug.Log($"leftBorderWave :{leftBorderWave}");
        Debug.Log($"rightBorderWave :{rightBorderWave}");
        Debug.Log($"-------");
        Debug.Log($"widthSat :{widthSat}");
        Debug.Log($"centerXSat :{centerXSat}");
        Debug.Log($"leftBorderSat :{leftBorderSat}");
        Debug.Log($"rightBorderSat :{rightBorderSat}");
        Debug.Log($"-------");
        //
        var leftValue = Math.Max(leftBorderWave,leftBorderSat);
        var rightValue = Math.Min(rightBorderWave, rightBorderSat);

        return rightValue - leftValue;
    }


    //private float GetNextWaveScale(float nextWaveWidht)
    //{

    //}
}
