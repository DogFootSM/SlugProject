using System.Collections;
using UnityEngine;

public class SkillActive_Two : MonoBehaviour
{
    [SerializeField] bool isActived;
    [SerializeField] int CoolTime;


    public void SkillOne()
    {
        StartCoroutine(SkillTwoCoolTime());
    }

    IEnumerator SkillTwoCoolTime()
    {
        while (isActived)
        {
            Debug.Log("�ι�° ��ų ����");
            yield return new WaitForSeconds(CoolTime);
        }
    }
}
