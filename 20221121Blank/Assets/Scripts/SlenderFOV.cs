using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderFOV : MonoBehaviour
{
    // �� ĳ������ ���� ���� �Ÿ��� ����
    public float viewRange = 15.0f;
    // �� ĳ������ �þ� ��
    public float viewAngle = 120.0f;

    private Transform SlenderTr;
    private Transform playerTr;
    private int playerLayer;
    private int obstacleLayer;
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ʈ ����
        SlenderTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        // ���̾� ����ũ �� ���
        playerLayer = LayerMask.NameToLayer("Player");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }

   // �־��� ������ ���� ���� ���� ���� ��ǩ���� ����ϴ� �Լ�
    public Vector3 CirclePoint(float angle)
    {
        // ���� ��ǥ�� �������� �����ϱ� ���� �� ĳ������ Y ȸ�� ���� ����
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),
                                     0,
                                     Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool isTracePlayer()
    {
        bool isTrace = false;

        // ���� �ݰ� ���� �ȿ��� ���ΰ� ĳ���͸� ����
        Collider[] colls = Physics.OverlapSphere(SlenderTr.position
                                                 , viewRange
                                                 , 1 << playerLayer);

        // �迭�� ������ 1�� �� ���ΰ��� ���� �ȿ� �ִٰ� �Ǵ�
        if(colls.Length == 1)
        {
            // �� ĳ���Ϳ� ���ΰ� ������ ���� ���͸� ���
            Vector3 dir = (playerTr.position - SlenderTr.position).normalized;

            // �� ĳ������ �þ߰��� ���Դ����� �Ǵ�
            if(Vector3.Angle(SlenderTr.forward, dir) < viewAngle * 0.5f)
            {
                isTrace = true;
            }
        }

        return isTrace;
    }

    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit;

        // �� ĳ���Ϳ� ���ΰ� ������ ���� ���͸� ���
        Vector3 dir = (playerTr.position - SlenderTr.position).normalized;

        // ����ĳ��Ʈ�� �����ؼ� ��ֹ��� �ִ��� ���θ� �Ǵ�
        if(Physics.Raycast(SlenderTr.position, dir, out hit, viewRange, layerMask))
        {
            isView = (hit.collider.CompareTag("Player"));
        }
        return isView;
    }
}
