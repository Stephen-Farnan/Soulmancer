using System.Collections;
using UnityEngine;


public class Aoe_Special_Ability : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public Gameplay_Manager local_Gameplay_Manager;
    public Player_Manager local_Player_Manager;
    public UI_Manager local_UI_Manager;
    public Player_Input local_Player_Input;
    public Camera_Controller local_Camera_Controller;

    public GameObject aoe_Particles_Red;
    public GameObject aoe_Particles_Blue;
    public GameObject aoe_Particles_Yellow;
    public GameObject aoe_Particles_Purple;
    public GameObject aoe_Particles_Green;
    public GameObject aoe_Particles_Orange;
    public GameObject aoe_Particles_Default;


    int player_Colour = 0;
    bool aoe_Attack_Is_On_Colldown;
    float temp;

    public SphereCollider aoe_Collider;

    Collider[] enemies_Hit;


    [HideInInspector]
    public int knockback_Strength_Level = 1;

    [HideInInspector]
    public int knockback_Duration_Level = 1;
    [HideInInspector]
    public int aoe_Attack_Cooldown_Level = 1;
    [HideInInspector]
    public int slow_Reduciton_Level = 1;
    [HideInInspector]
    public int aoe_Radius_Level = 1;
    [HideInInspector]
    public int aoe_Damage_Level = 1;
    //upgradeable stats
    [Header("Variables To Be Set")]
    public float knockback_Strength = .45f;
    public float knockback_Duration = .2f;
    public float aoe_Attack_Cooldown = 2f;
    public float slow_Reduction = 3f;
    public float aoe_Radius = 12f;
    public int aoe_Damage = 3;
    public float particle_Duration = 2f;

    Animator anim;
    #endregion

    private void Awake()
    {
        Initialise_Upgradeable_Stats();
        anim = local_Player_Manager.anim;
    }

    /// <summary>
    /// Sets the stats based on purchased upgrades
    /// </summary>
    void Initialise_Upgradeable_Stats()
    {

        knockback_Strength_Level = local_Player_Manager.AOE_Special_Ability_Level;
        knockback_Duration_Level = local_Player_Manager.AOE_Special_Ability_Level;
        aoe_Attack_Cooldown_Level = local_Player_Manager.AOE_Special_Ability_Level;
        slow_Reduciton_Level = local_Player_Manager.AOE_Special_Ability_Level;
        aoe_Radius_Level = local_Player_Manager.AOE_Special_Ability_Level;
        aoe_Damage_Level = local_Player_Manager.AOE_Special_Ability_Level;

        switch (knockback_Strength_Level)
        {
            case 2:
                knockback_Strength += .1f;
                break;

            case 3:
                knockback_Strength += .2f;
                break;

            case 4:
                knockback_Strength += .3f;
                break;

            case 5:
                knockback_Strength += .4f;
                break;
        }

        switch (aoe_Attack_Cooldown_Level)
        {
            case 2:
                aoe_Attack_Cooldown -= .5f;
                break;

            case 3:
                aoe_Attack_Cooldown -= 1f;
                break;

            case 4:
                aoe_Attack_Cooldown -= 1.5f;
                break;

            case 5:
                aoe_Attack_Cooldown -= 2f;
                break;
        }

        switch (slow_Reduciton_Level)
        {
            case 2:
                slow_Reduction += 1;
                break;

            case 3:
                slow_Reduction += 2;
                break;

            case 4:
                slow_Reduction += 3;
                break;

            case 5:
                slow_Reduction += 4;
                break;

        }

        switch (aoe_Radius_Level)
        {
            case 2:
                aoe_Radius += .5f;
                break;

            case 3:
                aoe_Radius += 1f;
                break;

            case 4:
                aoe_Radius += 1.5f;
                break;

            case 5:
                aoe_Radius += 2f;
                break;

        }

        switch (aoe_Damage_Level)
        {
            case 2:
                aoe_Damage += 2;
                break;

            case 3:
                aoe_Damage += 4;
                break;

            case 4:
                aoe_Damage += 6;
                break;

            case 5:
                aoe_Damage += 8;
                break;
        }


    }


    /// <summary>
    /// Initial call to start the ability
    /// </summary>
    /// <param name="colour">Type of colour to use with ability</param>
    public void Aoe_Abilitiy(int colour)
    {
        player_Colour = colour;
        if (!aoe_Attack_Is_On_Colldown)
        {
            //Setting animation trigger for ability
            anim.SetTrigger("Spell");
            local_Player_Manager.Aoe_Sound.Play();
            local_Camera_Controller.StartCoroutine("Chromatic");


            switch (local_Player_Manager.local_Selected_Colour)
            {
                case Player_Manager.Selected_Colour.RED:
                    StartCoroutine("Play_Particle_Effect", aoe_Particles_Red);
                    break;

                case Player_Manager.Selected_Colour.BLUE:
                    StartCoroutine("Play_Particle_Effect", aoe_Particles_Blue);
                    break;

                case Player_Manager.Selected_Colour.YELLOW:
                    StartCoroutine("Play_Particle_Effect", aoe_Particles_Yellow);
                    break;

                case Player_Manager.Selected_Colour.PURPLE:
                    StartCoroutine("Play_Particle_Effect", aoe_Particles_Purple);
                    break;

                case Player_Manager.Selected_Colour.GREEN:
                    StartCoroutine("Play_Particle_Effect", aoe_Particles_Green);
                    break;

                case Player_Manager.Selected_Colour.ORANGE:
                    StartCoroutine("Play_Particle_Effect", aoe_Particles_Orange);
                    break;

                case Player_Manager.Selected_Colour.DEFAULT:
                    StartCoroutine("Play_Particle_Effect", aoe_Particles_Default);
                    break;
            }

            local_Player_Input.StopCoroutine("Halt_Player");
            local_Player_Input.StartCoroutine("Halt_Player");
            local_UI_Manager.aoe_Cooldown_Timer = aoe_Attack_Cooldown;
            enemies_Hit = Physics.OverlapSphere(gameObject.transform.position, aoe_Radius);
            StartCoroutine("Knock_Enemies_Back");
            StartCoroutine("Stop_Knockback");
            StartCoroutine("Reset_Knockback_cooldown");

        }

    }


    /// <summary>
    /// function over time to move the enemies away from the player
    /// </summary>
    /// <returns></returns>
    IEnumerator Knock_Enemies_Back()
    {
        int i = 0;
        foreach (Collider go in enemies_Hit)
        {
            if (go.gameObject.tag == "Enemy")
            {
                i++;
                go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed -= slow_Reduction;
                if (go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed < 1)
                {
                    go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed = 1;
                }
            }

            if (go.gameObject.tag == "Enemy_Boss")
            {
                i++;
                go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed -= slow_Reduction;
                if (go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed < 1)
                {
                    go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed = 1;
                }

            }

        }

        if (i >= local_Gameplay_Manager.number_Of_Enemies_To_Hit_At_Once)
        {
            local_Gameplay_Manager.enemies_Hit_At_Once = true;
        }

        while (true)
        {
            Knock_Enemies_Back_Function();
            yield return new WaitForSeconds(0.01f);
        }

    }


    /// <summary>
    /// Handles the over time knockback 
    /// </summary>
    void Knock_Enemies_Back_Function()
    {

        aoe_Attack_Is_On_Colldown = true;
        foreach (Collider go in enemies_Hit)
        {
            if (go.gameObject.tag == "Enemy")
            {
                go.gameObject.transform.position = Vector3.MoveTowards(go.transform.position, gameObject.transform.position, -knockback_Strength);

            }

            if (go.gameObject.tag == "Enemy_Boss")
            {
                go.gameObject.transform.position = Vector3.MoveTowards(go.transform.position, gameObject.transform.position, -knockback_Strength);

            }

        }
    }


    /// <summary>
    /// Stop enemies from moving, return them to their original speeds or set defaults in fringe cases
    /// </summary>
    /// <returns></returns>
    IEnumerator Stop_Knockback()
    {
        yield return new WaitForSeconds(knockback_Duration);
        StopCoroutine("Knock_Enemies_Back");
        foreach (Collider go in enemies_Hit)
        {
            if (go.gameObject.tag == "Enemy")
            {

                go.gameObject.GetComponent<Base_Enemy>().Enemy_Take_Damage(aoe_Damage, player_Colour, false);
                go.gameObject.GetComponent<Enemy_AI>().local_Nav_Mesh_Agent.speed += slow_Reduction;

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
            }

            if (go.gameObject.tag == "Enemy_Boss")
            {

                go.gameObject.GetComponent<Base_Enemy>().Enemy_Take_Damage(aoe_Damage, player_Colour, false);
                go.gameObject.GetComponent<Boss_AI>().local_Nav_Mesh_Agent.speed += slow_Reduction;

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
            }

        }

        System.Array.Clear(enemies_Hit, 0, enemies_Hit.Length);


    }

    IEnumerator Play_Particle_Effect(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(particle_Duration);
        go.SetActive(false);
    }


    /// <summary>
    /// enable the function to be called again after a certain amount of time
    /// </summary>
    /// <returns></returns>
    IEnumerator Reset_Knockback_cooldown()
    {
        yield return new WaitForSeconds(aoe_Attack_Cooldown);
        aoe_Attack_Is_On_Colldown = false;

    }
}


