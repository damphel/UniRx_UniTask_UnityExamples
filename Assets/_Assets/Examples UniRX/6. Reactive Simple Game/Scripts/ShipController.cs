using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace DamphelDev.UniRX.Examples
{
    public class ShipController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float speed = 5f;
        private Rigidbody2D shipRb;
        Vector2 movement;

        [Header("Fire")]
        [SerializeField] GameObject laser;
        [SerializeField] float fireRate = 0.8f;
        [SerializeField] float laserSpeed = 600f;
        private float nextFire = 0f;

        [Header("Explosion")]
        [SerializeField] GameObject explosion;

        [Header("Other")]
        [SerializeField] float immortalityDuration = 1f;

        private void Awake()
        {
            shipRb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            IObservable<Unit> update = this.UpdateAsObservable();

            update
                .Do(_ => HandleMovement()) // Do operator makes the action happen always independently what we do next
                .Where(_ => CanShoot()) // When we CanShoot()
                .Subscribe(_ => Fire()); // We Fire()
        }

        private void HandleMovement()
        {
            movement.x = Input.GetAxisRaw("Horizontal");

            shipRb.MovePosition(shipRb.position + movement * speed * Time.fixedDeltaTime);
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

            controller.SubstractLive();
        }

        private void Fire()
        {
            GameObject firedLaser = Instantiate(
                laser,
                transform.position,
                transform.rotation
            );

            Rigidbody2D firedLaserRigidBody2D = firedLaser.GetComponent<Rigidbody2D>();
            firedLaserRigidBody2D.AddForce(Vector3.up * laserSpeed);

            Physics2D.IgnoreCollision(firedLaser.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            nextFire = Time.time + fireRate;
        }

        private bool CanShoot() => Input.GetButton("Fire1") && Time.time > nextFire;
    }
}
