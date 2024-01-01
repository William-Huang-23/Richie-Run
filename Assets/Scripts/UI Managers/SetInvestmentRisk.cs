using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class SetInvestmentRisk : MonoBehaviour
{
    [Header("GAME DATA")]

    private int index = 1;  // Sangat Konservatif = 0, removed

    [Header("CLASS")]

    private GameData game_data;
    private AudioManager audio_manager;

    private InvestmentData investment_data;

    [Header("UI")]

    [Header("RISK LEVEL")]

    [SerializeField]
    private Text risk_name_text;
    [SerializeField]
    private Text risk_description_text;

    [Header("WARNING POP UP")]

    [SerializeField]
    private GameObject warning_pop_up;

    [SerializeField]
    private Text warning_pop_up_text;

    void Start()
    {
        game_data = GameObject.Find("Game Data(Clone)").GetComponent<GameData>();
        audio_manager = GameObject.Find("Audio Manager(Clone)").GetComponent<AudioManager>();

        konservatif();
    }

    // public

    /*public void sangat_konservatif()
    {
        audio_manager.play_sound("Button Sound");

        index = 0;

        risk_name_text.text = "Profil Risiko: Sangat Konservatif";
        risk_description_text.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko sangat konservatif, tingkat risiko dan imbal hasil relatif rendah.";

        warning_pop_up_text.text = "Profil Risiko: Sangat Konservatif";
        //warning_pop_up_description.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko sangat konservatif, tingkat risiko dan imbal hasil relatif rendah.\n\nPilihan hanya dapat diubah x1 setiap hari melalui menu Pengaturan. Apakah anda yakin dengan pilihan anda?";
    }*/

    public void konservatif()
    {
        audio_manager.play_sound("Button Sound");

        index = 0;

        risk_name_text.text = "Profil Risiko: Konservatif";
        risk_description_text.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko konservatif, tingkat risiko dan imbal hasil relatif lebih rendah.";

        warning_pop_up_text.text = "Profil Risiko: Konservatif";
        //warning_pop_up_description.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko konservatif, tingkat risiko dan imbal hasil relatif lebih rendah.\n\nPilihan hanya dapat diubah x1 setiap hari melalui menu Pengaturan. Apakah anda yakin dengan pilihan anda?";
    }

    public void moderat()
    {
        audio_manager.play_sound("Button Sound");

        index = 1;

        risk_name_text.text = "Profil Risiko: Moderat";
        risk_description_text.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko moderat, tingkat risiko dan imbal hasil relatif sedang.";

        warning_pop_up_text.text = "Profil Risiko: Moderat";
        //warning_pop_up_description.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko moderat, tingkat risiko dan imbal hasil relatif lebih tinggi.\n\nPilihan hanya dapat diubah x1 setiap hari melalui menu Pengaturan. Apakah anda yakin dengan pilihan anda?";
    }

    public void agresif()
    {
        audio_manager.play_sound("Button Sound");

        index = 2;

        risk_name_text.text = "Profil Risiko: Agresif";
        risk_description_text.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko agresif, tingkat risiko dan imbal hasil relatif lebih tinggi.";

        warning_pop_up_text.text = "Profil Risiko: Agresif";
        //warning_pop_up_description.text = "Dalam investasi Reksa Dana, pilihan ini akan mempengaruhi hasil investasimu.\n\nUntuk profil risiko agresif, tingkat risiko dan imbal hasil relatif tinggi.\n\nPilihan hanya dapat diubah x1 setiap hari melalui menu Pengaturan. Apakah anda yakin dengan pilihan anda?";
    }

    public void select()
    {
        audio_manager.play_sound("Button Sound");

        warning_pop_up.SetActive(true);
    }

    public void warning_pop_up_yes()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            game_data.set_data_ready(false);

            game_data.set_chosen_risk_level(index);

            get_data();
        }
    }

    public void warning_pop_up_no()
    {
        audio_manager.play_sound("Button Sound");

        if (game_data.get_data_ready() == true)
        {
            warning_pop_up.SetActive(false); ;
        }
    }

    // playfab

    void get_data()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), get_data_success, error);
    }

    void get_data_success(GetUserDataResult result)
    {
        investment_data = JsonConvert.DeserializeObject<InvestmentData>(result.Data["Investment"].Value);

        investment_data.chosen_risk_level = index;

        set_data();
    }

    void set_data()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Investment", JsonConvert.SerializeObject(investment_data)},
            }
        };

        PlayFabClientAPI.UpdateUserData(request, set_data_success, error);
    }

    void set_data_success(UpdateUserDataResult result)
    {
        game_data.set_data_ready(true);

        SceneManager.LoadScene("Investment");
    }

    void error(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
}