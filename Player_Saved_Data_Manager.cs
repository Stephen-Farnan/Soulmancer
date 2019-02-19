using UnityEngine;
using UnityEngine.Audio;

public class Player_Saved_Data_Manager : MonoBehaviour
{

    int player_Currency = 0;
    [HideInInspector]
    public int levels_Unlocked = 0;
    [HideInInspector]
    public int player_Resolution_Index;
    int health_Level = 1;
    int saved_Player_Pickup_Radius_Level = 1;
    int saved_Fire_Rate_Level = 1;
    int saved_Rune_Drop_Ability_Level = 1;
    int saved_Shield_Ability_Level = 1;
    int saved_AOE_Special_Ability_Level = 1;
    int saved_Graphics_Quality_Level;
    float master_Volume_Setting = 7f;
    float bgm_Volume_Setting = 7f;
    float sfx_Volume_Setting = 7f;
    [HideInInspector]
    public int game_Full_Screen_Mode = 1;

    [Header("References To Be Set")]
    public AudioMixer audio_Mixer;


    /// <summary>
    /// Initialises playerprefs to default values unless a stored value is found
    /// </summary>
    private void Awake()
    {


        if (!PlayerPrefs.HasKey("game_Fullscreen"))
        {
            PlayerPrefs.SetInt("game_Fullscreen", 1);
            game_Full_Screen_Mode = PlayerPrefs.GetInt("game_Fullscreen");
        }
        else
        {
            game_Full_Screen_Mode = PlayerPrefs.GetInt("game_Fullscreen");
        }

        if (PlayerPrefs.HasKey("player_Selected_Resolution"))
        {
            player_Resolution_Index = PlayerPrefs.GetInt("player_Selected_Resolution");
        }

        else
        {

            int temp_Int = 11;
            switch (Screen.currentResolution.width)
            {
                case 640:
                    temp_Int = 0;
                    break;

                case 720:
                    temp_Int = 1;
                    break;

                case 800:
                    temp_Int = 2;
                    break;

                case 1024:
                    temp_Int = 3;
                    break;

                case 1152:
                    temp_Int = 4;
                    break;

                case 1280:
                    temp_Int = 5;
                    break;

                case 1360:
                    temp_Int = 6;
                    break;

                case 1440:
                    temp_Int = 7;
                    break;

                case 1600:
                    temp_Int = 8;
                    break;

                case 1680:
                    temp_Int = 9;
                    break;

                case 1920:
                    temp_Int = 10;
                    break;

                case 2715:
                    temp_Int = 11;
                    break;
            }

            PlayerPrefs.SetInt("player_Selected_Resolution", temp_Int);
            player_Resolution_Index = PlayerPrefs.GetInt("player_Selected_Resolution");
        }


        if (!PlayerPrefs.HasKey("player_Currency"))
        {
            PlayerPrefs.SetInt("player_Currency", 0);
        }
        else
        {
            player_Currency = PlayerPrefs.GetInt("Player_Currency");
        }

        if (!PlayerPrefs.HasKey("levels_Unlocked"))
        {
            PlayerPrefs.SetInt("levels_Unlocked", 0);
        }
        else
        {
            levels_Unlocked = PlayerPrefs.GetInt("Levels_Unlocked");
        }

        if (!PlayerPrefs.HasKey("health_Level"))
        {
            PlayerPrefs.SetInt("health_Level", 1);
        }
        else
        {
            health_Level = PlayerPrefs.GetInt("health_Level");
        }

        if (!PlayerPrefs.HasKey("saved_Player_Pickup_Radius_Level"))
        {
            PlayerPrefs.SetInt("saved_Player_Pickup_Radius_Level", 1);
        }

        else
        {
            saved_Player_Pickup_Radius_Level = PlayerPrefs.GetInt("saved_Player_Pickup_Radius_Level");
        }

        if (!PlayerPrefs.HasKey("saved_Fire_Rate_Level"))
        {
            PlayerPrefs.SetInt("saved_Fire_Rate_Level", 1);
        }
        else
        {
            saved_Fire_Rate_Level = PlayerPrefs.GetInt("saved_Fire_Rate_Level", 1);
        }

        if (!PlayerPrefs.HasKey("saved_Rune_Drop_Ability_Level"))
        {
            PlayerPrefs.SetInt("saved_Rune_Drop_Ability_Level", 1);
        }
        else
        {
            saved_Rune_Drop_Ability_Level = PlayerPrefs.GetInt("saved_Rune_Drop_Ability_Level");
        }

        if (!PlayerPrefs.HasKey("saved_Shield_Ability_Level"))
        {
            PlayerPrefs.SetInt("saved_Shield_Ability_Level", 1);
        }

        else
        {
            saved_Shield_Ability_Level = PlayerPrefs.GetInt("saved_Shield_Ability_Level");
        }

        if (!PlayerPrefs.HasKey("saved_AOE_Special_Ability_Level"))
        {
            PlayerPrefs.SetInt("saved_AOE_Special_Ability_Level", 1);
        }

        else
        {
            saved_AOE_Special_Ability_Level = PlayerPrefs.GetInt("saved_AOE_Special_Ability_Level");
        }

        if (!PlayerPrefs.HasKey("master_Volume_Setting"))
        {
            PlayerPrefs.SetFloat("master_Volume_Setting", 7f);
        }
        else
        {
            master_Volume_Setting = PlayerPrefs.GetFloat("master_Volume_Setting");
        }

        if (!PlayerPrefs.HasKey("bgm_Volume_Setting"))
        {
            PlayerPrefs.SetFloat("bgm_Volume_Setting", 7f);
        }
        else
        {
            bgm_Volume_Setting = PlayerPrefs.GetFloat("bgm_Volume_Setting");
        }

        if (!PlayerPrefs.HasKey("sfx_Volume_Setting"))
        {
            PlayerPrefs.SetFloat("sfx_Volume_Setting", 7f);
        }
        else
        {
            sfx_Volume_Setting = PlayerPrefs.GetFloat("sfx_Volume_Setting");
        }
    }

