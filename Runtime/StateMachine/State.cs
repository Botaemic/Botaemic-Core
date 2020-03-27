using System;

namespace Botaemic.Core
{
    public abstract class State<T>
    {
        public abstract Type EnterState();
        public abstract Type ExitState();
        public abstract Type UpdateState();
    }
}