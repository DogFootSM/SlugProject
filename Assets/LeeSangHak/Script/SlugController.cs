using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlugController : MonoBehaviour
{
    public GameObject targetMonster = null;
    public float attackCooldown = 0f;
    [SerializeField] GameObject weaponPrefab;
    public int attackRange;
    [SerializeField] float times;
    [SerializeField] GameObject[] monsters;
    public GameObject muzzlePoint;
    [SerializeField] Animator slugAnim;
    [SerializeField] Animator slugsubAnim;
    private float attackSpeed;


    private void Start()
    {
        // �� ������ : ���� �ӵ� �� ��Ÿ�
        attackSpeed = 5f;
        attackRange = 20;
    }



    private void Update()
    {
        // if (Time.timeScale == 0)
        // {
        times = Time.time;

        // Ÿ������ ������ ���Ͱ� ����ְ� ���Ͱ� Ȱ��ȭ�� �ƴ� ��
        if (targetMonster == null || !targetMonster.activeSelf)
        {
            FindTarget();
        }

        if (targetMonster != null && Time.time >= attackCooldown)
        {
            Attack();
        }
        //  }

    }

    private void FindTarget()
    {
        slugAnim.SetBool("isMove", false);
        if (slugsubAnim != null)
        {
            slugsubAnim.gameObject.SetActive(true);
        }
        


        monsters = GameObject.FindGameObjectsWithTag("Monster");

        float closestDistance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float distance = Vector2.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetMonster = monster;
            }
        }
    }

    public void Attack()
    {
        if (targetMonster != null && Vector2.Distance(transform.position, targetMonster.transform.position) < attackRange)
        {
            slugAnim.SetBool("isMove", true);
            
            if (slugsubAnim != null)
            {
                slugsubAnim.gameObject.SetActive(false);
            }


            ShootBullet();

            attackCooldown = Time.time + attackSpeed;
        }
    }

    private void ShootBullet()
    {

        weaponPrefab.GetComponent<SlugWeapon>().shot();

    }
}