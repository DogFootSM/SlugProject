using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseManager : MonoBehaviour
{
    [SerializeField] List<Skill> ActiveSkill = new();

    [Header("Auto")]
    [SerializeField] bool AutoOnOff;
    [SerializeField] Button ActiveAuto;
    [SerializeField] TextMeshProUGUI AutoOnText;
    [SerializeField] TextMeshProUGUI AutoOffText;

    [Header("Toggle Move")]
    [SerializeField] Animator ani;
    private int curHash;
    private static int EnableHash = Animator.StringToHash("Enable");
    private static int DisableHash = Animator.StringToHash("Disable");

    // ========

    void AniPlay()
    {
        int checkAniHash;

        if (AutoOnOff == false) { checkAniHash = DisableHash; }
        else if (AutoOnOff) { checkAniHash = EnableHash; }
        else return;

        if (curHash != checkAniHash)
        {
            curHash = checkAniHash;
            ani.Play(curHash);
        }
    }

    // ========

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
        AniPlay();

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
            colorBlock.selectedColor = new Color32(185, 0, 25, 255);
            colorBlock.highlightedColor = colorBlock.selectedColor;
            colorBlock.normalColor = colorBlock.selectedColor;

            AutoOnText.gameObject.SetActive(false);
            AutoOffText.gameObject.SetActive(true);
        }

        else if (AutoOnOff)
        {
            colorBlock.selectedColor = new Color32(85, 210, 0, 255);
            colorBlock.highlightedColor = colorBlock.selectedColor;
            colorBlock.normalColor = colorBlock.selectedColor;

            AutoOnText.gameObject.SetActive(true);
            AutoOffText.gameObject.SetActive(false);
        }

        ActiveAuto.colors = colorBlock;
    }
}
