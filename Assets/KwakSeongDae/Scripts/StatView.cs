using System;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [Serializable]
    struct StatTextView
    {
        public PlayerStatStoreData stat;
        public string prefixLevelText;
        public string prefixStatText;
        public string prefixBuyText;
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI StatText;
        public TextMeshProUGUI BuyText;
    }
    [Header("���� ��� �⺻ ����")]
    [SerializeField] private StatStore statStore;
    [SerializeField] private StatTextView[] textViews;

    void Update()
    {
        foreach (var textView in textViews)
        {
            UpdateView(textView);
        }
    }

    /// <summary>
    /// ��ϵ� ���� ��ġ �ؽ�Ʈ�� �𵨿� ����� ��ġ�� ����ȭ��Ű�� �Լ�
    /// </summary>
    /// <param name="textView"></param>
    void UpdateView(StatTextView textView)
    {
        if (PlayerDataModel.Instance == null ||
            textView.LevelText == null ||
            textView.StatText == null ||
            textView.BuyText == null) return;

        switch (textView.stat)
        {
            case PlayerStatStoreData.Health:
                textView.LevelText?.SetText($"{textView.prefixLevelText}: {PlayerDataModel.Instance.HealthLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText}: {PlayerDataModel.Instance.Health}");
                textView.BuyText?.SetText($"{textView.prefixBuyText}: {statStore.health.upValue}");
                break;

            case PlayerStatStoreData.HealthRegen:
                textView.LevelText?.SetText($"{textView.prefixLevelText}: {PlayerDataModel.Instance.HealthRegenLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText}: {PlayerDataModel.Instance.HealthRegen:F2}");
                textView.BuyText?.SetText($"{textView.prefixBuyText}: {statStore.healthRegen.upValue}");
                break;

            case PlayerStatStoreData.Attack:
                textView.LevelText?.SetText($"{textView.prefixLevelText}: {PlayerDataModel.Instance.AttackLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText}: {PlayerDataModel.Instance.Attack:F2}");
                textView.BuyText.text = $"{textView.prefixBuyText}: {statStore.attack.upValue.ToString()}";
                break;

            case PlayerStatStoreData.TouchAttack:
                textView.LevelText?.SetText($"{textView.prefixLevelText}: {PlayerDataModel.Instance.TouchAttackLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText}: {PlayerDataModel.Instance.TouchAttack:F2}");
                textView.BuyText?.SetText($"{textView.prefixBuyText}: {statStore.touchAttack.upValue}");
                break;

            case PlayerStatStoreData.AttackSpeed:
                textView.LevelText?.SetText($"{textView.prefixLevelText}: {PlayerDataModel.Instance.AttackSpeedLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText}: {PlayerDataModel.Instance.AttackSpeed:F2}");
                textView.BuyText?.SetText($"{textView.prefixBuyText}: {statStore.attackSpeed.upValue}");
                break;
        }

    }
}
