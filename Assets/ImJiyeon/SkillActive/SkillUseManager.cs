using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseManager : MonoBehaviour
{
    [Header("Active Skills")]
    [SerializeField] Button[] ActiveSkill;

    [Header("Aoto")]
    [SerializeField] bool AutoOnOff;
    [SerializeField] Button ActiveAoto;


    private void Awake()
    {
        // ���� ��ų ��� UI�� �����ϸ� ��Ȱ��ȭ�� ���� ����
        for (int i = 0; i < ActiveSkill.Length; i++)
        {
            ActiveSkill[i].interactable = true;
        }

        AutoOnOff = false;
        ColorChange();
    }


    // Ŭ������ �����ư Ȱ��ȭ - ��Ȱ��ȭ ���� ��ȯ ����
    public void AutoClick()
    {
        // Ȱ��ȭ - ��Ȱ��ȭ
        if (AutoOnOff)
        {
            AutoOnOff = false;
        }
        // ��Ȱ��ȭ - Ȱ��ȭ
        else if (AutoOnOff == false)
        {
            AutoOnOff = true;
        }

        StartCoroutine(SkillAuto());
    }

    IEnumerator SkillAuto()
    {
        ColorChange();

        while (AutoOnOff)
        {
            Debug.Log("�����ư Ȱ��ȭ��...");
            yield return new FixedUpdate();
        }

        while (AutoOnOff == false)
        {
            Debug.Log("�����ư ��Ȱ��ȭ");
            yield break;
        }
    }

    // ========

    // ��ư ���� �������� Ȱ��ȭ/��Ȱ��ȭ �������� Ȯ���� �� �ִ� �Լ�
    // Ȱ��ȭ = �ʷϻ� / ��Ȱ��ȭ = ������
    void ColorChange()
    {
        ColorBlock colorBlock = ActiveAoto.colors;

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

        ActiveAoto.colors = colorBlock;
    }
}
