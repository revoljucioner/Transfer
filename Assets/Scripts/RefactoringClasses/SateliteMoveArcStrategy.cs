using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static System.Math;

namespace Assets.Scripts.RefactoringClasses
{
    public class SateliteMoveArcStrategy: IMoveStrategy
    {
        public float StartSpeed = Variables.SateliteStartArcSpeed;
        public float maxWeight = 1.2f;
        //public float orbit;
        public float orbit = 6f;
        public float phaseChange;
        public bool mobile = true;
        public float Speed => StartSpeed + 0.5f * (float)Atan(Variables.Time);

        public void MoveObject(GameObject gameObject)
        {
            gameObject.transform.position = CalculatePositionAsFigure();
        }

        public Vector2 CalculatePositionAsFigure()
        {
            var x = (float)(maxWeight * Sin(Speed * Variables.Time + phaseChange));
            var y = (float)Sqrt(Pow(orbit, 2) - Pow(x, 2));
            //сейчас орбита идет по кругу в центре которого находится земная станция. закоменчено движение по орбите в центре которой центр Земли
            //var y = (float)Sqrt(Pow(orbit - EarthCenterY, 2) - Pow(x, 2)) + EarthCenterY;
            var position = new Vector2(x, y);
            return position;
        }
    }
}
