using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UniRx.Operators;

namespace DamphelDev.UniRX.Examples.DoubleClick
{
    public class DoubleClickDetectorReactive : MonoBehaviour
    {
        private void Start()
        {
            var mouseEvent = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0));

            mouseEvent.Buffer(
                mouseEvent.Throttle(TimeSpan.FromSeconds(0.25f)))
                .Where(x => x.Count >= 2)
                .Subscribe(_ => Debug.Log("Double Click Reactive"));
        }
    }
}
