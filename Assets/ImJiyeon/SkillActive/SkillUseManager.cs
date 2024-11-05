using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseManager : MonoBehaviour
{
    [SerializeField] List<Skill> ActiveSkill = new();
    private string skillAutoCoroutineName = "SkillAuto";

    [Header("Auto")]
    [SerializeField] bool AutoOnOff;

    // ========

    private void Awake()
    {
        // ���� ��ų ��� UI�� �����ϸ� ��Ȱ��ȭ�� ���� ����
        for (int i = 0; i < ActiveSkill.Count; i++)
        {
            ActiveSkill[i].gameObject.GetComponent<Button>().interactable = true;
        }

        AutoOnOff = false;
    }


    // Ŭ������ �����ư Ȱ��ȭ - ��Ȱ��ȭ ���� ��ȯ ����
    public void AutoClick()
    {
        // ��Ȱ��ȭ - Ȱ��ȭ
        if (AutoOnOff == false) { AutoOnOff = true; }

        // Ȱ��ȭ - ��Ȱ��ȭ
        else if (AutoOnOff) { AutoOnOff = false; }

        StartCoroutine(SkillAuto());
    }


    IEnumerator SkillAuto()
    {
        while (AutoOnOff)
        {
            Debug.Log("�ڵ� ��Ƽ�� ��� Ȱ��ȭ");

            for (int i = 0; i < ActiveSkill.Count; i++)
            {
                if (ActiveSkill[i].isActived == true)
                {
                    Debug.Log($"{i}��° ��ų ������...");
                    ActiveSkill[i].Activate();
                }
            }
            // �ڵ� ��Ƽ�� ��� ��� : ������ �ϳ� �� �����Ͽ�, �� ��ų�� ��Ÿ���� ���� �� ���� �ٷ� ����� �� �ֵ��� ��
            yield return new WaitForFixedUpdate();
        }

        while (AutoOnOff == false)
        {
            Debug.Log("�ڵ� ��Ƽ�� ��� ��Ȱ��ȭ");
            yield break;
        }
    }
}
