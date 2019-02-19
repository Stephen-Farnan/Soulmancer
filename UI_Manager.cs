using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public Player_Manager local_Player_Manager;
    public Player_Saved_Data_Manager local_Player_Saved_Data_Manager;
    public Aoe_Special_Ability local_Aoe_Special_Ability;
    public Shield_Special_Ability local_Shield_Special_Ability;
    public Player_Movement_Controls local_Player_Movement_Controls;
    public Rune_Drop_Special_Ability local_Rune_Drop_Special_Ability;

    [Header("Level Failed Screen")]
    public GameObject Level_Failed_Panel;
    [Header("Level Passed Screen")]
    public GameObject Level_Passed_Panel;

    [Header("Level Breakdown Panel")]
    public GameObject Level_Breakdown_Panel;

    [Header("Ammo And Other Gameplay UI")]
    public Text current_Combo;
    public Slider health_Meter;

    [Header("Info To Display Player Upgrade Levels")]
    public Text player_Currency;
    public Text levels_Unlocked;
    public Text health_Level;
    public Text saved_Player_Pickup_Radius_Level;
    public Text saved_Fire_Rate_Level;
    public Text saved_Rune_Drop_Ability_Level;
    public Text saved_Shield_Ability_Level;
    public Text saved_AOE_Special_Ability_Level;

    [Header("Buttons For Purchasing Upgrades")]
    public GameObject Main_Menu_Panel;
    public GameObject Upgrades_Panel;
    public Button Health_Level_Two;
    public Button Health_Level_Three;
    public Button Health_Level_Four;
    public Button Health_Level_Five;

    public Button Player_Pickup_Radius_Level_Two;
    public Button Player_Pickup_Radius_Level_Three;
    public Button Player_Pickup_Radius_Level_Four;
    public Button Player_Pickup_Radius_Level_Five;

    public Button Fire_Rate_Level_Two;
    public Button Fire_Rate_Level_Three;
    public Button Fire_Rate_Level_Four;
    public Button Fire_Rate_Level_Five;

    public Button Rune_Ability_Level_Two;
    public Button Rune_Ability_Level_Three;
    public Button Rune_Ability_Level_Four;
    public Button Rune_Ability_Level_Five;

    public Button Shield_Ability_Level_Two;
    public Button Shield_Ability_Level_Three;
    public Button Shield_Ability_Level_Four;
    public Button Shield_Ability_Level_Five;

    public Button AOE_Ability_Level_Two;
    public Button AOE_Ability_Level_Three;
    public Button AOE_Ability_Level_Four;
    public Button AOE_Ability_Level_Five;

    [Header("Player Colours")]
    public Material player_Colour_Red;
    public Material player_Colour_Blue;
    public Material player_Colour_Yellow;
    public Material player_Colour_Purple;
    public Material player_Colour_Green;
    public Material player_Colour_Orange;
    public Material player_Colour_Default;

    [Header("Cursor Image")]
    public Image cursor_Image;

    [Header("HUD_Damage_Flash")]
    public Image HUD_Damage_Flash;

    [Header("Crystal Image")]
    public Image crystal_Image;

    [Header("Ammo_Slider")]
    public Slider Ammo_Slider;
    public Image Ammo_Silder_Fill;

    [Header("Next Colour Indicators")]
    public Image Player_Colour_Circle_One;
    public Image Player_Colour_Circle_Two;
    public Image Player_Colour_Circle_Three;
    public Image Player_Colour_Circle_Four;
    public Image Player_Colour_Circle_Five;
    public Image Player_Colour_Circle_Six;

    [Header("Options Menu")]
    public GameObject options_Panel;

    [Header("Volume Sliders")]
    public Slider Master_Volume_Slider;
    public Slider BGM_Volume_Slider;
    public Slider SFX_Volume_Slider;

    [Header("Tutorial Info")]
    public Text tutorial_Text_Box;
    public GameObject tutorial_Panel;
    [Header("Graphics Menu")]
    public GameObject graphics_Settings_Button;
    public GameObject graphics_Panel;
    public Text current_Graphics_Setting;


    [Header("Audio Menu")]
    public GameObject audio_Settings_Button;
    public GameObject audio_Panel;


    [Header("Set The Cost Of Upgrading Abilities")]
    int level_Two_Cost = 30;
    int level_Three_Cost = 60;
    int level_Four_Cost = 90;
    int level_Five_Cost = 120;

    [Header("Set If This Scene Is The Main Menu")]
    public bool main_Menu = false;

    float aoe_Cooldown_Mutliplier;
    float dash_Cooldown_Multiplier;
    float shield_Cooldown_Multiplier;

    public float aoe_Cooldown_Timer;
    public float dash_Cooldown_Timer;
    public float shield_Cooldown_Timer;

    public Image aoe_Cooldown;
    public Image shield_Cooldown;
    public Image dash_Cooldown;
    public Image rune_Cooldown;

    public Text current_Time_Bonus;
    public Text current_Combo_Bonus;
    public Text current_Bonus_Objective;
    public Text current_Total_Gold;

    public Text Bonus_Objective_Text;
    public Text Current_Level_Text;
    public Text Current_Wave_Text;

    public GameObject Bonus_Objective_Panel;
    public GameObject Current_Level_Panel;
    public GameObject Current_Wave_Panel;

    public GameObject Loading_Screen;
    #endregion

    private void Awake()
    {

        local_Player_Saved_Data_Manager = GameObject.FindGameObjectWithTag("Player_Settings").GetComponent<Player_Saved_Data_Manager>();
        Master_Volume_Slider.value = local_Player_Saved_Data_Manager.Get_Master_Audio();
        BGM_Volume_Slider.value = local_Player_Saved_Data_Manager.Get_BGM_Audio_Level();
        SFX_Volume_Slider.value = local_Player_Saved_Data_Manager.Get_SFX_Audio_Level();

        Master_Volume_Slider.onValueChanged.AddListener(delegate { MasterValueChangeCheck(); });
        SFX_Volume_Slider.onValueChanged.AddListener(delegate { BGMValueChangeCheck(); });
        BGM_Volume_Slider.onValueChanged.AddListener(delegate { SFXValueChangeCheck(); });



        if (!main_Menu)
        {
            StartCoroutine("Update_UI");
        }

        else
        {
            Cursor.visible = true;
        }

        StartCoroutine("Update_Audio_Settings");
        if (!main_Menu)
        {
            Change_Player_UI_Colour();
            aoe_Cooldown_Mutliplier = 1 / local_Aoe_Special_Ability.aoe_Attack_Cooldown;
            dash_Cooldown_Multiplier = 1 / local_Player_Movement_Controls.dash_Cooldown;
            shield_Cooldown_Multiplier = 1 / local_Shield_Special_Ability.cooldown;
        }




    }

    public void Loading_Screen_Set_Active()
    {
        Loading_Screen.SetActive(true);
    }

    public void Display_Current_Wave(string current_Wave)
    {
        Current_Wave_Panel.SetActive(true);
        Current_Wave_Text.text = current_Wave;
    }

    public void Display_Current_Level(string current_Level)
    {
        Current_Level_Panel.SetActive(true);
        Current_Level_Text.text = current_Level;
    }

    public void Display_Bonus_Objective()
    {
        Bonus_Objective_Panel.SetActive(true);
    }

    public void Hide_Current_Wave()
    {
        Current_Wave_Panel.SetActive(false);
    }

    public void Hide_Current_Level()
    {
        Current_Level_Panel.SetActive(false);
    }

    public void Hide_Bonus_Objective()
    {
        Bonus_Objective_Panel.SetActive(false);
    }

    /// <summary>
    /// Updates the cooldown bars of the special abilities on screen
    /// </summary>
    public void Set_Cooldown_Bars()
    {
        aoe_Cooldown.fillAmount = aoe_Cooldown_Mutliplier * aoe_Cooldown_Timer;
        shield_Cooldown.fillAmount = shield_Cooldown_Multiplier * shield_Cooldown_Timer;
        dash_Cooldown.fillAmount = dash_Cooldown_Multiplier * dash_Cooldown_Timer;
        if (local_Rune_Drop_Special_Ability.number_Of_Runes_Placed >= local_Rune_Drop_Special_Ability.player_Rune_Capacity)
        {
            rune_Cooldown.fillAmount = 1;
        }

        else
        {
            rune_Cooldown.fillAmount = 0;
        }
    }

    public void MasterValueChangeCheck()
    {
        StopCoroutine("Update_Audio_Settings");
        Set_Master_Volume();
        Set_BGM_Volume();
        Set_SFX_Volume();
        StartCoroutine("Update_Audio_Settings");
        PlayerPrefs.Save();
    }

    public void BGMValueChangeCheck()
    {
        StopCoroutine("Update_Audio_Settings");
        Set_Master_Volume();
        Set_BGM_Volume();
        Set_SFX_Volume();
        StartCoroutine("Update_Audio_Settings");
        PlayerPrefs.Save();
    }

    public void SFXValueChangeCheck()
    {
        StopCoroutine("Update_Audio_Settings");
        Set_Master_Volume();
        Set_BGM_Volume();
        Set_SFX_Volume();
        StartCoroutine("Update_Audio_Settings");
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Sets the current ammo colour in use and sets the other ui elements as unselected
    /// </summary>
    public void Change_Player_UI_Colour()
    {
        switch (local_Player_Manager.local_Selected_Colour)
        {
            case Player_Manager.Selected_Colour.RED:
                crystal_Image.color = player_Colour_Red.color;
                cursor_Image.color = player_Colour_Red.color;
                Ammo_Slider.value = local_Player_Manager.red_Colour_Amount;
                Ammo_Silder_Fill.color = player_Colour_Red.color;

                Player_Colour_Circle_One.color = player_Colour_Blue.color;
                Player_Colour_Circle_Two.color = player_Colour_Yellow.color;
                Player_Colour_Circle_Three.color = player_Colour_Purple.color;
                Player_Colour_Circle_Four.color = player_Colour_Green.color;
                Player_Colour_Circle_Five.color = player_Colour_Orange.color;
                Player_Colour_Circle_Six.color = player_Colour_Default.color;
                break;

            case Player_Manager.Selected_Colour.BLUE:
                crystal_Image.color = player_Colour_Blue.color;
                cursor_Image.color = player_Colour_Blue.color;
                Ammo_Slider.value = local_Player_Manager.blue_Colour_Amount;
                Ammo_Silder_Fill.color = player_Colour_Blue.color;

                Player_Colour_Circle_One.color = player_Colour_Yellow.color;
                Player_Colour_Circle_Two.color = player_Colour_Purple.color;
                Player_Colour_Circle_Three.color = player_Colour_Green.color;
                Player_Colour_Circle_Four.color = player_Colour_Orange.color;
                Player_Colour_Circle_Five.color = player_Colour_Default.color;
                Player_Colour_Circle_Six.color = player_Colour_Red.color;
                break;

            case Player_Manager.Selected_Colour.YELLOW:
                crystal_Image.color = player_Colour_Yellow.color;
                cursor_Image.color = player_Colour_Yellow.color;
                Ammo_Slider.value = local_Player_Manager.yellow_Colour_Amount;
                Ammo_Silder_Fill.color = player_Colour_Yellow.color;

                Player_Colour_Circle_One.color = player_Colour_Purple.color;
                Player_Colour_Circle_Two.color = player_Colour_Green.color;
                Player_Colour_Circle_Three.color = player_Colour_Orange.color;
                Player_Colour_Circle_Four.color = player_Colour_Default.color;
                Player_Colour_Circle_Five.color = player_Colour_Red.color;
                Player_Colour_Circle_Six.color = player_Colour_Blue.color;
                break;

            case Player_Manager.Selected_Colour.PURPLE:
                crystal_Image.color = player_Colour_Purple.color;
                cursor_Image.color = player_Colour_Purple.color;
                Ammo_Slider.value = local_Player_Manager.purple_Colour_Amount;
                Ammo_Silder_Fill.color = player_Colour_Purple.color;

                Player_Colour_Circle_One.color = player_Colour_Green.color;
                Player_Colour_Circle_Two.color = player_Colour_Orange.color;
                Player_Colour_Circle_Three.color = player_Colour_Default.color;
                Player_Colour_Circle_Four.color = player_Colour_Red.color;
                Player_Colour_Circle_Five.color = player_Colour_Blue.color;
                Player_Colour_Circle_Six.color = player_Colour_Yellow.color;
                break;

            case Player_Manager.Selected_Colour.GREEN:
                crystal_Image.color = player_Colour_Green.color;
                cursor_Image.color = player_Colour_Green.color;
                Ammo_Slider.value = local_Player_Manager.green_Colour_Amount;
                Ammo_Silder_Fill.color = player_Colour_Green.color;

                Player_Colour_Circle_One.color = player_Colour_Orange.color;
                Player_Colour_Circle_Two.color = player_Colour_Default.color;
                Player_Colour_Circle_Three.color = player_Colour_Red.color;
                Player_Colour_Circle_Four.color = player_Colour_Blue.color;
                Player_Colour_Circle_Five.color = player_Colour_Yellow.color;
                Player_Colour_Circle_Six.color = player_Colour_Purple.color;
                break;

            case Player_Manager.Selected_Colour.ORANGE:
                crystal_Image.color = player_Colour_Orange.color;
                cursor_Image.color = player_Colour_Orange.color;
                Ammo_Slider.value = local_Player_Manager.orange_Colour_Amount;
                Ammo_Silder_Fill.color = player_Colour_Orange.color;

                Player_Colour_Circle_One.color = player_Colour_Default.color;
                Player_Colour_Circle_Two.color = player_Colour_Red.color;
                Player_Colour_Circle_Three.color = player_Colour_Blue.color;
                Player_Colour_Circle_Four.color = player_Colour_Yellow.color;
                Player_Colour_Circle_Five.color = player_Colour_Purple.color;
                Player_Colour_Circle_Six.color = player_Colour_Green.color;
                break;

            case Player_Manager.Selected_Colour.DEFAULT:
                crystal_Image.color = player_Colour_Default.color;
                cursor_Image.color = player_Colour_Default.color;
                Ammo_Slider.value = Ammo_Slider.maxValue;
                Ammo_Silder_Fill.color = player_Colour_Default.color;

                Player_Colour_Circle_One.color = player_Colour_Red.color;
                Player_Colour_Circle_Two.color = player_Colour_Blue.color;
                Player_Colour_Circle_Three.color = player_Colour_Yellow.color;
                Player_Colour_Circle_Four.color = player_Colour_Purple.color;
                Player_Colour_Circle_Five.color = player_Colour_Green.color;
                Player_Colour_Circle_Six.color = player_Colour_Orange.color;
                break;
        }
    }

    public void Increase_Graphics_Settings()
    {
        QualitySettings.IncreaseLevel();
        local_Player_Saved_Data_Manager.Set_Graphics_Setting();
    }

    public void Decrease_Graphics_Settings()
    {
        QualitySettings.DecreaseLevel();
        local_Player_Saved_Data_Manager.Set_Graphics_Setting();
    }

    public void Load_Audio_Settings()
    {
        audio_Panel.SetActive(true);
    }

    public void Load_Graphics_Settings()
    {
        graphics_Panel.SetActive(true);
    }

    public void Exit_Graphics()
    {
        graphics_Panel.SetActive(false);
    }

    public void Exit_Audio_Settings()
    {
        audio_Panel.SetActive(false);
    }

    public void Exit_Options()
    {
        options_Panel.SetActive(false);
        Main_Menu_Panel.SetActive(true);
    }

    public void Load_Options()
    {
        options_Panel.SetActive(true);
        Main_Menu_Panel.SetActive(false);
    }

    /// <summary>
    /// Set graphical options
    /// </summary>
    public void Update_Graphics_Options()
    {

        switch (local_Player_Saved_Data_Manager.Get_Saved_Graphics_Level())
        {
            case 0:
                current_Graphics_Setting.text = "Lowest";
                break;

            case 1:
                current_Graphics_Setting.text = "Low";
                break;

            case 2:
                current_Graphics_Setting.text = "Medium";
                break;

            case 3:
                current_Graphics_Setting.text = "High";
                break;

            case 4:
                current_Graphics_Setting.text = "Very High";
                break;

            case 5:
                current_Graphics_Setting.text = "Ultra";
                break;
        }

        local_Player_Saved_Data_Manager.Set_Graphics_Setting();

    }

    public void Load_Upgrades_Panel()
    {
        Upgrades_Panel.SetActive(true);
        Initialise_Upgrades_Menu();
    }

    public void Close_Upgrades_Panel()
    {
        Upgrades_Panel.SetActive(false);
    }


    /// <summary>
    /// keeps track of the players ammo counts and currently selected colour
    /// </summary>
    /// <returns></returns>
    IEnumerator Update_UI()
    {
        while (true)
        {
            if (!main_Menu)
            {
                Change_Player_UI_Colour();
                Set_Cooldown_Bars();
            }

            Update_Audio_Settings();

            yield return new WaitForSeconds(0.01f);

        }
    }

    /// <summary>
    /// Wipes all progress for upgrades on the UI only
    /// </summary>
    public void Reset_Upgrade_Menu()
    {
        Health_Level_Two.interactable = false;
        Health_Level_Two.image.color = Color.white;
        Health_Level_Three.interactable = false;
        Health_Level_Three.image.color = Color.white;
        Health_Level_Four.interactable = false;
        Health_Level_Four.image.color = Color.white;
        Health_Level_Five.interactable = false;
        Health_Level_Five.image.color = Color.white;

        Player_Pickup_Radius_Level_Two.interactable = false;
        Player_Pickup_Radius_Level_Two.image.color = Color.white;
        Player_Pickup_Radius_Level_Three.interactable = false;
        Player_Pickup_Radius_Level_Three.image.color = Color.white;
        Player_Pickup_Radius_Level_Four.interactable = false;
        Player_Pickup_Radius_Level_Four.image.color = Color.white;
        Player_Pickup_Radius_Level_Five.interactable = false;
        Player_Pickup_Radius_Level_Five.image.color = Color.white;

        Fire_Rate_Level_Two.interactable = false;
        Fire_Rate_Level_Two.image.color = Color.white;
        Fire_Rate_Level_Three.interactable = false;
        Fire_Rate_Level_Three.image.color = Color.white;
        Fire_Rate_Level_Four.interactable = false;
        Fire_Rate_Level_Four.image.color = Color.white;
        Fire_Rate_Level_Five.interactable = false;
        Fire_Rate_Level_Five.image.color = Color.white;

        Rune_Ability_Level_Two.interactable = false;
        Rune_Ability_Level_Two.image.color = Color.white;
        Rune_Ability_Level_Three.interactable = false;
        Rune_Ability_Level_Three.image.color = Color.white;
        Rune_Ability_Level_Four.interactable = false;
        Rune_Ability_Level_Four.image.color = Color.white;
        Rune_Ability_Level_Five.interactable = false;
        Rune_Ability_Level_Five.image.color = Color.white;

        Shield_Ability_Level_Two.interactable = false;
        Shield_Ability_Level_Two.image.color = Color.white;
        Shield_Ability_Level_Three.interactable = false;
        Shield_Ability_Level_Three.image.color = Color.white;
        Shield_Ability_Level_Four.interactable = false;
        Shield_Ability_Level_Four.image.color = Color.white;
        Shield_Ability_Level_Five.interactable = false;
        Shield_Ability_Level_Five.image.color = Color.white;

        AOE_Ability_Level_Two.interactable = false;
        AOE_Ability_Level_Two.image.color = Color.white;
        AOE_Ability_Level_Three.interactable = false;
        AOE_Ability_Level_Three.image.color = Color.white;
        AOE_Ability_Level_Four.interactable = false;
        AOE_Ability_Level_Four.image.color = Color.white;
        AOE_Ability_Level_Five.interactable = false;
        AOE_Ability_Level_Five.image.color = Color.white;
    }

    /// <summary>
    /// Initialise values for buttons based on currently purchased upgrades
    /// </summary>
    public void Initialise_Upgrades_Menu()
    {
        switch (local_Player_Saved_Data_Manager.Get_Health_Level())
        {
            case 1:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Two_Cost)
                {
                    Health_Level_Two.interactable = true;

                }
                else
                {
                    Health_Level_Two.interactable = false;
                    Health_Level_Two.image.color = Color.white;

                }
                Health_Level_Three.interactable = false;
                Health_Level_Four.interactable = false;
                Health_Level_Five.interactable = false;
                break;

            case 2:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Three_Cost)
                {
                    Health_Level_Three.interactable = true;

                }
                else
                {
                    Health_Level_Three.interactable = false;
                    Health_Level_Three.image.color = Color.white;
                }
                Health_Level_Two.interactable = false;
                Health_Level_Two.image.color = Color.green;
                Health_Level_Four.interactable = false;
                Health_Level_Five.interactable = false;
                break;

            case 3:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Four_Cost)
                {
                    Health_Level_Four.interactable = true;
                }
                else
                {
                    Health_Level_Four.interactable = false;
                    Health_Level_Four.image.color = Color.white;
                }
                Health_Level_Two.interactable = false;
                Health_Level_Two.image.color = Color.green;
                Health_Level_Three.interactable = false;
                Health_Level_Three.image.color = Color.green;
                Health_Level_Five.interactable = false;
                break;

            case 4:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Five_Cost)
                {
                    Debug.Log("here");
                    Health_Level_Five.interactable = true;
                    Health_Level_Five.image.color = Color.white;
                }
                else
                {
                    Health_Level_Five.interactable = false;
                }
                Health_Level_Two.interactable = false;
                Health_Level_Two.image.color = Color.green;
                Health_Level_Three.interactable = false;
                Health_Level_Three.image.color = Color.green;
                Health_Level_Four.interactable = false;
                Health_Level_Four.image.color = Color.green;
                break;

            case 5:

                Health_Level_Two.interactable = false;
                Health_Level_Two.image.color = Color.green;
                Health_Level_Three.interactable = false;
                Health_Level_Three.image.color = Color.green;
                Health_Level_Four.interactable = false;
                Health_Level_Four.image.color = Color.green;
                Health_Level_Five.interactable = false;
                Health_Level_Five.image.color = Color.green;
                break;

            default:
                Health_Level_Two.interactable = false;
                Health_Level_Two.image.color = Color.green;
                Health_Level_Three.interactable = false;
                Health_Level_Three.image.color = Color.green;
                Health_Level_Four.interactable = false;
                Health_Level_Four.image.color = Color.green;
                Health_Level_Five.interactable = false;
                Health_Level_Five.image.color = Color.green;
                break;
        }

        switch (local_Player_Saved_Data_Manager.Get_Player_Pickup_Radius_Level())
        {
            case 1:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Two_Cost)
                {
                    Player_Pickup_Radius_Level_Two.interactable = true;
                }
                else
                {
                    Player_Pickup_Radius_Level_Two.interactable = false;
                    Player_Pickup_Radius_Level_Two.image.color = Color.white;
                }
                Player_Pickup_Radius_Level_Three.interactable = false;
                Player_Pickup_Radius_Level_Four.interactable = false;
                Player_Pickup_Radius_Level_Five.interactable = false;
                break;

            case 2:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Three_Cost)
                {
                    Player_Pickup_Radius_Level_Three.interactable = true;
                }
                else
                {
                    Player_Pickup_Radius_Level_Three.interactable = false;
                    Player_Pickup_Radius_Level_Three.image.color = Color.white;
                }
                Player_Pickup_Radius_Level_Two.interactable = false;
                Player_Pickup_Radius_Level_Two.image.color = Color.green;
                Player_Pickup_Radius_Level_Four.interactable = false;
                Player_Pickup_Radius_Level_Five.interactable = false;
                break;

            case 3:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Four_Cost)
                {
                    Player_Pickup_Radius_Level_Four.interactable = true;
                }
                else
                {
                    Player_Pickup_Radius_Level_Four.interactable = false;
                    Player_Pickup_Radius_Level_Four.image.color = Color.white;
                }
                Player_Pickup_Radius_Level_Two.interactable = false;
                Player_Pickup_Radius_Level_Two.image.color = Color.green;
                Player_Pickup_Radius_Level_Three.interactable = false;
                Player_Pickup_Radius_Level_Three.image.color = Color.green;
                Player_Pickup_Radius_Level_Five.interactable = false;
                break;

            case 4:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Five_Cost)
                {
                    Player_Pickup_Radius_Level_Five.interactable = true;
                }
                else
                {
                    Player_Pickup_Radius_Level_Five.interactable = false;
                    Player_Pickup_Radius_Level_Five.image.color = Color.white;
                }
                Player_Pickup_Radius_Level_Two.interactable = false;
                Player_Pickup_Radius_Level_Two.image.color = Color.green;
                Player_Pickup_Radius_Level_Three.interactable = false;
                Player_Pickup_Radius_Level_Three.image.color = Color.green;
                Player_Pickup_Radius_Level_Four.interactable = false;
                Player_Pickup_Radius_Level_Four.image.color = Color.green;
                break;

            case 5:

                Player_Pickup_Radius_Level_Two.interactable = false;
                Player_Pickup_Radius_Level_Two.image.color = Color.green;
                Player_Pickup_Radius_Level_Three.interactable = false;
                Player_Pickup_Radius_Level_Three.image.color = Color.green;
                Player_Pickup_Radius_Level_Four.interactable = false;
                Player_Pickup_Radius_Level_Four.image.color = Color.green;
                Player_Pickup_Radius_Level_Five.interactable = false;
                Player_Pickup_Radius_Level_Five.image.color = Color.green;
                break;

            default:
                Player_Pickup_Radius_Level_Two.interactable = false;
                Player_Pickup_Radius_Level_Two.image.color = Color.green;
                Player_Pickup_Radius_Level_Three.interactable = false;
                Player_Pickup_Radius_Level_Three.image.color = Color.green;
                Player_Pickup_Radius_Level_Four.interactable = false;
                Player_Pickup_Radius_Level_Four.image.color = Color.green;
                Player_Pickup_Radius_Level_Five.interactable = false;
                Player_Pickup_Radius_Level_Five.image.color = Color.green;
                break;
        }

        switch (local_Player_Saved_Data_Manager.Get_Fire_Rate_Level())
        {
            case 1:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Two_Cost)
                {
                    Fire_Rate_Level_Two.interactable = true;
                }
                else
                {
                    Fire_Rate_Level_Two.interactable = false;
                    Fire_Rate_Level_Two.image.color = Color.white;
                }
                Fire_Rate_Level_Three.interactable = false;
                Fire_Rate_Level_Four.interactable = false;
                Fire_Rate_Level_Five.interactable = false;
                break;

            case 2:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Three_Cost)
                {
                    Fire_Rate_Level_Three.interactable = true;
                }
                else
                {
                    Fire_Rate_Level_Three.interactable = false;
                    Fire_Rate_Level_Three.image.color = Color.white;
                }
                Fire_Rate_Level_Two.interactable = false;
                Fire_Rate_Level_Two.image.color = Color.green;
                Fire_Rate_Level_Four.interactable = false;
                Fire_Rate_Level_Five.interactable = false;
                break;

            case 3:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Four_Cost)
                {
                    Fire_Rate_Level_Four.interactable = true;
                }
                else
                {
                    Fire_Rate_Level_Four.interactable = false;
                    Fire_Rate_Level_Four.image.color = Color.white;
                }
                Fire_Rate_Level_Two.interactable = false;
                Fire_Rate_Level_Two.image.color = Color.green;
                Fire_Rate_Level_Three.interactable = false;
                Fire_Rate_Level_Three.image.color = Color.green;
                Fire_Rate_Level_Five.interactable = false;
                break;

            case 4:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Five_Cost)
                {
                    Fire_Rate_Level_Five.interactable = true;
                }
                else
                {
                    Fire_Rate_Level_Five.interactable = false;
                    Fire_Rate_Level_Five.image.color = Color.white;
                }
                Fire_Rate_Level_Two.interactable = false;
                Fire_Rate_Level_Two.image.color = Color.green;
                Fire_Rate_Level_Three.interactable = false;
                Fire_Rate_Level_Three.image.color = Color.green;
                Fire_Rate_Level_Four.interactable = false;
                Fire_Rate_Level_Four.image.color = Color.green;
                break;

            case 5:

                Fire_Rate_Level_Two.interactable = false;
                Fire_Rate_Level_Two.image.color = Color.green;
                Fire_Rate_Level_Three.interactable = false;
                Fire_Rate_Level_Three.image.color = Color.green;
                Fire_Rate_Level_Four.interactable = false;
                Fire_Rate_Level_Four.image.color = Color.green;
                Fire_Rate_Level_Five.interactable = false;
                Fire_Rate_Level_Five.image.color = Color.green;
                break;

            default:
                Fire_Rate_Level_Two.interactable = false;
                Fire_Rate_Level_Two.image.color = Color.green;
                Fire_Rate_Level_Three.interactable = false;
                Fire_Rate_Level_Three.image.color = Color.green;
                Fire_Rate_Level_Four.interactable = false;
                Fire_Rate_Level_Four.image.color = Color.green;
                Fire_Rate_Level_Five.interactable = false;
                Fire_Rate_Level_Five.image.color = Color.green;
                break;
        }

        switch (local_Player_Saved_Data_Manager.Get_Rune_Drop_Ability_Level())
        {
            case 1:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Two_Cost)
                {
                    Rune_Ability_Level_Two.interactable = true;
                }
                else
                {
                    Rune_Ability_Level_Two.interactable = false;
                    Rune_Ability_Level_Two.image.color = Color.white;
                }
                Rune_Ability_Level_Three.interactable = false;
                Rune_Ability_Level_Four.interactable = false;
                Rune_Ability_Level_Five.interactable = false;
                break;

            case 2:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Three_Cost)
                {
                    Rune_Ability_Level_Three.interactable = true;
                }
                else
                {
                    Rune_Ability_Level_Three.interactable = false;
                    Rune_Ability_Level_Three.image.color = Color.white;
                }
                Rune_Ability_Level_Two.interactable = false;
                Rune_Ability_Level_Two.image.color = Color.green;
                Rune_Ability_Level_Four.interactable = false;
                Rune_Ability_Level_Five.interactable = false;
                break;

            case 3:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Four_Cost)
                {
                    Rune_Ability_Level_Four.interactable = true;
                }
                else
                {
                    Rune_Ability_Level_Four.interactable = false;
                    Rune_Ability_Level_Four.image.color = Color.white;
                }
                Rune_Ability_Level_Two.interactable = false;
                Rune_Ability_Level_Two.image.color = Color.green;
                Rune_Ability_Level_Three.interactable = false;
                Rune_Ability_Level_Three.image.color = Color.green;
                Rune_Ability_Level_Five.interactable = false;
                break;

            case 4:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Five_Cost)
                {
                    Rune_Ability_Level_Five.interactable = true;
                }
                else
                {
                    Rune_Ability_Level_Five.interactable = false;
                    Rune_Ability_Level_Five.image.color = Color.white;
                }
                Rune_Ability_Level_Two.interactable = false;
                Rune_Ability_Level_Two.image.color = Color.green;
                Rune_Ability_Level_Three.interactable = false;
                Rune_Ability_Level_Three.image.color = Color.green;
                Rune_Ability_Level_Four.interactable = false;
                Rune_Ability_Level_Four.image.color = Color.green;
                break;

            case 5:

                Rune_Ability_Level_Two.interactable = false;
                Rune_Ability_Level_Two.image.color = Color.green;
                Rune_Ability_Level_Three.interactable = false;
                Rune_Ability_Level_Three.image.color = Color.green;
                Rune_Ability_Level_Four.interactable = false;
                Rune_Ability_Level_Four.image.color = Color.green;
                Rune_Ability_Level_Five.interactable = false;
                Rune_Ability_Level_Five.image.color = Color.green;
                break;

            default:
                Rune_Ability_Level_Two.interactable = false;
                Rune_Ability_Level_Two.image.color = Color.green;
                Rune_Ability_Level_Three.interactable = false;
                Rune_Ability_Level_Three.image.color = Color.green;
                Rune_Ability_Level_Four.interactable = false;
                Rune_Ability_Level_Four.image.color = Color.green;
                Rune_Ability_Level_Five.interactable = false;
                Rune_Ability_Level_Five.image.color = Color.green;
                break;
        }

        switch (local_Player_Saved_Data_Manager.Get_Shield_Ability_Level())
        {
            case 1:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Two_Cost)
                {
                    Shield_Ability_Level_Two.interactable = true;
                }
                else
                {
                    Shield_Ability_Level_Two.interactable = false;
                    Shield_Ability_Level_Two.image.color = Color.white;
                }
                Shield_Ability_Level_Three.interactable = false;
                Shield_Ability_Level_Four.interactable = false;
                Shield_Ability_Level_Five.interactable = false;
                break;

            case 2:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Three_Cost)
                {
                    Shield_Ability_Level_Three.interactable = true;
                }
                else
                {
                    Shield_Ability_Level_Three.interactable = false;
                    Shield_Ability_Level_Three.image.color = Color.white;
                }
                Shield_Ability_Level_Two.interactable = false;
                Shield_Ability_Level_Two.image.color = Color.green;
                Shield_Ability_Level_Four.interactable = false;
                Shield_Ability_Level_Five.interactable = false;
                break;

            case 3:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Four_Cost)
                {
                    Shield_Ability_Level_Four.interactable = true;
                }
                else
                {
                    Shield_Ability_Level_Four.interactable = false;
                    Shield_Ability_Level_Four.image.color = Color.white;
                }
                Shield_Ability_Level_Two.interactable = false;
                Shield_Ability_Level_Two.image.color = Color.green;
                Shield_Ability_Level_Three.interactable = false;
                Shield_Ability_Level_Three.image.color = Color.green;
                Shield_Ability_Level_Five.interactable = false;
                break;

            case 4:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Five_Cost)
                {
                    Shield_Ability_Level_Five.interactable = true;
                }
                else
                {
                    Shield_Ability_Level_Five.interactable = false;
                    Shield_Ability_Level_Five.image.color = Color.white;
                }
                Shield_Ability_Level_Two.interactable = false;
                Shield_Ability_Level_Two.image.color = Color.green;
                Shield_Ability_Level_Three.interactable = false;
                Shield_Ability_Level_Three.image.color = Color.green;
                Shield_Ability_Level_Four.interactable = false;
                Shield_Ability_Level_Four.image.color = Color.green;
                break;

            case 5:

                Shield_Ability_Level_Two.interactable = false;
                Shield_Ability_Level_Two.image.color = Color.green;
                Shield_Ability_Level_Three.interactable = false;
                Shield_Ability_Level_Three.image.color = Color.green;
                Shield_Ability_Level_Four.interactable = false;
                Shield_Ability_Level_Four.image.color = Color.green;
                Shield_Ability_Level_Five.interactable = false;
                Shield_Ability_Level_Five.image.color = Color.green;
                break;
            default:
                Shield_Ability_Level_Two.interactable = false;
                Shield_Ability_Level_Two.image.color = Color.green;
                Shield_Ability_Level_Three.interactable = false;
                Shield_Ability_Level_Three.image.color = Color.green;
                Shield_Ability_Level_Four.interactable = false;
                Shield_Ability_Level_Four.image.color = Color.green;
                Shield_Ability_Level_Five.interactable = false;
                Shield_Ability_Level_Five.image.color = Color.green;
                break;
        }

        switch (local_Player_Saved_Data_Manager.Get_AOE_Special_Ability_Level())
        {
            case 1:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Two_Cost)
                {
                    AOE_Ability_Level_Two.interactable = true;
                }
                else
                {
                    AOE_Ability_Level_Two.interactable = false;
                    AOE_Ability_Level_Two.image.color = Color.white;
                }
                AOE_Ability_Level_Three.interactable = false;
                AOE_Ability_Level_Four.interactable = false;
                AOE_Ability_Level_Five.interactable = false;
                break;

            case 2:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Three_Cost)
                {
                    AOE_Ability_Level_Three.interactable = true;
                }
                else
                {
                    AOE_Ability_Level_Three.interactable = false;
                    AOE_Ability_Level_Three.image.color = Color.white;
                }
                AOE_Ability_Level_Two.interactable = false;
                AOE_Ability_Level_Two.image.color = Color.green;
                AOE_Ability_Level_Four.interactable = false;
                AOE_Ability_Level_Five.interactable = false;
                break;

            case 3:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Four_Cost)
                {
                    AOE_Ability_Level_Four.interactable = true;
                }
                else
                {
                    AOE_Ability_Level_Four.interactable = false;
                    AOE_Ability_Level_Four.image.color = Color.white;
                }
                AOE_Ability_Level_Two.interactable = false;
                AOE_Ability_Level_Two.image.color = Color.green;
                AOE_Ability_Level_Three.interactable = false;
                AOE_Ability_Level_Three.image.color = Color.green;
                AOE_Ability_Level_Five.interactable = false;
                break;

            case 4:
                if (local_Player_Saved_Data_Manager.Get_Player_Currency() >= level_Five_Cost)
                {
                    AOE_Ability_Level_Five.interactable = true;
                }
                else
                {
                    AOE_Ability_Level_Five.interactable = false;
                    AOE_Ability_Level_Five.image.color = Color.white;
                }
                AOE_Ability_Level_Two.interactable = false;
                AOE_Ability_Level_Two.image.color = Color.green;
                AOE_Ability_Level_Three.interactable = false;
                AOE_Ability_Level_Three.image.color = Color.green;
                AOE_Ability_Level_Four.interactable = false;
                AOE_Ability_Level_Four.image.color = Color.green;
                break;

            case 5:

                AOE_Ability_Level_Two.interactable = false;
                AOE_Ability_Level_Two.image.color = Color.green;
                AOE_Ability_Level_Three.interactable = false;
                AOE_Ability_Level_Three.image.color = Color.green;
                AOE_Ability_Level_Four.interactable = false;
                AOE_Ability_Level_Four.image.color = Color.green;
                AOE_Ability_Level_Five.interactable = false;
                AOE_Ability_Level_Five.image.color = Color.green;
                break;

            default:
                AOE_Ability_Level_Two.interactable = false;
                AOE_Ability_Level_Two.image.color = Color.green;
                AOE_Ability_Level_Three.interactable = false;
                AOE_Ability_Level_Three.image.color = Color.green;
                AOE_Ability_Level_Four.interactable = false;
                AOE_Ability_Level_Four.image.color = Color.green;
                AOE_Ability_Level_Five.interactable = false;
                AOE_Ability_Level_Five.image.color = Color.green;
                break;
        }

    }

    public void Update_Stats()
    {
        player_Currency.text = local_Player_Saved_Data_Manager.Get_Player_Currency().ToString();
        levels_Unlocked.text = local_Player_Saved_Data_Manager.Get_Player_Levels_Unlocked().ToString();
        health_Level.text = local_Player_Saved_Data_Manager.Get_Health_Level().ToString();
        saved_Player_Pickup_Radius_Level.text = local_Player_Saved_Data_Manager.Get_Player_Pickup_Radius_Level().ToString();
        saved_Fire_Rate_Level.text = local_Player_Saved_Data_Manager.Get_Fire_Rate_Level().ToString();
        saved_Rune_Drop_Ability_Level.text = local_Player_Saved_Data_Manager.Get_Rune_Drop_Ability_Level().ToString();
        saved_Shield_Ability_Level.text = local_Player_Saved_Data_Manager.Get_Shield_Ability_Level().ToString();
        saved_AOE_Special_Ability_Level.text = local_Player_Saved_Data_Manager.Get_AOE_Special_Ability_Level().ToString();
    }

    public void Update_Audio_Settings_Button()
    {
        StopCoroutine("Update_Audio_Settings");
        Set_Master_Volume();
        Set_BGM_Volume();
        Set_SFX_Volume();
        StartCoroutine("Update_Audio_Settings");
        PlayerPrefs.Save();
    }

    public IEnumerator Update_Audio_Settings()
    {
        while (true)
        {
            Set_Master_Volume();
            Set_BGM_Volume();
            Set_SFX_Volume();

            yield return new WaitForSeconds(0.01f);
        }

    }

    public void Set_Master_Volume()
    {
        local_Player_Saved_Data_Manager.Set_Master_Audio_Level(Master_Volume_Slider.value);
    }

    public void Set_BGM_Volume()
    {
        local_Player_Saved_Data_Manager.Set_BGM_Audio_Level(BGM_Volume_Slider.value);

    }

    public void Set_SFX_Volume()
    {
        local_Player_Saved_Data_Manager.Set_SFX_Audio_Level(SFX_Volume_Slider.value);
    }

    public void Set_Level_Passed_Panel_Active()
    {
        Level_Passed_Panel.SetActive(true);
    }

}