    /// <summary>
    /// Sets all values in class to default values and saves to file
    /// </summary>
    public void Reset_Player_Progress()
    {
        PlayerPrefs.SetInt("Player_Currency", 0);
        PlayerPrefs.SetInt("Levels_Unlocked", 0);
        PlayerPrefs.SetInt("health_Level", 1);
        PlayerPrefs.SetInt("saved_Player_Pickup_Radius_Level", 1);
        PlayerPrefs.SetInt("saved_Fire_Rate_Level", 1);
        PlayerPrefs.SetInt("saved_Rune_Drop_Ability_Level", 1);
        PlayerPrefs.SetInt("saved_Shield_Ability_Level", 1);
        PlayerPrefs.SetInt("saved_AOE_Special_Ability_Level", 1);
        PlayerPrefs.SetFloat("master_Volume_Setting", 8f);
        PlayerPrefs.SetFloat("bgm_Volume_Setting", 8f);
        PlayerPrefs.SetFloat("sfx_Volume_Setting", 8f);
        PlayerPrefs.SetInt("saved_Graphics_Quality_Level", 5);
        PlayerPrefs.Save();

        player_Currency = 0;
        levels_Unlocked = 0;
        health_Level = 1;
        saved_Player_Pickup_Radius_Level = 1;
        saved_Fire_Rate_Level = 1;
        saved_Rune_Drop_Ability_Level = 1;
        saved_Shield_Ability_Level = 1;
        saved_AOE_Special_Ability_Level = 1;
        saved_Graphics_Quality_Level = 5;
        master_Volume_Setting = 7f;
        bgm_Volume_Setting = 7f;
        sfx_Volume_Setting = 7f;

        audio_Mixer.SetFloat("Master_Volume", master_Volume_Setting);
        audio_Mixer.SetFloat("BGM_Volume", bgm_Volume_Setting);
        audio_Mixer.SetFloat("SFX_Volume", sfx_Volume_Setting);
        QualitySettings.SetQualityLevel(saved_Graphics_Quality_Level);
    }


    public void Increase_Player_Currency(int amount)
    {
        player_Currency += amount;
    }

    public void Decrease_Player_Currency(int amount)
    {
        player_Currency -= amount;
    }

    public void Increase_Player_Levels_Unlocked()
    {
        levels_Unlocked++;
    }


    public void Increase_Health_Level()
    {
        health_Level++;
    }
    public int Get_Health_Level()
    {
        return health_Level;
    }

