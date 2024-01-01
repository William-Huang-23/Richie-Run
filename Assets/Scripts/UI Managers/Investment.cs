using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;


public class Investment : MonoBehaviour
{
    [Header("GAME DATA")]

    private string[,,] product = new string[2, 3, 2];
    private string[] investment_manager = { "PT. A", "PT. B" };
    private string[,,] product_type = new string[2, 3, 2];
    private string[] risk_level = { "Konservatif", "Moderat", "Agresif" };

    private float[,,] risk_percentage = new float[2, 4, 2];

    private int investment_manager_index = 0;
    private int product_index = 0;

    private int unit_change = 0;

    private bool sell_indicator = false;
    private bool buy_indicator = true;

    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;
    private InvestmentData investment_data;
    private AchievementsData achievements_data;

    [Header("UI")]

    [SerializeField]
    private Text money_text;

    [SerializeField]
    private Scrollbar scrollbar;

    [Header("PRODUCTS")]

    [SerializeField]
    private Text[] product_name_text;
    //[SerializeField]
    //private Text[] investment_manager_name_text;
    [SerializeField]
    private Text[] product_type_text;
    [SerializeField]
    private Text[] risk_level_text;
    [SerializeField]
    private Text[] performance_text;
    [SerializeField]
    private Text[] unit_count_text;
    [SerializeField]
    private Text[] unit_price_text;

    [Header("MANAGE PRODUCT POP UP")]

    [SerializeField]
    private GameObject manage_product_pop_up;

    [SerializeField]
    private Scrollbar history_scrollbar;
    [SerializeField]
    private Text[] history_text;

    [SerializeField]
    private Text manage_product_unit_count_text;
    [SerializeField]
    private Text manage_product_unit_price_text;
    [SerializeField]
    private Text manage_product_price_estimation;
    [SerializeField]
    private Text adjust_unit_text;
    [SerializeField]
    private Text total_price_text;

    [Header("WARNING POP UP")]

    [SerializeField]
    private GameObject warning_pop_up;

    [SerializeField]
    private Text warning_pop_up_text;

    [Header("ARE YOU SURE POP UP")]

    [SerializeField]
    private GameObject are_you_sure_pop_up;

    [SerializeField]
    private Text are_you_sure_pop_up_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        scrollbar.value = 1;

        set_value();

