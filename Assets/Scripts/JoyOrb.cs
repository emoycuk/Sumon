using UnityEngine;

public class JoyOrb : Orb
{
    public override void ActivateEffect()
    {
        Debug.Log("Joy Orb alındı! Oyuncu mutlu oldu, zıplama arttı.");
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ModifyJump(2f); // Zıplama yüksekliğini artır
        }
        base.ActivateEffect();
    }
}
