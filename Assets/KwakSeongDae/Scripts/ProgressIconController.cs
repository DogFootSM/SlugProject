using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIconController : MonoBehaviour
{
    [Header("�⺻ ����")]
    [Tooltip("���α׷������� ForeGround")]
    [SerializeField] private Image progressForeground;
    [Tooltip("ForeGround�� �ڽĿ� �ִ� ���α׷��� ������\n �θ��� Anchor�� ������ ���� ���� �ʿ�")]
    [SerializeField] private RectTransform progressIcon;
    [Tooltip("������ ��ġ ������")]
    [SerializeField] private Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        if (progressForeground != null && progressIcon != null)
        {
            var width = progressForeground.rectTransform.sizeDelta.x;
            progressIcon.anchoredPosition = new Vector3(width * progressForeground.fillAmount + offset.x, offset.y);
        }
    }
}
