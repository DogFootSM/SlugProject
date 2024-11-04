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
    /// (Stage_statusIncNum * ( 10 ^ Stage_statusIncUnit)) * (1 - Stage_statusIncdistributionPer) * Enemy_atk
    /// ü�� ����
    /// (Stage_statusIncNum * ( 10 ^ Stage_statusIncUnit)) * Stage_statusIncDistributionPer * Enemy_hp
    /// </summary>
    public void MonsterIncreaseAbility()
    { 
        float monsterHP = 0f;   
        int monsterAtk = 0; 
        float statusIncNum = stageCSV.State[curStageIndex].Stage_statusIncNum;
        float statusIncUnit = Mathf.Pow(10, stageCSV.State[curStageIndex].Stage_statusIncUnit);
        float statusIncdistributionPer = 1f - stageCSV.State[curStageIndex].Stage_statusIncdistributionPer;

        monsterAtk = (int)((statusIncNum * statusIncUnit) * statusIncdistributionPer * monsterModel.MonsterAttack);
        monsterModel.MonsterAttack = monsterAtk;
        
        if (curStageIndex % waveCount <= 3)
        { 
            monsterHP = (statusIncNum * statusIncUnit) * statusIncdistributionPer * monsterModel.MonsterHP;
            monsterModel.MonsterHP = monsterHP;
        }
        else
        {
            monsterHP = (statusIncNum * statusIncUnit) * statusIncdistributionPer * monsterModel.MonsterHP * bossValue;
            monsterModel.MonsterHP = monsterHP;
        }  
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
