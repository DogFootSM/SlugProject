using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class StageDifficult : MonoBehaviour
{
    private MonsterModel monsterModel;
    private int curStageIndex = 0;

 
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
    public void GetStageIndex(int stageIndex)
    {
        curStageIndex = stageIndex;
    }

}
