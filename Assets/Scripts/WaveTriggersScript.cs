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
            var nextWaveScale = GetSizeSlanted(gameObject) / nextWaveWidht;
            WaveGenerationScript.Spawn((float)nextWaveScale);
        }
    }

    private float GetNextWaveWidht(GameObject satReflectorObject)
    {
        var bordersSatelite = GetLeftRightObjectPositions(satReflectorObject);
        var bordersWave = GetLeftRightObjectPositions(gameObject);

        var leftValue = Math.Max(bordersWave.Item1, bordersSatelite.Item1);
        var rightValue = Math.Min(bordersWave.Item2, bordersSatelite.Item2);

        var nextWaveProjectionX = rightValue - leftValue;
        var waveAngle = GetObjectAngleRad(gameObject);
        var nextWaveWidht = nextWaveProjectionX / Math.Cos(waveAngle);
        return (float)nextWaveWidht;
    }

    private Tuple<double, double> GetLeftRightObjectPositions(GameObject _gameObject)
    {
        var sizeSlanted = GetSizeSlanted(_gameObject);
        var centerX = _gameObject.transform.position.x;
        var leftBorder = centerX - sizeSlanted / 2;
        var rightBorder = centerX + sizeSlanted / 2;
        return Tuple.Create(leftBorder, rightBorder);
    }

    private double GetSizeSlanted(GameObject _gameObject)
    {
        var colider = _gameObject.GetComponent<BoxCollider2D>();
        var size = colider.size.x;
        var angleRad = GetObjectAngleRad(_gameObject);
        var sizeSlanted = Math.Cos(angleRad) * size;
        return sizeSlanted;
    }

    private double GetObjectAngleRad(GameObject _gameObject)
    {

        var angleRad = Helper.DegreesToRad(Helper.Degrees360ToPlusMinus180(_gameObject.transform.eulerAngles.z));
        if (_gameObject.CompareTag("wave"))
            angleRad += Math.PI;
        return angleRad;
    }
}
