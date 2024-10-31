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
    [SerializeField] GameObject weaponPrefab;
    public int ammo, attackRange;
    [SerializeField] float times;
    [SerializeField] GameObject[] monsters;
    public GameObject muzzlePoint;
    [SerializeField] Animator upperAnim;
    [SerializeField] Animator lowerAnim;
    [SerializeField] GameObject[] weapons;
    public List<GameObject> bullets;
    private float attackSpeed;
    private bool respawn;

    IEnumerator Respawn()
    {
        Debug.Log("�ڷ�ƾ ����");
        upperAnim.SetBool("Death", true);
        lowerAnim.gameObject.SetActive(false);

        // �÷��̾ �׾��� �� ��� źȯ ����
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        bullets.Clear(); // ����Ʈ �ʱ�ȭ

        PlayerDataModel.Instance.Health = 500;
        yield return new WaitForSeconds(1f);
        upperAnim.SetBool("Death", false);
        lowerAnim.gameObject.SetActive(true);

        respawn = true;


    }


    private void Start()
    {
        // �� ������ : ���� �ӵ�
        attackSpeed = 1f;
        attackRange = 20;
    }



    private void Update()
    {
        /*if (targetMonster == null || targetMonster.activeSelf != true)
        {
            targetMonster = null;
        }*/

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

        if (PlayerDataModel.Instance.Health <= 0)
        {
            Debug.Log("���");
            Death();            
        }

        if (weaponPrefab != null)
        {
            upperAnim.SetBool("isWeapon", true);
        }

        if (respawn == true)
        {
            StopCoroutine(Respawn());
            respawn = false;
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
        }
    }

    public void Attack()
    {
        if (targetMonster != null && Vector2.Distance(transform.position, targetMonster.transform.position) < attackRange)
        {
            lowerAnim.SetBool("Walk", false);
            upperAnim.SetBool("Atk", true);
            upperAnim.SetFloat("speed", attackSpeed + (PlayerDataModel.Instance.AttackSpeedLevel * 0.01f));

            ShootBullet();

            attackCooldown = Time.time + PlayerDataModel.Instance.AttackSpeed;            
        }
    }

    public void RemoveBullets(GameObject bullet)
    {
            bullets.Remove(bullet);
    }


    public void Death()
    {
        StartCoroutine(Respawn());
    }

    public void SwapWeapon(GameObject weapon)
    {
        if (weaponPrefab != null)
        {
            weaponPrefab.gameObject.SetActive(false);
            weaponPrefab = weapon;
            weaponPrefab.gameObject.SetActive(true);
        }
        else if(weaponPrefab == null)
        {
            weaponPrefab = weapon;
            weaponPrefab.gameObject.SetActive(true);
        }
    }

    private void ShootBullet()
    {
        if (weaponPrefab == null)
        {
            GameObject bulletGameObj = Instantiate(bulletPrefab, muzzlePoint.transform.position, transform.rotation);
            Bullet bullet = bulletGameObj.GetComponent<Bullet>();
            bullets.Add(bulletGameObj);
            bullet.SetTarget(targetMonster);
        }
        else
        {
            weaponPrefab.GetComponent<Weapon>().shot();
        }

        
    }
}