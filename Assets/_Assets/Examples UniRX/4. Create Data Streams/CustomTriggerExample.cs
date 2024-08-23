using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class CustomTriggerExample : ObservableTriggerBase
    {
        private Subject<int> _onCustomUpdate;

        private void Update()
        {
            _onCustomUpdate.OnNext(1);
        }

        public IObservable<int> OnCustomUpdateAsObservable()
        {
            return _onCustomUpdate ?? (_onCustomUpdate = new Subject<int>());
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            _onCustomUpdate?.OnCompleted();
        }
    }
}
