using System.Collections;
using UnityEngine;

public class Projectile_Manager : MonoBehaviour
{

    #region variables

    [Header("Properties To Be Set")]
    public float projectile_Speed = 5f;
    public float impact_Particle_Duration = .1f;
    public GameObject impact_Particles;
    public GameObject Main_Particles;

    public enum Projectile_Colour
    {
        RED,
        BLUE,
        YELLOW,
        PURPLE,
        GREEN,
        ORANGE,
        DEFAULT
    }

    public int damage = 1;

    public Projectile_Colour local_Projectile_Colour;
    #endregion

    public void On_Spawn()
    {
        StartCoroutine("Move_Projectile_Forwards");

    }


    /// <summary>
    /// move the object forward intermitently
    /// </summary>
    /// <returns></returns>
    IEnumerator Move_Projectile_Forwards()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * projectile_Speed);
            yield return new WaitForSeconds(0.001f);
        }
    }


    /// <summary>
    /// conditions for interacting with certain objects in the scene
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mixing_Stand")
        {
            gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine("Destroy_Projectiles", gameObject);
            StopCoroutine("Move_Projectile_Forwards");
        }

        if (other.gameObject.tag == "Enemy_Boss")
        {
            StartCoroutine("Destroy_Projectiles", gameObject);
            StopCoroutine("Move_Projectile_Forwards");
        }
        if (other.gameObject.tag == "Environment")
        {
            StartCoroutine("Destroy_Projectiles", gameObject);
            StopCoroutine("Move_Projectile_Forwards");
        }
    }


    /// <summary>
    /// sets the projectile inactive after a certain time
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    private IEnumerator Destroy_Projectiles(GameObject go)
    {
        impact_Particles.SetActive(true);
        gameObject.GetComponent<AudioSource>().Play();
        Main_Particles.SetActive(false);
        yield return new WaitForSeconds(impact_Particle_Duration);

        impact_Particles.SetActive(false);
        Main_Particles.SetActive(true);
        go.SetActive(false);
    }
}


