using Assets.Helpers;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RefactoringClasses
{
    public class ChangeStrategyManager : MonoBehaviour/*, IObservable<IMoveStrategy>*/
    {
        //private List<IObserver<IMoveStrategy>> _observers = new List<IObserver<IMoveStrategy>>();
        private SatelliteGenerationScript _satelliteGenerationScript = Camera.current.GetComponent<SatelliteGenerationScript>();
        private List<GameObject> _sateliteList = new List<GameObject>();

        public List<MoveModel> MoveStrategyCollection = new List<MoveModel>
        {
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveArcStrategy), Duration = TimeSpan.FromSeconds(Variables.Tarc), SatelitesCount = 2 },
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveTowardScript), Duration = TimeSpan.FromSeconds(Variables.Trel), SatelitesCount = 2 },
        };

        public void Start()
        {
            GameObject[] satellites = GameObject.FindGameObjectsWithTag("satBody");
            var g = satellites[0];
            var g2 = g.GetComponent<MoveScript>();
            //_observers.Add(g2);

            _ = ChangeStrategies();
        }

        public async Task ChangeStrategies()
        {
            foreach (var moveModel in MoveStrategyCollection)
            {
                GenerateSatelites(moveModel.SatelitesCount);
                var strategies = PrepareMoveStrategies(moveModel.SateliteMoveScriptType, moveModel.SatelitesCount);
                //SetStrategyForSatelites((IMoveStrategy)Activator.CreateInstance(moveModel.SateliteMoveScriptType));
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

        //private void SetStrategyForSatelites(IMoveStrategy moveStrategy)
        //{
        //    _sateliteList.Select(i => i.GetComponent<MoveScript>()).ForEach(i => i.SetStrategy(moveStrategy));
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

        private IEnumerable<IMoveStrategy> PrepareMoveStrategies(Type type,int satCount)
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

        //public IDisposable Subscribe(IObserver<IMoveStrategy> observer)
        //{
        //    _observers.Add(observer);

        //    // TODO:
        //    // need to return IDisposable
        //    return null;
        //}

        //public void NotifyObservers(Type type)
        //{
        //    foreach (IObserver<IMoveStrategy> obs in _observers)
        //    {
        //        var ms = (IMoveStrategy)Activator.CreateInstance(type);
        //        obs.OnNext(ms);
        //    }
        //}
    }
}
