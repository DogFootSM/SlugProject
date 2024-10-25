using System;
using UnityEngine;

public enum PlayerData
{
    Health, Attack, AttackSpeed, Money, Size
}

public class StatStore : MonoBehaviour
{
    [Serializable]
    public struct StatChangeAmount<T>
    {
        public T value;
        public int cost;
    }

    [Header("Stat Change Amount Setting")]
    public StatChangeAmount<int> health;
    public StatChangeAmount<float> attack;
    public StatChangeAmount<float> attackSpeed;

    private PlayerDataModel playerDataModel;

    private void Start()
    {
        playerDataModel = PlayerDataModel.Instance;
    }

    /// <summary>
    /// ü�� ������ 1�� ����ϴ� �Լ�
    /// </summary>
    public void HealthStatUp()
    {
        if (playerDataModel == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� üũ
        if (playerDataModel.Money - health.cost < 0) return;
        playerDataModel.Money -= health.cost;
        playerDataModel.Health += health.value;
    }

    /// <summary>
    /// ���ݷ� ������ 1�� ����ϴ� �Լ�
    /// </summary>
    public void AttackStatUp()
    {
        if (playerDataModel == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� üũ
        if (playerDataModel.Money - attack.cost < 0) return;
        playerDataModel.Money -= attack.cost;
        playerDataModel.Attack += attack.value;
    }

    /// <summary>
    /// ���ݼӵ� ������ 1�� ����ϴ� �Լ�
    /// </summary>
    public void AttackSpeeedStatUp()
    {
        if (playerDataModel == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� üũ
        if (playerDataModel.Money - attackSpeed.cost < 0) return;
        playerDataModel.Money -= attackSpeed.cost;
        playerDataModel.AttackSpeed += attackSpeed.value;
    }
}
