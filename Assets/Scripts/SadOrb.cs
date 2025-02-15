using UnityEngine;

public class SadOrb : Orb
{
    public override void ActivateEffect()
    {
        Debug.Log("Sad Orb alındı! Oyuncu depresif oldu, hız yavaşladı.");
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifySpeed(-1f); // Hızı yavaşlat
        }
        base.ActivateEffect();
    }
}
