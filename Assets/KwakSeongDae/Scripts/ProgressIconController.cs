using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIconController : MonoBehaviour
{
    [Header("�⺻ ����")]
    [Tooltip("���α׷������� ForeGround")]
    [SerializeField] private Image progressForeground;
    [Tooltip("���� ���α׷��� ������")]
    [SerializeField] private RectTransform progressIcon;

    // Update is called once per frame
    void Update()
    {
        if (progressForeground != null && progressIcon != null)
        {
            var pos = progressForeground.rectTransform.anchoredPosition;
            var width = progressForeground.rectTransform.sizeDelta.x;
            progressIcon.anchoredPosition = new Vector3(pos.x + (width * progressForeground.fillAmount), pos.y);
        }
    }
}
