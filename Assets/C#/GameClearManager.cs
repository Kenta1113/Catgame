using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearManager : MonoBehaviour
{
    [SerializeField] GameObject gameClearPanel;
    [SerializeField] Button titleButton;

    [SerializeField] AudioClip gameClearBGM; // Åöí«â¡

    private void Start()
    {
        gameClearPanel.SetActive(false);
        titleButton.onClick.AddListener(GoToTitle);
    }

    public void ShowGameClear()
    {
        gameClearPanel.SetActive(true);
        Time.timeScale = 0f;

        // ÅöBGMêÿÇËë÷Ç¶
        if (BGMPlayer.Instance != null && gameClearBGM != null)
        {
            BGMPlayer.Instance.PlayBGM(gameClearBGM);
        }
    }

    void GoToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
