using System.Collections;
using UnityEngine;

public class Player_Combat_Controller : MonoBehaviour


{
    #region variables
    [Header("References To Be Set")]
    public GameObject[] default_Attack_Projectiles = new GameObject[10];
    public GameObject[] red_Attack_Projectiles = new GameObject[10];
    public GameObject[] blue_Attack_Projectiles = new GameObject[10];
    public GameObject[] yellow_Attack_Projectiles = new GameObject[10];
    public GameObject[] purple_Attack_Projectiles = new GameObject[10];
    public GameObject[] green_Attack_Projectiles = new GameObject[10];
    public GameObject[] orange_Attack_Projectiles = new GameObject[10];
    [Space]
    public Player_Manager local_Player_Manager;
    public Transform player_Projectile_Origin;

    int current_Default_Projectile = 0;
    int current_Red_Projectile = 0;
    int current_Blue_Projectile = 0;
    int current_Yellow_Projectile = 0;
    int current_Purple_Projectile = 0;
    int current_Green_Projectile = 0;
    int current_Orange_Projectile = 0;
    [Header("Properties To Be Set")]
    public float projectile_Despawn_Time = 10f;
    public int player_Damage = 1;
    Vector3 TEMP_Mouse_Click_Position;
    #endregion


    /// <summary>
    /// takes in a mouse click and reduces the players ammo of that colour before firing a projectile
    /// </summary>
    /// <param name="hit"></param>
    public void Detect_Mouse_Click(RaycastHit hit)
    {

        if (local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.RED && local_Player_Manager.red_Colour_Amount > 0)
        {
            local_Player_Manager.red_Colour_Amount--;
            Fire_Spell(hit.point, 0);
        }
        else if (local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.BLUE && local_Player_Manager.blue_Colour_Amount > 0)
        {
            local_Player_Manager.blue_Colour_Amount--;
            Fire_Spell(hit.point, 1);
        }
        else if (local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.YELLOW && local_Player_Manager.yellow_Colour_Amount > 0)
        {
            local_Player_Manager.yellow_Colour_Amount--;
            Fire_Spell(hit.point, 2);
        }
        else if (local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.PURPLE && local_Player_Manager.purple_Colour_Amount > 0)
        {
            local_Player_Manager.purple_Colour_Amount--;
            Fire_Spell(hit.point, 3);
        }
        else if (local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.GREEN && local_Player_Manager.green_Colour_Amount > 0)
        {
            local_Player_Manager.green_Colour_Amount--;
            Fire_Spell(hit.point, 4);
        }
        else if (local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.ORANGE && local_Player_Manager.orange_Colour_Amount > 0)
        {
            local_Player_Manager.orange_Colour_Amount--;
            Fire_Spell(hit.point, 5);
        }
        else if (local_Player_Manager.local_Selected_Colour == Player_Manager.Selected_Colour.DEFAULT)
        {
            Fire_Spell(hit.point, 6);
        }

    }


