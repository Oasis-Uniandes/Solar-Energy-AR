using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using UnityEngine;

public class API : MonoBehaviour
{
    private const string KEY = "AqZzAdsbn8FKYcWfFM1p8m4afntUaIOay2GkD0j4";
    public string ciudad;

    void Start(){
        string response = get();
        print(response);
    }

// Start is called before the first frame update
    private string get(){
        string peticion = String.Format("https://developer.nrel.gov/api/pvwatts/v8.json?api_key={0}&azimuth=180&system_capacity=4&losses=14&array_type=1&module_type=0&gcr=0.4&dc_ac_ratio=1.2&inv_eff=96.0&radius=0&dataset=nsrdb&tilt=10&address=boulder,%20co&soiling=12|4|45|23|9|99|67|12.54|54|9|0|7.6&albedo=0.3&bifaciality=0.7", KEY);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(peticion);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        return jsonResponse;
    }
}
