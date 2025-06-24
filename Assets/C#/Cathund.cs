using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PawsButtonEffect : MonoBehaviour
{
    [SerializeField] GameObject pawImage;             // 肉球の画像（Canvas直下で非表示にしておく）
    [SerializeField] List<Button> targetButtons;      // 対象のボタン複数
    [SerializeField] Vector2 offset = Vector2.zero;   // 追加で微調整したい場合のオフセット（Yはマイナスにすると下方向）

    Canvas canvas; // 肉球の親Canvas参照

    void Start()
    {
        if (pawImage != null)
        {
            pawImage.SetActive(false);

            // 肉球画像のRaycast Targetはオフにする（UIイベントを奪わないため）
            var img = pawImage.GetComponent<Image>();
            if (img != null) img.raycastTarget = false;

            // 親Canvas取得（ScreenSpace-Overlayを想定）
            canvas = pawImage.GetComponentInParent<Canvas>();
        }

        // 各ボタンにHoverHandlerを付けて初期化
        foreach (Button btn in targetButtons)
        {
            var hoverHandler = btn.gameObject.AddComponent<ButtonHoverHandler>();
            hoverHandler.Initialize(this, btn);
        }
    }

    public void ShowPaw(Button button)
    {
        if (pawImage == null || button == null) return;

        pawImage.SetActive(true);

        RectTransform btnRect = button.GetComponent<RectTransform>();
        RectTransform pawRect = pawImage.GetComponent<RectTransform>();

        Vector2 btnSize = btnRect.sizeDelta;
        Vector2 pawSize = pawRect.sizeDelta;

        // ボタンの高さ/2 + 肉球の高さ/2 を下方向にずらす（Yはマイナス）
        Vector2 dynamicOffset = new Vector2(0, -(btnSize.y / 2 + pawSize.y / 2));

        // ボタンのワールド位置をスクリーン座標に変換
        Vector3 buttonPos = btnRect.position;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, buttonPos);

        // 肉球の位置をスクリーン座標でセット（動的オフセット + 任意の微調整オフセット）
        pawRect.position = screenPos + dynamicOffset + offset;

        // 肉球を最前面に
        pawImage.transform.SetAsLastSibling();
    }

    public void HidePaw()
    {
        if (pawImage != null)
            pawImage.SetActive(false);
    }
}

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    PawsButtonEffect parent;
    Button button;
    bool isHovering = false;

    public void Initialize(PawsButtonEffect parentScript, Button btn)
    {
        parent = parentScript;
        button = btn;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering) return;
        isHovering = true;
        parent.ShowPaw(button);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHovering) return;
        isHovering = false;
        parent.HidePaw();
    }
}