    /// <summary>
    /// finds an appropriate projectile based on colour from an array and initialises it at the player before moving the array index of that array on
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="type_Of_Spell"></param>
    public void Fire_Spell(Vector3 temp, int type_Of_Spell)
    {
        temp = new Vector3(temp.x, player_Projectile_Origin.transform.position.y, temp.z);
        local_Player_Manager.Attack_Sound.Play();
        switch (type_Of_Spell)
        {


            case 0:

                red_Attack_Projectiles[current_Red_Projectile].transform.position = player_Projectile_Origin.transform.position;
                red_Attack_Projectiles[current_Red_Projectile].transform.LookAt(temp);
                red_Attack_Projectiles[current_Red_Projectile].SetActive(true);
                red_Attack_Projectiles[current_Red_Projectile].GetComponent<Projectile_Manager>().damage = player_Damage;
                red_Attack_Projectiles[current_Red_Projectile].GetComponent<Projectile_Manager>().On_Spawn();
                StartCoroutine("Destroy_Projectiles", red_Attack_Projectiles[current_Red_Projectile]);
                if (current_Red_Projectile < red_Attack_Projectiles.Length - 1)
                {
                    current_Red_Projectile++;
                }
                else
                {
                    current_Red_Projectile = 0;
                }
                break;

            case 1:
                blue_Attack_Projectiles[current_Blue_Projectile].transform.position = player_Projectile_Origin.transform.position;
                blue_Attack_Projectiles[current_Blue_Projectile].transform.LookAt(temp);
                blue_Attack_Projectiles[current_Blue_Projectile].SetActive(true);
                blue_Attack_Projectiles[current_Blue_Projectile].GetComponent<Projectile_Manager>().damage = player_Damage;
                blue_Attack_Projectiles[current_Blue_Projectile].GetComponent<Projectile_Manager>().On_Spawn();
                StartCoroutine("Destroy_Projectiles", blue_Attack_Projectiles[current_Blue_Projectile]);
                if (current_Blue_Projectile < blue_Attack_Projectiles.Length - 1)
                {
                    current_Blue_Projectile++;
                }
                else
                {
                    current_Blue_Projectile = 0;
                }
                break;

            case 2:
                yellow_Attack_Projectiles[current_Yellow_Projectile].transform.position = player_Projectile_Origin.transform.position;
                yellow_Attack_Projectiles[current_Yellow_Projectile].transform.LookAt(temp);
                yellow_Attack_Projectiles[current_Yellow_Projectile].SetActive(true);
                yellow_Attack_Projectiles[current_Yellow_Projectile].GetComponent<Projectile_Manager>().damage = player_Damage;
                yellow_Attack_Projectiles[current_Yellow_Projectile].GetComponent<Projectile_Manager>().On_Spawn();
                StartCoroutine("Destroy_Projectiles", yellow_Attack_Projectiles[current_Yellow_Projectile]);
                if (current_Yellow_Projectile < yellow_Attack_Projectiles.Length - 1)
                {
                    current_Yellow_Projectile++;
                }
                else
                {
                    current_Yellow_Projectile = 0;
                }
                break;

            case 3:
                purple_Attack_Projectiles[current_Purple_Projectile].transform.position = player_Projectile_Origin.transform.position;
                purple_Attack_Projectiles[current_Purple_Projectile].transform.LookAt(temp);
                purple_Attack_Projectiles[current_Purple_Projectile].SetActive(true);
                purple_Attack_Projectiles[current_Purple_Projectile].GetComponent<Projectile_Manager>().damage = player_Damage;
                purple_Attack_Projectiles[current_Purple_Projectile].GetComponent<Projectile_Manager>().On_Spawn();
                StartCoroutine("Destroy_Projectiles", purple_Attack_Projectiles[current_Purple_Projectile]);
                if (current_Purple_Projectile < purple_Attack_Projectiles.Length - 1)
                {
                    current_Purple_Projectile++;
                }
                else
                {
                    current_Purple_Projectile = 0;
                }
                break;

            case 4:
                green_Attack_Projectiles[current_Green_Projectile].transform.position = player_Projectile_Origin.transform.position;
                green_Attack_Projectiles[current_Green_Projectile].transform.LookAt(temp);
                green_Attack_Projectiles[current_Green_Projectile].SetActive(true);
                green_Attack_Projectiles[current_Green_Projectile].GetComponent<Projectile_Manager>().damage = player_Damage;
                green_Attack_Projectiles[current_Green_Projectile].GetComponent<Projectile_Manager>().On_Spawn();
                StartCoroutine("Destroy_Projectiles", green_Attack_Projectiles[current_Green_Projectile]);
                if (current_Green_Projectile < green_Attack_Projectiles.Length - 1)
                {
                    current_Green_Projectile++;
                }
                else
                {
                    current_Green_Projectile = 0;
                }
                break;

            case 5:
                orange_Attack_Projectiles[current_Orange_Projectile].transform.position = player_Projectile_Origin.transform.position;
                orange_Attack_Projectiles[current_Orange_Projectile].transform.LookAt(temp);
                orange_Attack_Projectiles[current_Orange_Projectile].SetActive(true);
                orange_Attack_Projectiles[current_Orange_Projectile].GetComponent<Projectile_Manager>().damage = player_Damage;
                orange_Attack_Projectiles[current_Orange_Projectile].GetComponent<Projectile_Manager>().On_Spawn();
                StartCoroutine("Destroy_Projectiles", orange_Attack_Projectiles[current_Orange_Projectile]);
                if (current_Orange_Projectile < orange_Attack_Projectiles.Length - 1)
                {
                    current_Orange_Projectile++;
                }
                else
                {
                    current_Orange_Projectile = 0;
                }
                break;

            case 6:
                default_Attack_Projectiles[current_Default_Projectile].transform.position = player_Projectile_Origin.transform.position;
                default_Attack_Projectiles[current_Default_Projectile].transform.LookAt(temp);
                default_Attack_Projectiles[current_Default_Projectile].SetActive(true);
                default_Attack_Projectiles[current_Default_Projectile].GetComponent<Projectile_Manager>().damage = player_Damage;
                default_Attack_Projectiles[current_Default_Projectile].GetComponent<Projectile_Manager>().On_Spawn();
                StartCoroutine("Destroy_Projectiles", default_Attack_Projectiles[current_Default_Projectile]);
                if (current_Default_Projectile < default_Attack_Projectiles.Length - 1)
                {
                    current_Default_Projectile++;
                }
                else
                {
                    current_Default_Projectile = 0;
                }
                break;
        }

    }


    /// <summary>
    /// deactivates the projectile after a certain period of time
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    private IEnumerator Destroy_Projectiles(GameObject go)
    {
        yield return new WaitForSeconds(projectile_Despawn_Time);
        go.SetActive(false);

    }
}
