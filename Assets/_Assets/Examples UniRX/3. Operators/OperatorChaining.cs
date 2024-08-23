using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.Operators
{
    public class OperatorChaining : MonoBehaviour
    {
        private void Start()
        {
            Observable.Range(0, 5)
                .Merge(Observable.Range(0, 5))
                .Select(x => x * 2)
                .Where(x => x > 5)
                .Subscribe(x =>
                {
                    Debug.Log("Value: " + x); // 6, 6, 8, 8, 10, 10
                });
        }
    }
}
