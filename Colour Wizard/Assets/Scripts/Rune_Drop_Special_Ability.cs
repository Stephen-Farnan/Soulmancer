using UnityEngine;

public class Rune_Drop_Special_Ability : MonoBehaviour
{

    #region variables
    [Header("References To Be Set")]
    public GameObject[] red_Rune_Drop_Objects;
    public GameObject[] blue_Rune_Drop_Objects;
    public GameObject[] yellow_Rune_Drop_Objects;
    public GameObject[] purple_Rune_Drop_Objects;
    public GameObject[] green_Rune_Drop_Objects;
    public GameObject[] orange_Rune_Drop_Objects;
    public GameObject[] default_Rune_Drop_Objects;




    public Player_Manager local_Player_Manager;
    public Player_Input local_Player_Input;
    //  public UI_Manager local_UI_Manager;

    [HideInInspector]
    public int number_Of_Runes_Placed;
    int current_Red_Rune_Drop_Object = 0;
    int current_Blue_Rune_Drop_Object = 0;
    int current_Yellow_Rune_Drop_Object = 0;
    int current_Purple_Rune_Drop_Object = 0;
    int current_Green_Rune_Drop_Object = 0;
    int current_Orange_Rune_Drop_Object = 0;
    int current_Default_Rune_Drop_Object = 0;
    //int colour;

    [HideInInspector]
    public int damage_Per_Rune_Level = 1;
    [HideInInspector]
    public int slow_Amount_Level = 1;
    [HideInInspector]
    public int slow_Duration_Level = 1;
    [HideInInspector]
    public int rune_Blast_Radius_Level = 1;
    [HideInInspector]
    public int rune_Trigger_Radius_Level = 1;
    [HideInInspector]
    public int player_Rune_Capacity_Level = 1;
    //upgradeable stats

    [Header("Properties To Be Set")]
    public int damage_Per_Rune = 0;
    public float slow_Amount = 3;
    public float slow_Duration = 5;
    public float rune_Blast_Radius = 10f;
    public float rune_Trigger_Radius = 5f;
    public int player_Rune_Capacity = 5;

    Animator anim;

    private void Awake()
    {
        Initialise_Upgradeable_Stats();
        anim = local_Player_Manager.anim;
    }
    #endregion

    /// <summary>
    /// Initialises stats based on purchased upgrades
    /// </summary>
    void Initialise_Upgradeable_Stats()
    {
        damage_Per_Rune_Level = local_Player_Manager.rune_Drop_Ability_Level;
        slow_Amount_Level = local_Player_Manager.rune_Drop_Ability_Level;
        rune_Blast_Radius_Level = local_Player_Manager.rune_Drop_Ability_Level;
        rune_Trigger_Radius_Level = local_Player_Manager.rune_Drop_Ability_Level;
        player_Rune_Capacity_Level = local_Player_Manager.rune_Drop_Ability_Level;

        switch (damage_Per_Rune_Level)
        {
            case 2:
                break;

            case 3:
                damage_Per_Rune += 1;
                break;

            case 4:
                damage_Per_Rune += 3;
                break;

            case 5:
                damage_Per_Rune += 5;
                break;
        }

        switch (slow_Amount_Level)
        {
            case 2:
                slow_Amount += 1;
                break;

            case 3:
                slow_Amount += 2;
                break;

            case 4:
                slow_Amount += 3;
                break;

            case 5:
                slow_Amount += 4;
                break;

        }

        switch (slow_Duration_Level)
        {
            case 2:
                slow_Duration += 1f;
                break;

            case 3:
                slow_Duration += 2f;
                break;

            case 4:
                slow_Duration += 3f;
                break;

            case 5:
                slow_Duration += 4f;
                break;

        }

        switch (rune_Blast_Radius_Level)
        {
            case 2:
                rune_Blast_Radius += 2f;
                break;

            case 3:
                rune_Blast_Radius += 4f;
                break;

            case 4:
                rune_Blast_Radius += 6f;
                break;

            case 5:
                rune_Blast_Radius += 8f;
                break;
        }

        switch (rune_Trigger_Radius_Level)
        {
            case 2:
                rune_Trigger_Radius += 2f;
                break;

            case 3:
                rune_Trigger_Radius += 4f;
                break;

            case 4:
                rune_Trigger_Radius += 6f;
                break;

            case 5:
                rune_Trigger_Radius += 8f;
                break;

        }

        switch (player_Rune_Capacity_Level)
        {
            case 2:
                player_Rune_Capacity += 1;
                break;

            case 3:
                player_Rune_Capacity += 2;
                break;

            case 4:
                player_Rune_Capacity += 3;
                break;

            case 5:
                player_Rune_Capacity += 4;
                break;

        }


    }


