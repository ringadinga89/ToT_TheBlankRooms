using UnityEngine;
using System.Collections;

public class Telpo : MonoBehaviour
{

    public Transform Target;

    void OnTriggerEnter(Collider _col)  // 트리거에 충돌이 되었을 때는 이 함수를 도출한다.
    {
        if (_col.gameObject.name == "Player")
        {
            _col.transform.position = Target.position; // 부딪힌 객체를 타겟의 위치로 보낸다.

        }
    }
}