        get_data();
    }

    // public

    public void manage_product(int index)
    {
        audio_manager.play_sound("Button Sound");

        switch (index)
        {
            case 0:
                {
                    investment_manager_index = 0;
                    product_index = 0;

                    break;
                }
            case 1:
                {
                    investment_manager_index = 0;
                    product_index = 1;

                    break;
                }
            case 2:
                {
                    investment_manager_index = 1;
                    product_index = 0;

                    break;
                }
            case 3:
                {
                    investment_manager_index = 1;
                    product_index = 1;

                    break;
                }
        }

        if (game_data.get_data_ready() == true)
        {
            unit_change = 0;

            set_manage_product();
            set_history();

            manage_product_pop_up.SetActive(true);

            history_scrollbar.value = 1;
        }
    }

    public void manage_product_back()
    {
        audio_manager.play_sound("Button Sound");

        manage_product_pop_up.SetActive(false);
    }

    public void decrease()
    {
        audio_manager.play_sound("Button Sound");

        if (unit_change > 0)
        {
            unit_change--;

            adjust_unit_text.text = unit_change.ToString();
            total_price_text.text = "Rp" + (unit_change * investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index]);
        }
    }

    public void increase()
    {
        audio_manager.play_sound("Button Sound");

        unit_change++;

        adjust_unit_text.text = unit_change.ToString();
        total_price_text.text = "Rp" + (unit_change * investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index]);
    }

    public void sell()
    {
        audio_manager.play_sound("Button Sound");


        if (unit_change > 0)
        {
            if (unit_change <= investment_data.unit_count[investment_manager_index, investment_data.chosen_risk_level, product_index])
            {
                sell_indicator = true;
                buy_indicator = false;

                are_you_sure_pop_up_text.text = "Apakah anda yakin ingin menjual " + unit_change + " unit dengan harga Rp" + (unit_change * investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index]) + "?";

                are_you_sure_pop_up.SetActive(true);
            }
            else
            {
                warning_pop_up_text.text = "Jumlah unit yang ingin dijual melebihi jumlah yang dimiliki!";

                warning_pop_up.SetActive(true);
            }
        }
    }

    public void buy()
    {
        audio_manager.play_sound("Button Sound");

        if (unit_change > 0)
        {
            if (unit_change * investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index] <= game_data.get_money())
            {
                sell_indicator = false;
                buy_indicator = true;

                are_you_sure_pop_up_text.text = "Apakah anda yakin ingin membeli " + unit_change + " unit dengan harga Rp" + (unit_change * investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index]) + "?";

                are_you_sure_pop_up.SetActive(true);
            }
            else
            {
                warning_pop_up_text.text = "Uang tidak cukup untuk membeli produk ini!";

                warning_pop_up.SetActive(true);
            }
        }
    }

    public void warning_pop_up_ok()
    {
        audio_manager.play_sound("Button Sound");

        warning_pop_up.SetActive(false);
    }

    public void are_you_sure_pop_up_yes()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            game_data.set_data_ready(false);

            if (sell_indicator == true)
            {
                game_data.set_money(game_data.get_money() + (unit_change * investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index]));

                player_profile.money = game_data.get_money();
                investment_data.unit_count[investment_manager_index, investment_data.chosen_risk_level, product_index] = investment_data.unit_count[investment_manager_index, investment_data.chosen_risk_level, product_index] - unit_change;

                achievements_data.achievements_data[5, 0] = 1;
            }
            else if (buy_indicator == true)
            {
                game_data.set_money(game_data.get_money() - (unit_change * investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index]));

                player_profile.money = game_data.get_money();
                investment_data.unit_count[investment_manager_index, investment_data.chosen_risk_level, product_index] = investment_data.unit_count[investment_manager_index, investment_data.chosen_risk_level, product_index] + unit_change;

                achievements_data.achievements_data[4, 0] = 1;
            }

            set_data();
        }
    }

    public void are_you_sure_pop_up_no()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            sell_indicator = false;
            buy_indicator = false;

            are_you_sure_pop_up.SetActive(false);
        }
    }

    // private

    void update_data()
    {
        var today = System.DateTime.Now;
        var last_seen = System.DateTime.Parse(investment_data.last_seen);

        int hours = (int)((today - last_seen).TotalHours);

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    for (int l = 0; l < hours; l++)
                    {
                        float increase = (int)Random.Range(0, (risk_percentage[i, j, k]/0.25f)) * 0.25f;
                        //float increase = Random.Range(0, risk_percentage[i, j, k]);

                        if (investment_data.unit_price[i, j, k] + (int)((increase / 100) * investment_data.unit_price[i, j, k]) <= 2000)
                        {
                            if (Random.Range(0, 101) <= 30)    // 30%
                            {
                                increase = -increase;
                            }
                        }
                        else if (investment_data.unit_price[i, j, k] + (int)((increase / 100) * investment_data.unit_price[i, j, k]) >= 3000)
                        {
                            if (Random.Range(0, 101) <= 60)    // 70%
                            {
                                increase = -increase;
                            }
                        }
                        else
                        {
                            if (Random.Range(0, 101) <= 47)    // 47%
                            {
                                increase = -increase;
                            }
                        }

                        if (investment_data.unit_price[i, j, k] + (int)((increase / 100) * investment_data.unit_price[i, j, k]) >= 100 && investment_data.unit_price[i, j, k] + (int)((increase / 100) * investment_data.unit_price[i, j, k]) <= 10000)
                        {
                            investment_data.unit_price[i, j, k] = investment_data.unit_price[i, j, k] + (int)((increase / 100) * investment_data.unit_price[i, j, k]);
                        }
                        else if (investment_data.unit_price[i, j, k] + (int)((increase / 100) * investment_data.unit_price[i, j, k]) >= 10000)
                        {
                            investment_data.unit_price[i, j, k] = 10000;
                        }
                        else if (investment_data.unit_price[i, j, k] + (int)((increase / 100) * investment_data.unit_price[i, j, k]) <= 100)
                        {
                            investment_data.unit_price[i, j, k] = 100;
                        }

                        for (int m = 23; m > 0; m--)
                        {
                            investment_data.history[i, j, k, m] = investment_data.history[i, j, k, m - 1];
                        }

                        investment_data.history[i, j, k, 0] = increase;
                    }
                }
            }
        }

        investment_data.last_seen = last_seen.AddHours(hours).ToString();

        set_data();
    }

    void set_manage_product()
    {
        manage_product_unit_count_text.text = "Total Aset: " + investment_data.unit_count[investment_manager_index, investment_data.chosen_risk_level, product_index];
        manage_product_unit_price_text.text = "Harga: Rp" + investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index];

        float average = 0;

        for (int i = 0; i < 24; i++)
        {
            average = average + investment_data.history[investment_manager_index, investment_data.chosen_risk_level, product_index, i];
        }

        average = average / 24;

        if (average < 0)
        {
            manage_product_price_estimation.GetComponent<Outline>().effectColor = Color.red;
        }
        else if (average >= 0)
        {
            manage_product_price_estimation.GetComponent<Outline>().effectColor = Color.green;
        }

        manage_product_price_estimation.text = "Estimasi Harga Besok: Rp" + (int)(investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index] + investment_data.unit_price[investment_manager_index, investment_data.chosen_risk_level, product_index] * (average/100));

        adjust_unit_text.text = unit_change.ToString();
        total_price_text.text = "Rp0";
    }

    void set_history()
    {
        for (int i = 0; i < 24; i++)
        {
            string clock = System.DateTime.Now.AddHours(-i).ToString("hh:00 tt");

            if (investment_data.history[investment_manager_index, investment_data.chosen_risk_level, product_index, i] < 0)
            {
                history_text[i].GetComponent<Outline>().effectColor = Color.red;
                history_text[i].text = clock + ": " + investment_data.history[investment_manager_index, investment_data.chosen_risk_level, product_index, i].ToString("F2") + "%";
            }
            else if (investment_data.history[investment_manager_index, investment_data.chosen_risk_level, product_index, i] >= 0)
            {
                history_text[i].GetComponent<Outline>().effectColor = Color.green;
                history_text[i].text = clock + ": +" + investment_data.history[investment_manager_index, investment_data.chosen_risk_level, product_index, i].ToString("F2") + "%";
            }

            /*if (System.DateTime.Now.AddHours(-i) < System.DateTime.Today)
            {
                history_text[i].text = history_text[i].text + " (Kemarin)";
            }*/
        }
    }

    void reset_UI()
    {
        money_text.text = "Rp" + game_data.get_money().ToString();

        int x = 0;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                float average = 0;

                for (int k = 0; k < 24; k++)
                {
                    average = average + investment_data.history[i, investment_data.chosen_risk_level, j, k];
                }

                average = average / 24;

                product_name_text[x].text = product[i, investment_data.chosen_risk_level, j];
                //investment_manager_name_text[x].text = "by " + investment_manager[i];
                product_type_text[x].text = "Jenis Produk: " + product_type[i, investment_data.chosen_risk_level, j];
                risk_level_text[x].text = "Level Risiko: " + risk_level[investment_data.chosen_risk_level];

                if (average < 0)
                {
                    performance_text[x].GetComponent<Outline>().effectColor = Color.red;
                    performance_text[x].text = "Kinerja 24 Jam Terakhir: " + average.ToString("F2") + "%";
                }
                else if (average >= 0)
                {
                    performance_text[x].GetComponent<Outline>().effectColor = Color.green;
                    performance_text[x].text = "Kinerja 24 Jam Terakhir: +" + average.ToString("F2") + "%";
                }
                
                unit_count_text[x].text = "Total Aset: " + investment_data.unit_count[i, investment_data.chosen_risk_level, j].ToString();
                unit_price_text[x].text = "Harga: Rp" + investment_data.unit_price[i, investment_data.chosen_risk_level, j].ToString();

                x++;
            }
        }
    }

    void set_value()
    {
        product[0, 0, 0] = "Tembaga";
        product[0, 0, 1] = "Besi";
        product[0, 1, 0] = "Rubi";
        product[0, 1, 1] = "Safir";
        product[0, 2, 0] = "Perak";
        product[0, 2, 1] = "Emas";

        product[1, 0, 0] = "Obsidian";
        product[1, 0, 1] = "Mutiara";
        product[1, 1, 0] = "Zamrud";
        product[1, 1, 1] = "Intan";
        product[1, 2, 0] = "Platinum";
        product[1, 2, 1] = "Berlian";

        product_type[0, 0, 0] = "Pasar Uang";
        product_type[0, 0, 1] = "Pasar Uang";
        product_type[0, 1, 0] = "Pendapatan Tetap";
        product_type[0, 1, 1] = "Campuran";
        product_type[0, 2, 0] = "Saham";
        product_type[0, 2, 1] = "Saham";

        product_type[1, 0, 0] = "Pasar Uang";
        product_type[1, 0, 1] = "Pendapatan Tetap";
        product_type[1, 1, 0] = "Pendapatan Tetap";
        product_type[1, 1, 1] = "Campuran";
        product_type[1, 2, 0] = "Saham";
        product_type[1, 2, 1] = "Saham";
        
        risk_percentage[0, 0, 0] = 2f;
        risk_percentage[0, 0, 1] = 2.25f;
        risk_percentage[0, 1, 0] = 4.5f;
        risk_percentage[0, 1, 1] = 4.75f;
        risk_percentage[0, 2, 0] = 7.75f;
        risk_percentage[0, 2, 1] = 8f;
        
        risk_percentage[1, 0, 0] = 2.25f;
        risk_percentage[1, 0, 1] = 2.5f;
        risk_percentage[1, 1, 0] = 4.25f;
        risk_percentage[1, 1, 1] = 4.5f;
        risk_percentage[1, 2, 0] = 8f;
        risk_percentage[1, 2, 1] = 8.25f;
    }

    // playfab

    void get_data()
    {
        game_data.set_data_ready(false);

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), get_data_success, error);
    }

    void get_data_success(GetUserDataResult result)
    {
        player_profile = JsonConvert.DeserializeObject<PlayerProfile>(result.Data["Player Profile"].Value);
        investment_data = JsonConvert.DeserializeObject<InvestmentData>(result.Data["Investment"].Value);
        achievements_data = JsonConvert.DeserializeObject<AchievementsData>(result.Data["Achievements"].Value);

        update_data();
    }

    void set_data()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Player Profile", JsonConvert.SerializeObject(player_profile)},
                {"Investment", JsonConvert.SerializeObject(investment_data)},
                {"Achievements", JsonConvert.SerializeObject(achievements_data)},
            }
        };

        PlayFabClientAPI.UpdateUserData(request, set_data_success, error);
    }

    void set_data_success(UpdateUserDataResult result)
    {
        reset_UI();

        manage_product_pop_up.SetActive(false);
        are_you_sure_pop_up.SetActive(false);

        game_data.set_data_ready(true);
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
