using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonCam : MonoBehaviour
{
    float speed; 
    public float turnSpeed = 4.0f; // 마우스 회전 속도    
    private float xRotate = 0.0f; // 내부 사용할 X축 회전량은 별도 정의 ( 카메라 위 아래 방향 )
    public float normalSpeed = 4.0f; // 이동 속도
    public float runSpeed = 8.0f; // 달리는 속도

    [SerializeField] private float maxStamina, runCost; // 스태미나 총량, 달릴 때마다 소비되는 스태미나량
    private float stamina;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina; // 시작은 스태미나 full 상태로
    }

    // Update is called once per frame
    void Update()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        // 현재 y축 회전값에 더한 새로운 회전각도 계산
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라가 회전할 양 계산(하늘, 바닥을 바라보는 동작)
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        // 위아래 회전량을 더해주지만 -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
        // Clamp 는 값의 범위를 제한하는 함수
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        // 카메라 회전량을 카메라에 반영(X, Y축만 회전)
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        //  키보드에 따른 이동량 측정
        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        // 이동량을 좌표에 반영
        transform.position += move * speed * Time.deltaTime;

        bool running = Input.GetButton("Fire3") && stamina > 0;
        speed = running ? runSpeed : normalSpeed;

        if (Input.GetButton("Fire3") && stamina > 0)  // 쉬프트 입력 && 스태미나 0 이상
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
            speed = normalSpeed; // 스태미나가 없으면 normarSpeed로 전환
            
        }
    }

    public float GetStamina() => stamina;
    public float GetMaxStamina() => maxStamina;
}