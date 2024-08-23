using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples.Observables
{
    public class CollisionDetector : MonoBehaviour
    {
        private void Start()
        {
            this.OnCollisionEnterAsObservable()
                .Subscribe(collision =>
                {
                    Debug.Log("Collision detected with " + collision.gameObject.name);
                });
        }
    }
}
