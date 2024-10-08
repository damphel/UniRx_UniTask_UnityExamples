using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples
{
    public class Ufo : MonoBehaviour
    {
        [SerializeField] GameObject explosion;
        [SerializeField] GameObject laser;
        [SerializeField] float laserSpeed = 200f;

        private void Start()
        {
            Physics2D.queriesStartInColliders = false;

            Observable
            .Interval(TimeSpan.FromSeconds(UnityEngine.Random.Range(1f, 2f)))
            .Where(_ => CanShoot())
            .Subscribe(_ => Fire())
            .AddTo(this);
        }

        public void Explode()
        {
            GameObject explosionInstance = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(explosionInstance, 1f);
            Destroy(gameObject);

            GameController controller =
                GameObject.FindGameObjectWithTag("GameController")
                .GetComponent("GameController")
                as GameController;

            controller.checkUfosLeft();
        }

        private void Fire()
        {
            GameObject firedLaser = Instantiate(
                laser,
                transform.position,
                Quaternion.Euler(0, 0, 180)
            );
            Physics2D.IgnoreCollision(firedLaser.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            Rigidbody2D firedLaserRigidBody2D = firedLaser.GetComponent<Rigidbody2D>();
            firedLaserRigidBody2D.AddForce(Vector2.down * laserSpeed);
        }

        private bool CanShoot()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            return !hit.collider.gameObject.CompareTag("Ufo") && UnityEngine.Random.Range(1, 4) == 2;
        }
    }
}
