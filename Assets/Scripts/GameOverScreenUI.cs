using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            Hide();
        });
    }

    private void Start()
    {
        Player.Instance.OnPlayerDeath += PlayerOnPlayerDeath;
        Hide();
    }

    private void PlayerOnPlayerDeath(object sender, EventArgs e)
    {
        Show();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
