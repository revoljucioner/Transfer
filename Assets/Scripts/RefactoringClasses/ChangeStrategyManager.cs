using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RefactoringClasses
{
    public class ChangeStrategyManager : MonoBehaviour, IObservable<IMoveStrategy>
    {
        private List<IObserver<IMoveStrategy>> _observers = new List<IObserver<IMoveStrategy>>();

        public List<MoveModel> MoveStrategyCollection = new List<MoveModel>
        {
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveArcStrategy), Duration = Variables.Tarc, SatelitesCount = 2 },
            new MoveModel { SateliteMoveScriptType = typeof(SateliteMoveTowardScript), Duration = Variables.Trel, SatelitesCount = 2 },
        };

        public void Start()
        {
            GameObject[] satellites = GameObject.FindGameObjectsWithTag("satBody");
            var g = satellites[0];
            var g2 = g.GetComponent<MoveScript>();
            _observers.Add(g2);

            _ = ChangeStrategies();
        }

        public async Task ChangeStrategies()
        {
            foreach (var moveModel in MoveStrategyCollection)
            {
                NotifyObservers(moveModel.SateliteMoveScriptType);
                await Task.Delay(TimeSpan.FromSeconds(moveModel.Duration));
            }
        }

        public IDisposable Subscribe(IObserver<IMoveStrategy> observer)
        {
            _observers.Add(observer);

            // TODO:
            // need to return IDisposable
            return null;
        }

        public void NotifyObservers(Type type)
        {
            foreach (IObserver<IMoveStrategy> obs in _observers)
            {
                var ms = (IMoveStrategy)Activator.CreateInstance(type);
                obs.OnNext(ms);
            }
        }
    }
}
