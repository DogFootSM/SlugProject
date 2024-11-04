using UnityEngine;
using UnityEngine.UI;

public class SkillActive_Two : Skill
{
    [SerializeField] Image LookCoolTime;
    [SerializeField] float CoolTime;

    public void SkillTwo()
    {
        Activate();
    }

    public override void Activate()
    {
        isActived = false;

        // ��ų �ڵ� �ۼ� ����
        Debug.Log("�ι�° ��ų ����");

        CoroutineManager.Instance.ManagerCoroutineStart(SetCurrentCooltime(CoolTime, LookCoolTime, gameObject.GetComponent<Button>()), SetCoolTimeCoroutineName);
    }
}
