using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlugBullet : MonoBehaviour
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
            // 타겟이 없거나 비활성화된 경우 초기 방향으로 이동
            transform.Translate(bfPosition * speed * Time.deltaTime, Space.World);
        }
        else
        {
            // 활성화된 타겟을 향해 이동
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        playerController.RemoveBullets(gameObject); // 탄환 제거 요청
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (check)
            return;

        if (collision.gameObject.tag == "Monster")
        {
            check = true;

            if (bAnim != null)
                bAnim.PlayHitAnimation();

            collision.GetComponent<MonsterModel>().MonsterHP -= damage;

            if (bAnim != null)
                bAnim.DestroyTime(gameObject);

            Destroy(gameObject);
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