using System;

namespace Assets.Scripts.RefactoringClasses
{
    public class MoveModel
    {
        public Type SateliteMoveScriptType;
        public Lazy<IMoveStrategy> SateliteMoveStrategy;
        public TimeSpan Duration;
        public int SatelitesCount;
    }
}
