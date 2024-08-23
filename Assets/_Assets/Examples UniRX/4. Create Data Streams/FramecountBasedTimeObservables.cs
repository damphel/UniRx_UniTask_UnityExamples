using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class FramecountBasedTimeObservables : MonoBehaviour
    {
        private void Start()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => Debug.Log("Emit Every Update"));

            Observable.TimerFrame(60)
                .Subscribe(_ => Debug.Log("Emit Once After 60 Frames"));

            Observable.IntervalFrame(60)
                .Subscribe(_ => Debug.Log("Emit Every 60 Frames"));
        }
    }
}
