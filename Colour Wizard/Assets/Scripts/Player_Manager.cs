using System.Collections;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    #region variables
    [Header("References To Be Set")]
    public Gameplay_Manager local_Gameplay_Manager;
    public Shield_Special_Ability local_Shield_Special_Ability;
    public UI_Manager local_UI_Manager;
    [HideInInspector]
    public Player_Saved_Data_Manager local_Player_Saved_Data_Manager;
    public Animator anim;
    public AudioSource Attack_Sound;
    public AudioSource Moving_Sound;
    public AudioSource Dash_Sound;
    public AudioSource Shield_Sound;
    public AudioSource Death_Sound;
    public AudioSource Aoe_Sound;
    public AudioSource Take_Damage_Sound;
    public AudioSource Rune_Sound;
    public AudioSource death_Music;

    [Header("Properties To Be Set")]
    public int red_Colour_Amount;
    public int blue_Colour_Amount;
    public int yellow_Colour_Amount;
    public int purple_Colour_Amount;
    public int green_Colour_Amount;
    public int orange_Colour_Amount;
    public int max_Ammo_Colour_Amount = 200;
    [HideInInspector]
    public int red_Damage_Reduction = 0;
    [HideInInspector]
    public int blue_Damage_Reduction = 0;
    [HideInInspector]
    public int yellow_Damage_Reduction = 0;
    [HideInInspector]
    public int purple_Damage_Reduction = 0;
    [HideInInspector]
    public int green_Damage_Reduction = 0;
    [HideInInspector]
    public int orange_Damage_Reduction = 0;
    [HideInInspector]
    public bool is_Invulnerable = false;

    //upgrade levels
    //[HideInInspector]
    public int health_Level = 1;
    //[HideInInspector]
    public int player_Pickup_Radius_Level = 1;
    //[HideInInspector]
    public int fire_Rate_Level = 1;
    //[HideInInspector]
    public int rune_Drop_Ability_Level = 1;
    //[HideInInspector]
    public int Shield_Ability_Level = 1;
    //[HideInInspector]
    public int AOE_Special_Ability_Level = 1;
    //Upgradeable stats
    public int health = 20;
    public float player_Pickup_Radius = 10f;
    public float fire_Rate = 1.4f;
    [HideInInspector]
    public int max_Health;
    bool is_Dead = false;
    Color default_Flash_COlour;



    public enum Selected_Colour
    {
        RED,
        BLUE,
        YELLOW,
        PURPLE,
        GREEN,
        ORANGE,
        DEFAULT
    }

    public Selected_Colour local_Selected_Colour;
    #endregion

    private void Awake()
    {
        local_Player_Saved_Data_Manager = GameObject.FindGameObjectWithTag("Player_Settings").GetComponent<Player_Saved_Data_Manager>();
        Initialise_Upgradeable_Stats();
        max_Health = health;
        local_UI_Manager.health_Meter.maxValue = max_Health;
        local_UI_Manager.health_Meter.value = health;
        local_UI_Manager.Ammo_Slider.maxValue = max_Ammo_Colour_Amount;
        default_Flash_COlour = local_UI_Manager.HUD_Damage_Flash.color;

    }

    /// <summary>
    /// Initialises stats based on current purchased upgrades
    /// </summary>
    void Initialise_Upgradeable_Stats()
    {

        switch (health_Level)
        {
            case 2:
                health += 5;
                break;

            case 3:
                health += 10;
                break;

            case 4:
                health += 15;
                break;

            case 5:
                health += 20;
                break;
        }

        switch (player_Pickup_Radius_Level)
        {
            case 2:
                player_Pickup_Radius += 2f;
                break;

            case 3:
                player_Pickup_Radius += 4f;
                break;

            case 4:
                player_Pickup_Radius += 6f;
                break;

            case 5:
                player_Pickup_Radius += 8f;
                break;
        }

        switch (fire_Rate_Level)
        {
            case 2:
                fire_Rate -= .2f;
                break;

            case 3:
                fire_Rate -= .4f;
                break;

            case 4:
                fire_Rate -= .6f;
                break;

            case 5:
                fire_Rate -= .8f;
                break;
        }
    }


    /// <summary>
    /// increases the player ammo count for a particular colour
    /// </summary>
    /// <param name="colour_Type"></param>
    /// <param name="amount"></param>
    public void Receive_Colours(int colour_Type, int amount)
    {
        switch (colour_Type)
        {
            case 0:
                red_Colour_Amount += amount;
                if (red_Colour_Amount > max_Ammo_Colour_Amount)
                {
                    red_Colour_Amount = max_Ammo_Colour_Amount;
                }
                break;

            case 1:
                blue_Colour_Amount += amount;
                if (blue_Colour_Amount > max_Ammo_Colour_Amount)
                {
                    blue_Colour_Amount = max_Ammo_Colour_Amount;
                }
                break;

            case 2:
                yellow_Colour_Amount += amount;
                if (yellow_Colour_Amount > max_Ammo_Colour_Amount)
                {
                    yellow_Colour_Amount = max_Ammo_Colour_Amount;
                }
                break;

            case 3:
                purple_Colour_Amount += amount;
                if (purple_Colour_Amount > max_Ammo_Colour_Amount)
                {
                    purple_Colour_Amount = max_Ammo_Colour_Amount;
                }
                break;

            case 4:
                green_Colour_Amount += amount;
                if (green_Colour_Amount > max_Ammo_Colour_Amount)
                {
                    green_Colour_Amount = max_Ammo_Colour_Amount;
                }
                break;

            case 5:
                orange_Colour_Amount += amount;
                if (orange_Colour_Amount > max_Ammo_Colour_Amount)
                {
                    orange_Colour_Amount = max_Ammo_Colour_Amount;
                }
                break;
        }
    }

    public int Get_Player_Health()
    {
        return health;
    }

    /// <summary>
    /// Sets player flashing after taking damage
    /// </summary>
    /// <returns></returns>
    IEnumerator Damage_Flash()
    {
        local_UI_Manager.HUD_Damage_Flash.enabled = true;
        local_UI_Manager.HUD_Damage_Flash.color = default_Flash_COlour;
        var tempColor = local_UI_Manager.HUD_Damage_Flash.color;
        tempColor.a = .5f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .45f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .4f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .35f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .30f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .25f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .20f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .15f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .10f;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
        yield return new WaitForSeconds(.02f);
        tempColor.a = .05f;

        local_UI_Manager.HUD_Damage_Flash.enabled = false;
        local_UI_Manager.HUD_Damage_Flash.color = tempColor;
    }


    /// <summary>
    /// Handles the player taking damage, applies any reductions that are in place due to other modifiers
    /// </summary>
    /// <param name="amount">Base amount of damage</param>
    /// <param name="colour_Type">Colour of damage</param>
    public void Take_Damage(int amount, int colour_Type)
    {
        if (!is_Dead)
        {
            Take_Damage_Sound.Play();
            StopCoroutine("Damage_Flash");
            StartCoroutine("Damage_Flash");
        }



        else
        {
            local_UI_Manager.HUD_Damage_Flash.enabled = false;
        }


        switch (colour_Type)
        {
            case 0:
                amount -= red_Damage_Reduction;
                if (red_Damage_Reduction == local_Shield_Special_Ability.selected_Colour_Reduction)
                {
                    local_Gameplay_Manager.number_Of_Correct_Colour_Blocked++;
                }
                if (amount <= 0)
                {
                    amount = 1;
                }
                break;

            case 1:
                amount -= blue_Damage_Reduction;
                if (blue_Damage_Reduction == local_Shield_Special_Ability.selected_Colour_Reduction)
                {
                    local_Gameplay_Manager.number_Of_Correct_Colour_Blocked++;
                }
                if (amount <= 0)
                {
                    amount = 1;
                }
                break;

            case 2:
                amount -= yellow_Damage_Reduction;
                if (yellow_Damage_Reduction == local_Shield_Special_Ability.selected_Colour_Reduction)
                {
                    local_Gameplay_Manager.number_Of_Correct_Colour_Blocked++;
                }
                if (amount <= 0)
                {
                    amount = 1;
                }
                break;

            case 3:
                amount -= purple_Damage_Reduction;
                if (purple_Damage_Reduction == local_Shield_Special_Ability.selected_Colour_Reduction)
                {
                    local_Gameplay_Manager.number_Of_Correct_Colour_Blocked++;
                }
                if (amount <= 0)
                {
                    amount = 1;
                }
                break;

            case 4:
                amount -= green_Damage_Reduction;
                if (green_Damage_Reduction == local_Shield_Special_Ability.selected_Colour_Reduction)
                {
                    local_Gameplay_Manager.number_Of_Correct_Colour_Blocked++;
                }
                if (amount <= 0)
                {
                    amount = 1;
                }
                break;

            case 5:
                amount -= orange_Damage_Reduction;
                if (orange_Damage_Reduction == local_Shield_Special_Ability.selected_Colour_Reduction)
                {
                    local_Gameplay_Manager.number_Of_Correct_Colour_Blocked++;
                }
                if (amount <= 0)
                {
                    amount = 1;
                }
                break;
        }
        if (!is_Invulnerable)
        {
            health -= amount;
            local_UI_Manager.health_Meter.value = health;

        }

        if (health <= 0 && !is_Dead)
        {
            health = 0;
            //local_UI_Manager.health.text = health.ToString();
            local_UI_Manager.health_Meter.value = health;
            local_Gameplay_Manager.StartCoroutine("Level_Failed");
            //Setting animation parameter
            anim.SetTrigger("Death");
            //play death sound
            Death_Sound.Play();
            local_Gameplay_Manager.level_Music.SetActive(false);
            if (local_Gameplay_Manager.boss_Level && local_Gameplay_Manager.boss_Music.isPlaying)
            {
                local_Gameplay_Manager.boss_Music.Stop();
            }
            death_Music.Play();
            is_Dead = true;
        }
    }


}
