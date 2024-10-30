using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public GameObject targetMonster = null;
    public float attackCooldown = 0f;
    [SerializeField] GameObject bulletPrefab;
    public int ammo, attackRange;
    [SerializeField] float times;
    [SerializeField] GameObject[] monsters;
    [SerializeField] GameObject muzzlePoint;
    [SerializeField] Animator upperAnim;
    [SerializeField] Animator lowerAnim;
    [SerializeField] GameObject[] weapons;
    [SerializeField] List<GameObject> bullets;

    private void Start()
    {
        // �� ������ : ���� �ӵ�
        attackRange = 10;
        Debug.Log(PlayerDataModel.Instance.AttackSpeed);
    }

    private void Update()
    {
        /*if (targetMonster == null || targetMonster.activeSelf != true)
        {
            targetMonster = null;
        }*/

        times = Time.time;

        // Ÿ������ ������ ���Ͱ� ����ְ� ���Ͱ� Ȱ��ȭ�� �ƴ� ��
        if (targetMonster == null || targetMonster.activeSelf != true)
        {
            FindTarget();
        }

        if (targetMonster != null && Time.time >= attackCooldown)
        {
            Attack();
        }

        if (PlayerDataModel.Instance.Health <= 0)
        {
            Death();
            upperAnim.SetBool("Death", true);
            lowerAnim.gameObject.SetActive(false);
        }
    }

    private void FindTarget()
    {
        upperAnim.SetBool("Atk", false);
        lowerAnim.SetBool("Walk", true);
        

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


    public void TakeHit(int damage)
    {
        PlayerDataModel.Instance.Health -= damage;
        if (PlayerDataModel.Instance.Health <= 0)
        {
            // Destroy(gameObject); < �׾��� �� 
            Debug.Log("�ǰݵ�");
        }
    }

    public void Attack()
    {
        Debug.Log("����");
        if (targetMonster != null && Vector2.Distance(transform.position, targetMonster.transform.position) < attackRange)
        {
            lowerAnim.SetBool("Walk", false);
            upperAnim.SetBool("Atk", true);
            upperAnim.SetFloat("speed", 5);


            Debug.Log("�߻�");
            //apon.shot();
            GameObject bulletGameObj = Instantiate(bulletPrefab, muzzlePoint.transform.position, transform.rotation);
            Bullet bullet = bulletGameObj.GetComponent<Bullet>();
            bullets.Add(bulletGameObj);
            bullet.SetTarget(targetMonster);
            attackCooldown = Time.time + PlayerDataModel.Instance.AttackSpeed;
        }
    }

    public void RemoveBullets(GameObject bullet)
    {
            bullets.Remove(bullet);
    }


    public void Death()
    {
        // �÷��̾ �׾��� �� ��� źȯ ����
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        bullets.Clear(); // ����Ʈ �ʱ�ȭ
    }
}