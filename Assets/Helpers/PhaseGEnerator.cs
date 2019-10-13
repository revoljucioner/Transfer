using System.Collections.Generic;

namespace Assets.Helpers
{
    public static class PhaseGenerator
    {
        public static float[] GetPhases(uint count)
        {
            var phases = new List<float>();
            for (var i = 0; i < count; i++)
            {
                phases.Add(i * 360 / count);
            }
            return phases.ToArray();
        }
    }
}
