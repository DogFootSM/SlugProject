using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel : MonoBehaviour
{
    #region Data Variable
    [Header("�÷��̾� ���� ����")]
    [Header("�÷��̾� ü��")]
    [Tooltip("�÷��̾� ���� ü��")]
    [SerializeField] private float health;
    public float Health
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
    public UnityAction<float> OnHealthChanged;

    [Tooltip("�÷��̾� �ִ� ü��")]
    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            // ü���� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                maxHealth = 0;
            }
            else
            {
                maxHealth = value;
            }
            OnMaxHealthChanged?.Invoke(health);
        }
    }
    public UnityAction<float> OnMaxHealthChanged;

    [Tooltip("�÷��̾� ü�� ����")]
    [SerializeField] private int healthLevel;
    public int HealthLevel
    {
        get { return healthLevel; }
        set
        {
            // ü�� ������ ���ܻ�Ȳ ó��
            if (value < 0)
            {
                healthLevel = 0;
            }
            else
            {
                healthLevel = value;
                // �÷��̾� ü�� ������ ����ؼ� ü�� ��������ŭ ���� ü��, �ִ� ü�� ����
                
                // ü�� ������ ���� CSV�����͸� �����ؼ� ����ȭ �۾� �ʿ�
            }
            OnHealthLevelChanged?.Invoke(healthLevel);
        }
    }
    public UnityAction<int> OnHealthLevelChanged;

    [Header("�÷��̾� ü�����")]
    [Tooltip("�÷��̾� ü�����")]
    [SerializeField] private float healthRegen;
    public float HealthRegen
    {
        get { return healthRegen; }
        set
        {
            // ü�� ����� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                healthRegen = 0;
            }
            else
            {
                healthRegen = value;
            }
            OnHealthRegenChanged?.Invoke(healthRegen);
        }
    }
    public UnityAction<float> OnHealthRegenChanged;

    [Tooltip("�÷��̾� ü����� ����")]
    [SerializeField] private int healthRegenLevel;
    public int HealthRegenLevel
    {
        get { return healthRegenLevel; }
        set
        {
            // ü�� ��� ������ ���ܻ�Ȳ ó��
            if (value < 0)
            {
                healthRegenLevel = 0;
            }
            else
            {
                healthRegenLevel = value;
                // �÷��̾� ü�� ����� ������ ����ؼ� ü�� ����� ��������ŭ ü�� ����� ����

                // ü�� ������ ������ ���� CSV�����͸� �����ؼ� ����ȭ �۾� �ʿ�
            }
            OnHealthRegenLevelChanged?.Invoke(healthRegenLevel);
        }
    }
    public UnityAction<int> OnHealthRegenLevelChanged;

    [Header("�÷��̾� �⺻ ���ݷ�")]
    [Tooltip("�÷��̾� �⺻ ���ݷ�")]
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

    [Tooltip("�÷��̾� �⺻ ���ݷ� ����")]
    [SerializeField] private int attackLevel;
    public int AttackLevel
    {
        get { return attackLevel; }
        set
        {
            // �⺻ ���ݷ� ������ ���ܻ�Ȳ ó��
            if (value < 0)
            {
                attackLevel = 0;
            }
            else
            {
                attackLevel = value;
                // �÷��̾� ���ݷ� ������ ����ؼ� ���ݷ� ��������ŭ ���ݷ� ����

                // ���ݷ� ������ ���� CSV�����͸� �����ؼ� ����ȭ �۾� �ʿ�
            }
            OnAttackLevelChanged?.Invoke(attackLevel);
        }
    }
    public UnityAction<int> OnAttackLevelChanged;

    [Header("�÷��̾� ��ġ ���ݷ�")]
    [Tooltip("�÷��̾� ��ġ ���ݷ�")]
    [SerializeField] private float touchAttack;
    public float TouchAttack
    {
        get { return touchAttack; }
        set
        {
            // ��ġ ���ݷ��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                touchAttack = 0;
            }
            else
            {
                touchAttack = value;
            }
            OnTouchAttackChanged?.Invoke(touchAttack);
        }
    }
    public UnityAction<float> OnTouchAttackChanged;

    [Tooltip("�÷��̾� ��ġ ���ݷ� ����")]
    [SerializeField] private int touchAttackLevel;
    public int TouchAttackLevel
    {
        get { return touchAttackLevel; }
        set
        {
            // �⺻ ���ݷ� ������ ���ܻ�Ȳ ó��
            if (value < 0)
            {
                touchAttackLevel = 0;
            }
            else
            {
                touchAttackLevel = value;
                // �÷��̾� ��ġ ���ݷ� ������ ����ؼ� ��ġ ���ݷ� ��������ŭ ��ġ ���ݷ� ����

                // ��ġ ���ݷ� ������ ���� CSV�����͸� �����ؼ� ����ȭ �۾� �ʿ�
            }
            OnTouchAttackLevelChanged?.Invoke(touchAttackLevel);
        }
    }
    public UnityAction<int> OnTouchAttackLevelChanged;

    [Header("�÷��̾� �⺻ ���ݼӵ�")]
    [Tooltip("�÷��̾� �⺻ ���ݼӵ�")]
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

    [Tooltip("�÷��̾� �⺻ ���ݼӵ� ����")]
    [SerializeField] private int attackSpeedLevel;
    public int AttackSpeedLevel
    {
        get { return attackSpeedLevel; }
        set
        {
            // �⺻ ���ݷ� ������ ���ܻ�Ȳ ó��
            if (value < 0)
            {
                attackSpeedLevel = 0;
            }
            else
            {
                attackSpeedLevel = value;
                // �÷��̾� ���ݼӵ� ������ ����ؼ� ���ݼӵ� ��������ŭ ���ݼӵ� ����

                // ���ݼӵ� ������ ���� CSV�����͸� �����ؼ� ����ȭ �۾� �ʿ�
            }
            OnAttackSpeedLevelChanged?.Invoke(attackSpeedLevel);
        }
    }
    public UnityAction<int> OnAttackSpeedLevelChanged;

    [Header("�÷��̾� ���� ��ȭ")]
    [Tooltip("�÷��̾ ������ �⺻ ��ȭ")]
    [SerializeField] private int money;
    public int Money
    {
        get { return money; }
        set
        {
            // �⺻ ��ȭ�� ���ܻ�Ȳ ó��
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

    [Tooltip("�÷��̾ ������ ���� ������ ��ȭ ��ȭ (���ǻ�, ũ����Ż�� ��Ī)")]
    [SerializeField] private int crystal;
    public int Crystal
    {
        get { return crystal; }
        set
        {
            // ũ����Ż�� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                crystal = 0;
            }
            else
            {
                crystal = value;
            }
            OnCrystalChanged?.Invoke(crystal);
        }
    }
    public UnityAction<int> OnCrystalChanged;

    /// <summary>
    /// ��ų �ر� ���� �Ǵ��ϱ� ���� �÷��� ������ Ÿ�� ����
    /// </summary>
    [Flags]
    public enum SkillType
    {
        None = 0,
        FirstSkill = 1 << 0,
        SecondSkill = 1 << 1,
        ThirdSkill = 1 << 2,
        FourthSkill = 1 << 3,
    }

    /// <summary>
    /// ���� �ر� ���� �Ǵ��ϱ� ���� �÷��� ������ Ÿ�� ����
    /// </summary>
    [Flags]
    public enum WeaponType
    {
        None = 0,
        FirstWeapon = 1 << 0,
        SecondWeapon = 1 << 1,
        ThirdWeapon = 1 << 2,
        FourthWeapon = 1 << 3,
    }


    [Header("�κ��丮 ������ ���� ����")]
    [Tooltip("��ų �ر� ���� �÷���")]
    [SerializeField] private SkillType canUseSkill;
    public SkillType CanUseSkill
    {
        get { return canUseSkill; }
        set { canUseSkill = value; OnCanUseSkillChanged?.Invoke(canUseSkill); }

    }
    public UnityAction<SkillType> OnCanUseSkillChanged;

    [Tooltip("���� �ر� ���� �÷���")]
    [SerializeField] private WeaponType canUseWeapon;
    public WeaponType CanUseWeapon
    {
        get { return canUseWeapon; }
        set { canUseWeapon = value; OnCanUseWeaponChanged?.Invoke(canUseWeapon); }

    }
    public UnityAction<WeaponType> OnCanUseWeaponChanged;
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
