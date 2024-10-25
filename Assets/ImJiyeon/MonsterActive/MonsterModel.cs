using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    // ������ ü��  (���ʹ� ü���� ȸ������ �����Ƿ�, �ִ� ü�� ���� �ʿ����� ���� ������ �Ǵ���)
    [SerializeField] float monsterHP;
    public float MonsterHP { get { return monsterHP; } set { monsterHP = value; } }

    // ���� �̵� �ӵ�
    [SerializeField] float monsterMoveSpeed;
    public float MonsterMoveSpeed { get { return monsterMoveSpeed; } set { monsterMoveSpeed = value; } }

    // ���Ͱ� ����Ʈ���� ��ȭ ����
    [SerializeField] int dropGold;
    public int DropGold { get { return dropGold; } set { dropGold = value; } }


    [Header("Attack")]
    // ������ ���ݷ�
    [SerializeField] float monsterAttack;
    public float MonsterAttack { get { return monsterAttack; } set { monsterAttack = value; } }

    // ������ ���ݼӵ�
    [SerializeField] float monsterAttackSpeed;
    public float MonsterAttackSpeed { get { return monsterAttackSpeed; } set { monsterAttackSpeed = value; } }


    [Header("Range")]
    // ���Ͱ� �÷��̾ �����ϴ� �ּ� �Ÿ� ����. �ش� �Ÿ� ���� ������ ���ʹ� ���Ÿ� ������ �����Ѵ�.
    [SerializeField] int attackRange;
    public int AttackRange { get { return attackRange; } set { attackRange = value; } }
}
