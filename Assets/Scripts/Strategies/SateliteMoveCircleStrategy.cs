using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static System.Math;

namespace Assets.Scripts.RefactoringClasses
{
    public class SateliteMoveCircleStrategy : IMoveStrategy
    {
        public float Speed => 0.2f;
        //public override float Speed => StartSpeed + 0.5f * (float)Atan(Variables.Time - Variables.Tarc);
        private float RadiusOfRotate = 1.8f;
        public float PhaseChange { get; set; }
        public float Orbit { get; set; }

        public void MoveObject(GameObject gameObject)
        {
            gameObject.transform.position = CalculatePositionAsFigure();
            ChangePhase();
        }

        public Vector2 CalculatePositionAsFigure()
        {
            var x = SetPositionX();
            var y = SetPositionY();
            return new Vector2(x, y);
        }

        private float SetPositionX()
        {
            return RadiusOfRotate * Mathf.Cos((90 - PhaseChange) * Mathf.Deg2Rad);
        }

        private float SetPositionY()
        {
            return 0.7f * RadiusOfRotate * Mathf.Sin((90 - PhaseChange) * Mathf.Deg2Rad) + Orbit;
        }

        private void ChangePhase()
        {
            PhaseChange = PhaseChange - Speed;
        }
    }
}
