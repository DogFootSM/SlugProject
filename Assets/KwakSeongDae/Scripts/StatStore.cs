using System;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStatStoreData
{
    Health,HealthRegen, Attack, TouchAttack, AttackSpeed
}

public class StatStore : MonoBehaviour
{
    /// <summary>
    /// ���� �������� ���� ���� ���, ���� �� ��ư�� ���� ���ǵ� �ڷ��� Ÿ��
    /// </summary>
    /// <typeparam name="T"> ������ �ڷ����� ��ġ </typeparam>
    [Serializable]
    public struct StatChangeAmount<T>
    {
        public T upValue;
        public int curCost;
        public Button buyButton;
    }

    [Header("���� ����")]
    public StatChangeAmount<int> health;
    public StatChangeAmount<float> healthRegen;
    public StatChangeAmount<float> attack;
    public StatChangeAmount<float> touchAttack;
    public StatChangeAmount<float> attackSpeed;

    private void OnEnable()
    {
        if (PlayerDataModel.Instance == null) return;
        // ������ ��ȭ�� ���� ü�� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateHealthBuyState;
        // ������ ��ȭ�� ���� ü�� ��� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateHealthRegenBuyState;
        // ������ ��ȭ�� ���� ���ݷ� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateAttackBuyState;
        // ������ ��ȭ�� ���� ��ġ���ݷ� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateTouchAttackBuyState;
        // ������ ��ȭ�� ���� ���ݼӵ� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateAttackSpeedBuyState;
    }
    private void Start()
    {
        // ������ ��ȭ�� ���� ü�� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateHealthBuyState;
        // ������ ��ȭ�� ���� ü�� ��� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateHealthRegenBuyState;
        // ������ ��ȭ�� ���� ���ݷ� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateAttackBuyState;
        // ������ ��ȭ�� ���� ��ġ���ݷ� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateTouchAttackBuyState;
        // ������ ��ȭ�� ���� ���ݼӵ� ��ư ���� ������Ʈ
        PlayerDataModel.Instance.OnMoneyChanged += UpdateAttackSpeedBuyState;
    }

