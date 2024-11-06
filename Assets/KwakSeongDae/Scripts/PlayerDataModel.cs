using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static StatStore;

public class PlayerDataModel : MonoBehaviour
{
    #region Data Variable
    [Header("�÷��̾� ���� ����")]
    #region ü�� ����
    [Header("�÷��̾� ü��")]
    [Tooltip("�÷��̾� ���� ü��")]
    [SerializeField] private long health;
    public long Health
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
                if (value > MaxHealth)
                {
                    health = MaxHealth;
                }
                else health = value;
            }
            OnHealthChanged?.Invoke(health);
        }
    }
    public UnityAction<long> OnHealthChanged;

    [Tooltip("�÷��̾� �ִ� ü��")]
    [SerializeField] private long maxHealth;
    public long MaxHealth
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
    public UnityAction<long> OnMaxHealthChanged;

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
                if (StoreCSV.Instance != null &&  StoreCSV.Instance.downloadCheck == true)
                {
                    long stat = DemicalDataFromStoreCSV(healthLevel, healthMinIndex, healthMaxIndex);
                    // �ִ� ü���� csv�����ͷ� ����
                    long difHealth = stat - MaxHealth;
                    MaxHealth = stat;
                    Health += difHealth;
                }
                // ü�� ������ ���� CSV�����͸� �����ؼ� ����ȭ �۾� �ʿ�
            }
            OnHealthLevelChanged?.Invoke(healthLevel);
        }
    }
    public UnityAction<int> OnHealthLevelChanged;

    [Header("�÷��̾� ü�����")]
    [Tooltip("�÷��̾� ü�����")]
    [SerializeField] private long healthRegen;
    public long HealthRegen
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
    public UnityAction<long> OnHealthRegenChanged;

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
                if (StoreCSV.Instance != null && StoreCSV.Instance.downloadCheck == true)
                {
                    long stat = DemicalDataFromStoreCSV(healthRegenLevel, healthRegenMinIndex, healthRegenMaxIndex);
                    HealthRegen = stat;
                }
            }
            OnHealthRegenLevelChanged?.Invoke(healthRegenLevel);
        }
    }
    public UnityAction<int> OnHealthRegenLevelChanged;
    #endregion

    #region ���� ����
    [Header("�÷��̾� �⺻ ���ݷ�")]
    [Tooltip("�÷��̾� �⺻ ���ݷ�")]
    [SerializeField] private long attack;
    public long Attack
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
                // ���ݷ� ��� �ý���
                var attackStat = DemicalDataFromStoreCSV(attackLevel, attackMinIndex, attackMaxIndex);
                attack = attackStat + (long)BulletAttack;
            }
            OnAttackChanged?.Invoke(attack);
        }
    }

    public UnityAction<long> OnAttackChanged;

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
                // ���ݷ� ����
                Attack = Attack;
            }
            OnAttackLevelChanged?.Invoke(attackLevel);
        }
    }
    public UnityAction<int> OnAttackLevelChanged;

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
                if (StoreCSV.Instance != null && StoreCSV.Instance.downloadCheck == true)
                {
                    float stat = FloatDataFromStoreCSV(attackSpeedLevel, attackSpeedMinIndex, attackSpeedMaxIndex);
                    AttackSpeed = stat;
                }
            }
            OnAttackSpeedLevelChanged?.Invoke(attackSpeedLevel);
        }
    }
    public UnityAction<int> OnAttackSpeedLevelChanged;

    [Header("�÷��̾� �Ѿ� ���ݷ�")]
    [Tooltip("�÷��̾� �⺻ ���ݼӵ�")]
    [SerializeField] private float bulletAttack;
    public float BulletAttack
    {
        get { return bulletAttack; }
        set
        {
            // ���ݼӵ��� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                bulletAttack = 0;
            }
            else
            {
                bulletAttack = value;
            }
            OnBulletAttackChanged?.Invoke(attackSpeed);
        }
    }
    public UnityAction<float> OnBulletAttackChanged;

    [Tooltip("�÷��̾� �Ѿ� �ְ� ����")]
    [SerializeField] private int bulletLevel;
    public int BulletLevel
    {
        get { return bulletLevel; }
        set
        {
            // �⺻ ���ݷ� ������ ���ܻ�Ȳ ó��
            if (value < 0)
            {
                bulletLevel = 0;
            }
            else
            {
                bulletLevel = value;
                if (StoreCSV.Instance != null && StoreCSV.Instance.downloadCheck == true)
                {
                    float stat = FloatDataFromStoreCSV(bulletLevel, attackSpeedMinIndex, attackSpeedMaxIndex);
                    AttackSpeed = stat;
                }
                // ���ݷ� ����
                Attack = Attack;
            }
            OnBulletLevelChanged?.Invoke(bulletLevel);
        }
    }
    public UnityAction<int> OnBulletLevelChanged;
    #endregion

    #region ��ȭ ����
    [Header("�÷��̾� ���� ��ȭ")]
    [Tooltip("�÷��̾ ������ �⺻ ��ȭ")]
    [SerializeField] private long money;
    public long Money
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
    public UnityAction<long> OnMoneyChanged;

    [Tooltip("�÷��̾ ������ ���� ������ ��ȭ ��ȭ (���ǻ�, ũ����Ż�� ��Ī)")]
    [SerializeField] private long jewel;
    public long Jewel
    {
        get { return jewel; }
        set
        {
            // ũ����Ż�� ���ܻ�Ȳ ó��
            if (value < 0)
            {
                jewel = 0;
            }
            else
            {
                jewel = value;
            }
            OnJewelChanged?.Invoke(jewel);
        }
    }
    public UnityAction<long> OnJewelChanged;

    #endregion

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

    // CSV�κ��� �޾ƿ� ������ �ε��� ����
    [Header("CSV ���� ����")]
    [SerializeField] int attackMinIndex;
    [SerializeField] int attackMaxIndex;
    [SerializeField] int attackSpeedMinIndex;
    [SerializeField] int attackSpeedMaxIndex;
    [SerializeField] int healthMinIndex;
    [SerializeField] int healthMaxIndex;
    [SerializeField] int healthRegenMinIndex;
    [SerializeField] int healthRegenMaxIndex;
    [SerializeField] int bulletMinIndex;
    [SerializeField] int bulletMaxIndex;

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

        InitializeValue();
    }

    long DemicalDataFromStoreCSV(int level, int minIndex, int maxIndex)
    {
        if (StoreCSV.Instance.downloadCheck == false)
        {
            print("CSV �ε� �ȵ�");
            return -1;
        }
        int currentIndex = minIndex + level - 1;
        if (currentIndex <= maxIndex && currentIndex >= minIndex)
        {
            var store = StoreCSV.Instance.Store;
            return (long)(store[currentIndex].StatusStore_satatusNum * Mathf.Pow(10, store[currentIndex].StatusStore_satatusUnit));
        }
        print($"���������� �ε��� level: {level} minIndex: {minIndex} maxIndex: {maxIndex}");

        return -1;
    }

    float FloatDataFromStoreCSV(int level, int minIndex, int maxIndex)
    {
        if (StoreCSV.Instance.downloadCheck == false)
        {
            print("CSV �ε� �ȵ�");
            return -1;
        }
        int currentIndex = minIndex + level - 1;

        if (currentIndex <= maxIndex && currentIndex >= minIndex)
        {
            var store = StoreCSV.Instance.Store;
            return store[currentIndex].StatusStore_satatusNum * Mathf.Pow(10, store[currentIndex].StatusStore_satatusUnit);
        }
        print($"���������� �ε��� level: {level} minIndex: {minIndex} maxIndex: {maxIndex}");
        return -1;
    }

    float FloatDataFromBulletCSV(int level, int minIndex, int maxIndex)
    {
        if (BulletCSV.Instance == null || BulletCSV.Instance.downloadCheck == false)
        {
            print("CSV �ε� �ȵ�");
            return -1;
        }

        int currentIndex = minIndex + level - 1;
        if (currentIndex <= maxIndex && currentIndex >= minIndex)
        {
            var bullet = BulletCSV.Instance.Bullet;

            return bullet[currentIndex].Bullet_num * Mathf.Pow(10, bullet[currentIndex].Bullet_unit);
        }
        print($"���������� �ε��� level: {level} minIndex: {minIndex} maxIndex: {maxIndex}");
        return -1;
    }

    void InitializeValue()
    {
        healthLevel = 1;
        healthRegenLevel = 1;
        attackLevel = 1;
        attackSpeedLevel = 1;
        bulletLevel = 1;

        health = DemicalDataFromStoreCSV(healthLevel, healthMinIndex, healthMaxIndex);
        maxHealth = health;
        healthRegen = DemicalDataFromStoreCSV(healthRegenLevel, healthRegenLevel, healthRegenMaxIndex);
        attack = DemicalDataFromStoreCSV(attackLevel, attackMinIndex, attackMaxIndex);
        attackSpeed = DemicalDataFromStoreCSV(attackSpeedLevel, attackSpeedMinIndex, attackSpeedMaxIndex);
        bulletAttack = DemicalDataFromStoreCSV(bulletLevel, bulletMinIndex, bulletMaxIndex);

        OnHealthChanged?.Invoke(health);
        OnMaxHealthChanged?.Invoke(maxHealth);
        OnHealthRegenChanged?.Invoke(healthRegen);
        OnAttackChanged?.Invoke(attack);
        OnAttackSpeedChanged?.Invoke(attackSpeed);
        OnBulletAttackChanged?.Invoke(bulletAttack);
    }
}
