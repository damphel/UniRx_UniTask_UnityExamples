using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DamphelDev.UniTaskExamples
{
    public class SampleClass
    {
        private CancellationTokenSource _cancellationTokenSource;

        public async void LogDelay()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3f), DelayType.DeltaTime, PlayerLoopTiming.Update);
            Debug.Log("Hello World");
        }
    }
}
