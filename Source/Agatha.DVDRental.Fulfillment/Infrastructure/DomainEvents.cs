using System;
using System.Collections.Generic;

namespace Agatha.DVDRental.Fulfillment.Infrastructure
{
    /// <summary>
    /// Provides logic for raising and handling domain events.
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic]
        private static List<Delegate> _actions;
        private static List<Delegate> Actions
        {
            get
            {
                if (_actions == null)
                {
                    _actions = new List<Delegate>();
                }
                return _actions;
            }
        }

        /// <summary>
        /// Rejestruje prcedure obsługi zdarzenia.
        /// </summary>
        /// <param name="callback">Procedura osbługi zdarzenia.</param>
        /// <returns></returns>
        public static IDisposable Register<T>(Action<T> callback)
        {
            Actions.Add(callback);
            return new DomainEventRegistrationRemover(() => Actions.Remove(callback));
        }

        /// <summary>
        /// Sygnalizuje zdarzenie.
        /// </summary>
        public static void Raise<T>(T eventArgs)
        {
            foreach (Delegate action in Actions)
            {
                Action<T> typedAction = action as Action<T>;
                if (typedAction != null)
                {
                    typedAction(eventArgs);
                }
            }
        }

        /// <summary>
        /// Klasa pomocnicza.
        /// </summary>
        private sealed class DomainEventRegistrationRemover : IDisposable
        {
            private readonly Action _callOnDispose;

            public DomainEventRegistrationRemover(Action toCall)
            {
                _callOnDispose = toCall;
            }

            public void Dispose()
            {
                _callOnDispose();
            }
        }
    }
}
