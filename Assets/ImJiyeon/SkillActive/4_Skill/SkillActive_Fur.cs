using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillActive_Fur : Skill
{
    [SerializeField] Image LookCoolTime;
    [SerializeField] float CoolTime;

    public void SkillFur()
    {
        Activate();
    }

    public override void Activate()
    {
        isActived = false;

        // ��ų �ڵ� �ۼ� ����
        Debug.Log("�׹�° ��ų ����");

        CoroutineManager.Instance.ManagerCoroutineStart(SetCurrentCooltime(CoolTime, LookCoolTime, gameObject.GetComponent<Button>()), SetCoolTimeCoroutineName);
    }
}
