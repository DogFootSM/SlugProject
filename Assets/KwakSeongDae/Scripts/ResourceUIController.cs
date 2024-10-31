using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �ڿ� UI�� PlayerDataModel�� �����Ϳ� ������Ű�� ���� ��ũ��Ʈ
/// </summary>
public class ResourceUIController : MonoBehaviour
{
    [Header("�ڿ� UI �⺻ ����")]
    [Tooltip("������� �⺻ ��ȭ�� ǥ���ϴ� TextMeshUI")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [Tooltip("���� �� ������ ��ȭ �ÿ� ���Ǵ� ��ȭ�� ǥ���ϴ� TextMeshUI")]
    [SerializeField] private TextMeshProUGUI crystalText;

    private void Start()
    {
        if (PlayerDataModel.Instance != null)
        {
            PlayerDataModel.Instance.OnMoneyChanged += UpdateMoney;
            PlayerDataModel.Instance.OnCrystalChanged += UpdateCrystal;

            moneyText?.SetText(PlayerDataModel.Instance.Money.ToString());
            crystalText?.SetText(PlayerDataModel.Instance.Money.ToString());
        }
    }

    private void OnEnable()
    {
        if (PlayerDataModel.Instance != null)
        {
            PlayerDataModel.Instance.OnMoneyChanged += UpdateMoney;
            PlayerDataModel.Instance.OnCrystalChanged += UpdateCrystal;
        }
    }

    private void OnDisable()
    {
        if (PlayerDataModel.Instance != null)
        {
            PlayerDataModel.Instance.OnMoneyChanged -= UpdateMoney;
            PlayerDataModel.Instance.OnCrystalChanged -= UpdateCrystal;
        }
    }

    void UpdateMoney(int newMoney)
    {
        moneyText?.SetText(newMoney.ToString());
    }

    void UpdateCrystal(int newCrystal)
    {
        crystalText?.SetText(newCrystal.ToString());
    }
}
