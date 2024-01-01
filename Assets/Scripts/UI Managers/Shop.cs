using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class Shop : MonoBehaviour
{
    [Header("GAME DATA")]

    private int[] income_upgrade = { 0, 10, 20, 30, 40, 50 };
    private int[] shield_upgrade = { 0, 1, 2, 3, 4, 5 };
    private int[] magnet_upgrade = { 0, 1, 2, 3, 4, 5 };

    private int[] income_price = { 2500, 5000, 7500, 10000, 12500 };
    private int[] shield_price = { 3000, 6000, 9000, 12000, 15000 };
    private int[] magnet_price = { 3000, 6000, 9000, 12000, 15000 };
    private int athlete_price = 10000;
    private int doctor_price = 10000;
    private int dam_price = 50000;

    int index = 0;

    [Header("CLASS")]

    [SerializeField]
    private Data data;

    private GameData game_data;
    private AudioManager audio_manager;

    private PlayerProfile player_profile;
    private AchievementsData achievements_data;

    [Header("UI")]

    [SerializeField]
    private Text money_text;

    [SerializeField]
    private Scrollbar scrollbar;

    [Header("ITEMS")]

    [SerializeField]
    private Text[] description_text;
    [SerializeField]
    private Text[] price_text;

    [SerializeField]
    private Image[] level_bar;
    [SerializeField]
    private Image[] locked_sign;

    [SerializeField]
    private Sprite[] level_bar_sprite;
    [SerializeField]
    private Sprite[] locked_sign_sprite;

    [Header("CONFIRMATION POP UP")]

    [SerializeField]
    private GameObject confirmation_pop_up;

    [SerializeField]
    private Text confirmation_pop_up_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        scrollbar.value = 1;

        get_data();
    }

    // public

    public void buy(int index)
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            this.index = index;

            switch (index)
            {
                case 0:
                    {
                        if (game_data.get_income() < 5 && income_price[game_data.get_income()] <= game_data.get_money())
                        {
                            confirmation_pop_up.SetActive(true);

                            confirmation_pop_up_text.text = "Apakah anda yakin ingin mengupgrade Income ke level " + (game_data.get_income() + 1) + " dengan harga Rp" + income_price[game_data.get_income()] + "?";
                        }

                        break;
                    }
                case 1:
                    {
                        if (game_data.get_shield() < 5 && shield_price[game_data.get_shield()] <= game_data.get_money())
                        {
                            confirmation_pop_up.SetActive(true);

                            confirmation_pop_up_text.text = "Apakah anda yakin ingin mengupgrade Perisai ke level " + (game_data.get_shield() + 1) + " dengan harga Rp" + shield_price[game_data.get_shield()] + "?";
                        }

                        break;
                    }
                case 2:
                    {
                        if (game_data.get_magnet() < 5 && magnet_price[game_data.get_magnet()] <= game_data.get_money())
                        {
                            confirmation_pop_up.SetActive(true);

                            confirmation_pop_up_text.text = "Apakah anda yakin ingin mengupgrade Magnet ke level " + (game_data.get_magnet() + 1) + " dengan harga Rp" + magnet_price[game_data.get_magnet()] + "?";
                        }

                        break;
                    }
                case 3:
                    {
                        if (game_data.get_atlet() < 1 && athlete_price <= game_data.get_money())
                        {
                            confirmation_pop_up.SetActive(true);

                            confirmation_pop_up_text.text = "Apakah anda yakin ingin mengunlock profesi: Atlet dengan harga Rp" + athlete_price + "?";
                        }

                        break;
                    }
                case 4:
                    {
                        if (game_data.get_dokter() < 1 && doctor_price <= game_data.get_money())
                        {
                            confirmation_pop_up.SetActive(true);

                            confirmation_pop_up_text.text = "Apakah anda yakin ingin mengunlock profesi: Dokter dengan harga Rp" + doctor_price + "?";
                        }

                        break;
                    }
                case 5:
                    {
                        if (game_data.get_dam() < 1 && dam_price <= game_data.get_money())
                        {
                            confirmation_pop_up.SetActive(true);

                            confirmation_pop_up_text.text = "Apakah anda yakin ingin memperbaiki bendungan dengan harga Rp" + dam_price + "?";
                        }

                        break;
                    }
            }
        }
    }

    public void confirmation_pop_up_yes()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            game_data.set_data_ready(false);

            switch (index)
            {
                case 0:
                    {
                        game_data.set_money(game_data.get_money() - income_price[game_data.get_income()]);
                        game_data.set_income(game_data.get_income() + 1);

                        achievements_data.achievements_data[6, 0] = 1;

                        if (game_data.get_income() >= 5)
                        {
                            achievements_data.achievements_data[7, 0]++;
                        }

                        break;
                    }
                case 1:
                    {
                        game_data.set_money(game_data.get_money() - shield_price[game_data.get_shield()]);
                        game_data.set_shield(game_data.get_shield() + 1);

                        achievements_data.achievements_data[6, 0] = 1;

                        if (game_data.get_shield() >= 5)
                        {
                            achievements_data.achievements_data[7, 0]++;
                        }

                        break;
                    }
                case 2:
                    {
                        game_data.set_money(game_data.get_money() - magnet_price[game_data.get_magnet()]);
                        game_data.set_magnet(game_data.get_magnet() + 1);

                        achievements_data.achievements_data[6, 0] = 1;

                        if (game_data.get_magnet() >= 5)
                        {
                            achievements_data.achievements_data[7, 0]++;
                        }

                        break;
                    }
                case 3:
                    {
                        game_data.set_money(game_data.get_money() - athlete_price);
                        game_data.set_atlet(1);

                        achievements_data.achievements_data[6, 0] = 1;
                        achievements_data.achievements_data[7, 0]++;

                        break;
                    }
                case 4:
                    {
                        game_data.set_money(game_data.get_money() - doctor_price);
                        game_data.set_dokter(1);

                        achievements_data.achievements_data[6, 0] = 1;
                        achievements_data.achievements_data[7, 0]++;

                        break;
                    }
                case 5:
                    {
                        game_data.set_money(game_data.get_money() - dam_price);
                        game_data.set_dam(1);

                        achievements_data.achievements_data[9, 0] = 1;

                        break;
                    }
            }

            player_profile.money = game_data.get_money();

            set_data();
        }
    }

    public void confirmation_pop_up_no()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            confirmation_pop_up.SetActive(false);
        }
    }

    // private

    void reset_UI()
    {
        money_text.text = "Rp" + game_data.get_money();

        reset_income_UI();
        reset_shield_UI();
        reset_magnet_UI();
        reset_atlet_UI();
        reset_dokter_UI();
        reset_dam_UI();
    }

    void reset_income_UI()
    {
        if (game_data.get_income() < 5)
        {
            description_text[0].text = "Jumlah uang yang diperoleh \n dari koin +" + income_upgrade[game_data.get_income()] + "% (+" + income_upgrade[game_data.get_income() + 1] + "%)";
            price_text[0].text = "Rp" + income_price[game_data.get_income()];
            
        }
        else
        {
            description_text[0].text = "Jumlah uang yang diperoleh \n dari koin +" + income_upgrade[game_data.get_income()] + "%";
            price_text[0].text = "Max!";
        }

        level_bar[0].sprite = level_bar_sprite[game_data.get_income()];
    }

    void reset_shield_UI()
    {
        if (game_data.get_shield() < 5)
        {
            description_text[1].text = "Perisai dapat menahan +" + shield_upgrade[game_data.get_shield()] + " (+" + shield_upgrade[game_data.get_shield() + 1] + ") \n lebih rintangan sebelum hancur";
            price_text[1].text = "Rp" + shield_price[game_data.get_shield()];

        }
        else
        {
            description_text[1].text = "Perisai dapat menahan +" + shield_upgrade[game_data.get_shield()] + "\n lebih rintangan sebelum hancur";
            price_text[1].text = "Max!";
        }

        level_bar[1].sprite = level_bar_sprite[game_data.get_shield()];
    }

    void reset_magnet_UI()
    {
        if (game_data.get_magnet() < 5)
        {
            description_text[2].text = "Durasi magnet +" + magnet_upgrade[game_data.get_magnet()] + " (+" + magnet_upgrade[game_data.get_magnet() + 1] + ") detik \n lebih lama sebelum hilang";
            price_text[2].text = "Rp" + magnet_price[game_data.get_magnet()];

        }
        else
        {
            description_text[2].text = "Durasi magnet +" + magnet_upgrade[game_data.get_magnet()] + " detik \n lebih lama sebelum hilang";
            price_text[2].text = "Max!";
        }

        level_bar[2].sprite = level_bar_sprite[game_data.get_magnet()];
    }

    void reset_atlet_UI()
    {
        description_text[3].text = "Atlet dapat menghindari\nrintangan dengan lebih mudah";

        if (game_data.get_atlet() == 0)
        {
            price_text[3].text = "Rp" + athlete_price;
        }
        else
        {
            price_text[3].text = "Telah\nDibeli!";
        }

        locked_sign[0].sprite = locked_sign_sprite[game_data.get_atlet()];
    }

    void reset_dokter_UI()
    {
        description_text[4].text = "Dokter mendapatkan perlindungan\nyang lebih lama dari perisai";

        if (game_data.get_dokter() == 0)
        {
            price_text[4].text = "Rp" + doctor_price;
        }
        else
        {
            price_text[4].text = "Telah\nDibeli!";
        }

        locked_sign[1].sprite = locked_sign_sprite[game_data.get_dokter()];
    }

    void reset_dam_UI()
    {
        description_text[5].text = "Beli bahan bangunan yang diperlukan\nuntuk menyelesaikan perbaikan";

        if (game_data.get_dam() == 0)
        {
            price_text[5].text = "Rp" + dam_price;
        }
        else
        {
            price_text[5].text = "Telah\nDibeli!";
        }

        locked_sign[2].sprite = locked_sign_sprite[game_data.get_dam()];
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
        achievements_data = JsonConvert.DeserializeObject<AchievementsData>(result.Data["Achievements"].Value);

        reset_UI();

        game_data.set_data_ready(true);
    }

    void set_data()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Player Profile", JsonConvert.SerializeObject(player_profile)},
                {"Shop", JsonConvert.SerializeObject(data.return_shop_items(game_data.get_income(), game_data.get_shield(), game_data.get_magnet(), game_data.get_atlet(), game_data.get_dokter(), game_data.get_dam()))},
                {"Achievements", JsonConvert.SerializeObject(achievements_data)},
            }
        };

        PlayFabClientAPI.UpdateUserData(request, set_data_success, error);
    }

    void set_data_success(UpdateUserDataResult result)
    {
        reset_UI();

        confirmation_pop_up.SetActive(false);

        game_data.set_data_ready(true);
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}
