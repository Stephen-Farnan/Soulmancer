using System.Collections;
using UnityEngine;

public class Soul_Mixer_Manager : MonoBehaviour
{

    #region variables
    [Header("References To Be Set")]
    public GameObject[] purple_Soul_Projectiles = new GameObject[10];
    public GameObject[] green_Soul_Projectiles = new GameObject[10];
    public GameObject[] orange_Soul_Projectiles = new GameObject[10];
    [Space]
    public Transform soul_Mixer_Particle_Origin;
    public GameObject player_Position;
    public Player_Manager local_Player_Manager;
    public Player_Combat_Controller local_Player_Combat_Controller;
    public AudioSource mixing_Sound;
    public int soul_Amount_Needed_To_Put_In = 5;
    public int soul_Amount_To_Get_Back = 20;
    int current_Purple_Projectile = 0;
    int current_Green_Projectile = 0;
    int current_Orange_Projectile = 0;
    bool contains_Red_Colour = false;
    bool contains_Blue_Colour = false;
    bool contains_Yellow_Colour = false;
    [Header("Properties To Be Set")]
    public float mixing_Time = 1.5f;

    #endregion


    /// <summary>
    ///     Takes in a colour based on the players selected colour, subtracts an amout from the palyer, and then checks if there is another colour stored in it already
    ///     if there is, it gives the player a colour that is a mix of the two colour and updates their ammo for that colour
    /// </summary>
    public void Take_In_Colour()
    {
        if ((local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.RED && local_Player_Manager.red_Colour_Amount >= soul_Amount_Needed_To_Put_In) && !contains_Red_Colour)
        {
            local_Player_Combat_Controller.Fire_Spell(gameObject.transform.position, 0);
            local_Player_Manager.red_Colour_Amount -= soul_Amount_Needed_To_Put_In;
            contains_Red_Colour = true;
        }
        else if ((local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.BLUE && local_Player_Manager.blue_Colour_Amount >= soul_Amount_Needed_To_Put_In) && !contains_Blue_Colour)
        {
            local_Player_Combat_Controller.Fire_Spell(gameObject.transform.position, 1);
            local_Player_Manager.blue_Colour_Amount -= soul_Amount_Needed_To_Put_In;
            contains_Blue_Colour = true;
        }
        else if ((local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.YELLOW && local_Player_Manager.yellow_Colour_Amount >= soul_Amount_Needed_To_Put_In) && !contains_Yellow_Colour)
        {
            local_Player_Combat_Controller.Fire_Spell(gameObject.transform.position, 2);
            local_Player_Manager.yellow_Colour_Amount -= soul_Amount_Needed_To_Put_In;
            contains_Yellow_Colour = true;
        }

        if (contains_Red_Colour && contains_Blue_Colour)
        {
            StartCoroutine("Fire_Colour_At_Player", 0);
            mixing_Sound.Play();
        }


        if (contains_Blue_Colour && contains_Yellow_Colour)
        {
            StartCoroutine("Fire_Colour_At_Player", 1);
            mixing_Sound.Play();
        }

        if (contains_Red_Colour && contains_Yellow_Colour)
        {
            StartCoroutine("Fire_Colour_At_Player", 2);
            mixing_Sound.Play();
        }
    }


    /// <summary>
    /// updating the player ammo counts
    /// </summary>
    /// <param name="colour_Type">Colour to be given to player</param>
    public void Give_Out_Colour(int colour_Type)
    {
        switch (colour_Type)
        {
            case 0:
                contains_Red_Colour = false;
                contains_Blue_Colour = false;
                local_Player_Manager.purple_Colour_Amount += soul_Amount_To_Get_Back;
                break;

            case 1:

                contains_Blue_Colour = false;
                contains_Yellow_Colour = false;
                local_Player_Manager.green_Colour_Amount += soul_Amount_To_Get_Back;
                break;

            case 2:
                contains_Red_Colour = false;
                contains_Yellow_Colour = false;
                local_Player_Manager.orange_Colour_Amount += soul_Amount_To_Get_Back;
                break;
        }
    }


    /// <summary>
    /// chooses an appropriate projectile from the array and initialises it, before moving the array index on
    /// </summary>
    /// <param name="colour"></param>
    /// <returns></returns>
    public IEnumerator Fire_Colour_At_Player(int colour)
    {
        yield return new WaitForSeconds(mixing_Time);
        switch (colour)
        {
            case 0:
                purple_Soul_Projectiles[current_Purple_Projectile].transform.position = soul_Mixer_Particle_Origin.position;
                purple_Soul_Projectiles[current_Purple_Projectile].SetActive(true);
                purple_Soul_Projectiles[current_Purple_Projectile].GetComponent<Soul_Mixer_Particle_Controller>().local_Soul_Mixer_Manager = gameObject.GetComponent<Soul_Mixer_Manager>();
                purple_Soul_Projectiles[current_Purple_Projectile].GetComponent<Soul_Mixer_Particle_Controller>().StartCoroutine("move_Towards_Player");
                if (current_Purple_Projectile < purple_Soul_Projectiles.Length - 1)
                {
                    current_Purple_Projectile++;
                }
                else
                {
                    current_Purple_Projectile = 0;
                }
                break;

            case 1:
                green_Soul_Projectiles[current_Green_Projectile].transform.position = soul_Mixer_Particle_Origin.position;
                green_Soul_Projectiles[current_Green_Projectile].SetActive(true);
                green_Soul_Projectiles[current_Green_Projectile].GetComponent<Soul_Mixer_Particle_Controller>().local_Soul_Mixer_Manager = gameObject.GetComponent<Soul_Mixer_Manager>();
                green_Soul_Projectiles[current_Green_Projectile].GetComponent<Soul_Mixer_Particle_Controller>().StartCoroutine("move_Towards_Player");
                if (current_Green_Projectile < green_Soul_Projectiles.Length - 1)
                {
                    current_Green_Projectile++;
                }
                else
                {
                    current_Green_Projectile = 0;
                }
                break;

            case 2:
                orange_Soul_Projectiles[current_Orange_Projectile].transform.position = soul_Mixer_Particle_Origin.position;
                orange_Soul_Projectiles[current_Orange_Projectile].SetActive(true);
                orange_Soul_Projectiles[current_Orange_Projectile].GetComponent<Soul_Mixer_Particle_Controller>().local_Soul_Mixer_Manager = gameObject.GetComponent<Soul_Mixer_Manager>();
                orange_Soul_Projectiles[current_Orange_Projectile].GetComponent<Soul_Mixer_Particle_Controller>().StartCoroutine("move_Towards_Player");
                if (current_Orange_Projectile < orange_Soul_Projectiles.Length - 1)
                {
                    current_Orange_Projectile++;
                }
                else
                {
                    current_Orange_Projectile = 0;
                }
                break;


        }
    }

}
