using System.Collections;
using UnityEngine;

public class Rune_Manager : MonoBehaviour
{
    #region variables
    float slow_Duration;
    float blast_Radius;
    float slow_Amount;
    float trigger_Radius = 5f;
    public float despawn_Time = 30f;
    bool isTriggered;
    int damage_Amount;
    int colour_Type;
    Collider[] enemies_Affected;
    [Header("References To Be Set")]
    public SphereCollider rune_Collider;
    public Rune_Drop_Special_Ability local_Rune_Drop_Special_Ability;
    public GameObject Rune_Particles_Explosion;
    public GameObject base_Particles;
    public AudioSource rune_Explosion_Sound;

    #endregion

    private void OnEnable()
    {
        StartCoroutine("Deactivate_Rune_Over_Time");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!isTriggered)
            {
                StartCoroutine("Activate_Rune");
                isTriggered = true;

            }

        }

    }


    /// <summary>
    /// calls the functionality for the ability when it is cast, and when it wears off
    /// </summary>
    /// <returns></returns>
    IEnumerator Activate_Rune()
    {
        Change_Values();
        rune_Explosion_Sound.Play();
        Rune_Particles_Explosion.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Rune_Particles_Explosion.SetActive(false);
        yield return new WaitForSeconds(slow_Duration - 1.5f);
        Reset_Values();
    }

    IEnumerator Deactivate_Rune_Over_Time()
    {
        yield return new WaitForSeconds(despawn_Time);
        isTriggered = false;
        local_Rune_Drop_Special_Ability.number_Of_Runes_Placed--;
        gameObject.SetActive(false);
    }


    /// <summary>
    /// finds all enemies in a certain radius from the rune, and slows their movement speed
    /// </summary>
    void Change_Values()
    {
        enemies_Affected = Physics.OverlapSphere(gameObject.transform.position, blast_Radius);
        foreach (Collider go in enemies_Affected)
        {
            if (go.gameObject.tag == "Enemy")
            {
                go.gameObject.GetComponent<Base_Enemy>().isSlowed = true;
                if ((int)go.gameObject.GetComponent<Base_Enemy>().enemy_Colour_Weakness == colour_Type)
                {
                    go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed -= (slow_Amount + 1);
                    go.gameObject.GetComponent<Base_Enemy>().health -= (damage_Amount + 1);
                    if (go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed < 1)
                    {
                        go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed = 1;
                    }
                }

                else
                {
                    go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed -= slow_Amount;
                    go.gameObject.GetComponent<Base_Enemy>().health -= damage_Amount;
                }

            }

            if (go.gameObject.tag == "Enemy_Boss")
            {
                go.gameObject.GetComponent<Base_Enemy>().isSlowed = true;
                if ((int)go.gameObject.GetComponent<Base_Enemy>().enemy_Colour_Weakness == colour_Type)
                {
                    go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed -= (slow_Amount + 1);
                    go.gameObject.GetComponent<Base_Enemy>().health -= (damage_Amount + 1);
                    if (go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed < 1)
                    {
                        go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed = 1;
                    }
                }

                else
                {
                    go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed -= slow_Amount;
                    go.gameObject.GetComponent<Base_Enemy>().health -= damage_Amount;
                }

            }

        }
    }


    /// <summary>
    /// sets the altered enemies's movement speed back to its original speed
    /// </summary>
    void Reset_Values()
    {
        foreach (Collider go in enemies_Affected)
        {
            if (go.gameObject.tag == "Enemy")
            {
                if ((int)go.gameObject.GetComponent<Base_Enemy>().enemy_Colour_Weakness == colour_Type)
                {
                    go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed += (slow_Amount + 1);
                }
                go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed += slow_Amount;

                if (go.gameObject.GetComponent<Enemy_AI>().local_Enemy_Type == Enemy_AI.Enemy_Type.MELEE)
                {
                    if (go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed > go.gameObject.GetComponent<Enemy_AI>().melee_Move_Speed)
                    {
                        go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed = go.gameObject.GetComponent<Enemy_AI>().melee_Move_Speed;
                    }
                }
                else
                {
                    if (go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed > go.gameObject.GetComponent<Enemy_AI>().ranged_Move_Speed)
                    {
                        go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed = go.gameObject.GetComponent<Enemy_AI>().ranged_Move_Speed;
                    }
                }

                go.gameObject.GetComponent<Base_Enemy>().isSlowed = false;
            }

            if (go.gameObject.tag == "Enemy_Boss")
            {
                if ((int)go.gameObject.GetComponent<Base_Enemy>().enemy_Colour_Weakness == colour_Type)
                {
                    go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed += (slow_Amount + 1);
                }
                go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed += slow_Amount;

                if (go.gameObject.GetComponent<Boss_AI>().local_Enemy_Type == Boss_AI.Enemy_Type.MELEE)
                {
                    if (go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed > go.gameObject.GetComponent<Boss_AI>().melee_Move_Speed)
                    {
                        go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed = go.gameObject.GetComponent<Boss_AI>().melee_Move_Speed;
                    }
                }
                else
                {
                    if (go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed > go.gameObject.GetComponent<Boss_AI>().ranged_Move_Speed)
                    {
                        go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed = go.gameObject.GetComponent<Boss_AI>().ranged_Move_Speed;
                    }
                }

                go.gameObject.GetComponent<Base_Enemy>().isSlowed = false;
            }
        }

        System.Array.Clear(enemies_Affected, 0, enemies_Affected.Length);
        isTriggered = false;
        local_Rune_Drop_Special_Ability.number_Of_Runes_Placed--;
        StopCoroutine("Deactivate_Rune_Over_Time");
        base_Particles.SetActive(true);
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Initialises the rune when called, based on attributes from the player
    /// </summary>
    /// <param name="slow_Magnitude">Base amount to slow by</param>
    /// <param name="damage">damage of rune</param>
    /// <param name="radius">radius of rune</param>
    /// <param name="slow_Length">duration of rune slow</param>
    /// <param name="colour">colour type of rune</param>
    /// <param name="player_trigger_radius">Radius which rune can trigger from</param>
    public void Set_Up_Rune(float slow_Magnitude, int damage, float radius, float slow_Length, int colour, float player_trigger_radius)
    {
        slow_Amount = slow_Magnitude;
        damage_Amount = damage;
        blast_Radius = radius;
        slow_Duration = slow_Length;
        colour_Type = colour;
        trigger_Radius = player_trigger_radius;
        rune_Collider.radius = trigger_Radius;
    }
}
