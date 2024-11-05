using System;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStatStoreData
{
    Health,HealthRegen, Attack, AttackSpeed
}

public class StatStore : MonoBehaviour
{
    /// <summary>
    /// ���� �������� ���� ���� ���, ���� �� ��ư�� ���� ���ǵ� �ڷ��� Ÿ��
    /// </summary>
    [Serializable]
    public struct FloatStatChangeAmount
    {
        public float upValue;
        public long curCost;
        public Button buyButton;
        public Image inActiveImage;
    }
    [Serializable]
    public struct DemicalStatChangeAmount
    {
        public long upValue;
        public long curCost;
        public Button buyButton;
        public Image inActiveImage;
    }

    [Header("���� ����")]
    public DemicalStatChangeAmount health;
    public DemicalStatChangeAmount healthRegen;
    public DemicalStatChangeAmount attack;
    public FloatStatChangeAmount attackSpeed;

    [Header("���� ���� CSV ���� ����")]
    [SerializeField] int attackMinIndex;
    [SerializeField] int attackMaxIndex;
    [SerializeField] int attackSpeedMinIndex;
    [SerializeField] int attackSpeedMaxIndex;
    [SerializeField] int healthMinIndex;
    [SerializeField] int healthMaxIndex;
    [SerializeField] int healthRegenMinIndex;
    [SerializeField] int healthRegenMaxIndex; 

    // �ش� ������ �غ�� ��쿡��, ���� ���� ��� Ȱ��ȭ
    private bool isInit;
    private void OnEnable()
    {
        isInit = true;
    }
    private void OnDisable()
    {
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateHealthBuyState;
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateHealthRegenBuyState;
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateAttackBuyState;
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateAttackSpeedBuyState;
    }

    
    private void Update()
    {
        if (StoreCSV.Instance == null || PlayerDataModel.Instance == null) return;

        // CSV ������ �ٿ�� ��, �ʱ�ȭ ����
        if (StoreCSV.Instance.downloadCheck && isInit)
        {
            if (PlayerDataModel.Instance == null) return;
            // ������ ��ȭ�� ���� ü�� ��ư ���� ������Ʈ
            PlayerDataModel.Instance.OnMoneyChanged += UpdateHealthBuyState;
            // ������ ��ȭ�� ���� ü�� ��� ��ư ���� ������Ʈ
            PlayerDataModel.Instance.OnMoneyChanged += UpdateHealthRegenBuyState;
            // ������ ��ȭ�� ���� ���ݷ� ��ư ���� ������Ʈ
            PlayerDataModel.Instance.OnMoneyChanged += UpdateAttackBuyState;
            // ������ ��ȭ�� ���� ���ݼӵ� ��ư ���� ������Ʈ
            PlayerDataModel.Instance.OnMoneyChanged += UpdateAttackSpeedBuyState;

            print("HHHi");

            // �ʱ� ������Ʈ ����
            UpdateHealthBuyState(PlayerDataModel.Instance.Money);
            UpdateHealthRegenBuyState(PlayerDataModel.Instance.Money);
            UpdateAttackBuyState(PlayerDataModel.Instance.Money);
            UpdateAttackSpeedBuyState(PlayerDataModel.Instance.Money);

            isInit = false;
        }
    }

