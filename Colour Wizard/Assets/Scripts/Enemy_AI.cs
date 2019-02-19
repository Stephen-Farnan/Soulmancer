using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public GameObject[] red_Enemy_Projectiles;
    public GameObject[] blue_Enemy_Projectiles;
    public GameObject[] yellow_Enemy_Projectiles;
    public GameObject[] purple_Enemy_Projectiles;
    public GameObject[] green_Enemy_Projectiles;
    public GameObject[] orange_Enemy_Projectiles;

    int current_Red_Enemy_Projectile = 0;
    int current_Blue_Enemy_Projectile = 0;
    int current_Yellow_Enemy_Projectile = 0;
    int current_Purple_Enemy_Projectile = 0;
    int current_Green_Enemy_Projectile = 0;
    int current_Orange_Enemy_Projectile = 0;

    public Gameplay_Manager local_Gameplay_Manager;
    public Base_Enemy local_Base_Enemy;
    public Player_Manager local_Player_Manager;
    [HideInInspector]
    public GameObject objective_Gameobject;
    public GameObject enemy_Projectile_Origin;
    public GameObject player_Gameobject;
    [HideInInspector]
    public NavMeshAgent local_Nav_Mesh_Agent;

    public bool first_Objective_Is_Destroyed;

    public bool is_Attacking;
    [HideInInspector]
    public bool recently_Hit_Player = false;
    [HideInInspector]
    public bool has_Hit_Player = false;
    bool is_Playing_Movement_Animation;
    float distance_To_Player;
    float attack_Speed;
    float distance_To_Objective;
    float attack_Range;
    float distance_To_Nearest_Objective = 1000f;
    int damage_Per_Attack;
    int objective_Gameobject_Index;
    [HideInInspector]
    public int current_Target_Index;

    [Header("Enemy Proprties TO Be Set")]
    public float melee_Move_Speed;
    public float melee_Attack_Speed;
    public int melee_Damage_Per_Hit;
    public float melee_Attack_Range;
    [Space]
    public float ranged_Attack_Speed;
    public int ranged_Damage_Per_hit;
    public float ranged_Move_Speed;
    public float ranged_Attack_Range;
    public float projectile_Duration = 5;
    [Space]
    public float aggro_Range;
    public float max_Attack_Range;
    [Space]
    public Enemy_Type local_Enemy_Type;



    public enum Enemy_Type
    {
        MELEE,
        RANGED
    }




    public enum Enemy_States
    {
        MOVING_TO_OBJECTIVE,
        MOVING_TO_PLAYER,
        ATTACKING_PLAYER,
        ATTACKING_OBJECTIVE,
        DEAD
    }
    //[HideInInspector]
    public Enemy_States local_Enemy_States;

    public Animator anim;
    #endregion

    /// <summary>
    /// sets up the enemy class variables based on whether melee or ranged
    /// </summary>
    void Start()
    {
        local_Nav_Mesh_Agent = gameObject.GetComponent<NavMeshAgent>();
        if (local_Enemy_Type == Enemy_Type.MELEE)
        {
            attack_Speed = melee_Attack_Speed;
            damage_Per_Attack = melee_Damage_Per_Hit;
            local_Nav_Mesh_Agent.speed = melee_Move_Speed;
            attack_Range = melee_Attack_Range;
        }
        else
        {
            attack_Speed = ranged_Attack_Speed;
            damage_Per_Attack = ranged_Damage_Per_hit;
            local_Nav_Mesh_Agent.speed = ranged_Move_Speed;
            attack_Range = ranged_Attack_Range;
        }

        Set_Target_Objective();


        StartCoroutine("Update_AI_State");
    }

    private void OnEnable()
    {
        local_Enemy_States = Enemy_States.MOVING_TO_OBJECTIVE;
    }

    public void On_Spawn()
    {
        local_Enemy_States = Enemy_States.MOVING_TO_OBJECTIVE;
        Set_Target_Objective();
    }

    /// <summary>
    /// Decides which target the enemy should move towards
    /// </summary>
    public void Set_Target_Objective()
    {
        if (!first_Objective_Is_Destroyed)
        {
            int i = 0;
            foreach (GameObject go in local_Gameplay_Manager.objective_GameObjects)
            {
                if (Vector3.Distance(gameObject.transform.position, local_Gameplay_Manager.objective_GameObjects[i].gameObject.transform.position) < distance_To_Nearest_Objective)
                {
                    distance_To_Nearest_Objective = Vector3.Distance(gameObject.transform.position, local_Gameplay_Manager.objective_GameObjects[i].gameObject.transform.position);
                    objective_Gameobject = local_Gameplay_Manager.objective_GameObjects[i];
                    objective_Gameobject_Index = i;
                }
                i++;
            }

        }

        else
        {
            objective_Gameobject = local_Gameplay_Manager.final_Objective;
        }

    }


    /// <summary>
    /// State machine handling AI logic
    /// </summary>
    /// <returns></returns>
    IEnumerator Update_AI_State()
    {
        while (true)
        {
            distance_To_Player = Vector3.Distance(transform.position, player_Gameobject.transform.position);
            distance_To_Objective = Vector3.Distance(transform.position, objective_Gameobject.transform.position);

            switch (local_Enemy_States)
            {
                case Enemy_States.MOVING_TO_OBJECTIVE:
                    Set_Target_Objective();
                    if (distance_To_Player <= aggro_Range && !is_Attacking)
                    {
                        local_Enemy_States = Enemy_States.MOVING_TO_PLAYER;
                    }
                    else if (distance_To_Objective <= attack_Range)
                    {
                        local_Enemy_States = Enemy_States.ATTACKING_OBJECTIVE;
                    }
                    else
                    {
                        local_Nav_Mesh_Agent.destination = objective_Gameobject.transform.position;

                        if (!is_Playing_Movement_Animation)
                        {
                            anim.SetBool("Running", true);
                            if (!local_Base_Enemy.moving_Sound.isPlaying)
                            {
                                //local_Base_Enemy.moving_Sound.Play();
                            }

                            is_Playing_Movement_Animation = true;
                        }

                    }
                    break;

                case Enemy_States.MOVING_TO_PLAYER:
                    if (distance_To_Player > attack_Range && distance_To_Player <= max_Attack_Range)
                    {
                        local_Nav_Mesh_Agent.destination = player_Gameobject.transform.position;

                        if (!is_Playing_Movement_Animation)
                        {
                            anim.SetBool("Running", true);
                            if (!local_Base_Enemy.moving_Sound.isPlaying)
                            {
                                // local_Base_Enemy.moving_Sound.Play();
                            }
                            is_Playing_Movement_Animation = true;
                        }
                    }
                    else if (distance_To_Player >= max_Attack_Range)
                    {
                        local_Enemy_States = Enemy_States.MOVING_TO_OBJECTIVE;
                    }
                    else
                    {
                        local_Enemy_States = Enemy_States.ATTACKING_PLAYER;
                    }
                    break;

                case Enemy_States.ATTACKING_PLAYER:
                    gameObject.transform.LookAt(player_Gameobject.transform);
                    gameObject.transform.localEulerAngles = new Vector3(0f, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                    local_Nav_Mesh_Agent.destination = gameObject.transform.position;
                    if (distance_To_Player >= max_Attack_Range)
                    {
                        local_Enemy_States = Enemy_States.MOVING_TO_OBJECTIVE;
                    }
                    else if (distance_To_Player > attack_Range && !is_Attacking)
                    {
                        local_Enemy_States = Enemy_States.MOVING_TO_PLAYER;
                    }
                    else
                    {
                        if (!recently_Hit_Player)
                        {
                            current_Target_Index = 0;
                            StartCoroutine("Cast_Attack", player_Gameobject);
                        }

                        anim.SetBool("Running", false);
                        if (local_Base_Enemy.moving_Sound.isPlaying)
                        {
                            //local_Base_Enemy.moving_Sound.Stop();
                        }
                        is_Playing_Movement_Animation = false;
                    }


                    break;

                case Enemy_States.ATTACKING_OBJECTIVE:
                    gameObject.transform.LookAt(objective_Gameobject.transform);
                    gameObject.transform.localEulerAngles = new Vector3(0f, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                    distance_To_Objective = Vector3.Distance(transform.position, objective_Gameobject.transform.position);
                    local_Nav_Mesh_Agent.destination = gameObject.transform.position;

                    if (distance_To_Player <= distance_To_Objective && distance_To_Player <= aggro_Range)
                    {
                        local_Enemy_States = Enemy_States.MOVING_TO_PLAYER;
                    }
                    else if (objective_Gameobject.GetComponent<Objective_Manager>().is_Destroyed && !is_Attacking)
                    {
                        first_Objective_Is_Destroyed = true;
                        Set_Target_Objective();
                        local_Enemy_States = Enemy_States.MOVING_TO_OBJECTIVE;
                    }
                    else if (distance_To_Objective > attack_Range && !is_Attacking)
                    {
                        local_Enemy_States = Enemy_States.MOVING_TO_OBJECTIVE;
                    }
                    else if (!recently_Hit_Player)
                    {
                        current_Target_Index = 1;
                        StartCoroutine("Cast_Attack", objective_Gameobject);
                    }

                    anim.SetBool("Running", false);
                    if (local_Base_Enemy.moving_Sound.isPlaying)
                    {
                        // local_Base_Enemy.moving_Sound.Stop();
                    }
                    is_Playing_Movement_Animation = false;
                    Set_Target_Objective();
                    break;

                case Enemy_States.DEAD:
                    local_Nav_Mesh_Agent.destination = gameObject.transform.position;
                    break;

            }
            local_Base_Enemy.health_Bar.transform.LookAt(Camera.main.transform);
            yield return new WaitForSeconds(0.002f);
        }

    }


    /// <summary>
    /// Sends an attack out based on the enemy attack speed
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    IEnumerator Cast_Attack(GameObject go)
    {
        Cast_Attack_Fucntion(go);
        yield return new WaitForSeconds(.3f);
        is_Attacking = true;
        yield return new WaitForSeconds(1f);
        is_Attacking = false;
        yield return new WaitForSeconds(attack_Speed);
        has_Hit_Player = false;
        recently_Hit_Player = false;
    }

    IEnumerator Spawn_Projectile(GameObject go)
    {
        yield return new WaitForSeconds(.20f);
        Spawn_New_Projectile_Function(go);
    }

    /// <summary>
    /// Chooses the appropriate colour projectile to spawn from the object pool
    /// </summary>
    /// <param name="go">Target for projectile</param>
    void Spawn_New_Projectile_Function(GameObject go)
    {
        switch ((int)local_Base_Enemy.local_Colour_type)
        {
            case 0:
                red_Enemy_Projectiles[current_Red_Enemy_Projectile].transform.position = enemy_Projectile_Origin.transform.position;
                red_Enemy_Projectiles[current_Red_Enemy_Projectile].transform.LookAt(go.transform);
                red_Enemy_Projectiles[current_Red_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().trail_Renderer.Clear();
                red_Enemy_Projectiles[current_Red_Enemy_Projectile].SetActive(true);
                red_Enemy_Projectiles[current_Red_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().current_Objective = objective_Gameobject;
                red_Enemy_Projectiles[current_Red_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().On_Spawn();
                if (current_Target_Index == 0)
                {
                    red_Enemy_Projectiles[current_Red_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.PLAYER;
                }
                else
                {
                    red_Enemy_Projectiles[current_Red_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.OBJECTIVE;
                }

                red_Enemy_Projectiles[current_Red_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Enemy_Ai = gameObject.GetComponent<Enemy_AI>();
                red_Enemy_Projectiles[current_Red_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().Set_Target_Pos();
                StartCoroutine("Destroy_Enemy_Particles", red_Enemy_Projectiles[current_Red_Enemy_Projectile]);
                if (current_Red_Enemy_Projectile < red_Enemy_Projectiles.Length - 1)
                {
                    current_Red_Enemy_Projectile++;
                }
                else
                {
                    current_Red_Enemy_Projectile = 0;
                }
                break;

            case 1:
                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].transform.position = enemy_Projectile_Origin.transform.position;
                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].transform.LookAt(go.transform);
                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().trail_Renderer.Clear();
                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].SetActive(true);
                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().current_Objective = objective_Gameobject;
                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().On_Spawn();
                if (current_Target_Index == 0)
                {
                    blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.PLAYER;
                }
                else
                {
                    blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.OBJECTIVE;
                }

                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Enemy_Ai = gameObject.GetComponent<Enemy_AI>();
                blue_Enemy_Projectiles[current_Blue_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().Set_Target_Pos();
                StartCoroutine("Destroy_Enemy_Particles", blue_Enemy_Projectiles[current_Blue_Enemy_Projectile]);
                if (current_Blue_Enemy_Projectile < blue_Enemy_Projectiles.Length - 1)
                {
                    current_Blue_Enemy_Projectile++;
                }
                else
                {
                    current_Blue_Enemy_Projectile = 0;
                }
                break;

            case 2:
                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].transform.position = enemy_Projectile_Origin.transform.position;
                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].transform.LookAt(go.transform);
                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().trail_Renderer.Clear();
                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].SetActive(true);
                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().current_Objective = objective_Gameobject;
                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().On_Spawn();
                if (current_Target_Index == 0)
                {
                    yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.PLAYER;
                }
                else
                {
                    yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.OBJECTIVE;
                }

                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Enemy_Ai = gameObject.GetComponent<Enemy_AI>();
                yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().Set_Target_Pos();
                StartCoroutine("Destroy_Enemy_Particles", yellow_Enemy_Projectiles[current_Yellow_Enemy_Projectile]);
                if (current_Yellow_Enemy_Projectile < yellow_Enemy_Projectiles.Length - 1)
                {
                    current_Yellow_Enemy_Projectile++;
                }
                else
                {
                    current_Yellow_Enemy_Projectile = 0;
                }
                break;

            case 3:
                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].transform.position = enemy_Projectile_Origin.transform.position;
                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].transform.LookAt(go.transform);
                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().trail_Renderer.Clear();
                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].SetActive(true);
                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().current_Objective = objective_Gameobject;
                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().On_Spawn();
                if (current_Target_Index == 0)
                {
                    purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.PLAYER;
                }
                else
                {
                    purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.OBJECTIVE;
                }

                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Enemy_Ai = gameObject.GetComponent<Enemy_AI>();
                purple_Enemy_Projectiles[current_Purple_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().Set_Target_Pos();
                StartCoroutine("Destroy_Enemy_Particles", purple_Enemy_Projectiles[current_Purple_Enemy_Projectile]);
                if (current_Purple_Enemy_Projectile < purple_Enemy_Projectiles.Length - 1)
                {
                    current_Purple_Enemy_Projectile++;
                }
                else
                {
                    current_Purple_Enemy_Projectile = 0;
                }
                break;

            case 4:
                green_Enemy_Projectiles[current_Green_Enemy_Projectile].transform.position = enemy_Projectile_Origin.transform.position;
                green_Enemy_Projectiles[current_Green_Enemy_Projectile].transform.LookAt(go.transform);
                green_Enemy_Projectiles[current_Green_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().trail_Renderer.Clear();
                green_Enemy_Projectiles[current_Green_Enemy_Projectile].SetActive(true);
                green_Enemy_Projectiles[current_Green_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().current_Objective = objective_Gameobject;
                green_Enemy_Projectiles[current_Green_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().On_Spawn();
                if (current_Target_Index == 0)
                {
                    green_Enemy_Projectiles[current_Green_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.PLAYER;
                }
                else
                {
                    green_Enemy_Projectiles[current_Green_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.OBJECTIVE;
                }

                green_Enemy_Projectiles[current_Green_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Enemy_Ai = gameObject.GetComponent<Enemy_AI>();
                green_Enemy_Projectiles[current_Green_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().Set_Target_Pos();
                StartCoroutine("Destroy_Enemy_Particles", green_Enemy_Projectiles[current_Green_Enemy_Projectile]);
                if (current_Green_Enemy_Projectile < green_Enemy_Projectiles.Length - 1)
                {
                    current_Green_Enemy_Projectile++;
                }
                else
                {
                    current_Green_Enemy_Projectile = 0;
                }
                break;

            case 5:
                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].transform.position = enemy_Projectile_Origin.transform.position;
                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].transform.LookAt(go.transform);
                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().trail_Renderer.Clear();
                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].SetActive(true);
                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().current_Objective = objective_Gameobject;
                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().On_Spawn();
                if (current_Target_Index == 0)
                {
                    orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.PLAYER;
                }
                else
                {
                    orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Target = Enemy_Projectile_Collision.Target.OBJECTIVE;
                }

                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().local_Enemy_Ai = gameObject.GetComponent<Enemy_AI>();
                orange_Enemy_Projectiles[current_Orange_Enemy_Projectile].GetComponent<Enemy_Projectile_Collision>().Set_Target_Pos();
                StartCoroutine("Destroy_Enemy_Particles", orange_Enemy_Projectiles[current_Orange_Enemy_Projectile]);
                if (current_Orange_Enemy_Projectile < orange_Enemy_Projectiles.Length - 1)
                {
                    current_Orange_Enemy_Projectile++;
                }
                else
                {
                    current_Orange_Enemy_Projectile = 0;
                }
                break;
        }
    }


    /// <summary>
    /// The attack functionality for the attack over time function
    /// </summary>
    /// <param name="go"></param>
    void Cast_Attack_Fucntion(GameObject go)
    {
        recently_Hit_Player = true;
        //If the enemy is ranged, find an appropriate projectile from an array, based on the enemy colour
        if (local_Enemy_Type == Enemy_Type.RANGED)
        {
            //Play animation for BOW here
            anim.SetTrigger("Attack_Ranged");
            local_Base_Enemy.ranged_attack_Sound.Play();

            StartCoroutine("Spawn_Projectile", go);


        }


        else
        {
            //play animation for swing
            anim.SetTrigger("Attack_Melee");
            local_Base_Enemy.melee_attack_Sound.Play();
        }



    }


    /// <summary>
    /// Destroy the projectile after a certain time
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    IEnumerator Destroy_Enemy_Particles(GameObject go)
    {
        yield return new WaitForSeconds(projectile_Duration);
        go.SetActive(false);
    }


    /// <summary>
    /// Deal damage to the player with a melee weapon if attacking is enabled
    /// </summary>
    public void Weapon_Deal_Damage_To_Player()
    {

        local_Player_Manager.Take_Damage(damage_Per_Attack, (int)local_Base_Enemy.local_Colour_type);
    }


    /// <summary>
    /// Deal damage to the player with a projectile if attacking is enabled
    /// </summary>
    public void Projectile_Deal_Damage_To_Player()
    {
        local_Player_Manager.Take_Damage(damage_Per_Attack, (int)local_Base_Enemy.local_Colour_type);
    }


    /// <summary>
    /// Deal damage to the objective with a melee weapon if attacking is enabled
    /// </summary>
    public void Weapon_Deal_Damage_To_Objective()
    {
        local_Gameplay_Manager.Objective_Attacked(damage_Per_Attack, objective_Gameobject_Index, gameObject);
    }


    /// <summary>
    /// Deal damage to the objective with a projectile if attacking is enabled
    /// </summary>
    public void Projectile_Deal_Damage_To_Objective()
    {
        local_Gameplay_Manager.Objective_Attacked(damage_Per_Attack, objective_Gameobject_Index, gameObject);
    }

    public void Projectile_Deal_Damage_To_Final_Objective()
    {
        local_Gameplay_Manager.Final_Objective_Attacked(damage_Per_Attack);
    }

    public void Weapon_Deal_Damage_To_Final_Objective()
    {
        if (is_Attacking)
        {
            local_Gameplay_Manager.Final_Objective_Attacked(damage_Per_Attack);
        }
    }

}
