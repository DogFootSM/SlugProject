using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIContorller : MonoBehaviour
{
    private void Awake()
    {
        // UI�� ���� ��ȯ�ǵ� ������� �ʵ��� ��ġ
        DontDestroyOnLoad(gameObject);
    }
}