    #region ü�� ���� ���
    /// <summary>
    /// ü�� ������ upValue�� ����ϴ� �Լ�
    /// </summary>
    public void HealthStatUp()
    {
        if (PlayerDataModel.Instance == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� �ٽ� üũ
        if (health.curCost < 0 || PlayerDataModel.Instance.Money - health.curCost < 0) return;
        PlayerDataModel.Instance.Money -= health.curCost;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.Health);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateHealthBuyState(long newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (health.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (health.curCost < 0 || newMoney - health.curCost < 0)
        {
            health.buyButton.interactable = false;
            health.inActiveImage.gameObject.SetActive(true);
        }
        else
        {
            health.buyButton.interactable = true;
            health.inActiveImage.gameObject.SetActive(false);
        }
    }
    #endregion

    #region ü����� ���� ���
    /// <summary>
    /// ü����� ������ upValue�� ����ϴ� �Լ�
    /// </summary>
    public void HealthRegenStatUp()
    {
        if (PlayerDataModel.Instance == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        if (StoreCSV.Instance.downloadCheck == false)
        {
            Debug.Log("���� ���� ���� �����Ͱ� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� �ٽ� üũ
        if (healthRegen.curCost < 0 || PlayerDataModel.Instance.Money - healthRegen.curCost < 0) return;
        PlayerDataModel.Instance.Money -= healthRegen.curCost;
        PlayerDataModel.Instance.HealthRegen += healthRegen.upValue;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.HealthRegen);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateHealthRegenBuyState(long newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (healthRegen.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (healthRegen.curCost < 0 || newMoney - healthRegen.curCost < 0)
        {
            healthRegen.buyButton.interactable = false;
            healthRegen.inActiveImage.gameObject.SetActive(true);
        }
        else
        {
            healthRegen.buyButton.interactable = true;
            healthRegen.inActiveImage.gameObject.SetActive(false);
        }
    }
    #endregion

    #region ���ݷ� ���� ���
    /// <summary>
    /// ���ݷ� ������ upValue�� ����ϴ� �Լ�
    /// </summary>
    public void AttackStatUp()
    {
        if (PlayerDataModel.Instance == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        if (StoreCSV.Instance.downloadCheck == false)
        {
            Debug.Log("���� ���� ���� �����Ͱ� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� �ٽ� üũ
        if (attack.curCost < 0 || PlayerDataModel.Instance.Money - attack.curCost < 0) return;
        PlayerDataModel.Instance.Money -= attack.curCost;

        // ������ ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.Attack);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateAttackBuyState(long newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (attack.buyButton == null) return;
        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (attack.curCost < 0 || newMoney - attack.curCost < 0)
        {
            attack.buyButton.interactable = false;
            attack.inActiveImage.gameObject.SetActive(true);
        }
        else
        {
            attack.buyButton.interactable = true;
            attack.inActiveImage.gameObject.SetActive(false);
        }
    }
    #endregion

    #region ���ݼӵ� ���� ���
    /// <summary>
    /// ���ݼӵ� ������ upValue�� ����ϴ� �Լ�
    /// </summary>
    public void AttackSpeedStatUp()
    {
        if (PlayerDataModel.Instance == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        if (StoreCSV.Instance.downloadCheck == false)
        {
            Debug.Log("���� ���� ���� �����Ͱ� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� �ٽ� üũ
        if ( attackSpeed.curCost < 0 || PlayerDataModel.Instance.Money - attackSpeed.curCost < 0) return;
        PlayerDataModel.Instance.Money -= attackSpeed.curCost;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.AttackSpeed);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateAttackSpeedBuyState(long newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (attackSpeed.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (attackSpeed.curCost < 0 || newMoney - attackSpeed.curCost < 0)
        {
            attackSpeed.buyButton.interactable = false;
            attackSpeed.inActiveImage.gameObject.SetActive(true);
        }
        else
        {
            attackSpeed.buyButton.interactable = true;
            attackSpeed.inActiveImage.gameObject.SetActive(false);
        }
    }
    #endregion

    /// <summary>
    /// �Ű������� ������ Ÿ�԰� ���õ� ������ ������Ʈ�ϴ� �Լ�
    /// ���� ����, ���� ������ ���, ������ �� �������� ������Ʈ
    /// </summary>
    void UpdateStatOnLevelUp(PlayerStatStoreData dataType)
    {
        switch (dataType)
        {
            case PlayerStatStoreData.Health:
                PlayerDataModel.Instance.HealthLevel = UpdateDemicalStat(PlayerDataModel.Instance.HealthLevel, healthMinIndex, healthMaxIndex,ref health);
                break;
            case PlayerStatStoreData.HealthRegen:
                PlayerDataModel.Instance.HealthRegenLevel = UpdateDemicalStat(PlayerDataModel.Instance.HealthRegenLevel, healthRegenMinIndex, healthRegenMaxIndex,ref healthRegen);
                break;
            case PlayerStatStoreData.Attack:
                PlayerDataModel.Instance.AttackLevel = UpdateDemicalStat(PlayerDataModel.Instance.AttackLevel, attackMinIndex, attackMaxIndex,ref attack);
                break;
            case PlayerStatStoreData.AttackSpeed:
                PlayerDataModel.Instance.AttackSpeedLevel = UpdateFloatStat(PlayerDataModel.Instance.AttackSpeedLevel, attackSpeedMinIndex, attackSpeedMaxIndex,ref attackSpeed);
                break;
        }
    }
    int UpdateDemicalStat(int level, int minIndex, int maxIndex, ref DemicalStatChangeAmount stat)
    {
        int nextLevel = level + 1;
        int curIndex = minIndex + level - 1;
        // ������ �̹� �ִ� �����̸� ���� ������ ��ȯ
        if (curIndex + 1 > maxIndex)
        {
            stat.upValue = -1;
            stat.curCost = -1;
            return level;
        }
        else
        {
            var store = StoreCSV.Instance.Store;
            // ���� ���Ȱ� ���� ���Ȱ��� ������ ���
            long curUpStat = (long)(store[curIndex].StatusStore_satatusNum * Mathf.Pow(10, store[curIndex].StatusStore_satatusUnit));
            long nxtUpStat = (long)(store[curIndex+1].StatusStore_satatusNum * Mathf.Pow(10, store[curIndex+1].StatusStore_satatusUnit));
            long nxtPriceGold = (long)(store[curIndex].StatusStore_priceGoldNum * Mathf.Pow(10, store[curIndex].StatusStore_priceGoldUnit));

            stat.upValue = nxtUpStat - curUpStat;
            stat.curCost = nxtPriceGold;
            return nextLevel;
        }
    }

    int UpdateFloatStat(int level, int minIndex, int maxIndex, ref FloatStatChangeAmount stat)
    {
        int nextLevel = level + 1;
        int currentIndex = minIndex + nextLevel - 1;

        if (currentIndex + 1 > maxIndex)
        {
            stat.upValue = -1;
            stat.curCost = -1;
            return nextLevel;
        }
        else
        {
            var store = StoreCSV.Instance.Store;
            // ���� ���Ȱ� ���� ���Ȱ��� ������ ���
            float curUpStat = store[currentIndex].StatusStore_satatusNum * Mathf.Pow(10, store[currentIndex].StatusStore_satatusUnit);
            float nxtUpStat = store[currentIndex + 1].StatusStore_satatusNum * Mathf.Pow(10, store[currentIndex + 1].StatusStore_satatusUnit);
            long nxtPriceGold = (long)(store[currentIndex + 1].StatusStore_priceGoldNum * Mathf.Pow(10, store[currentIndex + 1].StatusStore_priceGoldUnit));

            stat.upValue = nxtUpStat - curUpStat;
            stat.curCost = nxtPriceGold;
            return nextLevel;
        }
    }
}
