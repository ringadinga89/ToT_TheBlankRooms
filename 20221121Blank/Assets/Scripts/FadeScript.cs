using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Image Panel;
    float time = 0f; // ���ӽð�
    float F_time = 20f; // ���̵尡 ���ӵǴ� �ð�
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true); //��ƾ�� ���۵� �� �̹����� Ȱ��ȭ
        time = 0f; // �ʱ�ȭ
        Color alpha = Panel.color; // Color ���� ������ Panel.color�� �ʱ�ȭ

        while (alpha.a < 1f) // �ݺ��� : ���İ��� 1���� ������ ��� �ݺ�
        {
            time += Time.deltaTime / F_time; // �� ������ deltatime�� F_time���� ���� ���� time�� ������
            alpha.a = Mathf.Lerp(0, 1, time); // 0���� 1���� �ε巴�� ���ϰ� �������
            Panel.color = alpha; // alpha ���� �� ������ �̹����� �÷����� ����
            yield return null;
        }
        time = 0f; //�ʱ�ȭ

        yield return new WaitForSeconds(1f); //1���� ���ð�
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha; 
            yield return null;
        }

        Panel.gameObject.SetActive(false); //�̹����� �ٽ� ��Ȱ��ȭ
        yield return null;
    }
}
