using UnityEngine;
public enum Stat
{
    Health, Attack, AttackSpeed, Size
}

public class StatSystem : MonoBehaviour
{
    /// <summary>
    /// ���� ���� ó���ϴ� ��ũ��Ʈ
    /// 
    /// �� ���� �� ��ġ�� �Ͻ������� 1�� ����
    /// 
    /// </summary>
    private PlayerDataModel playerDataModel;

    private void Start()
    {
        playerDataModel = PlayerDataModel.Instance;
    }

    /// <summary>
    /// ������ ������ 1�� ����ϴ� �Լ�
    /// https://github.com/NK-Studio/Visible-Enum-Attribute�� �߰����� ������ EnumŸ�� �Ű������� �̺�Ʈ ���� �ÿ�, �ν����Ϳ��� ã�� �� ����
    /// </summary>
    //public void StatUp(Stat stat)
    //{
    //    if (playerDataModel == null)
    //    {
    //        Debug.Log("PlayerDataModel�� �ִ� ��쿡 ���� ���� �ý����� Ȱ��ȭ�˴ϴ�.");
    //        return;
    //    }
    //    switch (stat)
    //    {
    //        case Stat.Health:
    //            playerDataModel.Health += 1;
    //            break;
    //        case Stat.Attack:
    //            playerDataModel.Attack += 1;
    //            break;
    //        case Stat.AttackSpeed:
    //            playerDataModel.AttackSpeed += 1;
    //            break;
    //        default:
    //            break;
    //    }
    //}

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
        playerDataModel.Health += 1;
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
        playerDataModel.Attack += 1;
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
        playerDataModel.AttackSpeed += 1;
    }
}
