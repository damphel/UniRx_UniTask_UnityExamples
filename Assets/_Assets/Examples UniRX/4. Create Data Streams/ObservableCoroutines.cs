using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class ObservableCoroutines : MonoBehaviour
    {
        private void Start()
        {
            Observable.FromCoroutine(WaitForThreeSeconds)
                .Subscribe(_ => Debug.Log("Done!"));

            WaitForThreeSeconds().ToObservable()
                .Subscribe(_ => Debug.Log("Done!"));
        }

        private IEnumerator WaitForThreeSeconds()
        {
            Debug.Log("Waiting...");
            yield return new WaitForSeconds(3);
        }
    }
}
