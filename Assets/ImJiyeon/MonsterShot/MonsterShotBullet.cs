using UnityEngine;

public class MonsterShotBullet : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] PlayerDataModel playerDataModel;
             private Rigidbody2D rb;

    [SerializeField] float monsterAttackSpeed;
    [SerializeField] int   monsterAttack;
    [SerializeField] float returnTime;
             private float remainTime;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerDataModel = Player.GetComponent<PlayerDataModel>();
        rb = GetComponent<Rigidbody2D>();
    }

    // ������Ʈ Ȱ��ȭ ��, ��Ÿ���� ���� ����
    void OnEnable() { remainTime = returnTime; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾��� �ݶ��̴��� ������ �Ѿ��� �´���� ��, �ش� �Ѿ��� �����ȴ�.
        if (Player == collision.gameObject)
        {
            playerDataModel.Health -= monsterAttack;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // �Ѿ��� ���� �� �ڵ����� �÷��̾ ���� �߻�ȴ�.
        transform.Translate(Player.transform.position * monsterAttackSpeed * Time.deltaTime);

        // �Ѿ��� ���� �ʰ� ���� �ð��� ������ ��, �ڵ����� �����ȴ�.
        remainTime -= Time.deltaTime;
        if (remainTime < 0) { Destroy(gameObject); }
    }
}
