using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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
             private bool isAttacked;           // ���� ���� �Ǵ� ����
    [SerializeField] GameObject MonsterBullet;  // ���� �Ѿ� ������Ʈ (������ ��ü ����)
             private GameObject bullet;
    [SerializeField] Transform muzzlePoint;     // ������ �Ѿ��� ������ �������� �� ������Ʈ


    #region State Ŭ���� ����
    private BaseState[] States = new BaseState[(int)MonsterState.Size];
    [SerializeField] MoveState moveState;
    [SerializeField] AttackState attackState;
    [SerializeField] DeadState deadState;
    #endregion

    [Header("Animation")]
    #region �ִϸ��̼� Hash ����
    [SerializeField] Animator monsterAnimator;
    [SerializeField] Animator muzzlePointAnimator;

    private int curAniHash;
    private static int monsterMoveHash = Animator.StringToHash("MonsterMove");
    private static int monsterAttackHash = Animator.StringToHash("MonsterAtk");
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
        monsterModel.MonsterHP = 300f;
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
            if (Model.MonsterHP < 0.01f) { Monster.ChangeState(MonsterState.Dead); }
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
                Monster.StartCoroutine(Monster.WaitingShot());


            }

            // �ٸ� ���·� ��ȯ
            if (Model.MonsterHP < 0.01f) { Monster.ChangeState(MonsterState.Dead); }
        }
    }

    #region �߻� �ڷ�ƾ
    IEnumerator WaitingShot()
    {
        // �κ��丮�� �������� ���� ��Ȳ������ �ڷ�ƾ�� ����ǵ��� ��
        yield return new WaitUntil(() => GameManager.Instance.IsOpenInventory == false);

        while (isAttacked)
        {
            AnimatorPlay();
            yield return null;
        }
    }

    void shot()
    {
        Debug.Log("���� �Ѿ� �߻�");
        bullet = Instantiate(MonsterBullet, muzzlePoint.transform.position, muzzlePoint.transform.rotation);

        MonsterShotBullet bulletScript = bullet.GetComponent<MonsterShotBullet>();
        bulletScript.Damage = monsterModel.MonsterAttack;
        bulletScript.Speed = monsterModel.MonsterAttackSpeed;
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
            Monster.AnimatorPlay();
        }
    }

    void Dead()
    {
        Debug.Log("���� ������");

        // ������ UI�� ���� �������� �ִϸ��̼� ���
        PlayerDataModel.Money += monsterModel.DropGold;
        // ���� ��ü�� ������Ʈ Ǯ �������� �����ϰ� �־ ������
        Destroy(gameObject);
    }

    //====================================================

    void AnimatorPlay()
    {
        int checkAniHash;
        monsterAnimator.SetFloat("MoveSpeed", 0.2f * monsterModel.MonsterMoveSpeed);
        monsterAnimator.SetFloat("AtkSpeed", 0.25f * monsterModel.MonsterAttackSpeed);

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

    void MuzzlePointAni() { muzzlePointAnimator.Play("BulletStart"); }
}
