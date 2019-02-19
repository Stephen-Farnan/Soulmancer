using System.Collections;
using UnityEngine;

public class Soul_Pickup_Manager : MonoBehaviour
{
    #region variables
    int value;
    float death_Effect_Duration = .05f;


    public enum Colour_Type
    {
        RED,
        BLUE,
        YELLOW,
        PURPLE,
        GREEN,
        ORANGE
    }
    [Header("References To Be Set")]
    public Player_Manager local_Player_Manager;


    public SphereCollider sphere_Collider;

    GameObject player_Gameobject;
    float distance_To_Player;


    [Header("Properties To Be Set")]
    public float move_Speed = .01f;
    public Colour_Type local_Colour_Type;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player_Gameobject = other.gameObject;
            StartCoroutine("Move_To_Player", value);
        }
    }

    public void On_Spawn()
    {
        sphere_Collider.radius = local_Player_Manager.player_Pickup_Radius;
    }

    IEnumerator destroy_Self()
    {
        yield return new WaitForSeconds(death_Effect_Duration);
        gameObject.SetActive(false);
    }

    void Pass_On_Values_To_Player(int v)
    {
        local_Player_Manager.Receive_Colours((int)local_Colour_Type, value);
    }


    /// <summary>
    /// moves the pickup to the player when the get within a certain range of it
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    IEnumerator Move_To_Player(int val)
    {
        while (true)
        {
            distance_To_Player = Vector3.Distance(transform.position, player_Gameobject.transform.position);
            if (distance_To_Player <= 1.5f)
            {
                Pass_On_Values_To_Player(val);

                StartCoroutine("destroy_Self");
                value = 0;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player_Gameobject.transform.position, move_Speed);
            }
            yield return new WaitForSeconds(0.0002f);
        }

    }

    /// <summary>
    /// takes in the number of souls to drop from the enemy, and the colour, to initialise the pickup object with
    /// </summary>
    /// <param name="value_Of_Souls_From_Monster">Amount of souls to drop</param>
    /// <param name="monster_Colour">Colour type of soul</param>
    public void SetValues(int value_Of_Souls_From_Monster, int monster_Colour)
    {
        value = value_Of_Souls_From_Monster;

    }
}