    private void OnDisable()
    {
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateHealthBuyState;
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateHealthRegenBuyState;
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateAttackBuyState;
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateTouchAttackBuyState;
        PlayerDataModel.Instance.OnMoneyChanged -= UpdateAttackSpeedBuyState;
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
        if (PlayerDataModel.Instance.Money - health.curCost < 0) return;
        PlayerDataModel.Instance.Money -= health.curCost;
        PlayerDataModel.Instance.Health += health.upValue;
        // ü���� �ִ�ü�°� ����ü���� ���ÿ� ����
        PlayerDataModel.Instance.MaxHealth += health.upValue;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.Health);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateHealthBuyState(int newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (health.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (newMoney - health.curCost < 0)
        {
            health.buyButton.interactable = false;
        }
        else
        {
            health.buyButton.interactable = true;
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
        // ���� ���� ���� �ٽ� üũ
        if (PlayerDataModel.Instance.Money - healthRegen.curCost < 0) return;
        PlayerDataModel.Instance.Money -= healthRegen.curCost;
        PlayerDataModel.Instance.HealthRegen += healthRegen.upValue;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.HealthRegen);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateHealthRegenBuyState(int newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (healthRegen.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (newMoney - healthRegen.curCost < 0)
        {
            healthRegen.buyButton.interactable = false;
        }
        else
        {
            healthRegen.buyButton.interactable = true;
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
        // ���� ���� ���� �ٽ� üũ
        if (PlayerDataModel.Instance.Money - attack.curCost < 0) return;
        PlayerDataModel.Instance.Money -= attack.curCost;
        PlayerDataModel.Instance.Attack += attack.upValue;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.Attack);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateAttackBuyState(int newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (attack.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (newMoney - attack.curCost < 0)
        {
            attack.buyButton.interactable = false;
        }
        else
        {
            attack.buyButton.interactable = true;
        }
    }
    #endregion

    #region ��ġ���ݷ� ���� ���
    /// <summary>
    /// ���ݷ� ������ upValue�� ����ϴ� �Լ�
    /// </summary>
    public void TouchAttackStatUp()
    {
        if (PlayerDataModel.Instance == null)
        {
            Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
            return;
        }
        // ���� ���� ���� �ٽ� üũ
        if (PlayerDataModel.Instance.Money - touchAttack.curCost < 0) return;
        PlayerDataModel.Instance.Money -= touchAttack.curCost;
        PlayerDataModel.Instance.TouchAttack += touchAttack.upValue;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.TouchAttack);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateTouchAttackBuyState(int newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (touchAttack.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (newMoney - touchAttack.curCost < 0)
        {
            touchAttack.buyButton.interactable = false;
        }
        else
        {
            touchAttack.buyButton.interactable = true;
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
        // ���� ���� ���� �ٽ� üũ
        if (PlayerDataModel.Instance.Money - attackSpeed.curCost < 0) return;
        PlayerDataModel.Instance.Money -= attackSpeed.curCost;
        PlayerDataModel.Instance.AttackSpeed += attackSpeed.upValue;

        // �������� ���� ���� ����
        UpdateStatOnLevelUp(PlayerStatStoreData.AttackSpeed);
    }
    /// <summary>
    /// ������ ������Ʈ �ÿ� ��ư�� Ȱ��ȭ ���� �Ǵ� �Լ�
    /// </summary>
    void UpdateAttackSpeedBuyState(int newMoney)
    {
        // �ش� ��ư ������ �ȵ��ִ� ��� ����ó��
        if (attackSpeed.buyButton == null) return;

        // ���� ���� ���ο� ���� ��ư ��Ȱ��ȭ
        if (newMoney - attackSpeed.curCost < 0)
        {
            attackSpeed.buyButton.interactable = false;
        }
        else
        {
            attackSpeed.buyButton.interactable = true;
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
                var nextHealthLevel = ++PlayerDataModel.Instance.HealthLevel;
                
                // StatusStoreCSV�� �Ϸ�Ǹ� ��� �߰�

                // ���� ������ ���� ���� �ľ�

                // ���� ������ ������ ���, �ش� ���� ��ġ�� �������ͽ� �������� ������Ʈ
                
                break;
            case PlayerStatStoreData.HealthRegen:
                var nextHealthRegenLevel = ++PlayerDataModel.Instance.HealthRegenLevel;

                // StatusStoreCSV�� �Ϸ�Ǹ� ��� �߰�

                // ���� ������ ���� ���� �ľ�

                // ���� ������ ������ ���, �ش� ���� ��ġ�� �������ͽ� �������� ������Ʈ
                break;
            case PlayerStatStoreData.Attack:
                var nextAttackLevel = ++PlayerDataModel.Instance.AttackLevel;

                // StatusStoreCSV�� �Ϸ�Ǹ� ��� �߰�

                // ���� ������ ���� ���� �ľ�

                // ���� ������ ������ ���, �ش� ���� ��ġ�� �������ͽ� �������� ������Ʈ
                break;
            case PlayerStatStoreData.TouchAttack:
                var nextTouchAttackLevel = ++PlayerDataModel.Instance.TouchAttackLevel;

                // StatusStoreCSV�� �Ϸ�Ǹ� ��� �߰�

                // ���� ������ ���� ���� �ľ�

                // ���� ������ ������ ���, �ش� ���� ��ġ�� �������ͽ� �������� ������Ʈ
                break;
            case PlayerStatStoreData.AttackSpeed:
                var nextAttackSpeedLevel = ++PlayerDataModel.Instance.AttackSpeedLevel;

                // StatusStoreCSV�� �Ϸ�Ǹ� ��� �߰�

                // ���� ������ ���� ���� �ľ�

                // ���� ������ ������ ���, �ش� ���� ��ġ�� �������ͽ� �������� ������Ʈ
                break;
            default:
                break;
        }
    }
}
