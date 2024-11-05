using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseUIContorller : MonoBehaviour
{
    [Header("UI ��� ����")]
    [Header("������ ���� UI")]
    [SerializeField] Sprite userIcon;
    public Sprite UserIcon
    {
        get { return userIcon; }
        set { userIcon = value; OnUserIconChanged?.Invoke(userIcon); }
    }
    public UnityAction<Sprite> OnUserIconChanged;

    [SerializeField] private int battlePower;
    public int BattlePower
    {
        get { return battlePower; }
        set
        {
            battlePower = value;
            OnBattlePowerChanged?.Invoke(battlePower);
        }
    }
    public UnityAction<int> OnBattlePowerChanged;

    [Header("���������� �����Ȳ UI")]
    [Tooltip("���� �������� ���� ������")]
    [SerializeField] private Sprite bossIcon;
    public Sprite BossIcon
    {
        get { return bossIcon; }
        set
        {
            bossIcon = value;
            OnBossIconChanged?.Invoke(bossIcon);
        }
    }
    public UnityAction<Sprite> OnBossIconChanged;

    [Header("������ ���� (�� �ʱ⿡�� �۵�, ���� ������� ���� �κ��丮 �����۰� ����)")]
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject[] partners;
    [SerializeField] private GameObject[] slugs;

    [Header("\n\n(���� X,�����տ� ���� �ȵ� ��� ����) UI ���")]
    [Header("���÷��̾� ���� UI")]
    [SerializeField] private Image userIconImage;
    [SerializeField] private TextMeshProUGUI battlePowerText;
    [Header("���������� ���� UI")]
    [SerializeField] private Image bossIconImage;
    [SerializeField] private TextMeshProUGUI stageText;
    [Header("���κ��丮 UI")]
    [SerializeField] private Transform weaponInventory;
    [SerializeField] private Transform partnerInventory;
    [SerializeField] private Transform slugInventory;

    private void Awake()
    {
        // UI�� ���� ��ȯ�ǵ� ������� �ʵ��� ��ġ
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        OnUserIconChanged += ChangeUserIcon;
        OnBattlePowerChanged += ChangeBattlePowerText;
        OnBossIconChanged += ChangeBossIcon;

    }

    private void OnDisable()
    {
        OnUserIconChanged -= ChangeUserIcon;
        OnBattlePowerChanged -= ChangeBattlePowerText;
        OnBossIconChanged -= ChangeBossIcon;

    }

    private void Start()
    {
        // �� �κ��丮�� ������ �κ��丮 �����۵�� �ʱ�ȭ
        if (weaponInventory != null && partnerInventory != null && slugInventory != null)
        {
            // �� ���Կ� �κ��丮 ������ ����
            InventoryItem[] inventoryWeapons = weaponInventory.GetComponentsInChildren<InventoryItem>();
            for (int i = 0; i < inventoryWeapons.Length; i++)
            {
                if (weapons.Length <= i) break;
                inventoryWeapons[i].ItemObject = weapons[i];
                inventoryWeapons[i].SlotCheck();
            }
            InventoryItem[] inventoryPartners = partnerInventory.GetComponentsInChildren<InventoryItem>();
            for (int i = 0; i < inventoryPartners.Length; i++)
            {
                if (partners.Length <= i) break;
                inventoryPartners[i].ItemObject = partners[i];
                inventoryPartners[i].SlotCheck();
            }
            InventoryItem[] inventorySlugs = slugInventory.GetComponentsInChildren<InventoryItem>();
            for (int i = 0; i < inventorySlugs.Length; i++)
            {
                if (partners.Length <= i) break;
                inventorySlugs[i].ItemObject = partners[i];
                inventorySlugs[i].SlotCheck();
            }
        }

        ChangeUserIcon(userIcon);
        ChangeBattlePowerText(battlePower);
        ChangeBossIcon(bossIcon);
    }

    // �ν����Ϳ��� ����� ���뵵 �̺�Ʈ ȣ��ǵ��� ����
    private void OnValidate()
    {
        OnUserIconChanged?.Invoke(userIcon);
        OnBattlePowerChanged?.Invoke(battlePower);
        OnBossIconChanged?.Invoke(bossIcon);
    }

    // ���� �������� �ٲ�� �̺�Ʈ ó�� �Լ�
    void ChangeUserIcon(Sprite newSprite)
    {
        // ���� �̹����� ������ ����
        if (userIconImage != null)
        {
            userIconImage.sprite = newSprite;
        }
    }

    // ������ �ؽ�Ʈ �ٲ�� �̺�Ʈ ó�� �Լ�
    void ChangeBattlePowerText (int newBattlePower)
    {
        // ������ �ؽ�Ʈ ����
        battlePowerText?.SetText(newBattlePower.ToString());
    }

    // ���� �������� �ٲ�� �̺�Ʈ ó�� �Լ�
    void ChangeBossIcon(Sprite newSprite)
    {
        // ���� �̹����� ������ ����
        if (bossIconImage != null)
        {
            bossIconImage.sprite = newSprite;
        }
    }
}
