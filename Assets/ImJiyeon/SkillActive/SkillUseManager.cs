using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseManager : MonoBehaviour
{
    public static SkillUseManager SkillInstance;

    private CheckBox checkBox;
    private Button AutoButton;

    [SerializeField] List<Skill> ActiveSkill = new();
    [SerializeField] GameObject AutoSkillCheckButton;
    [SerializeField] public bool AutoOnOff;


    private void Awake()
    {
        checkBox = AutoSkillCheckButton.GetComponent<CheckBox>();

        // ���� ����Ʈ UI�� �����ϸ� ��Ȱ��ȭ�� ���� ����
        for (int i = 0; i < ActiveSkill.Count; i++)
        {
            ActiveSkill[i].gameObject.GetComponent<Button>().interactable = true;
        }

        AutoOnOff = false;
    }


    // Ŭ������ �����ư Ȱ��ȭ - ��Ȱ��ȭ ���� ��ȯ ����
    public void AutoClick()
    {
        if (AutoOnOff == false) { AutoOnOff = true; }
        else if (AutoOnOff) { AutoOnOff = false; }

        CoroutineManager.Instance.ManagerCoroutineStart(StartCoroutine(SkillAuto()), this);
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
