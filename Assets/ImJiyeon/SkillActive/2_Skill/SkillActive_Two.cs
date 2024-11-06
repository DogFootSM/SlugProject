using UnityEngine;
using UnityEngine.UI;

public class SkillActive_Two : Skill
{
    public void SkillTwo()
    {
        Activate();
    }

    public override void Activate()
    {
        isActived = false;

        Debug.Log("�ι�° ��ų ����");

        for (int i = 0; i < gameManager.StageInstance.monsters.Length; i++)
        {
            if (gameManager.StageInstance.monsters[i] != null)
            {
                gameManager.StageInstance.monsters[i].MonsterHP -= SkillAttack;
            }
        }

        CoroutineManager.Instance.ManagerCoroutineStart(StartCoroutine(SetCurrentCooltime(CoolTime, LookCoolTime, gameObject.GetComponent<Button>())), this);
    }
}
