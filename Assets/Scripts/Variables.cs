using System;
using UnityEngine;

public static class Variables
{
    private static GameObject MainCamera => GameObject.FindGameObjectWithTag("MainCamera");
    private static ScoreScript ScoreScript => MainCamera.GetComponent<ScoreScript>();

    private const float _waveMoveSpeed = 0.02f;
    private const float _sateliteMoveSpeed = 1;
    private const float _backgroundMoveSpeed = 0.05f;

    public static Vector3 EarthCenter = new Vector3(0, -5f, 0);
    //
    //Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    //

    public static Single Time => UnityEngine.Time.time;

    public static float WaveMoveSpeed()
    {
        var waveMoveSpeed = _waveMoveSpeed+ 0.005f*ScoreScript.Score;
        return waveMoveSpeed;
    }

    public static float SateliteMoveSpeed()
    {
        var sateliteMoveSpeed = _sateliteMoveSpeed + Time/50;
        return sateliteMoveSpeed;
    }

    public static float BackgroundMoveSpeed()
    {
        var backgroundMoveSpeed = _backgroundMoveSpeed;
        return backgroundMoveSpeed;
    }

    public static float CameraWidth()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return stageDimensions.x;
    }
}