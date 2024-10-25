using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    // ������ ü��  (���ʹ� ü���� ȸ������ �����Ƿ�, �ִ� ü�� ���� �ʿ����� ���� ������ �Ǵ���)
    [SerializeField] float monsterHP;
    public float MonsterHP { get { return monsterHP; } set { monsterHP = value; } }


    // ������ ���ݷ�
    [SerializeField] float monsterAttack;
    public float MonsterAttack { get { return monsterAttack; } set { monsterAttack = value; } }


    // ������ ���ݼӵ�
    [SerializeField] float monsterAttackSpeed;
    public float MonsterAttackSpeed { get { return monsterAttackSpeed; } set { monsterAttackSpeed = value; } }


    // ���Ͱ� ����Ʈ���� ��ȭ ����
    [SerializeField] int dropGold;
    public int DropGold { get { return dropGold; } set { dropGold = value; } }


    // ���� �̵� �ӵ�
    [SerializeField] float monsterMoveSpeed;
    public float MonsterMoveSpeed { get { return monsterMoveSpeed; }  set { monsterMoveSpeed = value; } }



    // ���ʹ� �Ѿ� ������ �⺻ ���̽��� �ϹǷ�, ���� �÷��̾ ���� �Ÿ� �ȿ� ���� ��� ������ ���ϴ� ����� �����
}
