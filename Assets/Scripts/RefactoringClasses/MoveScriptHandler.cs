using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.RefactoringClasses
{
    public class MoveScriptHandler
    {
        private GameObject[] Satellites => GameObject.FindGameObjectsWithTag("satBody");
        private Type _currentMoveStrategy;

        public Dictionary<OrbitType, MoveModel> MoveStrategyCollection = new Dictionary<OrbitType, MoveModel>
        {
            [OrbitType.Arc] = new MoveModel{SateliteMoveScriptType = typeof(SateliteMoveArcScript), Duration = Variables.Tarc/*, SatelitesCount = 2*/},
            [OrbitType.RelToEllipse] = new MoveModel{SateliteMoveScriptType = typeof(SateliteMoveTowardScript), Duration = Variables.Trel/*, SatelitesCount = 3*/},
            [OrbitType.Ellipse] = new MoveModel{SateliteMoveScriptType = typeof(SateliteMoveCircleScript), Duration = Variables.Tellipse/*, SatelitesCount = 3 */},
        };

        public void Update()
        {

        }

        public void Foo()
        {
            foreach (var moveModel in MoveStrategyCollection)
            {

            }
        }
    }

    public class MoveModel
    {
        public Type SateliteMoveScriptType;
        public Lazy<IMoveStrategy> SateliteMoveStrategy;
        // seconds
        public float Duration;
        public int SatelitesCount;
    }
}
