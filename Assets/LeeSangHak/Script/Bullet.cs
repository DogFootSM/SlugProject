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
    [SerializeField] Animator animator;
    private bool check = false;

    private void Start()
    {
        Destroy(gameObject, 10f);
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (target != null)
        {
            bfPosition = (target.transform.position - transform.position).normalized;
        }
    }

    private void Update()
    {
        if (target == null || !target.activeSelf)
        {
            // Ÿ���� ���ų� ��Ȱ��ȭ�� ��� �ʱ� �������� �̵�
            transform.Translate(bfPosition * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // Ȱ��ȭ�� Ÿ���� ���� �̵�
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        playerController.RemoveBullets(gameObject); // źȯ ���� ��û
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (check)
            return;
        Debug.Log("����");
        if (collision.gameObject.tag == "Monster")
        {
            check = true;
            if (animator != null)
            {
                animator.SetBool("isHit", true);
            }
            collision.GetComponent<MonsterModel>().MonsterHP -= PlayerDataModel.Instance.Attack;
            if (gameObject.name == "Player_Flame(Clone)")
            {
                Destroy(gameObject, 0.8f);
            }
            else
            {
                Debug.Log("����");
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}