using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using UnityEngine;
using Unity.VisualScripting;

public class API : MonoBehaviour
{
    private const string KEY = "AqZzAdsbn8FKYcWfFM1p8m4afntUaIOay2GkD0j4";

    private enum FormatOption
    {
        json,
        xml
    }

    [SerializeField]
    private FormatOption format;

    [SerializeField]
    private double system_capacity;

    private enum ModuleOption
    {
        Standard = 0,
        Premium = 1,
        Thin_Film = 2
    }

    [SerializeField]
    private ModuleOption module_type;

    [SerializeField] 
    private int losses;

    private enum ArrayOption
    {
        Fixed_Open = 0,
        Fixed_Roof = 1,
        One_Axis = 2,
        One_Axis_Backtraking = 3,
        Two_Axis = 4
    }

    [SerializeField]
    private ArrayOption array_type;

    [SerializeField]
    private int tilt;

    [SerializeField]
    private int azimuth;

    [SerializeField]
    private double lat;

    [SerializeField]
    private double lon;

    private enum TimeframeOption
    {
        monthly,
        hourly
    }

    [SerializeField]
    private TimeframeOption timeframe;

    [SerializeField]
    private double dc_ac_ratio;

    [SerializeField]
    private double gcr;

    [SerializeField]
    private double inv_eff;

    [SerializeField]
    private double bifaciality;

    [SerializeField]
    private double albedo;

    [SerializeField]
    private double[] albedoArray;

    [SerializeField]
    private double[] soiling;

    void Start(){
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string response = GenerarPeticion();
        print(response);
    }

    string GenerarPeticion() {
        string peticion = "https://developer.nrel.gov/api/pvwatts/v8.";
        //Formato
        switch (format)
        {
            case FormatOption.json:
                peticion = peticion + "json?";
                break;
            case FormatOption.xml:
                peticion = peticion + "xml?";
                break;
        }

        //API Key
        peticion = peticion + "api_key=" + KEY;

        //System Capacity
        peticion = peticion + "&system_capacity=" + system_capacity;

        //Module Type
        peticion = peticion + "&module_type=" + (int) module_type;

        //Losses
        if (losses >= -5 && losses <= 99)
        {
            peticion = peticion + "&losses=" + losses;
        }

        //Array Type
        peticion = peticion + "&array_type=" + (int) array_type;

        //Tilt
        if (tilt >= 0 && tilt <= 90)
        {
            peticion = peticion + "&tilt=" + tilt;
        }

        //Azimuth
        if (azimuth >= 0 && azimuth <= 360)
        {
            peticion = peticion + "&azimuth=" + azimuth;
        }

        //Latitud
        if (lat >= -90 && lat <= 90)
        {
            peticion = peticion + "&lat=" + lat;
        }

        //Longitud
        if (lon >= -180 && lon <= 180)
        {
            peticion = peticion + "&lon=" + lon;
        }

        //Timeframe
        switch (timeframe)
        {
            case TimeframeOption.monthly:
                break;
            case TimeframeOption.hourly:
                peticion = peticion + "&timeframe=hourly";
                break;
        }

        //DC to AC ratio
        if (dc_ac_ratio > 0)
        {
            peticion = peticion + "&dc_ac_ratio=" + dc_ac_ratio;
        }

        //GCR
        if (gcr >= 0.01 && gcr <= 0.99)
        {
            peticion = peticion + "&gcr=" + gcr;
        }

        //Inv_Eff
        if (inv_eff >= 90 && inv_eff <= 99.5)
        {
            peticion = peticion + "&inv_eff=" + inv_eff;
        }

        //Bifaciality}
        if (bifaciality >= 0 && bifaciality <= 1)
        {
            peticion = peticion + "&bifaciality=" + bifaciality;
        }

        //Albedo
        if (albedo > 0 && albedo < 1)
        {
            peticion = peticion + "&albedo=" + albedo;
        }
        else if (albedoArray.Length > 0)
        {
            peticion = peticion + "&albedo=";
            for (int i = 0; i < albedoArray.Length; i++)
            {
                peticion = peticion + albedoArray[i];
                if (i < albedoArray.Length-1)
                {
                    peticion = peticion + "|";
                }
            }
        }

        //Soiling
        peticion = peticion + "&soiling=";
        for (int i = 0; i < soiling.Length; i++)
        {
            peticion = peticion + soiling[i];
            if (i < soiling.Length - 1)
            {
                peticion = peticion + "|";
            }
        }
        return peticion;
    }
    private string get(){
        string peticion = String.Format("https://developer.nrel.gov/api/pvwatts/v8.json?api_key={0}&azimuth=180&system_capacity=4&losses=14&array_type=1&module_type=0&gcr=0.4&dc_ac_ratio=1.2&inv_eff=96.0&radius=0&dataset=nsrdb&tilt=10&address=boulder,%20co&soiling=12|4|45|23|9|99|67|12.54|54|9|0|7.6&albedo=0.3&bifaciality=0.7", KEY);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(peticion);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        return jsonResponse;
    }
}
