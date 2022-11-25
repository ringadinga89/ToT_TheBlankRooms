using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlenderManAI : MonoBehaviour
{
    Rigidbody rigid;
    NavMeshAgent agent;
    Animator anim;

    public Transform[] arrWaypoint; // �������� �迭�� �־���

    private Vector3 destination;
    private Coroutine moveStop;

    // �þ߰� �� ���� �ݰ��� �����ϴ� EnemyFOV Ŭ������ ������ ����
    private SlenderFOV slenderFOV;

    private void Awake()
    {
        // �þ߰� �� ó�� �ݰ��� �����ϴ� SlenderFOV Ŭ������ ����
        slenderFOV = GetComponent<SlenderFOV>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody>();
        this.anim = this.GetComponent <Animator>();

        this.agent = this.GetComponent<NavMeshAgent>(); //agent�� AI�� ����

        Invoke("AiMove", 2);
    }

    // ***�þ߰��� ������ �����ϰԲ� ������ ��*** 

    private void AiMove()
    {
        int random = Random.Range(0, arrWaypoint.Length);
        Debug.LogFormat("random : {0}", random); // �������� ������ ����

        for (int i = 0; i < arrWaypoint.Length; i++)
        {
            if(i== random)
            {
                this.destination = this.arrWaypoint[i].position; // ������ �������� ���

                if(this.moveStop == null)
                {
                    Debug.Log("�ڷ�ƾ ����");
                    this.moveStop = this.StartCoroutine(this.crAiMove()); // �������� AI�� �Ÿ��� ����ϴ� �޼���
                                                                          // -> �ִϸ��̼� ������ ���� ���
                }

                this.agent.SetDestination(this.destination); // AI���� �������� �̵� ����� ����
                this.anim.Play("Walk");
                break; 
            }
        }
    }

    IEnumerator crAiMove()
    {
        while (true)
        {
            var dis = Vector3.Distance(this.transform.position, this.destination); // �������� AI ������ �Ÿ� ���

            if (dis <= 0.2f)
            {
                Debug.Log("������ ����");
                this.anim.Play("Idle"); // �����ϸ� �ִϸ��̼� �ٲ�
                if(this.moveStop != null)
                {
                    this.StopCoroutine(this.moveStop);
                    this.moveStop = null;
                    Invoke("AiMove", 1.5f); // 1.5�� �� �ٸ� ������ �̵���Ŵ
                    break;
                }
            }

            yield return null;  
        }
    }
}
