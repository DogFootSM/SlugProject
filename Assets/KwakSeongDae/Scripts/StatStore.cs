using System;
using UnityEngine;

public enum PlayerData
{
    Health, Attack, AttackSpeed, Money, Size
}

public class StatStore : MonoBehaviour
{
    /// <summary>
    /// ���� �������� ���� ���� ��뿡 ���� ���ǵ� �ڷ��� Ÿ��
    /// </summary>
    /// <typeparam name="T"> ������ �ڷ����� ��ġ </typeparam>
    [Serializable]
    public struct StatChangeAmount<T>
    {
        public T value;
        public int cost;
    }

    [Header("���� ������ �� ���� ���ź�� ����")]
    public StatChangeAmount<int> health;
    public StatChangeAmount<float> attack;
    public StatChangeAmount<float> attackSpeed;

    private PlayerDataModel playerDataModel;

    private void Start()
    {
        playerDataModel = PlayerDataModel.Instance;
    }

    /// <summary>
    /// ü�� ������ value�� ����ϴ� �Լ�
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
    /// ���ݷ� ������ value�� ����ϴ� �Լ�
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
    /// ���ݼӵ� ������ value�� ����ϴ� �Լ�
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
