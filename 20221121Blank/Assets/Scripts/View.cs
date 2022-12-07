using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class View : MonoBehaviour
{
    [SerializeField] float m_angle = 0f;
    [SerializeField] float m_distance = 0f;
    [SerializeField] LayerMask m_layerMask = 0; // 주변에 있는 플레이어 콜라이더를 검출

    public GameObject target;

    void Sight()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layerMask); // 주변에 있는 플레이어 콜라이더를 검출

        if (t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform; // 플레이어는 1명, 검출될 콜라이더도 한 개이므로 인덱스 0은 무조건 플레이어

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward); // AI의 방향 & 플레이어 방향의 각도차이가 
                                                                           // 시야각보다 작은지 확인

            if(t_angle < m_angle * 0.5f) // 시야각 * 0.5인 이유 = 정면 기준 (왼쪽 시야 + 오른쪽 시야) 가 (전체시야) 이기 때문
            {
                if(Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, m_distance)) // 시야각 안에 있다면 Ray를 플레이어에 쏨
                {
                    if(t_hit.transform.name == "Player")
                    {
                        transform.position = Vector3.Lerp(transform.position, t_hit.transform.position, 0.02f); // Ray에 닿은 객체가 Player라면
                                                                                                                // 둘 사이에 장애물이 없는 걸로 간주

                        Vector3 l_vector = target.transform.position - transform.position;
                        transform.rotation = Quaternion.LookRotation(l_vector).normalized;
                    }
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sight();
    }
}
