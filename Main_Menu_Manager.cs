using UnityEngine;
using UnityEngine.UI;

public class Main_Menu_Manager : MonoBehaviour
{
    [HideInInspector]
    public Player_Saved_Data_Manager local_Player_Saved_Data_Manager;
    public UI_Manager local_UI_Manager;

    [Header("References To Be Set")]
    public GameObject Level_Select_Panel;
    public GameObject Options_Panel;

    public Texture2D cursor_Image_Default;

    public Button Continue;
    public Button Level_One;
    public Button Level_Two;
    public Button Level_Three;
    public Button Level_Four;
    public Button Level_Five;
    public Button Level_Six;
    public Toggle windowed_Toggle;

    public Dropdown resolution_Options;
    int resolution_Active;

    public bool is_Game_Fullscreen;


    public void Set_Resolution()
    {
        resolution_Active = resolution_Options.value;
        Change_Resolution(resolution_Active);
    }

    public void Set_Windowed_Mode()
    {
        if (is_Game_Fullscreen)
        {
            is_Game_Fullscreen = false;
            Screen.fullScreen = false;
            Change_Resolution(resolution_Options.value);
            local_Player_Saved_Data_Manager.game_Full_Screen_Mode = 0;
        }

        else
        {
            is_Game_Fullscreen = true;
            Screen.fullScreen = true;
            Change_Resolution(resolution_Options.value);
            local_Player_Saved_Data_Manager.game_Full_Screen_Mode = 1;
        }

        local_Player_Saved_Data_Manager.Save_Player_Progress();

    }



    /// <summary>
    /// Sets references and initial vaues for the menu on startup
    /// </summary>
    private void Awake()
    {
        local_Player_Saved_Data_Manager = GameObject.FindGameObjectWithTag("Player_Settings").GetComponent<Player_Saved_Data_Manager>();
        Check_Can_Continue();
        Initialise_Level_Select();
        Cursor.visible = false;

        Cursor.SetCursor(cursor_Image_Default, Vector2.zero, CursorMode.ForceSoftware);

        if (local_Player_Saved_Data_Manager.game_Full_Screen_Mode == 1)
        {
            windowed_Toggle.isOn = false;
            is_Game_Fullscreen = true;
        }

        else
        {
            windowed_Toggle.isOn = true;
            is_Game_Fullscreen = false;
        }


        Initialise_Resolution_Selected();
        Change_Resolution(resolution_Options.value);
        local_UI_Manager.Update_Graphics_Options();

    }



    /// <summary>
    /// If there is save data, allows the player to continue
    /// </summary>
    public void Check_Can_Continue()
    {
        if (local_Player_Saved_Data_Manager.Get_Player_Levels_Unlocked() <= 0)
        {
            Continue.interactable = false;
        }
        else
        {
            Continue.interactable = true;

        }
    }



