using UnityEngine;
using UnityEngine.Pool;

public class MonsterShotBullet : MonoBehaviour
{
    //[SerializeField] GameObject target;
    [SerializeField] GameObject Player;
            private Rigidbody2D rb;

    [SerializeField] float monsterAttackSpeed;
    [SerializeField] float returnTime;
             private float remainTime;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // ������Ʈ Ȱ��ȭ ��, ��Ÿ���� ���� ����
    void OnEnable() { remainTime = returnTime; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾��� �ݶ��̴��� ������ �Ѿ��� �´���� ��, �ش� �Ѿ��� �����ȴ�.
        if (Player == collision.gameObject) { Destroy(gameObject); }

        // + �÷��̾� ü�¿� MonsterAttack ��ŭ -- ���ִ� �ڵ带 ������ ��
    }

    void Update()
    {
        transform.Translate(Player.transform.position * monsterAttackSpeed * Time.deltaTime);

        // �Ѿ��� ���� �ʰ� ���� �ð��� ������ ���� �ڵ����� �����ȴ�.
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { Destroy(gameObject); }
    }

    //public void SetTarget(GameObject target)
    //{
    //    this.target = target;
    //}

}
