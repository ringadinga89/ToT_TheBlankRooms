using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f; // 지속시간
    float F_time = 20f; // 페이드가 지속되는 시간
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true); //루틴이 시작될 때 이미지를 활성화
        time = 0f; // 초기화
        Color alpha = Panel.color; // Color 변수 선언후 Panel.color로 초기화

        while (alpha.a < 1f) // 반복문 : 알파값이 1보다 작으면 계속 반복
        {
            time += Time.deltaTime / F_time; // 매 프레임 deltatime을 F_time으로 나눈 값을 time에 더해줌
            alpha.a = Mathf.Lerp(0, 1, time); // 0부터 1까지 부드럽게 변하게 만들어줌
            Panel.color = alpha; // alpha 값을 매 프레임 이미지의 컬러값에 대입
            yield return null;
        }
        time = 0f; //초기화

        yield return new WaitForSeconds(1f); //1초의 대기시간
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha; 
            yield return null;
        }

        Panel.gameObject.SetActive(false); //이미지를 다시 비활성화
        yield return null;
    }
}
