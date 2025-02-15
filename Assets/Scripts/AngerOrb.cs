using UnityEngine;

public class AngerOrb : Orb
{
    public override void ActivateEffect()
    {
        Debug.Log("Anger Orb alındı! Oyuncu sinirlendi, hız arttı.");
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifySpeed(2f); // Hızı artır
        }
        base.ActivateEffect();
    }
}

