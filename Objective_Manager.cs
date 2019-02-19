using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective_Manager : MonoBehaviour
{
    #region variables

    //[HideInInspector]
    public bool is_Destroyed;
    [Header("References To Be Set")]
    public Gameplay_Manager local_Gameplay_Manager;
    public GameObject[] Objective_Projectiles;
    public GameObject objective_Projectile_Origin;
    public Image health_Bar;
    [Header("Properties To Be Set")]
    public float attack_Speed = 3f;
    [HideInInspector]
    public Collider[] nearby_Enemies;
    [HideInInspector]
    public List<GameObject> valid_Enemies = new List<GameObject>(0);
    public float attack_Range = 20f;
    public int health = 500;
    public int damage_Per_Hit;
    bool has_Health_Bar;
    public bool is_Final_Objective;
    public Slider final_Final_Objective_Healthbar;
    float objective_Health_Multiplier;
    public AudioSource destroyed_Sound;
    public Material destroyed_Material;
    public GameObject crystals;
    public AudioSource attack_Sound;

    #endregion

    /// <summary>
    /// Calculates taking damage and checks for death status
    /// </summary>
    /// <param name="amount">Amount of damage to be taken</param>
    public void Take_Damage(int amount)
    {

        if (!is_Destroyed)
        {
            if (!is_Final_Objective)
            {
                if (has_Health_Bar)
                {
                    health -= amount;
                    if (health < 0)
                    {
                        health = 0;
                    }
                    health_Bar.fillAmount = health * objective_Health_Multiplier;
                }
            }

            else
            {
                if (!final_Final_Objective_Healthbar.gameObject.activeInHierarchy)
                {
                    final_Final_Objective_Healthbar.gameObject.SetActive(true);
                }

                health -= amount;
                if (health < 0)
                {
                    health = 0;
                }
                final_Final_Objective_Healthbar.value = health;
            }


            if (health <= 0)
            {
                is_Destroyed = true;
                destroyed_Sound.Play();
                crystals.GetComponent<Renderer>().material = destroyed_Material;
                StopAllCoroutines();
            }
        }

    }

    private void Start()
    {
        StartCoroutine("Refresh_Nearby_Enemies");
        if (health_Bar != null)
        {
            objective_Health_Multiplier = 1.0f / health;
            health_Bar.fillAmount = health * objective_Health_Multiplier;
            has_Health_Bar = true;
        }

        if (is_Final_Objective)
        {
            final_Final_Objective_Healthbar.maxValue = health;
            final_Final_Objective_Healthbar.value = health;
        }

    }

    IEnumerator Refresh_Nearby_Enemies()
    {
        while (true)
        {

            Refresh_Enemies();
            yield return new WaitForSeconds(attack_Speed);
        }

    }

    /// <summary>
    /// Creates an array of nearby enemies that can be attacked
    /// </summary>
    void Refresh_Enemies()
    {
        nearby_Enemies = Physics.OverlapSphere(transform.position, attack_Range);
        foreach (Collider co in nearby_Enemies)
        {
            if (co.gameObject.tag == "Enemy")
            {
                valid_Enemies.Add(co.gameObject);

            }
        }


        for (int i = 0; i <= valid_Enemies.Count - 1; i++)
        {

            if (valid_Enemies != null)
            {
                Fire_Projectile(valid_Enemies[0]);
                System.Array.Clear(nearby_Enemies, 0, nearby_Enemies.Length);
                break;

            }

        }

        valid_Enemies.Clear();
    }

    /// <summary>
    /// Fires a projectile at an enemy
    /// </summary>
    /// <param name="go">Gameobject to be targeted</param>
    public void Fire_Projectile(GameObject go)
    {

        attack_Sound.Play();
        Objective_Projectiles[local_Gameplay_Manager.current_Objective_Projectiles].transform.position = objective_Projectile_Origin.transform.position;
        Objective_Projectiles[local_Gameplay_Manager.current_Objective_Projectiles].transform.LookAt(go.transform);
        Objective_Projectiles[local_Gameplay_Manager.current_Objective_Projectiles].SetActive(true);
        Objective_Projectiles[local_Gameplay_Manager.current_Objective_Projectiles].GetComponent<Objective_Projectile_Controller>().Set_Damage(damage_Per_Hit);
        Objective_Projectiles[local_Gameplay_Manager.current_Objective_Projectiles].GetComponent<Objective_Projectile_Controller>().On_Spawn();
        if (local_Gameplay_Manager.current_Objective_Projectiles < Objective_Projectiles.Length - 1)
        {
            local_Gameplay_Manager.current_Objective_Projectiles++;
        }
        else
        {
            local_Gameplay_Manager.current_Objective_Projectiles = 0;
        }
    }
}
