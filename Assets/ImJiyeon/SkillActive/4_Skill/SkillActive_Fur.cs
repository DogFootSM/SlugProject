using System.Collections;
using UnityEngine;

public class SkillActive_Fur : MonoBehaviour
{
    [SerializeField] bool isActived;
    [SerializeField] int CoolTime;


    public void SkillOne()
    {
        StartCoroutine(SkillFurCoolTime());
    }

    IEnumerator SkillFurCoolTime()
    {
        while (isActived)
        {
            Debug.Log("�ι�° ��ų ����");
            yield return new WaitForSeconds(CoolTime);
        }
    }
}
