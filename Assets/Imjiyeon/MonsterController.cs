using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    /*
        ����� ����
        1. ���� ���� (�ش� ����� �ٸ� ��ũ��Ʈ���� ���� ����)
        2. ������ ���Ͱ� ������ ������ (�ɾ�ٴϴ� ���Ͱ� �� �����Ƿ� �ɾ�ٴϴ� ���͸� ����Ʈ�� ����)
        3. ������ ������ ��, ���� ���� ���� �÷��̾��� �ݶ��̴��� ���� �� ������ ����
        4. ü���� 0�� �� ���, ��ü�� �����Ǹ� ���ÿ� ��ȭ�� ���. (���� �÷��̾� ��ȭ�� �����Ͽ� ��ϵ� ���� ++ �ϴ� ������ ������ �����ϰ� ����)

        ȭ�� �ۿ� �ִ� ���ʹ� Ʈ���Ű� on�� �Ǿ� ���� �ʵ��� ��
     */

    public enum MonsterState { Move, Attack, Dead, Size }
}
