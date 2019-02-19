using UnityEngine;

public class Enemy_Boss_Manager : MonoBehaviour
{

    [Header("References To Be Set")]
    public Base_Enemy local_Base_Enemy;
    public Boss_AI local_Boss_AI;
    public Renderer boss_Renderer;


    public Material red;
    public Material blue;
    public Material yellow;
    public Material purple;
    public Material green;
    public Material orange;



    /// <summary>
    /// Changes boss colour type, weakness, and material colour
    /// </summary>
    public void Rotate_Through_Enemy_Types()
    {

        switch (local_Base_Enemy.local_Colour_type)
        {
            case Base_Enemy.Enemy_Colour_Type.RED:
                local_Base_Enemy.local_Colour_type = Base_Enemy.Enemy_Colour_Type.BLUE;
                local_Base_Enemy.enemy_Colour_Weakness = Base_Enemy.Enemy_Colour_Type.YELLOW;
                boss_Renderer.material = blue;
                break;

            case Base_Enemy.Enemy_Colour_Type.BLUE:
                local_Base_Enemy.local_Colour_type = Base_Enemy.Enemy_Colour_Type.YELLOW;
                local_Base_Enemy.enemy_Colour_Weakness = Base_Enemy.Enemy_Colour_Type.RED;
                boss_Renderer.material = yellow;
                break;

            case Base_Enemy.Enemy_Colour_Type.YELLOW:
                local_Base_Enemy.local_Colour_type = Base_Enemy.Enemy_Colour_Type.PURPLE;
                local_Base_Enemy.enemy_Colour_Weakness = Base_Enemy.Enemy_Colour_Type.GREEN;
                boss_Renderer.material = purple;
                break;

            case Base_Enemy.Enemy_Colour_Type.PURPLE:
                local_Base_Enemy.local_Colour_type = Base_Enemy.Enemy_Colour_Type.GREEN;
                local_Base_Enemy.enemy_Colour_Weakness = Base_Enemy.Enemy_Colour_Type.ORANGE;
                boss_Renderer.material = green;
                break;

            case Base_Enemy.Enemy_Colour_Type.GREEN:
                local_Base_Enemy.local_Colour_type = Base_Enemy.Enemy_Colour_Type.ORANGE;
                local_Base_Enemy.enemy_Colour_Weakness = Base_Enemy.Enemy_Colour_Type.PURPLE;
                boss_Renderer.material = orange;
                break;

            case Base_Enemy.Enemy_Colour_Type.ORANGE:
                local_Base_Enemy.local_Colour_type = Base_Enemy.Enemy_Colour_Type.RED;
                local_Base_Enemy.enemy_Colour_Weakness = Base_Enemy.Enemy_Colour_Type.BLUE;
                boss_Renderer.material = red;
                break;
        }


    }
}
