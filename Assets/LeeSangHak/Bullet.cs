using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float speed;
    [SerializeField] GameObject target;
    [SerializeField] Transform trans;

    private void Update()
    {
        trans = target.transform;
        // Ÿ���� ����ְų� Ÿ���� ��Ȱ��ȭ�϶�
        if (target == null || target.activeSelf == false)
        {
            transform.Translate(trans.position * speed * Time.deltaTime);
            return;
        }

        
        // Ÿ���� �ְ� Ȱ��ȭ�϶�
        if (target != null && target.activeSelf == true)
        {
            transform.Translate(target.transform.position * speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                // ���Ϳ��� ������ ������
                //target.GetComponent<Monster>().TakeDamage(damage);

                // źȯ ����
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
