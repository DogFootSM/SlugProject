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

    private void Start()
    {
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
        if (collision.gameObject.tag == "Monster")
        {
            if(animator != null)
            {
                animator.SetBool("isHit", true);
            }            
            collision.GetComponent<MonsterModel>().MonsterHP -= PlayerDataModel.Instance.Attack;
            if(gameObject.name== "Player_Flame(Clone)")
            {
                Debug.Log("2.9초뒤 삭제");
                Destroy(gameObject, 0.8f);
            }
            else
            {
                Destroy(gameObject);
            }            
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
