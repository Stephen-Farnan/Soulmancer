using System.Collections;
using UnityEngine;
//using UnityEngine.UI;

public class Shield_Special_Ability : MonoBehaviour
{
    #region variables

    [Header("References To Be Set")]
    public Player_Manager local_Player_manager;
    public UI_Manager local_UI_Manager;
    public Player_Input local_Player_Input;
    public GameObject Shield_Particles_Red;
    public GameObject Shield_Particles_Yellow;
    public GameObject Shield_Particles_Blue;
    public GameObject Shield_Particles_Purple;
    public GameObject Shield_Particles_Green;
    public GameObject Shield_Particles_Orange;
    public GameObject Shield_Particles_Default;

    bool shield_Is_On_Cooldown;

    [Header("Properties To Be Set")]
    [HideInInspector]
    public int selected_Colour_Reduction_Level = 1;
    [HideInInspector]
    public int base_Reduction_Level = 1;
    [HideInInspector]
    public int shield_Duration_Level = 1;
    [HideInInspector]
    public int cooldown_Level = 1;
    //upgradeable stats
    public int selected_Colour_Reduction = 5;
    public int base_Reduction = 2;
    public float shield_Duration;
    public float cooldown = 8;
    bool can_Cast = true;

    Animator anim;
    #endregion

    private void Awake()
    {
        Initialise_Upgradeable_Stats();
        anim = local_Player_manager.anim;

    }

    /// <summary>
    /// Sets stats based on purchased upgrades
    /// </summary>
    void Initialise_Upgradeable_Stats()
    {

        selected_Colour_Reduction_Level = local_Player_manager.Shield_Ability_Level;
        base_Reduction_Level = local_Player_manager.Shield_Ability_Level;
        shield_Duration_Level = local_Player_manager.Shield_Ability_Level;
        cooldown_Level = local_Player_manager.Shield_Ability_Level;


        switch (selected_Colour_Reduction_Level)
        {
            case 2:
                selected_Colour_Reduction += 2;
                break;

            case 3:
                selected_Colour_Reduction += 4;
                break;

            case 4:
                selected_Colour_Reduction += 6;
                break;

            case 5:
                selected_Colour_Reduction += 8;
                break;

        }

        switch (base_Reduction_Level)
        {
            case 2:
                base_Reduction += 2;
                break;

            case 3:
                base_Reduction += 4;
                break;

            case 4:
                base_Reduction += 6;
                break;

            case 5:
                base_Reduction += 8;
                break;
        }

        switch (shield_Duration_Level)
        {
            case 2:
                shield_Duration += 2f;
                break;

            case 3:
                shield_Duration += 4f;
                break;

            case 4:
                shield_Duration += 6f;
                break;

            case 5:
                shield_Duration += 8f;
                break;

        }

        switch (cooldown_Level)
        {
            case 2:
                cooldown -= 1f;
                break;

            case 3:
                cooldown -= 1f;
                break;

            case 4:
                cooldown -= 1f;
                break;

            case 5:
                cooldown -= 1f;
                break;

        }



    }


