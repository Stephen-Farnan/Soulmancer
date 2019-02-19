using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay_Manager : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public GameObject[] Red_Enemies;
    public GameObject[] Blue_Enemies;
    public GameObject[] Yellow_Enemies;
    public GameObject[] Purple_Enemies;
    public GameObject[] Green_Enemies;
    public GameObject[] Orange_Enemies;
    public GameObject[] red_soul_Drops;
    public GameObject[] blue_soul_Drops;
    public GameObject[] yellow_soul_Drops;
    public GameObject[] purple_soul_Drops;
    public GameObject[] green_soul_Drops;
    public GameObject[] orange_soul_Drops;
    public GameObject[] spawn_Locations;
    public GameObject[] objective_GameObjects;
    public GameObject final_Objective;
    public GameObject cursor_Image;

    public Player_Saved_Data_Manager local_Player_Saved_Data_Manager;
    public UI_Manager local_UI_Manager;
    public Player_Input local_Player_Input;
    public Player_Movement_Controls local_Player_Movement_Controls;
    public GameObject player_Gameobjects;
    public GameObject enemy_Gameobjects;
    public GameObject soul_Mixer_Gameobjects;
    public GameObject level_Music;
    public AudioSource boss_Music;

    [Header("Set Only If Boss Enemy In Level")]
    public Enemy_Boss_Manager local_Enemy_Boss_Manager;
    public GameObject boss_Gameobject;

    [HideInInspector]
    public int current_Objective_Projectiles = 0;
    //GameObject current_Objective;

    int red_Current_Soul_Drop = 0;
    int blue_Current_Soul_Drop = 0;
    int yellow_Current_Soul_Drop = 0;
    int purple_Current_Soul_Drop = 0;
    int green_Current_Soul_Drop = 0;
    int orange_Current_Soul_Drop = 0;

    int current_Red_Enemy = 0;
    int current_Blue_Enemy = 0;
    int current_Yellow_Enemy = 0;
    int current_Purple_Enemy = 0;
    int current_Green_Enemy = 0;
    int current_Orange_Enemy = 0;
    int number_Of_Red_Enemies = 0;
    int number_Of_Blue_Enemies = 0;
    int number_Of_Yellow_Enemies = 0;
    int number_Of_Purple_Enemies = 0;
    int number_Of_Green_Enemies = 0;
    int number_Of_Orange_Enemies = 0;
    int random_Number;
    int random_Alt_Number;
    int total_Number_Of_Enemies;
    int total_Number_Of_Enemies_Left_To_Spawn;
    int special_Enemy_Start_Point;
    int number_Of_Special_Enemies;
    int enemies_Remaining;
    //int current_Active_Objective_Index;
    int number_Of_Objectives;
    int enemies_Left_On_Current_Wave;
    int gold_Earned_From_Combo = 0;

    [Header("Set Which Colours Cannot Spawn At The Start")]

    public bool red_Enemies_All_Spawned;
    public bool blue_Enemies_All_Spawned;
    public bool yellow_Enemies_All_Spawned;
    public bool purple_Enemies_All_Spawned;
    public bool green_Enemies_All_Spawned;
    public bool orange_Enemies_All_Spawned;

    bool objectives_Intact;
    bool waves_Can_Spawn = true;
    int enemies_Spawned_This_Wave;
    bool level_Passed;
    bool level_Failed;

    int current_Combo_Count = 0;
    int max_Combo_Count = 0;
    int current_Time_Bonus_Text = 0;
    int current_Combo_Bonus_Text = 0;
    int current_Bonus_Objective_Text = 0;
    int current_Total_Gold_text = 0;
    string bonus_Objective = "";

    [Space]
    [Header("Wave Properties To Be Set")]
    public float time_To_Wait_Before_Starting_Level = 5f;
    public float spawn_Rate = 2f;
    public float special_Enemy_Spawn_Rate = 10f;
    public int enemies_Spawned_Until_Special_Enemies = 10;
    public float time_Between_Waves = 5f;
    public int enemies_Per_Wave;
    public int number_To_Increase_Enemies_Per_Waves_By;
    public int wave_Number_For_Red_Enemies_To_Spawn_On = 0;
    public int wave_Number_For_Blue_Enemies_To_Spawn_On = 0;
    public int wave_Number_For_Yellow_Enemies_To_Spawn_On = 0;
    public int wave_Number_For_Purple_Enemies_To_Spawn_On = 0;
    public int wave_Number_For_Green_Enemies_To_Spawn_On = 0;
    public int wave_Number_For_Orange_Enemies_To_Spawn_On = 0;
    public int number_Of_Waves = 6;
    //int current_Wave_NUmber = 1;


    [Header("End Of Level Properties To Be Set")]
    public int gold_For_Level_Completion = 100;
    public int gold_Bonus_Amount = 50;
    public float time_Target;
    [HideInInspector]
    public int time_Bonus;

    [HideInInspector]
    public float time_Taken;


    [HideInInspector]
    public int current_Wave_Number = 1;


    bool bonus_Objective_Met;

    float start_Time;

    [HideInInspector]
    public int slowed_Enemies_Killed = 0;
    [Header("Set Targets For Bonus Objectives")]
    public int target_Number_Of_Slowed_Enemies_To_Kill = 15;
    public int number_Of_Enemies_To_Hit_At_Once = 10;
    [HideInInspector]
    public bool enemies_Hit_At_Once = false;
    [HideInInspector]
    public int number_Of_Correct_Colour_Blocked = 0;
    public int target_Number_Of_Enemies_To_Block = 10;

    [Header("Set If The Level Contains A Tutorial Section or Boss")]
    public bool boss_Level;
    public bool tutorial_Level;
    public enum Objective_Type
    {
        LOSE_NO_TOTEMS,
        KILL_SLOWED_ENEMIES,
        BLOCK_COLOURED_ATTACKS,
        HIT_A_GROUP_OF_ENEMIES_AT_ONCE

    }
    [HideInInspector]
    public Objective_Type local_Objective_Type;

    #endregion


    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        start_Time = Time.time;
        number_Of_Red_Enemies = Red_Enemies.Length;
        number_Of_Blue_Enemies = Blue_Enemies.Length;
        number_Of_Yellow_Enemies = Yellow_Enemies.Length;
        number_Of_Purple_Enemies = Purple_Enemies.Length;
        number_Of_Green_Enemies = Green_Enemies.Length;
        number_Of_Orange_Enemies = Orange_Enemies.Length;
        total_Number_Of_Enemies_Left_To_Spawn = number_Of_Red_Enemies + number_Of_Blue_Enemies + number_Of_Yellow_Enemies + number_Of_Purple_Enemies + number_Of_Green_Enemies + number_Of_Orange_Enemies;
        number_Of_Special_Enemies = number_Of_Purple_Enemies + number_Of_Green_Enemies + number_Of_Orange_Enemies;
        total_Number_Of_Enemies = total_Number_Of_Enemies_Left_To_Spawn;
        enemies_Remaining = total_Number_Of_Enemies_Left_To_Spawn;
        special_Enemy_Start_Point = total_Number_Of_Enemies_Left_To_Spawn - enemies_Spawned_Until_Special_Enemies;
        local_UI_Manager.current_Combo.text = current_Combo_Count.ToString();
        //current_Objective = objective_GameObjects[current_Active_Objective_Index].gameObject;
        enemies_Left_On_Current_Wave = enemies_Per_Wave;

        int random_Objective = Random.Range(0, 4);

        switch (random_Objective)
        {
            case 0:
                local_Objective_Type = Objective_Type.BLOCK_COLOURED_ATTACKS;
                bonus_Objective = "Block " + target_Number_Of_Enemies_To_Block + " attacks with the same colour shield";
                break;

            case 1:
                local_Objective_Type = Objective_Type.KILL_SLOWED_ENEMIES;
                bonus_Objective = "Kill " + target_Number_Of_Slowed_Enemies_To_Kill + " slowed enemies";
                break;

            case 2:
                local_Objective_Type = Objective_Type.LOSE_NO_TOTEMS;
                bonus_Objective = "Do not let any totems be destroyed";
                break;

            case 3:
                local_Objective_Type = Objective_Type.HIT_A_GROUP_OF_ENEMIES_AT_ONCE;
                bonus_Objective = "Hit " + number_Of_Enemies_To_Hit_At_Once + " enemies at once with your AOE attack";
                break;
        }

        local_UI_Manager.Bonus_Objective_Text.text = bonus_Objective;

        if (!tutorial_Level)
        {
            StartCoroutine("Delay_Start_Of_Level");
        }

        else
        {
            local_UI_Manager.Display_Current_Level("Level " + SceneManager.GetActiveScene().buildIndex);
            StartCoroutine("Remove_Tutorial_Heading");
        }





        // StartCoroutine("Change_Between_Objectives");

        if (number_Of_Purple_Enemies <= 0)
        {
            purple_Enemies_All_Spawned = true;
        }
        if (number_Of_Green_Enemies <= 0)
        {
            green_Enemies_All_Spawned = true;
        }
        if (number_Of_Orange_Enemies <= 0)
        {
            orange_Enemies_All_Spawned = true;
        }
    }

    IEnumerator Remove_Tutorial_Heading()
    {
        yield return new WaitForSeconds(time_To_Wait_Before_Starting_Level);
        local_UI_Manager.Hide_Current_Level();

    }

    IEnumerator Delay_Start_Of_Level()
    {
        if (!tutorial_Level)
        {
            local_UI_Manager.Display_Current_Level("Level " + SceneManager.GetActiveScene().buildIndex);
            local_UI_Manager.Display_Bonus_Objective();
        }

        yield return new WaitForSeconds(time_To_Wait_Before_Starting_Level);
        local_UI_Manager.Hide_Bonus_Objective();
        local_UI_Manager.Hide_Current_Level();
        if (tutorial_Level)
        {
            local_UI_Manager.Display_Bonus_Objective();
        }
        StartCoroutine("Pick_Enemy_Type_To_Spawn");
        local_UI_Manager.Display_Current_Wave("Wave " + current_Wave_Number);
        yield return new WaitForSeconds(5f);
        local_UI_Manager.Hide_Current_Wave();
        if (tutorial_Level)
        {
            local_UI_Manager.Hide_Bonus_Objective();
        }
    }

    public void Add_To_Combo_Count()
    {
        current_Combo_Count++;
        if (current_Combo_Count > max_Combo_Count)
        {
            max_Combo_Count = current_Combo_Count;

        }
        local_UI_Manager.current_Combo.text = current_Combo_Count.ToString();
    }

    public void Reset_Combo_Count()
    {
        current_Combo_Count = 0;
        local_UI_Manager.current_Combo.text = current_Combo_Count.ToString();
    }

    public int Get_Current_Combo()
    {
        return current_Combo_Count;
    }

    public int Get_Max_Combo()
    {
        return max_Combo_Count;
    }

    //gets the next soul drop in the array
    public GameObject Get_Soul_Drop(int colour)
    {
        switch (colour)
        {
            case 0:

                if (red_Current_Soul_Drop <= red_soul_Drops.Length - 2)
                {
                    red_Current_Soul_Drop++;
                }
                else
                {
                    red_Current_Soul_Drop = 0;
                }

                return red_soul_Drops[red_Current_Soul_Drop];


            case 1:

                if (blue_Current_Soul_Drop <= blue_soul_Drops.Length - 2)
                {
                    blue_Current_Soul_Drop++;
                }
                else
                {
                    blue_Current_Soul_Drop = 0;
                }

                return blue_soul_Drops[blue_Current_Soul_Drop];

            case 2:

                if (yellow_Current_Soul_Drop <= yellow_soul_Drops.Length - 2)
                {
                    yellow_Current_Soul_Drop++;
                }
                else
                {
                    yellow_Current_Soul_Drop = 0;
                }

                return yellow_soul_Drops[yellow_Current_Soul_Drop];

            case 3:

                if (purple_Current_Soul_Drop <= purple_soul_Drops.Length - 2)
                {
                    purple_Current_Soul_Drop++;
                }
                else
                {
                    purple_Current_Soul_Drop = 0;
                }

                return purple_soul_Drops[purple_Current_Soul_Drop];

            case 4:

                if (green_Current_Soul_Drop <= green_soul_Drops.Length - 2)
                {
                    green_Current_Soul_Drop++;
                }
                else
                {
                    green_Current_Soul_Drop = 0;
                }

                return green_soul_Drops[green_Current_Soul_Drop];

            case 5:

                if (orange_Current_Soul_Drop <= orange_soul_Drops.Length - 2)
                {
                    orange_Current_Soul_Drop++;
                }
                else
                {
                    orange_Current_Soul_Drop = 0;
                }

                return orange_soul_Drops[orange_Current_Soul_Drop];

            default:
                if (red_Current_Soul_Drop <= red_soul_Drops.Length - 2)
                {
                    red_Current_Soul_Drop++;
                }
                else
                {
                    red_Current_Soul_Drop = 0;
                }
                return red_soul_Drops[red_Current_Soul_Drop]; ;
        }


    }



    //public GameObject Get_Current_Objective()
    //{
    //return current_Objective;
    // }

    //Starts to look for the next enemy to spawn, repeats over time
    IEnumerator Pick_Enemy_Type_To_Spawn()
    {
        while (true)
        {

            Pick_Enemy_Type_To_Spawn_Function();

            yield return new WaitForSeconds(spawn_Rate);
        }
    }

    //checks if the current wave has been fully spawned or not
    void Pick_Enemy_Type_To_Spawn_Function()
    {
        if (waves_Can_Spawn)
        {
            Pick_Enemy();
        }
    }

    //Waits for a period of time before allowing a new wave to spawn

    IEnumerator Wait_Between_Waves()
    {
        yield return new WaitForSeconds(time_Between_Waves);
        Wait_Between_Waves_Fucntion();
        yield return new WaitForSeconds(5f);
        local_UI_Manager.Hide_Current_Wave();
    }



    void Wait_Between_Waves_Fucntion()
    {
        enemies_Per_Wave += number_To_Increase_Enemies_Per_Waves_By;
        enemies_Spawned_This_Wave = 0;
        enemies_Left_On_Current_Wave = enemies_Per_Wave;
        current_Wave_Number++;
        if ((wave_Number_For_Red_Enemies_To_Spawn_On == current_Wave_Number) && number_Of_Red_Enemies > 0)
        {
            red_Enemies_All_Spawned = false;
        }
        if ((wave_Number_For_Blue_Enemies_To_Spawn_On == current_Wave_Number) && number_Of_Blue_Enemies > 0)
        {
            blue_Enemies_All_Spawned = false;
        }
        if ((wave_Number_For_Yellow_Enemies_To_Spawn_On == current_Wave_Number) && number_Of_Yellow_Enemies > 0)
        {
            yellow_Enemies_All_Spawned = false;
        }
        if ((wave_Number_For_Purple_Enemies_To_Spawn_On == current_Wave_Number) && number_Of_Purple_Enemies > 0)
        {
            purple_Enemies_All_Spawned = false;
        }
        if ((wave_Number_For_Green_Enemies_To_Spawn_On == current_Wave_Number) && number_Of_Green_Enemies > 0)
        {
            green_Enemies_All_Spawned = false;
        }
        if ((wave_Number_For_Orange_Enemies_To_Spawn_On == current_Wave_Number) && number_Of_Orange_Enemies > 0)
        {
            orange_Enemies_All_Spawned = false;
        }


        waves_Can_Spawn = true;
        local_UI_Manager.Display_Current_Wave("Wave " + current_Wave_Number);
    }

    //Chooses the next enemy to spawn
    public void Pick_Enemy()
    {

        //checks if criteria is met to spawn a special enemy
        if (((total_Number_Of_Enemies_Left_To_Spawn % special_Enemy_Spawn_Rate) == 0) && (total_Number_Of_Enemies_Left_To_Spawn <= special_Enemy_Start_Point) && (number_Of_Special_Enemies > 0) && total_Number_Of_Enemies_Left_To_Spawn > 0 && (!purple_Enemies_All_Spawned || !green_Enemies_All_Spawned || !orange_Enemies_All_Spawned))
        {
            int max_Num = 3;
            if (orange_Enemies_All_Spawned)
            {
                max_Num = 2;
            }

            if (green_Enemies_All_Spawned)
            {
                max_Num = 1;
            }
            bool alt_Number_Found = false;
            random_Alt_Number = Random.Range(0, max_Num);
            do
            {
                if ((!purple_Enemies_All_Spawned) && (random_Alt_Number == 0))
                {
                    alt_Number_Found = true;
                    Spawn_Enemies(3);
                    Decrease_Enemy_Count(3);
                }

                else if ((!green_Enemies_All_Spawned) && (random_Alt_Number == 1))
                {
                    alt_Number_Found = true;
                    Spawn_Enemies(4);
                    Decrease_Enemy_Count(4);
                }
                else if ((!orange_Enemies_All_Spawned) && (random_Alt_Number == 2))
                {
                    alt_Number_Found = true;
                    Spawn_Enemies(5);
                    Decrease_Enemy_Count(5);
                }
                else if (purple_Enemies_All_Spawned && green_Enemies_All_Spawned && orange_Enemies_All_Spawned)
                {
                    alt_Number_Found = true;
                    break;
                }

                else
                {
                    random_Alt_Number = Random.Range(0, 3);
                }


            } while (!alt_Number_Found);

            number_Of_Special_Enemies--;
        }

        //otherwise spawns a random basic enemy
        else if (total_Number_Of_Enemies_Left_To_Spawn > 0)
        {
            int max = 3;
            if (yellow_Enemies_All_Spawned)
            {
                max = 2;
            }

            if (blue_Enemies_All_Spawned && yellow_Enemies_All_Spawned)
            {
                max = 1;
            }
            bool number_Found = false;
            random_Number = Random.Range(0, max);
            do
            {
                if (!red_Enemies_All_Spawned && random_Number == 0)
                {
                    number_Found = true;
                    Spawn_Enemies(0);
                    Decrease_Enemy_Count(0);
                }
                else if (!blue_Enemies_All_Spawned && random_Number == 1)
                {
                    number_Found = true;
                    Spawn_Enemies(1);
                    Decrease_Enemy_Count(1);
                }
                else if (!yellow_Enemies_All_Spawned && random_Number == 2)
                {
                    number_Found = true;
                    Spawn_Enemies(2);
                    Decrease_Enemy_Count(2);
                }
                else if (yellow_Enemies_All_Spawned && red_Enemies_All_Spawned && blue_Enemies_All_Spawned)
                {
                    number_Found = true;
                    break;
                }
                else
                {
                    random_Number = Random.Range(0, 3);
                }

            } while (!number_Found);

        }

        //if all enemies have been spawned, stop calling the spawner
        if (total_Number_Of_Enemies_Left_To_Spawn <= 0)
        {
            StopCoroutine("Pick_Enemy_Type_To_Spawn");

            return;
        }

        total_Number_Of_Enemies_Left_To_Spawn--;

        enemies_Spawned_This_Wave++;
        if ((enemies_Spawned_This_Wave) >= enemies_Per_Wave)
        {
            waves_Can_Spawn = false;

            // StartCoroutine("Wait_Between_Waves");

        }


    }

    //Once an enemy has been picked, this function picks an appropriate enemy from the array and sets them active, before moving the array index on
    public void Spawn_Enemies(int enemy_Type)
    {
        int rand_Spawn = Random.Range(0, 5);
        bool enemy_Found = false;


        switch (enemy_Type)
        {
            case 0:

                do
                {
                    if (Red_Enemies[current_Red_Enemy].gameObject.activeSelf == false)
                    {
                        Red_Enemies[current_Red_Enemy].gameObject.transform.position = spawn_Locations[rand_Spawn].transform.position;
                        Red_Enemies[current_Red_Enemy].gameObject.SetActive(true);
                        Red_Enemies[current_Red_Enemy].gameObject.GetComponent<Base_Enemy>().On_Spawn();

                        Red_Enemies[current_Red_Enemy].gameObject.GetComponent<Enemy_AI>().On_Spawn();

                        enemy_Found = true;
                        if (current_Red_Enemy < Red_Enemies.Length - 1)
                        {
                            current_Red_Enemy++;
                        }
                        else
                        {
                            current_Red_Enemy = 0;
                        }
                    }
                    else
                    {
                        if (current_Red_Enemy < Red_Enemies.Length - 1)
                        {
                            current_Red_Enemy++;
                        }
                        else
                        {
                            current_Red_Enemy = 0;
                        }
                    }
                } while (!enemy_Found);


                break;

            case 1:

                do
                {

                    if (Blue_Enemies[current_Blue_Enemy].gameObject.activeSelf == false)
                    {
                        Blue_Enemies[current_Blue_Enemy].gameObject.transform.position = spawn_Locations[rand_Spawn].transform.position;
                        Blue_Enemies[current_Blue_Enemy].gameObject.SetActive(true);
                        Blue_Enemies[current_Blue_Enemy].gameObject.GetComponent<Base_Enemy>().On_Spawn();

                        Blue_Enemies[current_Blue_Enemy].gameObject.GetComponent<Enemy_AI>().On_Spawn();

                        enemy_Found = true;
                        if (current_Blue_Enemy < Blue_Enemies.Length - 1)
                        {
                            current_Blue_Enemy++;
                        }
                        else
                        {
                            current_Blue_Enemy = 0;
                        }
                    }
                    else
                    {
                        if (current_Blue_Enemy < Blue_Enemies.Length - 1)
                        {
                            current_Blue_Enemy++;
                        }
                        else
                        {
                            current_Blue_Enemy = 0;
                        }
                    }
                } while (!enemy_Found);

                break;

            case 2:

                do
                {
                    if (Yellow_Enemies[current_Yellow_Enemy].gameObject.activeSelf == false)
                    {
                        Yellow_Enemies[current_Yellow_Enemy].gameObject.transform.position = spawn_Locations[rand_Spawn].transform.position;
                        Yellow_Enemies[current_Yellow_Enemy].gameObject.SetActive(true);
                        Yellow_Enemies[current_Yellow_Enemy].gameObject.GetComponent<Base_Enemy>().On_Spawn();

                        Yellow_Enemies[current_Yellow_Enemy].gameObject.GetComponent<Enemy_AI>().On_Spawn();

                        enemy_Found = true;
                        if (current_Yellow_Enemy < Yellow_Enemies.Length - 1)
                        {
                            current_Yellow_Enemy++;
                        }
                        else
                        {
                            current_Yellow_Enemy = 0;
                        }
                    }
                    else
                    {
                        if (current_Yellow_Enemy < Yellow_Enemies.Length - 1)
                        {
                            current_Yellow_Enemy++;
                        }
                        else
                        {
                            current_Yellow_Enemy = 0;
                        }
                    }
                } while (!enemy_Found);

                break;

            case 3:

                do
                {
                    if (Purple_Enemies[current_Purple_Enemy].gameObject.activeSelf == false)
                    {
                        Purple_Enemies[current_Purple_Enemy].gameObject.transform.position = spawn_Locations[rand_Spawn].transform.position;
                        Purple_Enemies[current_Purple_Enemy].gameObject.SetActive(true);
                        Purple_Enemies[current_Purple_Enemy].gameObject.GetComponent<Base_Enemy>().On_Spawn();

                        Purple_Enemies[current_Purple_Enemy].gameObject.GetComponent<Enemy_AI>().On_Spawn();

                        enemy_Found = true;
                        if (current_Purple_Enemy < Purple_Enemies.Length - 1)
                        {
                            current_Purple_Enemy++;
                        }
                        else
                        {
                            current_Purple_Enemy = 0;
                        }
                    }
                    else
                    {
                        if (current_Purple_Enemy < Purple_Enemies.Length - 1)
                        {
                            current_Purple_Enemy++;
                        }
                        else
                        {
                            current_Purple_Enemy = 0;
                        }
                    }
                } while (!enemy_Found);


                break;

            case 4:
                do
                {
                    if (Green_Enemies[current_Green_Enemy].gameObject.activeSelf == false)
                    {
                        Green_Enemies[current_Green_Enemy].gameObject.transform.position = spawn_Locations[rand_Spawn].transform.position;
                        Green_Enemies[current_Green_Enemy].gameObject.SetActive(true);
                        Green_Enemies[current_Green_Enemy].gameObject.GetComponent<Base_Enemy>().On_Spawn();

                        Green_Enemies[current_Green_Enemy].gameObject.GetComponent<Enemy_AI>().On_Spawn();

                        enemy_Found = true;
                        if (current_Green_Enemy < Green_Enemies.Length - 1)
                        {
                            current_Green_Enemy++;
                        }
                        else
                        {
                            current_Green_Enemy = 0;
                        }
                    }
                    else
                    {
                        if (current_Green_Enemy < Green_Enemies.Length - 1)
                        {
                            current_Green_Enemy++;
                        }
                        else
                        {
                            current_Green_Enemy = 0;
                        }
                    }
                } while (!enemy_Found);
                break;

            case 5:
                do
                {
                    if (Orange_Enemies[current_Orange_Enemy].gameObject.activeSelf == false)
                    {
                        Orange_Enemies[current_Orange_Enemy].gameObject.transform.position = spawn_Locations[rand_Spawn].transform.position;
                        Orange_Enemies[current_Orange_Enemy].gameObject.SetActive(true);
                        Orange_Enemies[current_Orange_Enemy].gameObject.GetComponent<Base_Enemy>().On_Spawn();

                        Orange_Enemies[current_Orange_Enemy].gameObject.GetComponent<Enemy_AI>().On_Spawn();

                        enemy_Found = true;
                        if (current_Orange_Enemy < Orange_Enemies.Length - 1)
                        {
                            current_Orange_Enemy++;
                        }
                        else
                        {
                            current_Orange_Enemy = 0;
                        }
                    }
                    else
                    {
                        if (current_Orange_Enemy < Orange_Enemies.Length - 1)
                        {
                            current_Orange_Enemy++;
                        }
                        else
                        {
                            current_Orange_Enemy = 0;
                        }
                    }
                } while (!enemy_Found);
                break;
        }
    }

    //Manages the number of enemies of each type left to spawn
    public void Decrease_Enemy_Count(int enemy_Type_NUmber)
    {
        switch (enemy_Type_NUmber)
        {
            case 0:
                number_Of_Red_Enemies--;
                if (number_Of_Red_Enemies <= 0)
                {
                    red_Enemies_All_Spawned = true;
                }
                break;

            case 1:
                number_Of_Blue_Enemies--;
                if (number_Of_Blue_Enemies <= 0)
                {
                    blue_Enemies_All_Spawned = true;
                }
                break;
            case 2:
                number_Of_Yellow_Enemies--;
                if (number_Of_Yellow_Enemies <= 0)
                {
                    yellow_Enemies_All_Spawned = true;
                }
                break;
            case 3:
                number_Of_Purple_Enemies--;
                if (number_Of_Purple_Enemies <= 0)
                {
                    purple_Enemies_All_Spawned = true;
                }
                break;
            case 4:
                number_Of_Green_Enemies--;
                if (number_Of_Green_Enemies <= 0)
                {
                    green_Enemies_All_Spawned = true;
                }
                break;
            case 5:
                number_Of_Orange_Enemies--;
                if (number_Of_Orange_Enemies <= 0)
                {
                    orange_Enemies_All_Spawned = true;
                }
                break;
        }


    }

    public int Get_Remaining_Enemies()
    {
        return enemies_Remaining;
    }

    public int Get_Total_Enemies()
    {
        return total_Number_Of_Enemies;
    }

    //handles any damage being done to the objectives and checks if they have been all destroyed, ending the level
    public void Objective_Attacked(int amount, int index, GameObject enemy)
    {

        if (!enemy.GetComponent<Enemy_AI>().first_Objective_Is_Destroyed)
        {
            objective_GameObjects[index].GetComponent<Objective_Manager>().Take_Damage(amount);
        }


    }

    public void Final_Objective_Attacked(int amount)
    {
        final_Objective.GetComponent<Objective_Manager>().Take_Damage(amount);
        if (final_Objective.GetComponent<Objective_Manager>().is_Destroyed)
        {
            StartCoroutine("Level_Failed");
        }
    }


    public void Enemy_Destroyed()
    {
        enemies_Remaining--;
        enemies_Left_On_Current_Wave--;

        if (enemies_Remaining <= 0)
        {
            if (!boss_Level)
            {
                StartCoroutine("Level_Passed");
            }

            else
            {
                StartCoroutine("Begin_Boss_Spawn");

            }
        }

        else if (enemies_Left_On_Current_Wave <= 0)
        {
            StartCoroutine("Wait_Between_Waves");
        }
    }

    IEnumerator Begin_Boss_Spawn()
    {
        local_UI_Manager.Display_Current_Wave("Boss");
        level_Music.SetActive(false);
        boss_Music.Play();
        yield return new WaitForSeconds(5f);
        local_UI_Manager.Hide_Current_Wave();
        boss_Gameobject.SetActive(true);

    }


    public IEnumerator Level_Failed()
    {

        if (!level_Failed)
        {
            level_Failed = true;
            local_Player_Input.enabled = false;
            local_Player_Movement_Controls.enabled = false;
            yield return new WaitForSeconds(4f);
            local_UI_Manager.Level_Failed_Panel.SetActive(true);
            enemy_Gameobjects.SetActive(false);
            //player_Gameobjects.SetActive(false);
            soul_Mixer_Gameobjects.SetActive(false);
            Cursor.visible = true;
            cursor_Image.SetActive(false);

        }

    }

    public IEnumerator Level_Passed()
    {


        if (!level_Passed)
        {
            level_Passed = true;
            time_Taken = Time.time - start_Time;
            time_Bonus = (int)time_Target - (int)time_Taken;
            if (time_Bonus < 0)
            {
                time_Bonus = 0;
            }

            switch (local_Objective_Type)
            {
                case Objective_Type.KILL_SLOWED_ENEMIES:
                    if (slowed_Enemies_Killed >= target_Number_Of_Slowed_Enemies_To_Kill)
                    {
                        bonus_Objective_Met = true;
                    }
                    break;

                case Objective_Type.LOSE_NO_TOTEMS:
                    bool any_Destroyed = true;
                    foreach (GameObject go in objective_GameObjects)
                    {
                        if (go.GetComponent<Objective_Manager>().is_Destroyed)
                        {
                            any_Destroyed = false;
                        }
                    }

                    if (any_Destroyed)
                    {
                        bonus_Objective_Met = true;
                    }
                    break;

                case Objective_Type.BLOCK_COLOURED_ATTACKS:
                    if (number_Of_Correct_Colour_Blocked >= target_Number_Of_Enemies_To_Block)
                    {
                        bonus_Objective_Met = true;
                    }
                    break;

                case Objective_Type.HIT_A_GROUP_OF_ENEMIES_AT_ONCE:
                    if (enemies_Hit_At_Once)
                    {
                        bonus_Objective_Met = true;
                    }
                    break;
            }

            if (!bonus_Objective_Met)
            {
                gold_Bonus_Amount = 0;
            }
            gold_Earned_From_Combo = max_Combo_Count * 5;
            if (gold_Earned_From_Combo > 500)
            {
                gold_Earned_From_Combo = 500;
            }
            gold_For_Level_Completion += gold_Earned_From_Combo;
            gold_For_Level_Completion += time_Bonus;
            gold_For_Level_Completion += gold_Bonus_Amount;
            local_Player_Saved_Data_Manager.Increase_Player_Currency(gold_For_Level_Completion);
            local_Player_Saved_Data_Manager.Save_Player_Progress();

            yield return new WaitForSeconds(4f);

            local_UI_Manager.Level_Breakdown_Panel.SetActive(true);
            enemy_Gameobjects.SetActive(false);
            player_Gameobjects.SetActive(false);
            soul_Mixer_Gameobjects.SetActive(false);
            Cursor.visible = true;
            cursor_Image.SetActive(false);


            Scene local_Scene = SceneManager.GetActiveScene();
            switch (local_Scene.buildIndex)

            {
                case 1:
                    if (local_Player_Saved_Data_Manager.levels_Unlocked == 0)
                    {
                        local_Player_Saved_Data_Manager.Increase_Player_Levels_Unlocked();
                        local_UI_Manager.Update_Stats();
                        local_Player_Saved_Data_Manager.Save_Player_Progress();

                    }
                    break;

                case 2:
                    if (local_Player_Saved_Data_Manager.levels_Unlocked == 1)
                    {
                        local_Player_Saved_Data_Manager.Increase_Player_Levels_Unlocked();
                        local_UI_Manager.Update_Stats();
                        local_Player_Saved_Data_Manager.Save_Player_Progress();
                    }
                    break;


            }


            StartCoroutine("Add_Up_End_Game_Stats");

        }

    }

    /// <summary>
    /// Calculates end game currency awarded based on level performance
    /// </summary>
    /// <returns></returns>
    IEnumerator Add_Up_End_Game_Stats()
    {
        bool adding_Up = true;
        while (adding_Up)
        {
            local_UI_Manager.current_Combo_Bonus.text = current_Combo_Bonus_Text.ToString();
            local_UI_Manager.current_Time_Bonus.text = current_Time_Bonus_Text.ToString();
            local_UI_Manager.current_Bonus_Objective.text = current_Bonus_Objective_Text.ToString();
            local_UI_Manager.current_Total_Gold.text = current_Total_Gold_text.ToString();

            if (current_Time_Bonus_Text < time_Bonus)
            {
                current_Time_Bonus_Text++;
                local_UI_Manager.current_Time_Bonus.text = current_Time_Bonus_Text.ToString();
            }

            else if (current_Combo_Bonus_Text < gold_Earned_From_Combo)
            {

                current_Combo_Bonus_Text++;
                local_UI_Manager.current_Combo_Bonus.text = current_Combo_Bonus_Text.ToString();
            }
            else if (current_Bonus_Objective_Text < gold_Bonus_Amount)
            {
                current_Bonus_Objective_Text++;
                local_UI_Manager.current_Bonus_Objective.text = current_Bonus_Objective_Text.ToString();
            }
            else if (current_Total_Gold_text < gold_For_Level_Completion)
            {
                current_Total_Gold_text++;
                local_UI_Manager.current_Total_Gold.text = current_Total_Gold_text.ToString();
            }
            else
            {
                adding_Up = false;
            }

            yield return new WaitForSeconds(.02f);
        }

    }




}
