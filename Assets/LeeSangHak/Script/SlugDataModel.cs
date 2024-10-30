using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlugDataModel : MonoBehaviour
{
    #region Data Variable
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
    #endregion

    private void OnEnable()
    {

    }
}
