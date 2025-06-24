using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PawsButtonEffect : MonoBehaviour
{
    [SerializeField] GameObject pawImage;             // �����̉摜�iCanvas�����Ŕ�\���ɂ��Ă����j
    [SerializeField] List<Button> targetButtons;      // �Ώۂ̃{�^������
    [SerializeField] Vector2 offset = Vector2.zero;   // �ǉ��Ŕ������������ꍇ�̃I�t�Z�b�g�iY�̓}�C�i�X�ɂ���Ɖ������j

    Canvas canvas; // �����̐eCanvas�Q��

    void Start()
    {
        if (pawImage != null)
        {
            pawImage.SetActive(false);

            // �����摜��Raycast Target�̓I�t�ɂ���iUI�C�x���g��D��Ȃ����߁j
            var img = pawImage.GetComponent<Image>();
            if (img != null) img.raycastTarget = false;

            // �eCanvas�擾�iScreenSpace-Overlay��z��j
            canvas = pawImage.GetComponentInParent<Canvas>();
        }

        // �e�{�^����HoverHandler��t���ď�����
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

        // �{�^���̍���/2 + �����̍���/2 ���������ɂ��炷�iY�̓}�C�i�X�j
        Vector2 dynamicOffset = new Vector2(0, -(btnSize.y / 2 + pawSize.y / 2));

        // �{�^���̃��[���h�ʒu���X�N���[�����W�ɕϊ�
        Vector3 buttonPos = btnRect.position;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, buttonPos);

        // �����̈ʒu���X�N���[�����W�ŃZ�b�g�i���I�I�t�Z�b�g + �C�ӂ̔������I�t�Z�b�g�j
        pawRect.position = screenPos + dynamicOffset + offset;

        // �������őO�ʂ�
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
