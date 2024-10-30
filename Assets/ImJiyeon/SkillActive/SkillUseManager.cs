using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseManager : MonoBehaviour
{
    [SerializeField] List<Skill> ActiveSkill = new();

    [Header("Auto")]
    [SerializeField] bool AutoOnOff;
    [SerializeField] Button ActiveAuto;


    private void Awake()
    {
        // ���� ��ų ��� UI�� �����ϸ� ��Ȱ��ȭ�� ���� ����
        for (int i = 0; i < ActiveSkill.Count; i++)
        {
            ActiveSkill[i].gameObject.GetComponent<Button>().interactable = true;
        }

        AutoOnOff = false;
        ColorChange();
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
        ColorChange();

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

    // ========

    // ��ư ���� �������� Ȱ��ȭ/��Ȱ��ȭ �������� Ȯ���� �� �ִ� �Լ�
    // Ȱ��ȭ = �ʷϻ� / ��Ȱ��ȭ = ������
    void ColorChange()
    {
        ColorBlock colorBlock = ActiveAuto.colors;

        if (AutoOnOff == false)
        {
            colorBlock.selectedColor = Color.red;
            colorBlock.highlightedColor = colorBlock.selectedColor;
            colorBlock.normalColor = colorBlock.selectedColor;
        }
        else if (AutoOnOff)
        {
            colorBlock.selectedColor = Color.green;
            colorBlock.highlightedColor = colorBlock.selectedColor;
            colorBlock.normalColor = colorBlock.selectedColor;
        }

        ActiveAuto.colors = colorBlock;
    }
}
