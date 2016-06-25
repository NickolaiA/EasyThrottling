using System;
using System.Threading;

namespace EasyThrottling
{
    public enum ThrottleState { Allowed, Busy, Continue }

    public class Throttle
    {
        private readonly object syncRoot = new object();
        private readonly int _limit;
        private readonly TimeSpan _duration;
        private DateTime _next = DateTime.MinValue;
        private int _currentItems;

        public delegate void NotifyDelegate(ThrottleState state, int numberOfItemsInProgress, int limit);
        public event NotifyDelegate OnNotify;

        public int CurrentItems
        {
            get { return _currentItems; }
        }

        public Throttle(TimeSpan duration, int limit)
        {
            this._duration = duration;
            this._limit = limit;
        }

        public void Wait()
        {
            lock (syncRoot)
            {
                DateTime now = DateTime.UtcNow;

                if (_next < now)
                {
                    _currentItems = 0; _next = now + _duration;
                }

                ++_currentItems;

                if (_currentItems <= _limit)
                {
                    Notify(ThrottleState.Allowed, _currentItems, _limit);
                    return;
                }
                else
                {
                    Notify(ThrottleState.Busy, _currentItems, _limit);
                }

                Thread.Sleep(_next - now);
                _next = DateTime.UtcNow + _duration;
                _currentItems = 1;
                Notify(ThrottleState.Continue, _currentItems, _limit);
            }
        }

        private void Notify(ThrottleState state, int numberOfItemsInProgress, int limit)
        {
            if (OnNotify != null)
            {
                OnNotify(state, numberOfItemsInProgress, limit);
            }
        }
    }
}
