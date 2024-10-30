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
    [SerializeField] Transform trans;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Awake()
    {

    }

    private void Update()
    {
        trans = target.transform;
        // Ÿ���� ����ְų� Ÿ���� ��Ȱ��ȭ�϶�
        if (target == null)
        {
            transform.Translate(trans.position * speed * Time.deltaTime);
        }


        // Ÿ���� �ְ� Ȱ��ȭ�϶�
        if (target != null && target.activeSelf == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, trans.position, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����");
        if (collision.gameObject.tag == "Monster" && collision.gameObject == target)
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
