using System.Collections;
using UnityEngine;

public class SkillActive_Thr : MonoBehaviour
{
    [SerializeField] bool isActived;
    [SerializeField] int CoolTime;


    public void SkillOne()
    {
        StartCoroutine(SkillThrCoolTime());
    }

    IEnumerator SkillThrCoolTime()
    {
        while (isActived)
        {
            Debug.Log("����° ��ų ����");
            yield return new WaitForSeconds(CoolTime);
        }
    }
}
