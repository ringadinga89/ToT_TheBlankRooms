using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SlenderFOV))]
public class FOVEditior : Editor
{
    private void OnSceneGUI()
    {
        // EnemyFOV Ŭ������ ����
        SlenderFOV fov = (SlenderFOV)target;

        // ���� ���� �������� ��ǥ�� ���(�־��� ������ 1/2)
        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);

        // ���� ������ ������� ����
        //Handles.color = Color.white;

        Handles.color = new Color(1, 1, 1, 0.2f);

        // �ܰ����� ǥ���ϴ� ������ �׸�
        Handles.DrawWireDisc(fov.transform.position // ���� ��ǥ
                             , Vector3.up // ��� ����
                             , fov.viewRange); // ���� ������

        // ��ä���� ������ ����
        Handles.DrawSolidArc(fov.transform.position // ���� ��ǥ
                             , Vector3.up // ��� ����
                             , fromAnglePos // ��ä���� ���� ��ǥ
                             , fov.viewAngle // ��ä���� ���� 
                             , fov.viewRange); // ��ä���� ������

        // �þ߰��� �ؽ�Ʈ�� ǥ��
        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f)
                      , fov.viewAngle.ToString());
    }
}


