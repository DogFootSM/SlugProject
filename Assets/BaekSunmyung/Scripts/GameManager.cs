using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private bool isOpenInventory;
    public bool IsOpenInventory
    {
        get { return isOpenInventory; }
        set { isOpenInventory = value; }
    }

    public BSM_Stage StageInstance;

    [SerializeField] DateTime exitTime;
    [SerializeField] DateTime startTime;
    [SerializeField] private double totalTime;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        //LoadData();
    }

    private void Update()
    {
        if (isOpenInventory)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }



        /* ��ġ ��ȭ �ý��� Holding
        if (Input.GetMouseButtonDown(0))
        {
#if UNITY_EDITOR
            SaveData();
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }


    public void SaveData()
    {
        exitTime = DateTime.Now;
        PlayerPrefs.SetString("ExitTime", exitTime.ToString());
    }

    public void LoadData()
    {
        startTime = DateTime.Now;
        string saveStr = PlayerPrefs.GetString("ExitTime");
        DateTime saveData;

        if (DateTime.TryParse(saveStr, out saveData))
        {
            TimeSpan diffTime = (startTime - saveData);
            totalTime = diffTime.TotalSeconds;
        }
    }
        */
    }
}
