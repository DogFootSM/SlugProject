using System;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [Serializable]
    struct StatTextView
    {
        public string prefixText;
        public PlayerData stat;
        public TextMeshProUGUI textMesh;
    }

    [SerializeField] private StatTextView[] textViews;

    private PlayerDataModel playerDataModel;

    void Start()
    {
        playerDataModel = PlayerDataModel.Instance;
        if (playerDataModel == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
    }

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
        if (playerDataModel == null) return;
        switch (textView.stat)
        {
            case PlayerData.Health:
                textView.textMesh.text = textView.prefixText + playerDataModel.Health.ToString();
                break;
            case PlayerData.Attack:
                textView.textMesh.text = textView.prefixText + playerDataModel.Attack.ToString("F2");
                break;
            case PlayerData.AttackSpeed:
                textView.textMesh.text = textView.prefixText + playerDataModel.AttackSpeed.ToString("F2");
                break;
            case PlayerData.Money:
                textView.textMesh.text = textView.prefixText + playerDataModel.Money.ToString();
                break;
            default:
                break;
        }
    }
}
