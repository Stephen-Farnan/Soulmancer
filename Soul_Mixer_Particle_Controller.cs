using System.Collections;
using UnityEngine;

public class Soul_Mixer_Particle_Controller : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public GameObject player_Position;
    public Soul_Mixer_Manager local_Soul_Mixer_Manager;
    [Header("Properties TO Be Set")]
    public float distance_Between_Steps = .015f;
    public float speed = 12f;

    public enum soul_Mixer_Particle_Colour
    {
        PURPLE,
        GREEN,
        ORANGE
    }

    [Space]
    public soul_Mixer_Particle_Colour local_Soul_Mixer_Particle_Colour;
    #endregion

    void Start()
    {
        StartCoroutine("move_Towards_Player");
    }


    /// <summary>
    /// Starts the projectile moving towarsd the player over time, constantly updates the player position to them
    /// </summary>
    /// <returns></returns>
    IEnumerator move_Towards_Player()
    {
        while (true)
        {
            transform.LookAt(player_Position.transform);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player_Position.transform.position, speed * Time.deltaTime);
            yield return new WaitForSeconds(distance_Between_Steps);
        }
    }


    /// <summary>
    /// when it collides with the player, give the player ammo and then deactivate it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine("move_Towards_Player");
            local_Soul_Mixer_Manager.Give_Out_Colour((int)local_Soul_Mixer_Particle_Colour);
            gameObject.SetActive(false);
        }
    }
}
