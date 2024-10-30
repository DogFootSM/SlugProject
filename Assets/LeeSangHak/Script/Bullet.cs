using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float speed;
    [SerializeField] GameObject target;
    private PlayerController playerController;
    private Vector2 bfPosition;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        bfPosition = (target.transform.position - transform.position).normalized;
    }

    private void Awake()
    {

    }

    private void Update()
    {
        // Ÿ���� ����ְų� Ÿ���� ��Ȱ��ȭ�϶�
        if (target == null)
        {
            transform.Translate(bfPosition * speed * Time.deltaTime, Space.World);
        }


        // Ÿ���� �ְ� Ȱ��ȭ�϶�
        if (target != null && target.activeSelf == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
            playerController.RemoveBullets(gameObject); // źȯ ���� ��û
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����");
        if (collision.gameObject.tag == "Monster")
        {
            Debug.Log("Ÿ������");
            collision.GetComponent<MonsterModel>().MonsterHP -= PlayerDataModel.Instance.Attack;
            Debug.Log("���Ͱ� �޴� ������");
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
