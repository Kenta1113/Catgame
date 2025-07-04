using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearManager : MonoBehaviour
{
    [SerializeField] GameObject gameClearPanel;
    [SerializeField] GameObject gameClearImage; // Å© âÊëúÇÃí«â¡
    [SerializeField] Button titleButton;

    [SerializeField] AudioClip gameClearBGM;

    private void Start()
    {
        if (gameClearPanel != null) gameClearPanel.SetActive(false);
        if (gameClearImage != null) gameClearImage.SetActive(false);
        if (titleButton != null) titleButton.onClick.AddListener(GoToTitle);
    }

    public void ShowGameClear()
    {
        if (gameClearPanel != null) gameClearPanel.SetActive(true);
        if (gameClearImage != null) gameClearImage.SetActive(true);

        Time.timeScale = 0f;

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
