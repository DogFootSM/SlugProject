using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillActive_One : MonoBehaviour
{
    [SerializeField] bool isActived;
    [SerializeField] int CoolTime;


    public void SkillOne()
    {
        if (isActived == true)
        {
            Debug.Log("ù��° ��ų ����");
            isActived = false;
        }

        StartCoroutine(SkillOneActive());
    }

    IEnumerator SkillOneActive()
    {
        gameObject.GetComponent<Button>().interactable = false;

        while (isActived == false)
        {
            for (int i = 0; CoolTime < i; CoolTime--)
            {
                Debug.Log($"��Ÿ�� ������ : {i}");
            }

            yield return new WaitForSeconds(CoolTime);
        }

        isActived = true;
        gameObject.GetComponent<Button>().interactable = true;
    }
}