    /// <summary>
    /// selects a rune from a certain array, based on player colour, calls for it to be initialised and then moves the index of the array on
    /// </summary>
    /// <param name="colour">Colour of rune used</param>
    public void Rune_Drop(int colour)
    {
        if (number_Of_Runes_Placed < player_Rune_Capacity)
        {
            //Setting animation trigger for ability
            anim.SetTrigger("Spell");
            local_Player_Manager.Rune_Sound.Play();
            local_Player_Input.StopCoroutine("Halt_Player");
            local_Player_Input.StartCoroutine("Halt_Player");
            switch ((int)local_Player_Manager.local_Selected_Colour)
            {
                case 0:
                    red_Rune_Drop_Objects[current_Red_Rune_Drop_Object].transform.position = new Vector3(gameObject.transform.position.x, red_Rune_Drop_Objects[current_Red_Rune_Drop_Object].transform.position.y, gameObject.transform.position.z);
                    red_Rune_Drop_Objects[current_Red_Rune_Drop_Object].SetActive(true);
                    red_Rune_Drop_Objects[current_Red_Rune_Drop_Object].GetComponent<Rune_Manager>().Set_Up_Rune(slow_Amount, damage_Per_Rune, rune_Blast_Radius, slow_Duration, colour, rune_Trigger_Radius);
                    number_Of_Runes_Placed++;
                    current_Red_Rune_Drop_Object++;
                    if (current_Red_Rune_Drop_Object > red_Rune_Drop_Objects.Length - 1)
                    {
                        current_Red_Rune_Drop_Object = 0;
                    }
                    break;

                case 1:
                    blue_Rune_Drop_Objects[current_Blue_Rune_Drop_Object].transform.position = new Vector3(gameObject.transform.position.x, blue_Rune_Drop_Objects[current_Blue_Rune_Drop_Object].transform.position.y, gameObject.transform.position.z);
                    blue_Rune_Drop_Objects[current_Blue_Rune_Drop_Object].SetActive(true);
                    blue_Rune_Drop_Objects[current_Blue_Rune_Drop_Object].GetComponent<Rune_Manager>().Set_Up_Rune(slow_Amount, damage_Per_Rune, rune_Blast_Radius, slow_Duration, colour, rune_Trigger_Radius);
                    number_Of_Runes_Placed++;
                    current_Blue_Rune_Drop_Object++;
                    if (current_Blue_Rune_Drop_Object > blue_Rune_Drop_Objects.Length - 1)
                    {
                        current_Blue_Rune_Drop_Object = 0;
                    }
                    break;

                case 2:
                    yellow_Rune_Drop_Objects[current_Yellow_Rune_Drop_Object].transform.position = new Vector3(gameObject.transform.position.x, yellow_Rune_Drop_Objects[current_Yellow_Rune_Drop_Object].transform.position.y, gameObject.transform.position.z);
                    yellow_Rune_Drop_Objects[current_Yellow_Rune_Drop_Object].SetActive(true);
                    yellow_Rune_Drop_Objects[current_Yellow_Rune_Drop_Object].GetComponent<Rune_Manager>().Set_Up_Rune(slow_Amount, damage_Per_Rune, rune_Blast_Radius, slow_Duration, colour, rune_Trigger_Radius);
                    number_Of_Runes_Placed++;
                    current_Yellow_Rune_Drop_Object++;
                    if (current_Yellow_Rune_Drop_Object > yellow_Rune_Drop_Objects.Length - 1)
                    {
                        current_Yellow_Rune_Drop_Object = 0;
                    }
                    break;

                case 3:
                    purple_Rune_Drop_Objects[current_Purple_Rune_Drop_Object].transform.position = new Vector3(gameObject.transform.position.x, purple_Rune_Drop_Objects[current_Purple_Rune_Drop_Object].transform.position.y, gameObject.transform.position.z);
                    purple_Rune_Drop_Objects[current_Purple_Rune_Drop_Object].SetActive(true);
                    purple_Rune_Drop_Objects[current_Purple_Rune_Drop_Object].GetComponent<Rune_Manager>().Set_Up_Rune(slow_Amount, damage_Per_Rune, rune_Blast_Radius, slow_Duration, colour, rune_Trigger_Radius);
                    number_Of_Runes_Placed++;
                    current_Purple_Rune_Drop_Object++;
                    if (current_Purple_Rune_Drop_Object > purple_Rune_Drop_Objects.Length - 1)
                    {
                        current_Purple_Rune_Drop_Object = 0;
                    }
                    break;

                case 4:
                    green_Rune_Drop_Objects[current_Green_Rune_Drop_Object].transform.position = new Vector3(gameObject.transform.position.x, green_Rune_Drop_Objects[current_Green_Rune_Drop_Object].transform.position.y, gameObject.transform.position.z);
                    green_Rune_Drop_Objects[current_Green_Rune_Drop_Object].SetActive(true);
                    green_Rune_Drop_Objects[current_Green_Rune_Drop_Object].GetComponent<Rune_Manager>().Set_Up_Rune(slow_Amount, damage_Per_Rune, rune_Blast_Radius, slow_Duration, colour, rune_Trigger_Radius);
                    number_Of_Runes_Placed++;
                    current_Green_Rune_Drop_Object++;
                    if (current_Green_Rune_Drop_Object > green_Rune_Drop_Objects.Length - 1)
                    {
                        current_Green_Rune_Drop_Object = 0;
                    }
                    break;

                case 5:
                    orange_Rune_Drop_Objects[current_Orange_Rune_Drop_Object].transform.position = new Vector3(gameObject.transform.position.x, orange_Rune_Drop_Objects[current_Orange_Rune_Drop_Object].transform.position.y, gameObject.transform.position.z);
                    orange_Rune_Drop_Objects[current_Orange_Rune_Drop_Object].SetActive(true);
                    orange_Rune_Drop_Objects[current_Orange_Rune_Drop_Object].GetComponent<Rune_Manager>().Set_Up_Rune(slow_Amount, damage_Per_Rune, rune_Blast_Radius, slow_Duration, colour, rune_Trigger_Radius);
                    number_Of_Runes_Placed++;
                    current_Orange_Rune_Drop_Object++;
                    if (current_Orange_Rune_Drop_Object > orange_Rune_Drop_Objects.Length - 1)
                    {
                        current_Orange_Rune_Drop_Object = 0;
                    }
                    break;

                case 6:
                    default_Rune_Drop_Objects[current_Default_Rune_Drop_Object].transform.position = new Vector3(gameObject.transform.position.x, default_Rune_Drop_Objects[current_Default_Rune_Drop_Object].transform.position.y, gameObject.transform.position.z);
                    default_Rune_Drop_Objects[current_Default_Rune_Drop_Object].SetActive(true);
                    default_Rune_Drop_Objects[current_Default_Rune_Drop_Object].GetComponent<Rune_Manager>().Set_Up_Rune(slow_Amount, damage_Per_Rune, rune_Blast_Radius, slow_Duration, colour, rune_Trigger_Radius);
                    number_Of_Runes_Placed++;
                    current_Default_Rune_Drop_Object++;
                    if (current_Default_Rune_Drop_Object > default_Rune_Drop_Objects.Length - 1)
                    {
                        current_Default_Rune_Drop_Object = 0;
                    }
                    break;

                default:
                    break;
            }

        }
    }
}
