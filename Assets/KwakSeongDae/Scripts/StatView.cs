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
        public string prefixStatUpText;
        public string prefixBuyText;
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI StatText;
        public TextMeshProUGUI StatUpText;
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
            textView.StatUpText == null ||
            textView.BuyText == null) return;

        switch (textView.stat)
        {
            case PlayerStatStoreData.Health:
                textView.LevelText?.SetText($"{textView.prefixLevelText} {PlayerDataModel.Instance.HealthLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText} {PlayerDataModel.Instance.MaxHealth}"); //�ִ� ü���� ǥ��
                textView.StatUpText?.SetText($"{textView.prefixStatUpText} {statStore.health.upValue}");
                textView.BuyText?.SetText($"{textView.prefixBuyText} {statStore.health.curCost}");
                break;

            case PlayerStatStoreData.HealthRegen:
                textView.LevelText?.SetText($"{textView.prefixLevelText} {PlayerDataModel.Instance.HealthRegenLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText} {PlayerDataModel.Instance.HealthRegen}");
                textView.StatUpText?.SetText($"{textView.prefixStatUpText} {statStore.healthRegen.upValue}");
                textView.BuyText?.SetText($"{textView.prefixBuyText} {statStore.healthRegen.curCost}");
                break;

            case PlayerStatStoreData.Attack:
                textView.LevelText?.SetText($"{textView.prefixLevelText} {PlayerDataModel.Instance.AttackLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText} {PlayerDataModel.Instance.Attack}");
                textView.StatUpText.text = $"{textView.prefixStatUpText} {statStore.attack.upValue.ToString()}";
                textView.BuyText?.SetText($"{textView.prefixBuyText} {statStore.attack.curCost}");
                break;

            case PlayerStatStoreData.AttackSpeed:
                textView.LevelText?.SetText($"{textView.prefixLevelText} {PlayerDataModel.Instance.AttackSpeedLevel}");
                textView.StatText?.SetText($"{textView.prefixStatText} {PlayerDataModel.Instance.AttackSpeed.ToString("F4")}");
                textView.StatUpText?.SetText($"{textView.prefixStatUpText} {statStore.attackSpeed.upValue.ToString("F4")}");
                textView.BuyText?.SetText($"{textView.prefixBuyText} {statStore.attackSpeed.curCost}");
                break;
        }

    }
}
