using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState { Move, Attack, Dead, Size }
    [SerializeField] MonsterState CurMonsterState = MonsterState.Move;

    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;

    [Header("Player")]
    [SerializeField] GameObject Player;               // �÷��̾� ������Ʈ
    [SerializeField] PlayerDataModel PlayerDataModel; // �÷��̾� �� ����

    [Header("Bullet")]
    [SerializeField] float CoolTime;            // �Ѿ� �߻翡 �ɸ��� ��Ÿ��
    [SerializeField] GameObject MonsterBullet;  // ���� �Ѿ� ������Ʈ (������ ��ü ����)
             private GameObject bullet;
    [SerializeField] Transform muzzlePoint;     // ������ �Ѿ��� ������ �������� �� ������Ʈ
             private bool isAttacked;           // ���� ���� �Ǵ� ����

    // ���� �÷��̾� ������Ʈ���� model�� GetComponent�Ͽ� ���� ��, ��ȭ �߰��� ������ ���� �Է¹��� ���� 
    // ���� �������� ȭ�� ���� ��, ȭ�鿡 ȭ�� �ȹ��� �����ϴ� �ݶ��̴��� �����Ͽ� �ǰ� ���θ� Ȯ���� ����

    #region State Ŭ���� ����
    private BaseState[] States = new BaseState[(int)MonsterState.Size];
    [SerializeField] MoveState moveState;
    [SerializeField] AttackState attackState;
    [SerializeField] DeadState deadState;
    #endregion

    [Header("Animation")]
    #region �ִϸ��̼� Hash ����
    [SerializeField] Animator monsterAnimator;
    private int curAniHash;
    private static int monsterMoveHash = Animator.StringToHash("MonsterMove");
    private static int monsterAttackHash = Animator.StringToHash("MonsterAttack");
    private static int monsterDeadHash = Animator.StringToHash("MonsterDead");
    #endregion


    private void Awake()
    {
        // ������ ���ÿ� �÷��̾� ������Ʈ�� �ڵ����� �����Ѵ�.
        Player = GameObject.FindGameObjectWithTag("Player");
        // �÷��̾� �� �ڵ�����
        PlayerDataModel = Player.GetComponent<PlayerDataModel>();
        // �� �� �ڵ� ����
        monsterModel = GetComponent<MonsterModel>();

        States[(int)MonsterState.Move] = moveState;
        States[(int)MonsterState.Attack] = attackState;
        States[(int)MonsterState.Dead] = deadState;
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
            if (Monster.isAttacked == true) { Monster.isAttacked = false; }

            // ���ʹ� ���� �� ��ٷ� ���� ���� �����Ѵ�.
            Monster.AnimatorPlay();
            Monster.transform.position = Vector2.MoveTowards(Monster.transform.position, Monster.Player.transform.position, Model.MonsterMoveSpeed * Time.deltaTime);

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

        private Rigidbody2D rigid;
        private Transform TargetPlayer;

        public override void Update()
        {
            if (Monster.isAttacked == false)
            {
                Monster.isAttacked = true;
                Monster.shot();
            }

            // �ٸ� ���·� ��ȯ
            if (Model.MonsterHP < 0.01f) { Monster.ChangeState(MonsterState.Dead); }
        }
    }

    #region �߻� �ڷ�ƾ
    void shot()
    {
        StartCoroutine(WaitingShot());
    }

    IEnumerator WaitingShot()
    {
        while (isAttacked)
        {
            Debug.Log("���� �Ѿ� �߻�");
            AnimatorPlay();
            bullet = Instantiate(MonsterBullet, muzzlePoint.transform.position, muzzlePoint.transform.rotation);

            MonsterShotBullet bulletScript = bullet.GetComponent<MonsterShotBullet>();
            bulletScript.Damage = monsterModel.MonsterAttack;
            bulletScript.Speed = monsterModel.MonsterAttackSpeed;

            yield return new WaitForSeconds(CoolTime);     
        }
    }
    #endregion

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
            Monster.AnimatorPlay();
            // ������ UI�� ���� �������� �ִϸ��̼� ���
            Monster.PlayerDataModel.Money += Model.DropGold;
            Destroy(Monster.gameObject); // ���� ��ü�� ������Ʈ Ǯ ������ �����ϰ� �־ ������
        }
    }

    //====================================================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ݶ��̴��� ���� ���� ������Ʈ�� �÷��̾��� �Ѿ��̾��� ��,
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("���Ͱ� �÷��̾� �Ѿ˿� ����");
            monsterModel.MonsterHP -= PlayerDataModel.Attack;
        }
    }

    // ==============================================

    void AnimatorPlay()
    {
        int checkAniHash;

        // Dead ����
        if (monsterModel.MonsterHP < 0.01f) { checkAniHash = monsterDeadHash; }
        // Attack ����
        else if (Vector2.Distance(transform.position, Player.transform.position) < monsterModel.AttackRange) { checkAniHash = monsterAttackHash; }
        // Move ����
        else { checkAniHash = monsterMoveHash; }


        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            monsterAnimator.Play(curAniHash);
        }
    }
}
