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

        public List<MoveModel> MoveStrategyCollection = new List<MoveModel>
        {
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveArcStrategy), Duration = TimeSpan.FromSeconds(Variables.Tarc), SatelitesCount = 2 },
            //new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveTowardScript), Duration = TimeSpan.FromSeconds(Variables.Trel), SatelitesCount = 2 },
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveTowardScript), Duration = TimeSpan.FromSeconds(Variables.Trel), SatelitesCount = 2 },
        };

        public void Start()
        {
            _satelliteGenerationScript = Variables.MainCamera.GetComponent<SatelliteGenerationScript>();
            _ = ChangeStrategies();
        }

        public async Task ChangeStrategies()
        {
            foreach (var moveModel in MoveStrategyCollection)
            {
                GenerateSatelites(moveModel.SatelitesCount);
                var strategies = PrepareMoveStrategies(moveModel.SateliteMoveScriptType, moveModel.SatelitesCount);
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

        private IEnumerable<IMoveStrategy> PrepareMoveStrategies(Type type, int satCount)
        {
            // TODO:
            // need to be refactored
            var strategies = Enumerable.Range(0, satCount).Select(i => (IMoveStrategy)Activator.CreateInstance(type));

            var newPhases = PhaseGenerator.GetPhases((uint)_sateliteList.Count());

            for (var i = 0; i < strategies.Count(); i++)
            {
                strategies.ElementAt(i).PhaseChange = newPhases.ElementAt(i);
            }

            return strategies;
        }
    }
}
