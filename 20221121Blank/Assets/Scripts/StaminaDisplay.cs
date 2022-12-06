using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplay : MonoBehaviour
{
    private firstPersonCam player;
    private Image fillImage;

    private void Start()
    {
        player = FindObjectOfType<firstPersonCam>();
        fillImage = GetComponent<Image>();
 
    }

    private void Update()
    {
       fillImage.fillAmount = player.GetStamina() / player.GetMaxStamina();
    }
}
