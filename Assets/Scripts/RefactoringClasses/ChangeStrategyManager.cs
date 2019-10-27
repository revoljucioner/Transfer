using Assets.Helpers;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RefactoringClasses
{
    public class ChangeStrategyManager : MonoBehaviour
    {
        private SatelliteGenerationScript _satelliteGenerationScript;
        private List<GameObject> _sateliteList = new List<GameObject>();

        //
        static Func<int, IEnumerable<float>> sameOrbits = (satCount) => Enumerable.Repeat(5.8f, satCount);
        static Func<int, IEnumerable<float>> evenlyOrbits = (satCount) =>
        {
            var min = 5f;
            var max = 6f;

            var span = (max - min)/(satCount-1);

            var list = new List<float>();

            for (var i = 0; i< satCount; i++)
            {
                list.Add(min+i* span);
            }

            return list;
        };
        //

        public List<MoveModel> MoveStrategyCollection = new List<MoveModel>
        {
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveArcStrategy), Duration = TimeSpan.FromSeconds(Variables.Tarc), SatelitesCount = 2 , GetOrbitFunc = evenlyOrbits},
            //new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveTowardStrategy), Duration = TimeSpan.FromSeconds(Variables.Trel), SatelitesCount = 2 , GetOrbitFunc = sameOrbits},
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveCircleStrategy), Duration = TimeSpan.FromSeconds(Variables.Trel), SatelitesCount = 2 , GetOrbitFunc = sameOrbits},
        };

        public void Awake()
        {
            _satelliteGenerationScript = Variables.MainCamera.GetComponent<SatelliteGenerationScript>();
            _ = ChangeStrategies();
        }

        public async Task ChangeStrategies()
        {
            foreach (var moveModel in MoveStrategyCollection)
            {
                GenerateSatelites(moveModel.SatelitesCount);
                var strategies = PrepareMoveStrategies(moveModel.SateliteMoveScriptType, moveModel.SatelitesCount, moveModel.GetOrbitFunc);
                SetStrategies(strategies);
                await Task.Delay(moveModel.Duration);
            }
        }

        private void GenerateSatelites(int satCount)
        {
            if (satCount < _sateliteList.Count())
                throw new ArgumentOutOfRangeException("Invalid count of sattelites");

            Func<GameObject> func = _satelliteGenerationScript.Spawn;
            var newSateliteCollection = func.Repeat(satCount - _sateliteList.Count());
            _sateliteList.AddRange(newSateliteCollection);
        }

        //private void GenerateSatelites(int satCount)
        //{
        //    if (satCount < _sateliteList.Count())
        //        throw new ArgumentOutOfRangeException("Invalid count of sattelites");

        //    try
        //    {
        //        //
        //        var f = Enumerable.Range(0, satCount - _sateliteList.Count()).ToArray();
        //        //var u = Camera.current;
        //        var u = Variables.MainCamera;
        //        var newSateliteCollection = f.Select(i => u.GetComponent<SatelliteGenerationScript>().Spawn());
        //        var y = newSateliteCollection.ToArray();
        //        //
        //        //Func<GameObject> func = Camera.current.GetComponent<SatelliteGenerationScript>().Spawn;
        //        //var newSateliteCollection = func.Repeat(satCount - _sateliteList.Count());
        //        _sateliteList.AddRange(newSateliteCollection);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        private void SetStrategies(IEnumerable<IMoveStrategy> strategies)
        {
            for (var i = 0; i < _sateliteList.Count(); i++)
            {
                var satelite = _sateliteList.ElementAt(i).GetComponent<MoveScript>();
                var strategy = strategies.ElementAt(i);
                satelite.SetStrategy(strategy);
            }
        }

        private IEnumerable<IMoveStrategy> PrepareMoveStrategies(Type type, int satCount, Func<int, IEnumerable<float>> orbitFunc)
        {
            // TODO:
            // need to be refactored
            var strategies = Enumerable.Range(0, satCount).Select(i => (IMoveStrategy)Activator.CreateInstance(type)).ToArray();

            var newPhases = PhaseGenerator.GetPhases((uint)_sateliteList.Count());
            var newOrbits = orbitFunc(_sateliteList.Count());

            for (var i = 0; i < strategies.Count(); i++)
            {
                strategies[i].PhaseChange = newPhases.ElementAt(i);
                strategies[i].Orbit = newOrbits.ElementAt(i);
            }

            return strategies;
        }
    }
}
