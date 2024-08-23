using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class ObservableNetworkOperations : MonoBehaviour
    {
        private void Start()
        {
            var progressNotifier = new ScheduledNotifier<float>();

            var requestStream =
                ObservableWWW.Get("http://google.com", progress: progressNotifier)
                    .Subscribe(Debug.Log);

            progressNotifier.Subscribe(i => Debug.Log($"Progress: {i}"));

            requestStream.Dispose(); // Cancel Request
        }
    }
}
