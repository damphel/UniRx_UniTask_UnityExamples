using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class YieldToObservable : MonoBehaviour
    {
        [Obsolete]
        private IEnumerator YieldToObservables()
        { 
            yield return Observable.Timer(TimeSpan.FromSeconds(1)).ToYieldInstruction();

            yield return transform.ObserveEveryValueChanged(x => x.position)
                .FirstOrDefault(p => p.x >= 100).ToYieldInstruction();

            yield return ObservableWWW.Get("http://unity3d.com").ToYieldInstruction();
        }
    }
}
