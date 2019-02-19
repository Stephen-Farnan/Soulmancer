using System.Collections;
using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{

    /// <summary>
    /// This script holds all the functionality for the triggers in the tutorial section
    /// </summary>
    /// 

    [Header("Reference To Be Set")]
    public Gameplay_Manager local_Gameplay_manager;
    public UI_Manager local_UI_Manager;
    public Player_Input local_Player_Input;

    public GameObject enemy_To_Teach_Shooting;
    public GameObject enemy_To_Teach_Colours;
    public GameObject enemy_To_Teach_AOE;
    public GameObject enemy_To_Teach_Shield;
    public GameObject enemy_To_Teach_Runes;

    public GameObject rune_UI;
    public GameObject aoe_UI;
    public GameObject shield_UI;

    public GameObject Player_Blocker;

    public AudioSource tutorial_Beep;

    public bool tutorial_Finished = false;



    bool first_Stage_Passed;
    bool second_Stage_Passed;
    bool third_Stage_Passed;
    bool fourth_Stage_Passed;
    bool fifth_Stage_Passed;
    bool sixth_Stage_Passed;

    private void Start()
    {
        local_Player_Input.can_Change_Colours = false;
        local_Player_Input.can_Use_AOE = false;
        local_Player_Input.can_Use_Runes = false;
        local_Player_Input.can_Use_Shield = false;

        rune_UI.SetActive(false);
        aoe_UI.SetActive(false);
        shield_UI.SetActive(false);
    }

    public void Trigger_Movement_Tutorial()
    {
        Disply_Tutorial_Text("Use WASD to move your character around the scene.");
    }

    public void Trigger_Dash_Tutorial()
    {

        Disply_Tutorial_Text("Press spacebar to dash forward a short distance.");
    }

    public void Trigger_Shooting_Tutorial()
    {
        enemy_To_Teach_Shooting.GetComponent<Enemy_AI>().enabled = true;
        Disply_Tutorial_Text("Aim with the mouse and left click to fire, hold the mouse button down to continously fire.");

    }

    public void Trigger_Colours_Tutorial()
    {
        local_Player_Input.can_Change_Colours = true;
        enemy_To_Teach_Colours.GetComponent<Enemy_AI>().enabled = true;
        Disply_Tutorial_Text("Use the mouse wheel to change your currently selected colour. Using a colour that an enemy is weak against will deal more damage and build up your combo. Consult the colour triangles below the minimap to see which colours are strong vs certain colours of enemies.");
    }

    public void Trigger_AOE_Tutorial()
    {
        aoe_UI.SetActive(true);
        local_Player_Input.can_Use_AOE = true;
        enemy_To_Teach_AOE.GetComponent<Enemy_AI>().enabled = true;
        Disply_Tutorial_Text("Press 1 to fire a area of effect knockback that also slightly damages enemies. The colour of your abilities is determined by which colour you have selected.");
    }

    public void Trigger_Shield_Tutorial()
    {
        shield_UI.SetActive(true);
        local_Player_Input.can_Use_Shield = true;
        enemy_To_Teach_Shield.GetComponent<Enemy_AI>().enabled = true;
        Disply_Tutorial_Text("Press 2 to gain a shield that reduces incoming damage for a short duration. It reduces all damage but greatly reduces damage of the same colour as the shield.");
    }

    public void Trigger_Runes_Tutorial()
    {
        rune_UI.SetActive(true);
        local_Player_Input.can_Use_Runes = true;
        enemy_To_Teach_Runes.GetComponent<Enemy_AI>().enabled = true;
        Disply_Tutorial_Text("Press 3 to place a rune at your location. Runes trigger when enemies walk into range, and explode, slowing down all enemies around. Enemies with a colour weak to that of the rune will be slowed even more.");
    }

    public void Trigger_Health_Fountain_Tutorial()
    {
        Disply_Tutorial_Text("Walk into a health shrine when it is active to regain some health. Health shrines recharge after a certain amount of time.");
    }

    public void Trigger_Mixing_Crystal_Tutorial()
    {
        Disply_Tutorial_Text("You can click on crystals to mix any two of red, blue, and yellow. If you have enough souls of the colour selected they are sent to the crystal. Click again with another colour to mix them.");
    }

    void Disply_Tutorial_Text(string tutorial_Text)
    {
        local_UI_Manager.tutorial_Panel.SetActive(true);
        local_UI_Manager.tutorial_Text_Box.text = tutorial_Text;
        tutorial_Beep.Play();

    }



    IEnumerator end_Tutorial()
    {
        Player_Blocker.SetActive(true);
        StartCoroutine("Disply_Tutorial_Text", "Enemies will spawn in a number of waves. Defeat all the waves to complete the level.");
        yield return new WaitForSeconds(3f);
        StartCoroutine("Disply_Tutorial_Text", "You have totems to help you defend, shown as white marks on your minimap. You have a number of minor totems and one central one. If the central totem falls you will fail the level");
        yield return new WaitForSeconds(5f);
        StartCoroutine("Disply_Tutorial_Text", "Waves Spawning");
        yield return new WaitForSeconds(3f);
        local_UI_Manager.tutorial_Text_Box.text = "";
        local_UI_Manager.tutorial_Panel.SetActive(false);
        tutorial_Finished = true;
        local_Gameplay_manager.StartCoroutine("Delay_Start_Of_Level");
    }
}
