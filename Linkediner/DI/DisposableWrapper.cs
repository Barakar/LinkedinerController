using System;

namespace Linkediner.DI
{
    public class DisposableWrapper : IDisposable
    {
        private readonly Action _action;

        public DisposableWrapper(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}