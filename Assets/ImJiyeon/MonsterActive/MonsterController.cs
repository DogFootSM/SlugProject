using UnityEngine;

public class MonsterController : MonoBehaviour
{
    /*
        ����� ����
        1. ���� ���� (�ش� ����� �ٸ� ��ũ��Ʈ���� ���� ����)
        2. ������ ���Ͱ� ������ ������ (�ɾ�ٴϴ� ���Ͱ� �� �����Ƿ� �ɾ�ٴϴ� ���͸� ����Ʈ�� ����)
        3. ������ ������ ��, ���� ���� ���� �÷��̾��� �ݶ��̴��� ���� �� ������ ����
        4. ü���� 0�� �� ���, ��ü�� �����Ǹ� ���ÿ� ��ȭ�� ���. (���� �÷��̾� ��ȭ�� �����Ͽ� ��ϵ� ���� ++ �ϴ� ������ ������ �����ϰ� ����)

        ȭ�� �ۿ� �ִ� ���ʹ� Ʈ���Ű� on�� �Ǿ� ���� �ʵ��� ��
     */

    public enum MonsterState { Move, Attack, Dead, Size }
    [SerializeField] MonsterState CurMonsterState = MonsterState.Move;

    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;

    [Header("Player")]
    [SerializeField] GameObject Player;         // �÷��̾� ������Ʈ
    [SerializeField] GameObject PlayerBullet;   // �÷��̾� �Ѿ� ������Ʈ (������ ��ü�� ���� ����)
    // ���� �÷��̾� ������Ʈ���� model�� GetComponent�Ͽ� ���� ��, ��ȭ �߰��� ������ ���� �Է¹��� ���� 


    #region State Ŭ���� ����
    private BaseState[] States = new BaseState[(int)MonsterState.Size];
    [SerializeField] MoveState moveState;
    [SerializeField] AttackState attackState;
    [SerializeField] DeadState deadState;
    #endregion

    #region �ִϸ��̼� Hash ���� (���� �߰� ����)


    #endregion


    private void Awake()
    {
        // ������ ���ÿ� �÷��̾� ������Ʈ�� �ڵ����� �����Ѵ�.
        Player = GameObject.FindGameObjectWithTag("Player");

        States[(int)MonsterState.Move] = moveState;
        States[(int)MonsterState.Attack] = attackState;
        States[(int)MonsterState.Dead] = deadState;

        // �� �� �ڵ� ����
        monsterModel = GetComponent<MonsterModel>();
    }

    #region Start, OnDestroy, Update �Լ�
    private void Start()
    {
        States[(int)CurMonsterState].Enter();
    }

    private void OnDestroy()
    {
        States[(int)CurMonsterState].Exit();
    }

    private void Update()
    {
        States[(int)CurMonsterState].Update();
    }
    #endregion


    //====================================================

    public void ChangeState(MonsterState nextState)
    {
        States[(int)CurMonsterState].Exit();
        CurMonsterState = nextState;
        States[(int)CurMonsterState].Enter();
    }

    //====================================================

    [System.Serializable]
    private class MoveState : BaseState
    {
        [SerializeField] MonsterController Monster;
        [SerializeField] MonsterModel Model;

        public override void Update()
        {
            // Move �ൿ ����
            // ���ʹ� ���� �� ��ٷ� ���� ���� �����Ѵ�.
            Monster.transform.position = Vector2.MoveTowards(Monster.transform.position, Monster.transform.forward, Model.MonsterMoveSpeed * Time.deltaTime);

            // ���� if���� ����, Ư�� �ݶ��̴� ��(=ȭ�� ��)���� �����Ͽ��� ��� isDamaged�� on�Ͽ� �Ѿ˰� ��ȣ�ۿ� �ϰ� �� �� ���� �� �ʹ�

            // �ٸ� ���·� ��ȯ
            // ������ ü���� 0 ������ ���, ���ʹ� �����ȴ�.
            if (Model.MonsterHP < 0.01f) { Monster.ChangeState(MonsterState.Dead); }
            // ���� �Ÿ��� �÷��̾ ������ ���, ���ʹ� ������ �����Ѵ�.
            else if (Vector2.Distance(Monster.transform.position, Monster.Player.transform.position) < Model.AttackRange) { Monster.ChangeState(MonsterState.Attack); }
        }
    }

    //====================================================

    [System.Serializable]
    private class AttackState : BaseState
    {
        [SerializeField] MonsterController Monster;
        [SerializeField] MonsterModel Model;

        public override void Update()
        {
            // Attack �ൿ ����
            // ��ȹ���� ���� �� �ڵ� ���� ����
            // ���Ÿ� �������� �˰�����

            // �ٸ� ���·� ��ȯ
            if (Model.MonsterHP < 0.01f) { Monster.ChangeState(MonsterState.Dead); }
        }
    }

    //====================================================

    [System.Serializable]
    private class DeadState : BaseState
    {
        [SerializeField] MonsterController Monster;
        [SerializeField] MonsterModel Model;

        public override void Update()
        {
            // Dead �ൿ ����
            Debug.Log("���� ������");
            // ���ʹ� �����ȴ�. (Ȥ�� ������Ʈ Ǯ ���� ���)
            Destroy(Monster.gameObject);
            // ���� ������ UI�� ���� �������� �ִϸ��̼� ���
            // ���������� �÷��̾��� ��ȭ�� �����ϰ� �ִ� �ڷ��� += Model.DropGold;
        }
    }

    //====================================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerBullet = collision.gameObject;

        // �ݶ��̴��� ���� ���� ������Ʈ�� �÷��̾��� �Ѿ��̾��� ��,
        if (collision.gameObject == PlayerBullet)
        {
            Debug.Log("�Ѿ˿� ����");
            float PlayerDamage = 10f; // �ӽ� ��������
            monsterModel.MonsterHP -= PlayerDamage;
        }
    }
}
