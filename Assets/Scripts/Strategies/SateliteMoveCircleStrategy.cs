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
        public float PhaseChange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float Orbit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void MoveObject(GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}
