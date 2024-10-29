using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUseManager : MonoBehaviour
{
    [SerializeField] Button ActiveOne;
    [SerializeField] Button ActiveTwo;
    [SerializeField] Button ActiveThr;
    [SerializeField] Button ActiveFur;

    private void Awake()
    {
        // ���� ��ų ��� UI�� �����ϸ� ��Ȱ��ȭ�� ���� ����
        ActiveOne.interactable = true;
        ActiveTwo.interactable = false;
        ActiveThr.interactable = false;
        ActiveFur.interactable = false;
    }
}
