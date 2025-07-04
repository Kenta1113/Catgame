using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSoundTrigger : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (ButtonClickSoundManager.Instance != null)
                ButtonClickSoundManager.Instance.PlayClickSound();
        });
    }
}
