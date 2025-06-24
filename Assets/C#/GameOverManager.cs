using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameOverText;
    [SerializeField] Button retryButton;
    [SerializeField] Button titleButton;

    [SerializeField] AudioClip gameOverBGM; // ★追加

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameOverText != null) gameOverText.SetActive(false);
        if (retryButton != null) retryButton.gameObject.SetActive(false);
        if (titleButton != null) titleButton.gameObject.SetActive(false);

        if (retryButton != null) retryButton.onClick.AddListener(Retry);
        if (titleButton != null) titleButton.onClick.AddListener(GoToTitle);
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverText != null) gameOverText.SetActive(true);
        if (retryButton != null) retryButton.gameObject.SetActive(true);
        if (titleButton != null) titleButton.gameObject.SetActive(true);

        Time.timeScale = 0f;

        // ★BGM切り替え
        if (BGMPlayer.Instance != null && gameOverBGM != null)
        {
            BGMPlayer.Instance.PlayBGM(gameOverBGM);
        }
    }

    public void Retry()
    {
        Debug.Log("Retryボタンが押されました！");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToTitle()
    {
        Debug.Log("Titleボタンが押されました！");
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
