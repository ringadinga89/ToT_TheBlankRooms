using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class View : MonoBehaviour
{
    [SerializeField] float m_angle = 0f;
    [SerializeField] float m_distance = 0f;
    [SerializeField] LayerMask m_layerMask = 0; // �ֺ��� �ִ� �÷��̾� �ݶ��̴��� ����

    public GameObject target;

    void Sight()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layerMask); // �ֺ��� �ִ� �÷��̾� �ݶ��̴��� ����

        if (t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform; // �÷��̾�� 1��, ����� �ݶ��̴��� �� ���̹Ƿ� �ε��� 0�� ������ �÷��̾�

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward); // AI�� ���� & �÷��̾� ������ �������̰� 
                                                                           // �þ߰����� ������ Ȯ��

            if(t_angle < m_angle * 0.5f) // �þ߰� * 0.5�� ���� = ���� ���� (���� �þ� + ������ �þ�) �� (��ü�þ�) �̱� ����
            {
                if(Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, m_distance)) // �þ߰� �ȿ� �ִٸ� Ray�� �÷��̾ ��
                {
                    if(t_hit.transform.name == "Player")
                    {
                        transform.position = Vector3.Lerp(transform.position, t_hit.transform.position, 0.02f); // Ray�� ���� ��ü�� Player���
                                                                                                                // �� ���̿� ��ֹ��� ���� �ɷ� ����

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
