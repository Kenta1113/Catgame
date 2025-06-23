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
        Time.timeScale = 0f; // �Q�[����~�i���o���j
    }

    void GoToTitle()
    {
        Time.timeScale = 1f; // ���Ԃ�߂�
        SceneManager.LoadScene("TitleScene"); // �r���h�ɒǉ�����Ă邩�m�F�I
    }
}
