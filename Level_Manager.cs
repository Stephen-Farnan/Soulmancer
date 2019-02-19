using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{

    /// <summary>
    /// This class handles moving between different levels
    /// </summary>
    /// 
    public UI_Manager local_UI_Manager;

    private void Awake()
    {
        local_Player_Saved_Data_Manager = GameObject.FindGameObjectWithTag("Player_Settings").GetComponent<Player_Saved_Data_Manager>();

    }
    [HideInInspector]
    public Player_Saved_Data_Manager local_Player_Saved_Data_Manager;

    public void Load_Main_Menu()
    {
        SceneManager.LoadScene("Main_Menu");
        if (local_UI_Manager != null)
        {
            local_UI_Manager.Loading_Screen_Set_Active();
        }

    }

    public void Load_Level_One()
    {
        SceneManager.LoadScene("Level_01");
        local_UI_Manager.Loading_Screen_Set_Active();
    }

    public void Load_Level_Two()
    {
        SceneManager.LoadScene("Level_02");
        local_UI_Manager.Loading_Screen_Set_Active();
    }

    public void Load_Level_Three()
    {
        SceneManager.LoadScene("Level_03");
        local_UI_Manager.Loading_Screen_Set_Active();
    }

    public void Play_New_Game()
    {
        Load_Level_One();
    }

    public void Load_Credits_Scene()
    {
        SceneManager.LoadScene("Credits_Scene");
        local_UI_Manager.Loading_Screen_Set_Active();
    }

    public void Continue_Progress()
    {
        //load current level TODO when we have player progression
        switch (local_Player_Saved_Data_Manager.Get_Player_Levels_Unlocked())
        {
            case 0:
                Load_Level_One();
                break;

            case 1:
                Load_Level_Two();
                break;

            case 2:
                Load_Level_Three();
                break;

            default:
                Load_Level_Three();
                break;

        }

    }


    public void Quit_Game()
    {
        Application.Quit();
    }
}
