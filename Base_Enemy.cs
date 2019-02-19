using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Base_Enemy : MonoBehaviour
{

    #region variables
    [Header("Set Enemy Type")]
    public Enemy_Colour_Type local_Colour_type;
    public Enemy_Colour_Type enemy_Colour_Weakness;
    [Space]
    [Header("References To Be Set")]
    public Gameplay_Manager local_Gameplay_Manager;
    public Slider health_Bar;
    public Animator anim;

    public AudioSource moving_Sound;
    public AudioSource death_Sound;
    public AudioSource melee_attack_Sound;
    public AudioSource ranged_attack_Sound;
    public AudioSource take_Damage_Sound;
    public AudioSource melee_Hit_Sound;
    public AudioSource idle_Sound;



    [Header("To Be Set If Boss Enemy")]
    public Enemy_Boss_Manager local_Enemy_Boss_Manager;
    [Space]
    [Header("Enemy Properties To Be Set")]
    public string enemy_Name;
    public Color enemy_Name_Colour;
    [Space]
    public int health = 5;
    int starting_Health;
    public int times_More_Damage_Versus_Weakness = 2;
    public int number_Of_Souls_To_Drop = 10;
    public float enemy_Death_Duration = 1f;

    bool killed = false;
    [HideInInspector]
    public bool isSlowed;
    GameObject soul_Drop;
    public bool is_Boss_Enemy;
    public bool is_Tutorial_Enemy;
    float temp_Wait_For_Idle_Sound;

    IEnumerator Play_Idle_Sounds()
    {
        temp_Wait_For_Idle_Sound = Random.Range(4f, 12f);
        yield return new WaitForSeconds(temp_Wait_For_Idle_Sound);
        idle_Sound.Play();
    }

    private void Awake()
    {
        starting_Health = health;
        On_Spawn();
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void On_Spawn()
    {
        health = starting_Health;

        health_Bar.maxValue = health;
        health_Bar.value = health;

    }

    public enum Enemy_Colour_Type
    {
        RED,
        BLUE,
        YELLOW,
        PURPLE,
        GREEN,
        ORANGE
    }

    public enum Enemy_Type
    {
        RED_MELEE,
        BLUE_MELEE,
        YELLOW_MELEE
    }

    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if (!killed)
        {
            if (other.gameObject.tag == "Player_Projectile")
            {

                Enemy_Take_Damage(other.GetComponent<Projectile_Manager>().damage, (int)other.gameObject.GetComponent<Projectile_Manager>().local_Projectile_Colour, true);
            }

        }

    }

    /// <summary>
    /// Takes in damage from a variety of sources and determines whether the enemy is weak against the attack or not
    /// </summary>
    /// <param name="amount">Base amount of damage taken</param>
    /// <param name="colour">Colour type of damage taken</param>
    /// <param name="is_Player_Damage">Whether or not it is damage from the player</param>
    public void Enemy_Take_Damage(int amount, int colour, bool is_Player_Damage)
    {

        if (colour == (int)enemy_Colour_Weakness)
        {
            amount *= times_More_Damage_Versus_Weakness;
            if (is_Player_Damage)
            {
                local_Gameplay_Manager.Add_To_Combo_Count();
            }

            if (is_Boss_Enemy)
            {
                local_Enemy_Boss_Manager.Rotate_Through_Enemy_Types();
            }

        }
        else
        {
            if (is_Player_Damage)
            {
                local_Gameplay_Manager.Reset_Combo_Count();
            }

        }

        health -= amount;
        health_Bar.value = health;

        if (health <= 0)
        {
            killed = true;
            if (gameObject.activeSelf)
            {
                StartCoroutine("Kill_Self");
            }

        }

        if (!killed)
        {
            take_Damage_Sound.Play();
        }




    }


    /// <summary>
    /// spawn drops after being killed, and then set the enemy to be inactive
    /// </summary>
    /// <returns></returns>
    IEnumerator Kill_Self()
    {
        soul_Drop = local_Gameplay_Manager.Get_Soul_Drop((int)(local_Colour_type));
        soul_Drop.transform.position = gameObject.transform.position;
        soul_Drop.GetComponent<Soul_Pickup_Manager>().SetValues(number_Of_Souls_To_Drop, (int)local_Colour_type);
        soul_Drop.SetActive(true);
        soul_Drop.GetComponent<Soul_Pickup_Manager>().On_Spawn();
        if (isSlowed)
        {
            local_Gameplay_Manager.slowed_Enemies_Killed++;
        }
        anim.SetTrigger("Death");
        death_Sound.Play();
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        if (!is_Boss_Enemy)
        {
            gameObject.GetComponent<Enemy_AI>().local_Enemy_States = Enemy_AI.Enemy_States.DEAD;
        }
        else
        {
            gameObject.GetComponent<Boss_AI>().local_Enemy_States = Boss_AI.Enemy_States.DEAD;
        }

        yield return new WaitForSeconds(enemy_Death_Duration);
        killed = false;
        if (!is_Tutorial_Enemy)
        {
            local_Gameplay_Manager.Enemy_Destroyed();
        }

        if (is_Boss_Enemy)
        {
            local_Gameplay_Manager.StartCoroutine("Level_Passed");
        }
        gameObject.SetActive(false);

    }
}
