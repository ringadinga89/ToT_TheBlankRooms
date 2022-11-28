using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.AI;

public class SlenderManAI : MonoBehaviour
{
    Rigidbody rigid;
    NavMeshAgent agent;
    Animator anim;

    public Transform[] arrWaypoint; // 목적지를 배열에 넣었음

    private Vector3 destination; // 트렌스폼으로 바꿈***
    private Coroutine moveStop;

    // 시야각 및 추적 반경을 제어하는 EnemyFOV 클래스를 저장할 변수
    private SlenderFOV slenderFOV;

    private void Awake()
    {
        // 시야각 및 처적 반경을 제어하는 SlenderFOV 클래스를 추출
        slenderFOV = GetComponent<SlenderFOV>(); 
    }
    // Start is called before the first frame update
    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody>();
        this.anim = this.GetComponent <Animator>();

        this.agent = this.GetComponent<NavMeshAgent>(); //agent가 AI를 조종

        Invoke("AiMove", 2);
    }

    /*private void Update()
    {
        if (slenderFOV.isTracePlayer())
        {
            Run();
        }
        else if (first)
        {
            invoke AiMove();
        }
        else
        {
            First = false;
        }
    }*/

    // ***시야각에 들어오면 공격하게끔 만들어야 됨*** 
    private void Run()
    {

    }

    private void AiMove()
    {
        int random = Random.Range(0, arrWaypoint.Length);
        Debug.LogFormat("random : {0}", random); // 랜덤으로 목적지 선정

        for (int i = 0; i < arrWaypoint.Length; i++)
        {
            if(i== random)
            {
                this.destination = this.arrWaypoint[i].position; // 선정한 목적지를 기억 작동 안 하고 있음 ***ㅏ 좌표가 0 0 0 으로 가는 거

                if(this.moveStop == null)
                {
                    Debug.Log("코루틴 시작");
                    this.moveStop = this.StartCoroutine(this.crAiMove()); // 목적지와 AI의 거리를 계산하는 메서드
                                                                          // -> 애니메이션 변경을 위해 사용
                }

                this.agent.SetDestination(this.destination); // AI에게 목적지로 이동 명령을 내림
                this.anim.Play("Walk");
                break; 
            }
        }
    }

    IEnumerator crAiMove()
    {
        while (true)
        {
            var dis = Vector3.Distance(this.transform.position, this.destination); // 목적지와 AI 사이의 거리 계산

            if (dis <= 0.5f)
            {
                Debug.Log("목적지 도착");
                // this.anim.Play("Idle"); // 도착하면 애니메이션 바꿈
                if(this.moveStop != null)
                {
                    this.StopCoroutine(this.moveStop);
                    this.moveStop = null;
                    Invoke("AiMove", 1.0f); // 1.5초 뒤 다른 곳으로 이동시킴
                    break;
                }
            }

            yield return null;  
        }
    }
}
