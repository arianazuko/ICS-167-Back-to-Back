using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript instance { get;  private set;}

    public bool isPaused = false;
    [SerializeField] protected GameObject pauseMenuUI;

    [SerializeField] protected GameObject gameOverUI;
    [SerializeField] protected GameObject winUI;
    [SerializeField] protected GameObject portalUI;

    [SerializeField] protected Slider healthUI;
    [SerializeField] protected Slider hitsUI;
    [SerializeField] protected Slider bulletsUI;
    [SerializeField] protected Slider specialUI;

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

        healthUI.value = GameController.instance.health;
        hitsUI.value = GameController.instance.numHits;
        bulletsUI.value = GameController.instance.numBullets;
        specialUI.value = GameController.instance.specialMeter;
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

    public void Reset()
    {
        Time.timeScale = 1f;
        GameObject gameController = GameObject.Find("Game Controller");
        Destroy(gameController);
        SceneManager.LoadScene("1-1");
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

    public void Win()
    {
        Time.timeScale = 0f;
        isPaused = true;
        winUI.SetActive(true);
    }

    public void GetToPortal()
    {
        portalUI.SetActive(!portalUI.activeSelf);
    }

}