    /// <summary>
    /// checks for the players selected colour, and sets damage reductions to certain colours based on this
    /// </summary>
    /// <param name="colour"></param>
    public void Shield_Ability(int colour)
    {
        if (can_Cast)
        {
            //Setting animation trigger for ability
            anim.SetTrigger("Spell");
            local_Player_manager.Shield_Sound.Play();
            local_Player_Input.StopCoroutine("Halt_Player");
            local_Player_Input.StartCoroutine("Halt_Player");
            can_Cast = false;
            local_UI_Manager.shield_Cooldown_Timer = cooldown;
            switch (colour)
            {
                case 0:
                    Shield_Particles_Red.SetActive(true);
                    local_Player_manager.red_Damage_Reduction = selected_Colour_Reduction;
                    local_Player_manager.blue_Damage_Reduction = base_Reduction;
                    local_Player_manager.yellow_Damage_Reduction = base_Reduction;
                    local_Player_manager.purple_Damage_Reduction = base_Reduction;
                    local_Player_manager.green_Damage_Reduction = base_Reduction;
                    local_Player_manager.orange_Damage_Reduction = base_Reduction;

                    StartCoroutine("End_Shield", Shield_Particles_Red);
                    break;

                case 1:
                    Shield_Particles_Blue.SetActive(true);
                    local_Player_manager.red_Damage_Reduction = base_Reduction;
                    local_Player_manager.blue_Damage_Reduction = selected_Colour_Reduction;
                    local_Player_manager.yellow_Damage_Reduction = base_Reduction;
                    local_Player_manager.purple_Damage_Reduction = base_Reduction;
                    local_Player_manager.green_Damage_Reduction = base_Reduction;
                    local_Player_manager.orange_Damage_Reduction = base_Reduction;
                    StartCoroutine("End_Shield", Shield_Particles_Blue);
                    break;

                case 2:
                    Shield_Particles_Yellow.SetActive(true);
                    local_Player_manager.red_Damage_Reduction = base_Reduction;
                    local_Player_manager.blue_Damage_Reduction = base_Reduction;
                    local_Player_manager.yellow_Damage_Reduction = selected_Colour_Reduction;
                    local_Player_manager.purple_Damage_Reduction = base_Reduction;
                    local_Player_manager.green_Damage_Reduction = base_Reduction;
                    local_Player_manager.orange_Damage_Reduction = base_Reduction;
                    StartCoroutine("End_Shield", Shield_Particles_Yellow);
                    break;

                case 3:
                    Shield_Particles_Purple.SetActive(true);
                    local_Player_manager.red_Damage_Reduction = base_Reduction;
                    local_Player_manager.blue_Damage_Reduction = base_Reduction;
                    local_Player_manager.yellow_Damage_Reduction = base_Reduction;
                    local_Player_manager.purple_Damage_Reduction = selected_Colour_Reduction;
                    local_Player_manager.green_Damage_Reduction = base_Reduction;
                    local_Player_manager.orange_Damage_Reduction = base_Reduction;
                    StartCoroutine("End_Shield", Shield_Particles_Purple);
                    break;

                case 4:
                    Shield_Particles_Green.SetActive(true);
                    local_Player_manager.red_Damage_Reduction = base_Reduction;
                    local_Player_manager.blue_Damage_Reduction = base_Reduction;
                    local_Player_manager.yellow_Damage_Reduction = base_Reduction;
                    local_Player_manager.purple_Damage_Reduction = base_Reduction;
                    local_Player_manager.green_Damage_Reduction = selected_Colour_Reduction;
                    local_Player_manager.orange_Damage_Reduction = base_Reduction;
                    StartCoroutine("End_Shield", Shield_Particles_Green);
                    break;

                case 5:
                    Shield_Particles_Orange.SetActive(true);
                    local_Player_manager.red_Damage_Reduction = base_Reduction;
                    local_Player_manager.blue_Damage_Reduction = base_Reduction;
                    local_Player_manager.yellow_Damage_Reduction = base_Reduction;
                    local_Player_manager.purple_Damage_Reduction = base_Reduction;
                    local_Player_manager.green_Damage_Reduction = base_Reduction;
                    local_Player_manager.orange_Damage_Reduction = selected_Colour_Reduction;
                    StartCoroutine("End_Shield", Shield_Particles_Orange);
                    break;

                case 6:
                    Shield_Particles_Default.SetActive(true);
                    local_Player_manager.red_Damage_Reduction = base_Reduction;
                    local_Player_manager.blue_Damage_Reduction = base_Reduction;
                    local_Player_manager.yellow_Damage_Reduction = base_Reduction;
                    local_Player_manager.purple_Damage_Reduction = base_Reduction;
                    local_Player_manager.green_Damage_Reduction = base_Reduction;
                    local_Player_manager.orange_Damage_Reduction = base_Reduction;
                    StartCoroutine("End_Shield", Shield_Particles_Default);
                    break;

                default:
                    break;
            }

            StartCoroutine("Cooldown_Reset");
        }





    }



    IEnumerator Cooldown_Reset()
    {
        yield return new WaitForSeconds(cooldown);
        can_Cast = true;
    }


    /// <summary>
    /// after a certain time, removes the damage reeductions
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    IEnumerator End_Shield(GameObject go)
    {
        yield return new WaitForSeconds(shield_Duration);
        local_Player_manager.red_Damage_Reduction = 0;
        local_Player_manager.blue_Damage_Reduction = 0;
        local_Player_manager.yellow_Damage_Reduction = 0;
        local_Player_manager.purple_Damage_Reduction = 0;
        local_Player_manager.green_Damage_Reduction = 0;
        local_Player_manager.orange_Damage_Reduction = 0;
        go.SetActive(false);
    }
}
