using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public static UIScript instance { get;  private set;}

    public bool isPaused = false;
    [SerializeField] protected GameObject pauseMenuUI;

    [SerializeField] protected GameObject gameOverUI;

    [SerializeField] protected TextMeshProUGUI healthUI;
    [SerializeField] protected TextMeshProUGUI hitsUI;
    [SerializeField] protected TextMeshProUGUI bulletsUI;
    [SerializeField] protected TextMeshProUGUI specialUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        healthUI.text = "Health:" + GameController.instance.health;
        hitsUI.text = "Shield:" + GameController.instance.numHits;
        bulletsUI.text = "Bullets:" + GameController.instance.numBullets;
        specialUI.text = "Special:" + GameController.instance.specialMeter;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isPaused = true;
        gameOverUI.SetActive(true);
    }

}
