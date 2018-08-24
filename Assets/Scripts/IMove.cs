
using UnityEngine;

namespace Assets.Scripts
{
    public interface IMove
    {
        float Speed { get; }

        Vector2 CalculatePositionAsFigure();
    }
}
