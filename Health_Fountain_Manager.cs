using System.Collections;
using UnityEngine;

public class Health_Fountain_Manager : MonoBehaviour
{
    [Header("References To Be Set")]
    public Player_Manager local_Player_Manager;
    public UI_Manager local_UI_Manager;
    public GameObject particle_Effects_1;
    public GameObject particle_Effects_2;
    public AudioSource health_Shrine_Sound;
    [Header("Variables To Be Set")]
    public float cooldown = 20f;
    public int heal_Amount = 5;
    bool is_Avaliable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && is_Avaliable)
        {
            Heal_Player();
        }
    }

    /// <summary>
    /// Heals the player on contact
    /// </summary>
    void Heal_Player()
    {
        local_Player_Manager.health += heal_Amount;
        if (local_Player_Manager.health > local_Player_Manager.max_Health)
        {
            local_Player_Manager.health = local_Player_Manager.max_Health;
        }
        is_Avaliable = false;
        local_UI_Manager.health_Meter.value = local_Player_Manager.health;
        StartCoroutine("Wait_For_Cooldown");
        health_Shrine_Sound.Play();
        particle_Effects_1.SetActive(false);
        particle_Effects_2.SetActive(false);
    }

    IEnumerator Wait_For_Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        is_Avaliable = true;
        particle_Effects_1.SetActive(true);
        particle_Effects_2.SetActive(true);
    }
}
