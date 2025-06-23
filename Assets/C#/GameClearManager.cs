using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearManager : MonoBehaviour
{
    [SerializeField] GameObject gameClearPanel;
    [SerializeField] Button titleButton;

    private void Start()
    {
        gameClearPanel.SetActive(false);
        titleButton.onClick.AddListener(GoToTitle);
    }

    public void ShowGameClear()
    {
        gameClearPanel.SetActive(true);
        Time.timeScale = 0f; // ゲーム停止（演出中）
    }

    void GoToTitle()
    {
        Time.timeScale = 1f; // 時間を戻す
        SceneManager.LoadScene("TitleScene"); // ビルドに追加されてるか確認！
    }
}
