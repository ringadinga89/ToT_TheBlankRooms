using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonCam : MonoBehaviour
{
    float speed; 
    public float turnSpeed = 4.0f; // ���콺 ȸ�� �ӵ�    
    private float xRotate = 0.0f; // ���� ����� X�� ȸ������ ���� ���� ( ī�޶� �� �Ʒ� ���� )
    public float normalSpeed = 4.0f; // �̵� �ӵ�
    public float runSpeed = 8.0f; // �޸��� �ӵ�

    [SerializeField] private float maxStamina, runCost; // ���¹̳� �ѷ�, �޸� ������ �Һ�Ǵ� ���¹̳���
    private float stamina;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina; // ������ ���¹̳� full ���·�
    }

    // Update is called once per frame
    void Update()
    {
        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        // ���Ʒ� ȸ������ ���������� -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
        // Clamp �� ���� ������ �����ϴ� �Լ�
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        // ī�޶� ȸ������ ī�޶� �ݿ�(X, Y�ุ ȸ��)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        //  Ű���忡 ���� �̵��� ����
        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        // �̵����� ��ǥ�� �ݿ�
        transform.position += move * speed * Time.deltaTime;

        bool running = Input.GetButton("Fire3") && stamina > 0;
        speed = running ? runSpeed : normalSpeed;

        if (Input.GetButton("Fire3") && stamina > 0)  // ����Ʈ �Է� && ���¹̳� 0 �̻�
        {
            speed = runSpeed;
            stamina -= runCost * Time.deltaTime;
            running = true;
        }
        else
        {
            speed = normalSpeed;
            stamina += Time.deltaTime;
            running = false;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);


        if (stamina <= 0)
        {
            running = false;
            speed = normalSpeed; // ���¹̳��� ������ normarSpeed�� ��ȯ
            
        }
    }

    public float GetStamina() => stamina;
    public float GetMaxStamina() => maxStamina;
}