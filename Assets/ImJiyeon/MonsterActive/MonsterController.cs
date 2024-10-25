using System.Collections;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public enum MonsterState { Move, Attack, Dead, Size }
    [SerializeField] MonsterState CurMonsterState = MonsterState.Move;

    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;

    [Header("Player")]
    [SerializeField] GameObject Player;         // �÷��̾� ������Ʈ
    [SerializeField] GameObject PlayerBullet;   // �÷��̾� �Ѿ� ������Ʈ (������ ��ü�� ���� ����)

    [Header("Bullet")]
    [SerializeField] GameObject MonsterBullet;  // ���� �Ѿ� ������Ʈ (������ ��ü ����)
    [SerializeField] Transform muzzlePoint;     // ������ �Ѿ��� ������ �������� �� ������Ʈ

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
            //Monster.AnimatorPlay();

            // ���� if���� ����, Ư�� �ݶ��̴� ��(=ȭ�� ��)���� �����Ͽ��� ���
            // isDamaged�� on�Ͽ� �Ѿ˰� ��ȣ�ۿ� �ϰ� �� �� ���� �� �ʹ�

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
            Monster.AnimatorPlay();

            // �Ѿ� ����
            GameObject bullet = Instantiate(Monster.MonsterBullet, Monster.muzzlePoint.position, Monster.muzzlePoint.rotation);
            //MonsterShotBullet bulletShot = bullet.GetComponent<MonsterShotBullet>();
            //bulletShot.SetTarget(Monster.Player);
            Monster.Wait();

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
            //Monster.AnimatorPlay();
            Destroy(Monster.gameObject); // ���� ��ü�� ������Ʈ Ǯ ������ �����ϰ� �־ ������
            // ������ UI�� ���� �������� �ִϸ��̼� ���
            // �÷��̾��� ��ȭ�� �����ϰ� �ִ� �ڷ��� += Model.DropGold;
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

    // ==============================================

    void Wait()
    {
        StartCoroutine(TimerUse());
    }

    IEnumerator TimerUse()
    {
            yield return new WaitForSeconds(1.5f);
    }

}