    /// <summary>
    /// Sets the resolution of the application to the chosen option
    /// </summary>
    /// <param name="resolution_Selected"></param>
    public void Change_Resolution(int resolution_Selected)
    {

        switch (resolution_Options.value)
        {
            case 0:
                Screen.SetResolution(640, 480, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 0;
                break;

            case 1:
                Screen.SetResolution(720, 480, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 1;
                break;

            case 2:
                Screen.SetResolution(720, 576, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 1;
                break;

            case 3:
                Screen.SetResolution(800, 600, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 2;
                break;

            case 4:
                Screen.SetResolution(1024, 768, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 3;
                break;

            case 5:
                Screen.SetResolution(1152, 864, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 4;
                break;

            case 6:
                Screen.SetResolution(1280, 720, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 5;
                break;

            case 7:
                Screen.SetResolution(1280, 768, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 5;
                break;

            case 8:
                Screen.SetResolution(1280, 800, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 5;
                break;

            case 9:
                Screen.SetResolution(1280, 960, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 5;
                break;

            case 10:
                Screen.SetResolution(1280, 1024, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 5;
                break;

            case 11:
                Screen.SetResolution(1360, 768, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 6;
                break;

            case 12:
                Screen.SetResolution(1440, 900, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 7;
                break;

            case 13:
                Screen.SetResolution(1600, 900, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 8;
                break;

            case 14:
                Screen.SetResolution(1600, 1024, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 8;
                break;

            case 15:
                Screen.SetResolution(1680, 1050, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 9;
                break;

            case 16:
                Screen.SetResolution(1920, 1024, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 10;
                break;

            case 17:
                Screen.SetResolution(2715, 1527, is_Game_Fullscreen);
                local_Player_Saved_Data_Manager.player_Resolution_Index = 11;
                break;


        }

        local_Player_Saved_Data_Manager.Save_Player_Progress();

    }

    void Initialise_Resolution_Selected()
    {
        switch (local_Player_Saved_Data_Manager.player_Resolution_Index)
        {
            case 0:
                resolution_Options.value = 0;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 0;
                break;

            case 1:
                resolution_Options.value = 2;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 1;
                break;

            case 2:
                resolution_Options.value = 3;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 2;
                break;

            case 3:
                resolution_Options.value = 4;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 3;
                break;

            case 4:
                resolution_Options.value = 5;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 4;
                break;

            case 5:
                resolution_Options.value = 7;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 5;
                break;

            case 6:
                resolution_Options.value = 11;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 6;
                break;

            case 7:
                resolution_Options.value = 12;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 7;
                break;

            case 8:
                resolution_Options.value = 13;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 8;
                break;

            case 9:
                resolution_Options.value = 15;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 9;
                break;

            case 10:
                resolution_Options.value = 16;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 10;
                break;

            case 11:
                resolution_Options.value = 17;
                local_Player_Saved_Data_Manager.player_Resolution_Index = 11;
                break;


        }

        local_Player_Saved_Data_Manager.Save_Player_Progress();
    }

    public void Load_Level_Select_Panel()
    {
        Initialise_Level_Select();
        Level_Select_Panel.SetActive(true);
    }

    public void Load_Options_Panel()
    {
        Options_Panel.SetActive(true);
    }

    public void Load_Main_Menu()
    {
        Level_Select_Panel.SetActive(false);
        Options_Panel.SetActive(false);
    }

    /// <summary>
    /// Sets level options active or inactive depending on whether they have been unlocked or not
    /// </summary>
    public void Initialise_Level_Select()
    {
        switch (local_Player_Saved_Data_Manager.Get_Player_Levels_Unlocked())
        {
            case 0:
                Level_One.interactable = true;
                Level_Two.interactable = false;
                Level_Three.interactable = false;
                Level_Four.interactable = false;
                Level_Five.interactable = false;
                Level_Six.interactable = false;
                break;

            case 1:
                Level_One.interactable = true;
                Level_Two.interactable = true;
                Level_Three.interactable = false;
                Level_Four.interactable = false;
                Level_Five.interactable = false;
                Level_Six.interactable = false;
                break;

            case 2:
                Level_One.interactable = true;
                Level_Two.interactable = true;
                Level_Three.interactable = true;
                Level_Four.interactable = false;
                Level_Five.interactable = false;
                Level_Six.interactable = false;
                break;

            case 3:
                Level_One.interactable = true;
                Level_Two.interactable = true;
                Level_Three.interactable = true;
                Level_Four.interactable = true;
                Level_Five.interactable = false;
                Level_Six.interactable = false;
                break;

            case 4:
                Level_One.interactable = true;
                Level_Two.interactable = true;
                Level_Three.interactable = true;
                Level_Four.interactable = true;
                Level_Five.interactable = true;
                Level_Six.interactable = false;
                break;

            case 5:
                Level_One.interactable = true;
                Level_Two.interactable = true;
                Level_Three.interactable = true;
                Level_Four.interactable = true;
                Level_Five.interactable = true;
                Level_Six.interactable = true;
                break;

            default:
                Level_One.interactable = true;
                Level_Two.interactable = true;
                Level_Three.interactable = true;
                Level_Four.interactable = true;
                Level_Five.interactable = true;
                Level_Six.interactable = true;
                break;


        }
    }





}
