using System;
using System.Collections.Generic;

namespace Assets.Scripts.RefactoringClasses
{
    public class MoveModel
    {
        public Type SateliteMoveScriptType;
        //public Lazy<IMoveStrategy> SateliteMoveStrategy;
        public TimeSpan Duration;
        public int SatelitesCount;
        public Func<int, IEnumerable<float>> GetOrbitFunc;
    }
}
