using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSM_StageManager : MonoBehaviour
{
    public static BSM_StageManager Instance { get; private set; }

    private int stageIndex = 0;
    public int StageIndex { get { return stageIndex; } set { stageIndex = value; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        stageIndex = PlayerPrefs.GetInt("StageIndex");
    }


}
