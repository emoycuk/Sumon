using UnityEngine;

[CreateAssetMenu(fileName = "Player1Status", menuName = "Scriptable Objects/Player1Status")]
public class Player1Status : ScriptableObject
{
    public int hp = 100;

    public void takeDmg(int dmg)
    {
        if (dmg > hp)
        {
            hp = 0;
            return;
        }

        hp = hp - dmg;
    }

    public bool isDead()
    {
        return (hp <= 0);
    }

}
