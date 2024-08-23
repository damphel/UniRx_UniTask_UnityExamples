using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.CreatingDataStreams
{
    public class MonoBehaviourTriggers : MonoBehaviour
    {
        private void Start()
        {
            this.OnEnableAsObservable()
                .Subscribe(_ => Debug.Log("Enabled Observable"));

            this.UpdateAsObservable()
                .Subscribe(_ => Debug.Log("Update Observable"));

            this.OnCollisionEnterAsObservable()
                .Subscribe(collision =>
                    Debug.Log($"Collided "));
        }
    }
}
