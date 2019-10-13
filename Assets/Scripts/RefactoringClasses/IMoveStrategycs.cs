using UnityEngine;

namespace Assets.Scripts
{
    public interface IMoveStrategy
    {
        float PhaseChange { get; set; }
        void MoveObject(GameObject gameObject);
    }
}
