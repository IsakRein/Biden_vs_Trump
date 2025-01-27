﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.Advertisements;

public class Main : MonoBehaviour
{
    public AudioManager audioManager;

    [Header("Blurs")]
    public Material blur_regular;
    public float blur_regular_value;

    public Material blur_pause;
    public float blur_pause_value;

    public Material blur_popup;
    public float blur_popup_value;

    [Header("Settings")]
    public bool settings_sound = true;
    public bool settings_music = true;
    public bool settings_vibration = true;
    public bool remove_ads;

    [Header("Ads")]
    public int plays_between_ads;
    public int plays_to_first_ad;
    public int plays_since_ad;
    public string nextPopUp = "Merch"; 
    public string nextAdType = "Ad"; 

    public string actual_last_scene;

    public string ios_ads_gameid = "3841622";
    public string android_ads_gameid = "3841623";
    public string default_ads_gameid = "3841623";

#if UNITY_IOS
    public string gameId = "3841622";
#elif UNITY_ANDROID
    public string gameId = "3841623";
#else
    public string gameId = "3841622";
#endif
    public bool testMode = false;

    [Header("Stats")]
    public int biden_level_1 =      0;
    public int biden_level_2 =      0;
    public int biden_level_3 =      0;
    public int biden_level_4 =      0;
    public int biden_level_5 =      0;
    public int biden_level_6 =      0;
    public int biden_player_count = 0;
    public int trump_level_1 =      0;
    public int trump_level_2 =      0;
    public int trump_level_3 =      0;
    public int trump_level_4 =      0;
    public int trump_level_5 =      0;
    public int trump_level_6 =      0;
    public int trump_player_count = 0;

    [Header("Database")]
    public bool loaded;
    public bool success;
    public DatabaseReference reference;

    void Awake()
    {
        
#if UNITY_IOS
            gameId = ios_ads_gameid;
#elif UNITY_ANDROID
            gameId = android_ads_gameid;
#else
            gameId =default_ads_gameid;
#endif


        plays_since_ad = plays_between_ads-plays_to_first_ad-1;

        Application.targetFrameRate = 300;
        Advertisement.Initialize(gameId, testMode);
        updateSettings();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Main");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        blur_regular.SetFloat("_Size", blur_regular_value);
        blur_pause.SetFloat("_Size", blur_pause_value);
        blur_popup.SetFloat("_Size", blur_popup_value);

        DontDestroyOnLoad(this.gameObject);
    }

    public void updateSettings()
    {
        if (PlayerPrefs.HasKey("settings_sound")) {
            settings_sound = PlayerPrefs.GetInt("settings_sound")== 1;
        } else {settings_sound = true; } 
        if (PlayerPrefs.HasKey("settings_music")) {
            settings_music = PlayerPrefs.GetInt("settings_music") == 1;
        } else {settings_music = true; }
        if (PlayerPrefs.HasKey("settings_vibration")) {
            settings_vibration = PlayerPrefs.GetInt("settings_vibration") == 1;
        } else {settings_vibration = true; }
        if (PlayerPrefs.HasKey("remove_ads")) {
            remove_ads = PlayerPrefs.GetInt("remove_ads") == 1;
        } else {remove_ads = false; }
    }

    public void AddToValue(string key)
    {
        DatabaseReference data_reference = reference.Child(key);

        data_reference.RunTransaction(data =>
        {
            int newValue = 0;

            if (data.Value == null)
            {
                newValue = PlayerPrefs.GetInt(key) + 1;
            }
            else
            {
                newValue = int.Parse(data.Value.ToString()) + 1;
            }

            data.Value = newValue;

            switch (key)
            {
                case "biden_level_1":       biden_level_1          = newValue; break; 
                case "biden_level_2":       biden_level_2          = newValue; break; 
                case "biden_level_3":       biden_level_3          = newValue; break; 
                case "biden_level_4":       biden_level_4          = newValue; break; 
                case "biden_level_5":       biden_level_5          = newValue; break; 
                case "biden_level_6":       biden_level_6          = newValue; break; 
                case "biden_player_count":  biden_player_count     = newValue; break; 
                case "trump_level_1":       trump_level_1          = newValue; break; 
                case "trump_level_2":       trump_level_2          = newValue; break; 
                case "trump_level_3":       trump_level_3          = newValue; break; 
                case "trump_level_4":       trump_level_4          = newValue; break; 
                case "trump_level_5":       trump_level_5          = newValue; break; 
                case "trump_level_6":       trump_level_6          = newValue; break; 
                case "trump_player_count":  trump_player_count     = newValue; break; 
            }

            return TransactionResult.Success(data);
        });
    }
}