    public void Increase_Player_Pickup_Radius_Level()
    {
        saved_Player_Pickup_Radius_Level++;
    }
    public int Get_Player_Pickup_Radius_Level()
    {
        return saved_Player_Pickup_Radius_Level;
    }

    public void Increase_Fire_Rate_Level()
    {
        saved_Fire_Rate_Level++;
    }

    public int Get_Fire_Rate_Level()
    {
        return saved_Fire_Rate_Level;
    }

    public void Increase_Rune_Drop_Ability_Level()
    {
        saved_Rune_Drop_Ability_Level++;
    }

    public int Get_Rune_Drop_Ability_Level()
    {
        return saved_Rune_Drop_Ability_Level;
    }

    public void Increase_Shield_Ability_Level()
    {
        saved_Shield_Ability_Level++;
    }
    public int Get_Shield_Ability_Level()
    {
        return saved_Shield_Ability_Level;
    }

    public void Increase_AOE_Special_Ability_Level()
    {
        saved_AOE_Special_Ability_Level++;
    }
    public int Get_AOE_Special_Ability_Level()
    {
        return saved_AOE_Special_Ability_Level;
    }

    public void Set_Master_Audio_Level(float master_Level)
    {
        master_Volume_Setting = master_Level;
        audio_Mixer.SetFloat("Master_Volume", master_Volume_Setting);
    }

    public float Get_Master_Audio()
    {
        return master_Volume_Setting;
    }

    public float Get_BGM_Audio_Level()
    {
        return bgm_Volume_Setting;
    }

    public float Get_SFX_Audio_Level()
    {
        return sfx_Volume_Setting;
    }

    public void Set_BGM_Audio_Level(float BGM_Level)
    {
        bgm_Volume_Setting = BGM_Level;
        audio_Mixer.SetFloat("BGM_Volume", bgm_Volume_Setting);
    }

    public void Set_SFX_Audio_Level(float SFX_Level)
    {
        sfx_Volume_Setting = SFX_Level;
        audio_Mixer.SetFloat("SFX_Volume", sfx_Volume_Setting);
    }

    /// <summary>
    /// Saves the current selected settings and player progress to playreprefs file
    /// </summary>
    public void Save_Player_Progress()
    {
        PlayerPrefs.SetInt("Player_Currency", player_Currency);
        PlayerPrefs.SetInt("Levels_Unlocked", levels_Unlocked);
        PlayerPrefs.SetInt("health_Level", health_Level);
        PlayerPrefs.SetInt("saved_Player_Pickup_Radius_Level", saved_Player_Pickup_Radius_Level);
        PlayerPrefs.SetInt("saved_Fire_Rate_Level", saved_Fire_Rate_Level);
        PlayerPrefs.SetInt("saved_Rune_Drop_Ability_Level", saved_Rune_Drop_Ability_Level);
        PlayerPrefs.SetInt("saved_Shield_Ability_Level", saved_Shield_Ability_Level);
        PlayerPrefs.SetInt("saved_AOE_Special_Ability_Level", saved_AOE_Special_Ability_Level);
        PlayerPrefs.SetFloat("master_Volume_Setting", master_Volume_Setting);
        PlayerPrefs.SetFloat("bgm_Volume_Setting", bgm_Volume_Setting);
        PlayerPrefs.SetFloat("sfx_Volume_Setting", sfx_Volume_Setting);
        PlayerPrefs.SetInt("saved_Graphics_Quality_Level", saved_Graphics_Quality_Level);
        PlayerPrefs.SetInt("player_Selected_Resolution", player_Resolution_Index);
        PlayerPrefs.SetInt("game_Fullscreen", game_Full_Screen_Mode);
        PlayerPrefs.Save();
    }

    public int Get_Player_Currency()
    {
        return player_Currency;
    }

    public int Get_Player_Levels_Unlocked()
    {
        return levels_Unlocked;
    }

    public int Get_Saved_Graphics_Level()
    {
        return saved_Graphics_Quality_Level;
    }

    public void Set_Graphics_Setting()
    {
        saved_Graphics_Quality_Level = QualitySettings.GetQualityLevel();
        PlayerPrefs.SetInt("saved_Graphics_Quality_Level", saved_Graphics_Quality_Level);
    }

}
