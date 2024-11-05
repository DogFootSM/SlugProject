using UnityEngine;

public class MonsterShotBullet : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] PlayerDataModel playerDataModel;

    [SerializeField] float returnTime;
             private float remainTime;


    private int damage;
    public int Damage { get { return damage; } set { damage = value; } }

    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }



    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerDataModel = Player.GetComponent<PlayerDataModel>();
        rigid = GetComponent<Rigidbody2D>();
    }


    void OnEnable() { remainTime = returnTime; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Player == collision.gameObject)
        {
            //Debug.Log("���� �Ѿ� �÷��̾�� ����");
            playerDataModel.Health -= damage;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // �Ѿ� �߻�
        Vector3 dir = (Player.transform.position - transform.position).normalized;
        rigid.velocity = new Vector2(dir.x * 2f * speed, dir.y * 2f * speed);

        // �Ѿ��� ���� �ʰ� ���� �ð��� ������ ��, �ڵ����� �����ȴ�.
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { Destroy(gameObject); }
    }
}
