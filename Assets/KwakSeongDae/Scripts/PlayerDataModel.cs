using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel : MonoBehaviour
{
    #region Data Variable
    /// <summary>
    /// �÷��̾� ü��
    /// </summary>
    [SerializeField] private int health;
    public int Health
    {
        get { return health; }
        set
        {
            // ü���� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                health = 0;
            }
            else
            {
                health = value;
            }
            OnHealthChanged?.Invoke(health);
        }
    }
    public UnityAction<int> OnHealthChanged;

    /// <summary>
    /// ���ݷ� ��ġ
    /// </summary>
    [SerializeField] private float attack;
    public float Attack
    {
        get { return attack; }
        set
        {
            // ���ݷ��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                attack = 0;
            }
            else
            {
                attack = value;
            }
            OnAttackChanged?.Invoke(attack);
        }
    }
    public UnityAction<float> OnAttackChanged;

    /// <summary>
    /// ���ݼӵ� ��ġ
    /// </summary>
    [SerializeField] private float attackSpeed;
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            // ���ݼӵ��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                attackSpeed = 0;
            }
            else
            {
                attackSpeed = value;
            }
            OnAttackSpeedChanged?.Invoke(attackSpeed);
        }
    }
    public UnityAction<float> OnAttackSpeedChanged;

    /// <summary>
    /// ������ �ں�
    /// </summary>
    [SerializeField] private int money;
    public int Money
    {
        get { return money; }
        set
        {
            // �ں��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                money = 0;
            }
            else
            {
                money = value;
            }
            OnMoneyChanged?.Invoke(money);
        }
    }
    public UnityAction<int> OnMoneyChanged;
    #endregion

    // �̱���
    public static PlayerDataModel Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
