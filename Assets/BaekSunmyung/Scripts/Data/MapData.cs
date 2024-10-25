using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "CreateMap/Map")]
public class MapData : ScriptableObject
{
    [Header("�� ������")]
    [Tooltip("���� �ߺз� ��")]
    [SerializeField] private MiddleMap middleStage;
    public MiddleMap MiddleStage { get { return middleStage; } }

    [Tooltip("�Һз� �ִ� �ܰ�")]
    [SerializeField] private int maxSmallStage;
    public int MaxSmallStage { get { return maxSmallStage; } }

    [Tooltip("�ߺз� �ʿ� ����� ��� �̹���")]
    [SerializeField] private Sprite backGroundSprite;
    public Sprite BackGroundSprite { get { return backGroundSprite; } }


}