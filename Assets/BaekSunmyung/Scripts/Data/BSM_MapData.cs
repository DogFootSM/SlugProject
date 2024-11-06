using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "CreateMap/Map")]
public class BSM_MapData : ScriptableObject
{
    [Header("�� ������")]
    [Tooltip("�ߺз� �ʿ� ����� ��� �̹���")]
    [SerializeField] private List<Sprite> backGroundSprite = new List<Sprite>();
    public List<Sprite> BackGroundSprite { get { return backGroundSprite; } }

    [Tooltip("�ߺз� �ʿ� ����� �ϴ� �̹���")]
    [SerializeField] private List<Sprite> skySprite;
    public List<Sprite> SkySprite { get { return skySprite; } }
    


}