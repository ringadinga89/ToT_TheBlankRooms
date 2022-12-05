using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject openText;

    public AudioSource doorSound;

    public bool inReach;

    private void Start()
    {
        inReach = false;
    }

    private void OnTriggerEnter(Collider other) // �浹 �� �� ��
    {
        if(other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other) // �浹 �� ������ ��
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    private void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            DoorOpens();
        }

        else
        {
            DoorCloses();
        }
    }

   void DoorOpens()
    {
        Debug.Log("�� ����");
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        doorSound.Play();
    }

    void DoorCloses()
    {
        Debug.Log("�� ����");
        door.SetBool("Open", false);
        door.SetBool("Closed", true);

    }
}
