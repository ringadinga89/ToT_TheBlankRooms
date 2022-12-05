using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplay : MonoBehaviour
{
    private player_move player;
    private Image fillImage;

    private void Start()
    {
        player = FindObjectOfType<player_move>();
        fillImage = GetComponent<Image>();
    }

    private void Update()
    {
       // fillImage.fillAmount = player.GetStamina() / player.GetMaxStamina();
    }
}
