using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel : MonoBehaviour
{
    #region Data Variable
    [Header("�÷��̾� ���� ����")]
    [Tooltip("�÷��̾� ü��")]
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

    [Tooltip("�÷��̾� ü�����")]
    [SerializeField] private int healthRegen;
    public int HealthRegen
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
    public UnityAction<int> OnHealthRegenChanged;

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

    [Header("��Ÿ ���� ����")]
    [Tooltip("���� ���ݷ� ��ġ")]
    [SerializeField] private float mergeAttack;
    public float MergeAttack
    {
        get { return mergeAttack; }
        set
        {
            // ���� ���ݷ��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                mergeAttack = 0;
            }
            else
            {
                mergeAttack = value;
            }
            OnMergeAttackChanged?.Invoke(mergeAttack);
        }
    }
    public UnityAction<float> OnMergeAttackChanged;

    [Tooltip("���� ���ݷ� ��ġ")]
    [SerializeField] private float weaponAttack;
    public float WeaponAttack
    {
        get { return weaponAttack; }
        set
        {
            // ���� ���ݷ��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                weaponAttack = 0;
            }
            else
            {
                weaponAttack = value;
            }
            OnWeaponAttackChanged?.Invoke(weaponAttack);
        }
    }
    public UnityAction<float> OnWeaponAttackChanged;

    [Tooltip("��ų ���ݷ� ��ġ")]
    [SerializeField] private float skillAttack;
    public float SkillAttack
    {
        get { return skillAttack; }
        set
        {
            // ��ų ���ݷ��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                skillAttack = 0;
            }
            else
            {
                skillAttack = value;
            }
            OnSkillAttackChanged?.Invoke(skillAttack);
        }
    }
    public UnityAction<float> OnSkillAttackChanged;


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
