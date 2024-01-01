using UnityEngine;

public class PlayerProfile
{
    public string display_name;
    public int money;
    public int kidal;
    public int login_streak;
    public string last_seen;

    public PlayerProfile(string display_name, int money, int kidal, int login_streak, string last_seen)
    {
        this.display_name = display_name;
        this.money = money;
        this.kidal = kidal;
        this.login_streak = login_streak;
        this.last_seen = last_seen;
    }
}

public class InvestmentData
{
    public int chosen_risk_level;
    public string last_changed;
    public string last_seen;
    public int[,,] unit_count;
    public int[,,] unit_price;
    public float[,,,] history;
    
    public InvestmentData(int chosen_risk_level, string last_changed, string last_seen, int[,,] unit_count, int[,,] unit_price, float[,,,] history)
    {
        this.chosen_risk_level = chosen_risk_level;
        this.last_changed = last_changed;
        this.last_seen = last_seen;
        this.unit_count = unit_count;
        this.unit_price = unit_price;
        this.history = history;
    }
}

public class ShopData
{
    public int income;
    public int shield;
    public int magnet;
    public int atlet;
    public int dokter;
    public int dam;

    public ShopData(int income, int shield, int magnet, int atlet, int dokter, int dam)
    {
        this.income = income;
        this.shield = shield;
        this.magnet = magnet;
        this.atlet = atlet;
        this.dokter = dokter;
        this.dam = dam;
    }
}

public class AchievementsData
{
    public int[,] achievements_data = new int[10, 2];

    public AchievementsData(int[,] achievements_data)
    {
        this.achievements_data = achievements_data;
    }
}

public class Data : MonoBehaviour
{
    public PlayerProfile return_player_profile(string display_name, int money, int kidal, int login_streak, string last_seen)
    {
        return new PlayerProfile(display_name, money, kidal, login_streak, last_seen);
    }

    public InvestmentData return_investment_products(int chosen_risk_level, string last_changed, string last_seen, int[,,] unit_count, int[,,] unit_price, float[,,,] history)
    {
        return new InvestmentData(chosen_risk_level, last_changed, last_seen, unit_count, unit_price, history);
    }

    public ShopData return_shop_items(int income, int shield, int magnet, int atlet, int dokter, int dam)
    {
        return new ShopData(income, shield, magnet, atlet, dokter, dam);
    }

    public AchievementsData return_achievements_data(int[,] achievements_data)
    {
        return new AchievementsData(achievements_data);
    }
}
