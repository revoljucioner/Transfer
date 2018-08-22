using System;

public static class SatelliteOverloadTimerScript 
{
    private static OrbitType GetCurrentOrvitType()
    {
        if (Variables.Time < Variables.Tarc)
            return OrbitType.Arc;
        if (Variables.Time >= Variables.Tarc && Variables.Time < Variables.Tarc + Variables.Trel)
            return OrbitType.RelToEllipse;
        if (Variables.Time >= Variables.Tarc + Variables.Trel)
            return OrbitType.Ellipse;
        else
            throw new Exception("Cannot get current orvit type");
    }

    public static bool IsCurrentOrbitType(OrbitType orbitType)
    {
        return orbitType.Equals(GetCurrentOrvitType());
    }
}
