using System;
using UnityEngine;

namespace Assets.Scripts.RefactoringClasses
{
    public class MoveScript : MonoBehaviour, IObserver<IMoveStrategy>
    {
        private IMoveStrategy _moveStrategy;

        //
        //public void Awake()
        //{
        //    var a = Camera.current.GetComponent<ChangeStrategyManager>();
        //    a.Subscribe(this);
        //}

        public void OnCompleted() => throw new NotImplementedException();
        public void OnError(Exception error) => throw new NotImplementedException();
        //

        public void OnNext(IMoveStrategy value)
        {
            SetStrategy(value);
        }

        public void SetStrategy(IMoveStrategy moveStrategy) => _moveStrategy = moveStrategy;

        public void Update()
        {
            _moveStrategy.MoveObject(gameObject);
        }
    }
}
