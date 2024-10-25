using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    [SerializeField] float speed;
    [SerializeField] GameObject target;

    private void Update()
    {
        if (target == null || target.activeSelf == false)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 distance = (target.transform.position - transform.position).normalized;
        transform.position += (Vector3)distance * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            // ���Ϳ��� ������ ������
            //target.GetComponent<Monster>().TakeDamage(damage);

            // źȯ ����
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }


}
