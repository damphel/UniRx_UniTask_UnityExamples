using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class CreateOperator : MonoBehaviour
    {
        private void Start()
        {
            // Create our own custom data stream by giving us access to the observer
            var datastream = Observable.Create<int>(observer =>
            {
                observer.OnNext(1);
                observer.OnError(new Exception());
                observer.OnCompleted();
                return Disposable.Empty;
            });

            datastream.Subscribe(
                i => Debug.Log($"Emitted: {i}"),
                e => Debug.LogError($"Error: {e.Message}"),
                () => Debug.Log("Completed")
                );
        }
    }
}
