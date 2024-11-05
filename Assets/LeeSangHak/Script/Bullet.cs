using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float speed;
    [SerializeField] GameObject target;
    public float damage;
    private PlayerController playerController;
    private Vector2 bfPosition;
    private BulletAnim bAnim;
    private bool check = false;


    private void Start()
    {
        Destroy(gameObject, 10f);
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        bAnim = GetComponent<BulletAnim>();

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

        if (collision.gameObject.tag == "Monster")
        {
            check = true;

            bAnim.PlayHitAnimation();
            
            if(playerController.weaponPrefab != null)
            {
                collision.GetComponent<MonsterModel>().MonsterHP -= PlayerDataModel.Instance.Attack * damage;
            }
            else if (playerController.weaponPrefab == null)
            {
                collision.GetComponent<MonsterModel>().MonsterHP -= PlayerDataModel.Instance.Attack;
            }

            bAnim.DestroyTime(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}