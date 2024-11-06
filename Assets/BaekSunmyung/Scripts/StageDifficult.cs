using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class StageDifficult : MonoBehaviour
{
    [SerializeField] int bossValue;

    private StageCSV stageCSV;
     
    private MonsterModel monsterModel;
    private int curStageIndex = 0;
    private int waveCount = 0;

    private void Start()
    {
        stageCSV = StageCSV.Instance;
    }

    /// <summary>
    /// ���� �� �ν��Ͻ��� �޾ƿ�
    /// </summary>
    /// <param name="monsterModel">������ ���� �ν��Ͻ�</param>
    public void GetMonsterInstance(MonsterModel monsterModel)
    {
        this.monsterModel = monsterModel;  
    }
     
    /// <summary>
    /// ���� �������� ���������� Table Index ���� �޾ƿ�
    /// </summary>
    /// <param name="stageIndex">Data Table Index</param>
    public void GetStageIndex(int stageIndex, int waveCount)
    {
        curStageIndex = stageIndex;
        this.waveCount = waveCount;
    }

    /// <summary>
    /// �� �������� �� ���� �߰� �ɷ�ġ ����
    /// ���ݷ� ����
    /// (Stage_AttackIncNum * ( 10 ^ Stage_AttackIncUnit)) * Enemy_atk
    /// ü�� ����
    /// (Stage_HpIncNum * ( 10 ^ Stage_HpIncUnit)) * Enemy_hp
    /// </summary>
    public void MonsterIncreaseAbility()
    { 
        float monsterHP = 0f;   
        int monsterAtk = 0; 

        //���ݷ� ���� ��ġ
        float attackNum = stageCSV.State[curStageIndex].Stage_AttackNum;
        float attackUnit = stageCSV.State[curStageIndex].Stage_attackUnit;

        //ü�� ���� ��ġ
        float hpNum = stageCSV.State[curStageIndex].Stage_hpNum;
        float hpUnit = stageCSV.State[curStageIndex].Stage_hpUnit;
        // ���̺��� ���� �������Ƿ� ���� ������ ��ġ�� ���Ŀ��� ����
        monsterAtk = (int)(attackNum * Mathf.Pow(10, attackUnit));
        monsterModel.MonsterAttack = monsterAtk;
        monsterHP = (hpNum * Mathf.Pow(10, hpUnit));
        monsterModel.MonsterHP = monsterHP; 
    }

    public float CurStageMonsterHP()
    {
        return monsterModel.MonsterHP;
    }

    public int CurStageMonsterAtk()
    {
        return monsterModel.MonsterAttack;
    }

}
