using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;

namespace DamphelDev.UniRX.Examples
{
    public class GameController : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private int lives = 3;
        public GameObject playerPrefab;
        private ReactiveProperty<int> reactiveLives; // A ReactiveProperty is a property that emmit it value as an observable when it changes, It is a property observable from every part.
        public GameObject ufosContainer;
        private ReactiveProperty<int> ufosLeft;

        [Header("UI")]
        public GameObject livesUI;
        public GameObject gameOverUI;
        public GameObject winUI;

        private TMP_Text livesLeftText;

        void Start()
        {
            reactiveLives = new ReactiveProperty<int>(lives); // Declaration of a ReactiveProperty with the initial value of lives

            livesLeftText = livesUI.GetComponent<TMP_Text>();
            livesLeftText.text = $"Lives: {lives}";

            ufosLeft = new ReactiveProperty<int>(1);

            BindRestartButtons();

            ObserveLives();
            ObserveUfos();
        }

        private void BindRestartButtons()
        {
            gameOverUI.transform.Find("RestartButton")
            .GetComponent<Button>()
            .onClick.AddListener(RestartGame);

            winUI.transform.Find("RestartButton")
            .GetComponent<Button>()
            .onClick.AddListener(RestartGame);
        }

        private void ObserveUfos()
        {
            ufosLeft
            .Where(ufos => ufos == 0)
            .Take(1)
            .Subscribe(_ => WinGame())
            .AddTo(this);
        }

        private void ObserveLives()
        {
            reactiveLives
            .Where(livesLeft => livesLeft == 0)
            .Take(1)
            .Subscribe(_ => GameOver())
            .AddTo(this);
        }

        public void GameOver()
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
        }

        public void WinGame()
        {
            winUI.SetActive(true);
            Time.timeScale = 0;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        private void SpawnPlayer()
        {
            Instantiate(playerPrefab, new Vector3(0, -4, 0), Quaternion.Euler(0, 0, 0));
        }

        public void checkUfosLeft()
        {
            Collider2D ufosContainerCollider = ufosContainer.GetComponent<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Ufos"));
            ufosLeft.Value = Physics2D.OverlapCollider(ufosContainerCollider, filter, new List<Collider2D>());
        }

        public void SubstractLive()
        {
            reactiveLives.Value -= 1;
            livesLeftText.text = $"Lives: {reactiveLives.Value}";

            if (reactiveLives.Value != 0)
            {
                SpawnPlayer();
            }
        }
    }
}
