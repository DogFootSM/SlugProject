using UnityEngine;
using UnityEngine.UI;

public class SkillActive_One : Skill
{
    [SerializeField] Image LookCoolTime;
    [SerializeField] float CoolTime;

    // 1. ��Ÿ��
    // 2. ���ݰ�


    public void SkillOne()
    {
        Activate();
    }

    public override void Activate()
    {
        isActived = false;

        // ��ų �ڵ� �ۼ� ����
        Debug.Log("ù��° ��ų ����");

        StartCoroutine(SetCurrentCooltime(CoolTime, LookCoolTime, gameObject.GetComponent<Button>()));
    }
}
