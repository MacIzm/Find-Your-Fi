using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net.NetworkInformation;
using System;
using Assets;

public class WiFiScanner : MonoBehaviour
{

    public Text Display;    // Text UI to display the network information.
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject whole;
#if !UNITY_EDITOR
   
    // Use this for initialization
    void Start()
    {
       // WiFiAdapter = new UniversalWiFi();
    }
#endif

    // This method is being called every frame.
    void Update()
    {
        try
        {
            Scan();
        }
        catch (System.Exception e)
        {

        }
        //Scan();
    }
    private void Scan()
    {
        /*if (WiFiAdapter != null)
        {
            var ssid = "eduroam"; // The desired SSID to scan.
            var signal = WiFiAdapter.GetSignal(ssid);
            var report = WiFiAdapter.GetNetworkReport();
            var lists = WiFiAdapter.getNetworks();
            //var rssi = WiFiAdapter.getRSSI();
            //Display.text = string.Format("{0} Signal: {1} RSSI: {2}",ssid, signal,rssi);
            //Display.text = string.Format("{0} signal:{1}{2}{3}", ssid, signal, Environment.NewLine, report);
            //Display.text = lists[0].Mac;
            //Display.text = string.Format(report);
            if (signal == 3)
            {
                three.GetComponent<Renderer>().material.color = Color.black;
                one.GetComponent<Renderer>().material.color = Color.gray;
                two.GetComponent<Renderer>().material.color = Color.gray;
            }
            else if (signal == 2)
            {
                two.GetComponent<Renderer>().material.color = Color.black;
                three.GetComponent<Renderer>().material.color = Color.black;
                one.GetComponent<Renderer>().material.color = Color.gray;
            }
            else if (signal == 1)
            {
                two.GetComponent<Renderer>().material.color = Color.black;
                three.GetComponent<Renderer>().material.color = Color.black;
                one.GetComponent<Renderer>().material.color = Color.black;
            }
            else if (signal == 0)
            {
                whole.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (signal == 4)
            {
                whole.GetComponent<Renderer>().material.color = Color.gray;
            }

            //Display.text = "HELLO"; 
        }*/
    }
}