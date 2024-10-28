using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    // ������ ȭ�� ���� ���� (ȭ�� ������ �����ϴ� �ݶ��̴����� ������ �ʿ��غ��δ�)
    [SerializeField] bool isInside;
    public bool IsInside { get { return isInside; } set { isInside = value; } }
     
    // ������ ü��  (���ʹ� ü���� ȸ������ �����Ƿ�, �ִ� ü�� ���� �ʿ����� ���� ������ �Ǵ���)
    [SerializeField] float monsterHP;
    public float MonsterHP { get { return monsterHP; } set { monsterHP = value; } }

    // ���� �̵� �ӵ�
    [SerializeField] float monsterMoveSpeed;
    public float MonsterMoveSpeed { get { return monsterMoveSpeed; } set { monsterMoveSpeed = value; } }

    // ���Ͱ� ����Ʈ���� ��ȭ ����
    [SerializeField] int dropGold;
    public int DropGold { get { return dropGold; } set { dropGold = value; } }



    [Header("Range")]
    // ���Ͱ� �÷��̾ �����ϴ� �ּ� �Ÿ� ����. �ش� �Ÿ� ���� ������ ���ʹ� ���Ÿ� ������ �����Ѵ�.
    [SerializeField] int attackRange;
    public int AttackRange { get { return attackRange; } set { attackRange = value; } }
}
