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
    [SerializeField] private TextMeshProUGUI jewelText;

    private void Start()
    {
        if (PlayerDataModel.Instance != null)
        {
            PlayerDataModel.Instance.OnMoneyChanged += UpdateMoney;
            PlayerDataModel.Instance.OnJewelChanged += UpdateJewel;

            moneyText?.SetText(PlayerDataModel.Instance.Money.ToString());
            jewelText?.SetText(PlayerDataModel.Instance.Money.ToString());
        }
    }

    private void OnEnable()
    {
        if (PlayerDataModel.Instance != null)
        {
            PlayerDataModel.Instance.OnMoneyChanged += UpdateMoney;
            PlayerDataModel.Instance.OnJewelChanged += UpdateJewel;
        }
    }

    private void OnDisable()
    {
        if (PlayerDataModel.Instance != null)
        {
            PlayerDataModel.Instance.OnMoneyChanged -= UpdateMoney;
            PlayerDataModel.Instance.OnJewelChanged -= UpdateJewel;
        }
    }

    void UpdateMoney(int newMoney)
    {
        moneyText?.SetText(newMoney.ToString());
    }

    void UpdateJewel(int newJewel)
    {
        jewelText?.SetText(newJewel.ToString());
    }
}
