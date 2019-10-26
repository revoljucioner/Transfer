using UnityEngine;

namespace Assets.Scripts
{
    public interface IMoveStrategy
    {
        float PhaseChange { get; set; }
        float Orbit { get; set; }
        void MoveObject(GameObject gameObject);
    }
}
