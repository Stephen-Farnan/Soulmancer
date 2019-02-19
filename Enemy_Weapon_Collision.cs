using System.Collections;
using UnityEngine;

public class Enemy_Weapon_Collision : MonoBehaviour
{
    public enum Enemy_Type
    {
        BASE_ENEMY,
        BOSS_ENEMY,
        TUTORIAL_ENEMY
    }
    [Header("Type Of Enemy To Be Set")]
    public Enemy_Type local_Enemy_Type;

    [Header("Reference To Be Set")]
    public Enemy_AI local_Enemy_Ai;
    public GameObject impact_Particles;
    public float impact_Particle_Duration = .1f;

    [Header("Only To Be Set If Boss Enemy")]
    public Boss_AI local_Boss_AI;


    /// <summary>
    /// set conditions for striking the player or the objective
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (local_Enemy_Ai.is_Attacking && !local_Enemy_Ai.has_Hit_Player)
        {


            switch (local_Enemy_Type)
            {
                case Enemy_Type.BASE_ENEMY:
                    if (other.gameObject.tag == "Player")
                    {
                        local_Enemy_Ai.Weapon_Deal_Damage_To_Player();
                        StartCoroutine("Weapon_Impact");
                        local_Enemy_Ai.has_Hit_Player = true;
                    }

                    if (other.gameObject.tag == "Objective")
                    {
                        local_Enemy_Ai.Weapon_Deal_Damage_To_Objective();
                        StartCoroutine("Weapon_Impact");
                        local_Enemy_Ai.has_Hit_Player = true;
                    }

                    if (other.gameObject.tag == "Final_Objective")
                    {
                        local_Enemy_Ai.Weapon_Deal_Damage_To_Final_Objective();
                        StartCoroutine("Weapon_Impact");
                        local_Enemy_Ai.has_Hit_Player = true;
                    }
                    break;

                case Enemy_Type.BOSS_ENEMY:
                    if (other.gameObject.tag == "Player")
                    {
                        local_Boss_AI.Weapon_Deal_Damage_To_Player();
                        StartCoroutine("Weapon_Impact");
                        local_Enemy_Ai.has_Hit_Player = true;
                    }
                    break;

                case Enemy_Type.TUTORIAL_ENEMY:
                    if (other.gameObject.tag == "Player")
                    {
                        local_Enemy_Ai.Weapon_Deal_Damage_To_Player();
                        StartCoroutine("Weapon_Impact");
                        local_Enemy_Ai.has_Hit_Player = true;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Visual and audio feedback for enemy attack
    /// </summary>
    /// <returns></returns>
    IEnumerator Weapon_Impact()
    {

        impact_Particles.SetActive(true);
        local_Enemy_Ai.local_Base_Enemy.melee_Hit_Sound.Play();
        yield return new WaitForSeconds(impact_Particle_Duration);
        impact_Particles.SetActive(false);
    }

}
