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

        //for (int i = 0; i < gameManager.StageInstance.Monsters.Length; i++)
        //{
        //    gameManager.StageInstance.Monsters[i].MonsterHP -= SkillAttack;
        //}

        StartCoroutine(SetCurrentCooltime(CoolTime, LookCoolTime, gameObject.GetComponent<Button>()));
    }
}
