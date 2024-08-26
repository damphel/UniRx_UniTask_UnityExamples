using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DamphelDev.UniTaskExamples
{
    public class SampleUniTask : MonoBehaviour
    {
        private CancellationTokenSource _cancellationTokenSource;

        async void Start()
        { 
            /*_cancellationTokenSource = new CancellationTokenSource();

            await UniTask.Delay(TimeSpan.FromSeconds(3f), DelayType.DeltaTime, PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            Debug.Log("Hello World");*/

            SampleClass sampleClass = new SampleClass();
            sampleClass.LogDelay();
        }
    }
}
