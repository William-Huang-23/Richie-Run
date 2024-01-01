using UnityEngine;

public class GameData : MonoBehaviour
{
    [Header("DATA")]

    [Header("LOCAL")]

    private bool data_ready = true;

    private int character = 0;
    private int level = 0;

    private bool on_PC = false;
    
    private bool first_main_menu = false;

    [Header("PLAYER PROFILE")]

    private string display_name;
    private int money = 0;
    private int kidal = 0;
    private int login_streak = 0;
    private string last_seen;

    [Header("INVESTMENT DATA")]

    private int chosen_risk_level = 0;

    [Header("SHOP DATA")]

    private int income = 0;
    private int shield = 0;
    private int magnet = 0;
    private int atlet = 0;
    private int dokter = 0;
    private int dam = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // local

    public void set_data_ready(bool data_ready)
    {
        this.data_ready = data_ready;
    }

    public bool get_data_ready()
    {
        return data_ready;
    }

    public void set_character(int character)
    {
        this.character = character;
    }

    public int get_character()
    {
        return character;
    }

    public void set_level(int level)
    {
        this.level = level;
    }

    public int get_level()
    {
        return level;
    }

    public void set_on_PC(bool on_PC)
    {
        this.on_PC = on_PC;
    }

    public bool get_on_PC()
    {
        return on_PC;
    }

    public void set_first_main_menu(bool first_main_menu)
    {
        this.first_main_menu = first_main_menu;
    }

    public bool get_first_main_menu()
    {
        return first_main_menu;
    }

    // player profile

    public void set_display_name(string display_name)
    {
        this.display_name = display_name;
    }

    public string get_display_name()
    {
        return display_name;
    }

    public void set_money(int money)
    {
        this.money = money;
    }

    public int get_money()
    {
        return money;
    }

    public void set_kidal(int kidal)
    {
        this.kidal = kidal;
    }

    public int get_kidal()
    {
        return kidal;
    }

    public void set_login_streak(int login_streak)
    {
        this.login_streak = login_streak;
    }

    public int get_login_streak()
    {
        return login_streak;
    }

    public void set_last_seen(string last_seen)
    {
        this.last_seen = last_seen;
    }

    public string get_last_seen()
    {
        return last_seen;
    }

    // investment data

    public void set_chosen_risk_level(int chosen_risk_level)
    {
        this.chosen_risk_level = chosen_risk_level;
    }

    public int get_chosen_risk_level()
    {
        return chosen_risk_level;
    }

    // shop data

    public void set_income(int income)
    {
        this.income = income;
    }

    public int get_income()
    {
        return income;
    }

    public void set_shield(int shield)
    {
        this.shield = shield;
    }

    public int get_shield()
    {
        return shield;
    }

    public void set_magnet(int magnet)
    {
        this.magnet = magnet;
    }

    public int get_magnet()
    {
        return magnet;
    }

    public void set_atlet(int atlet)
    {
        this.atlet = atlet;
    }

    public int get_atlet()
    {
        return atlet;
    }

    public void set_dokter(int dokter)
    {
        this.dokter = dokter;
    }

    public int get_dokter()
    {
        return dokter;
    }

    public void set_dam(int dam)
    {
        this.dam = dam;
    }

    public int get_dam()
    {
        return dam;
    }
}