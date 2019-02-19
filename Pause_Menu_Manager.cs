using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Manager : MonoBehaviour
{
    [Header("References To Be Set")]
    public GameObject pause_Panel;
    public GameObject options_Panel;
    public GameObject Options_Menu;
    public GameObject cursor_Image;
    public Player_Movement_Controls local_Player_Movement_Controls;
    public Player_Input local_Player_Input;
    public UI_Manager local_UI_Manager;
    [HideInInspector]
    public bool isPaused = false;
    bool audio_Otions_Is_Active;
    public bool is_Tutorial_Level = false;
    public Tutorial_Manager local_Tutorial_Manager;


    void Update()
    {

        if (Input.GetButtonDown("Pause Game"))
        {
            Pause_Game();
        }

        if (isPaused)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (!audio_Otions_Is_Active)
                {
                    Resume();
                }
                else
                {
                    Go_Back_To_Pause_Menu();
                }
            }
        }

    }

    public void Resume()
    {
        Pause_Game();
    }


    public void Load_Options_Menu()
    {
        options_Panel.SetActive(true);
        audio_Otions_Is_Active = true;
    }

    public void Go_Back_To_Pause_Menu()
    {
        options_Panel.SetActive(false);
        audio_Otions_Is_Active = false;
    }

    public void Load_Main_Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit_To_Windows()
    {
        Application.Quit();
    }


    /// <summary>
    /// pauses the game or unpauses it by setting the timescale and bringing up pause menu UI elements
    /// </summary>
    public void Pause_Game()
    {
        if (!isPaused)
        {
            Cursor.visible = true;
            cursor_Image.SetActive(false);
            pause_Panel.SetActive(true);
            local_Player_Input.enabled = false;
            local_Player_Movement_Controls.enabled = false;
            isPaused = true;
            local_UI_Manager.Display_Bonus_Objective();
            if (local_Tutorial_Manager != null)
            {
                if (is_Tutorial_Level && !local_Tutorial_Manager.tutorial_Finished)
                {
                    local_UI_Manager.tutorial_Panel.SetActive(false);
                }
            }

            // Gameplay.SetActive(false);
            Time.timeScale = 0f;
        }

        else
        {
            Time.timeScale = 1f;
            cursor_Image.SetActive(true);
            Cursor.visible = false;
            Options_Menu.SetActive(false);
            // Gameplay.SetActive(true);
            local_UI_Manager.Hide_Bonus_Objective();
            if (local_Tutorial_Manager != null)
            {
                if (is_Tutorial_Level && !local_Tutorial_Manager.tutorial_Finished)
                {
                    local_UI_Manager.tutorial_Panel.SetActive(true);
                }
            }

            pause_Panel.SetActive(false);
            local_Player_Input.enabled = true;
            local_Player_Movement_Controls.enabled = true;
            isPaused = false;
        }
    }


}
