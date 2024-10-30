using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    [Header("üũ�ڽ� �⺻ ����")]
    [Tooltip("üũ�ڽ� Ŭ�� ������ �� ��ư")]
    [SerializeField] private Button checkBoxButton;
    [Tooltip("üũ�ڽ� �̹���")]
    [SerializeField] private Image checkBoxImage;
    [Tooltip("üũ�ڽ� ���� ���� ��������Ʈ")]
    [SerializeField] private Sprite uncheckSprite;
    [Tooltip("üũ�ڽ� üũ ���� ��������Ʈ")]
    [SerializeField] private Sprite checkSprite;
    [Header("üũ�ڽ� ���� ����")]
    [Tooltip("üũ�ڽ� üũ ����")]
    public bool isCheck;

    // Start is called before the first frame update
    void Start()
    {
        if (checkBoxButton != null)
        {
            checkBoxButton.onClick.AddListener(ToggleCheckbox);
            UpdateImage();
        }
    }

    /// <summary>
    /// üũ�ڽ� Ŭ����, üũ ���� ����ϴ� �Լ�
    /// </summary>
    public void ToggleCheckbox()
    {
        if (checkBoxButton == null) return;
        // üũ ���� �ٲٱ� (���)
        isCheck ^= true;
        UpdateImage();
    }

    /// <summary>
    /// üũ ���¿� ���� �̹����� ������Ʈ�ϴ� �Լ�
    /// </summary>
    void UpdateImage()
    {
        if (isCheck)
        {
            // üũ �̹����� ��ȯ
            checkBoxImage.sprite = checkSprite;
        }
        else
        {
            // üũ ���� �̹����� ��ȯ
            checkBoxImage.sprite = uncheckSprite;
        }
    }
}
