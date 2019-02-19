using System.Collections;
using UnityEngine;

public class Objective_Projectile_Controller : MonoBehaviour
{

    [Header("Properties To Be Set")]
    public float travel_Speed = 35f;
    float impact_Particle_Duration = .2f;
    public GameObject impact_Particles;
    public GameObject main_Particles;
    bool can_Hit = true;
    public AudioSource hit_Sound;

    int damage = 0;

    public void On_Spawn()
    {
        StartCoroutine("Move_Objective_Projectile_Forwards");
    }

    IEnumerator Move_Objective_Projectile_Forwards()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * travel_Speed);
            yield return new WaitForSeconds(0.002f);
        }

    }

    public void Set_Damage(int new_Damage_Amount)
    {
        damage = new_Damage_Amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (can_Hit)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Base_Enemy>().Enemy_Take_Damage(damage, 7, false);
                can_Hit = false;
                StartCoroutine("Destroy_Objective_Projectiles", gameObject);
            }

            else if (other.gameObject.tag == "Environment")
            {
                StartCoroutine("Destroy_Objective_Projectiles", gameObject);
            }
        }

    }


    /// <summary>
    /// sets the projectile inactive after a certain time
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    private IEnumerator Destroy_Objective_Projectiles(GameObject go)
    {
        impact_Particles.SetActive(true);
        hit_Sound.Play();
        main_Particles.SetActive(false);
        StopCoroutine("Move_Objective_Projectile_Forwards");
        yield return new WaitForSeconds(impact_Particle_Duration);
        main_Particles.SetActive(true);

        impact_Particles.SetActive(false);
        can_Hit = true;
        go.SetActive(false);

    }
}
