using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillActive_Thr : Skill
{
    [SerializeField] Image LookCoolTime;
    [SerializeField] float CoolTime;

    public void SkillThr()
    {
        Activate();
    }

    public override void Activate()
    {
        isActived = false;

        // ��ų �ڵ� �ۼ� ����
        Debug.Log("����° ��ų ����");

        StartCoroutine(SetCurrentCooltime(CoolTime, LookCoolTime, gameObject.GetComponent<Button>()));
    }
}
