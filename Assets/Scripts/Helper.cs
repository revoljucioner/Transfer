using System;
using UnityEngine;
using static System.Math;

public static class Helper
{
    // TODO:
    // To remove
    public static Vector3 GetEulerAnglesToRotateObjectToObject(Vector3 objectToRotatePosition, Vector3 objectTargetPosition, bool revert)
    {
        var deltaY = objectToRotatePosition.y - objectTargetPosition.y;
        var deltaX = objectToRotatePosition.x - objectTargetPosition.x;

        var angleRad = Atan(deltaY / deltaX);
        var angleDeg = (float)((angleRad * 180) / PI);
        var eulerAngleZ = angleDeg > 0 ? 270 + angleDeg : 90 + angleDeg;

        if (revert)
            eulerAngleZ += 180;

        var newEulerAngles = new Vector3(0, 0, eulerAngleZ);
        return newEulerAngles;
    }

    public static Vector3 GetObjectPosition(string tag)
    {
        var baseRef = GameObject.FindGameObjectWithTag(tag);
        var baseReflectorPosition = baseRef.transform.position;
        return baseReflectorPosition;
    }

    public static float DegreesToRad(float degrees)
    {
        var angle = (float)(PI * degrees / 180.0);
        return angle;
    }
}
