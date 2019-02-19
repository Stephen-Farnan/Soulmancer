using System.Collections;
using UnityEngine;

public class Enemy_Projectile_Collision : MonoBehaviour
{

    #region variables
    [HideInInspector]
    public Enemy_AI local_Enemy_Ai;
    [Header("Enemy References To Be Set")]
    public GameObject player_Position;
    [HideInInspector]
    public GameObject current_Objective;
    Vector3 target_Position;
    public TrailRenderer trail_Renderer;
    public GameObject impact_Particles;
    public AudioSource ranged_Hit_Sound;

    [Header("Properties To Be Set")]
    public float projectile_Speed = .001f;
    public float impact_Particle_Duration = .3f;

    [Header("Set Only If Boss Enemy")]
    public Boss_AI local_Boss_AI;

    public enum Target
    {
        PLAYER,
        OBJECTIVE
    }
    [HideInInspector]
    public Target local_Target;
    #endregion


    /// <summary>
    /// Set the target of the projectile to be the player or the objective
    /// </summary>
    public void On_Spawn()
    {
        if (local_Target == Target.PLAYER)
        {
            target_Position = player_Position.transform.position;
        }
        else
        {
            target_Position = current_Objective.transform.position;
        }
        transform.LookAt(target_Position);
        StartCoroutine("Move_Towards_Target");
    }


    /// <summary>
    /// reset the target of the projectile to be the player or the objective
    /// </summary>
    public void Set_Target_Pos()
    {
        if (local_Target == Target.PLAYER)
        {
            target_Position = player_Position.transform.position;
        }
        else
        {
            target_Position = current_Objective.transform.position;
        }

        transform.LookAt(target_Position);
    }


    /// <summary>
    /// move towards the target over time
    /// </summary>
    /// <returns></returns>
    IEnumerator Move_Towards_Target()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * projectile_Speed);
            yield return new WaitForSeconds(.001f);
        }

    }


    /// <summary>
    /// set conditions for interacting with certain objects in the scene
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (local_Boss_AI == null)
        {
            if (other.gameObject.tag == "Player")
            {
                local_Enemy_Ai.Projectile_Deal_Damage_To_Player();
                StartCoroutine("Destroy_Projectile");
            }
            if (other.gameObject.tag == "Objective")
            {
                local_Enemy_Ai.Projectile_Deal_Damage_To_Objective();
                StartCoroutine("Destroy_Projectile");
            }
            if (other.gameObject.tag == "Environment")
            {
                StartCoroutine("Destroy_Projectile");
            }

            if (other.gameObject.tag == "Final_Objective")
            {
                local_Enemy_Ai.Projectile_Deal_Damage_To_Final_Objective();
                StartCoroutine("Destroy_Projectile");
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                local_Boss_AI.Projectile_Deal_Damage_To_Player();
                StartCoroutine("Destroy_Projectile");
            }
            if (other.gameObject.tag == "Objective")
            {
                StartCoroutine("Destroy_Projectile");
            }
            if (other.gameObject.tag == "Environment")
            {
                StartCoroutine("Destroy_Projectile");
            }

            if (other.gameObject.tag == "Final_Objective")
            {
                StartCoroutine("Destroy_Projectile");
            }
        }


    }

    /// <summary>
    /// Handles projectile recycling
    /// </summary>
    /// <returns></returns>
    IEnumerator Destroy_Projectile()
    {
        impact_Particles.SetActive(true);
        ranged_Hit_Sound.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StopCoroutine("Move_Towards_Target");
        yield return new WaitForSeconds(impact_Particle_Duration);
        impact_Particles.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.SetActive(false);
    }
}
