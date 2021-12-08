using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;
using System.Linq;
using Academy.HoloToolkit.Unity;

public class Available_Wifi : MonoBehaviour {

    public GameObject prefab;
    public Canvas canvas;
    public GameObject compassPrefab;
    public GameObject wifiBoxprefab;
    public GameObject linePrefab;
   

    [SerializeField] Transform menuPanel;
    [SerializeField] GameObject buttonPrefab;
#if !UNITY_EDITOR
    //private IWiFiAdapter<wifis> WiFiAdapter;
    //List<wifis> wifiList = new List<wifis>();
#endif
    TextMesh tt;
    TextMesh DB;

    
    List<int> rValues = new List<int>();
   
    HashSet<string> ssidNames = new HashSet<string>();
    Dictionary<string,double> dWifi = new Dictionary<string,double>();
    Dictionary<string, uint> sWifi = new Dictionary<string, uint>();
    int NameCount;

     //Use this for initialization
    void Start() {
#if !UNITY_EDITOR
         // WiFiAdapter = new UniversalWiFi();
#endif
        StartCoroutine(DrawPanels());
        InvokeRepeating("updatePanels", 20f, 5f);
        InvokeRepeating("updateSignal", 10f, 5f);

        InvokeRepeating("getNetworks", 0, 5f);
    }

    private void Update()
    {

       // var list = WiFiAdapter.getNetworks();

        //tt.text = list.ToString();
        // tt.transform.position = posXYZ("Wifi-Name");
    }


    IEnumerator DrawPanels()
    {
       yield return new WaitForSeconds(7f);
       CreatePanels();
        InvokeRepeating("CreatePanels",20f,5f);
    } 

    void getNetworks()
    {
#if !UNITY_EDITOR
     //var list = WiFiAdapter.getNetworks();
#endif
        dWifi.Clear();
        sWifi.Clear();
#if !UNITY_EDITOR
     /* for (int x = 0; x < list.Count; x++)
          {

            Debug.Log(list[x].Ssid+","+list[x].Mac + "," + list[x].RSSI + "," + list[x].Signal);
            ssidNames.Add(list[x].Ssid);
              // if the key doesnt exist add else update rssi value
              if(dWifi.ContainsKey(list[x].Ssid) == false){
                  dWifi.Add(list[x].Ssid, list[x].RSSI);
              }
              else
              {
                  dWifi[list[x].Ssid] = list[x].RSSI;
              }

              if (sWifi.ContainsKey(list[x].Ssid) == false)
              {
                  sWifi.Add(list[x].Ssid, list[x].Signal);
              }
              else
              {
                  sWifi[list[x].Ssid] = list[x].Signal;
              }


          }

          wifiList = list;

          list.Clear(); */
#endif
    }

    void updatePanels()
    {
        TextMesh[] txtMeshes;
        
       
        if(NameCount < ssidNames.Count)
        {
            for (var i = 0; i < ssidNames.Count; i++) {
                var Nobjects = GameObject.Find(ssidNames.ElementAt(i));
                Destroy(Nobjects);
                var Bobjects = GameObject.Find("box-"+ ssidNames.ElementAt(i));
                Destroy(Bobjects);
            }

            CreatePanels();
        }

       // Debug.Log("Wifi count: "+wifiList.Count);
        Debug.Log("Ssid count: "+ssidNames.Count);
        Debug.Log("In update: [" + string.Join(", ",ssidNames.ToArray())+"]");
        
        for (var i=0;i< ssidNames.Count;i++)
        {

            if (ssidNames.Count == dWifi.Count)//GameObject.Find(ssidNames.ElementAt(i)) != null
            {

                //GameObject button = (GameObject)Instantiate(buttonPrefab);
                //button.GetComponent<Text>().text = ssidNames.ElementAt(i);
                //button.transform.parent = menuPanel;
                //button.GetComponent<Button>().onClick
                //----------------------------------------
                txtMeshes = GameObject.Find(ssidNames.ElementAt(i)).GetComponentsInChildren<TextMesh>();
                Debug.Log(ssidNames.ElementAt(i));
                Debug.Log("Count of Text meshes in " + ssidNames.ElementAt(i) + ": " + txtMeshes.Count());
                Debug.Log("TM1: " + txtMeshes[0].text + " | TM2: " + txtMeshes[1].text);
                txtMeshes[1].text = "DB: " + dWifi[ssidNames.ElementAt(i)];
            }
            else
            {
                Debug.Log(ssidNames.ElementAt(i) + " panel has not been created yet");
            }    
        }
        var lines = dWifi.Select(kvp=> kvp.Key+": "+kvp.Value.ToString());
#if !UNITY_EDITOR
        Debug.Log("["+string.Join(", ",lines)+"]");
#endif
    }

    void updateSignal()
    {
        for (int x = 0; x < ssidNames.Count; x++)
        {
            if (ssidNames.Count == sWifi.Count)
            {
                Debug.Log("Signal: "+ sWifi.Select(kvp => kvp.Key + ": " + kvp.Value.ToString()));
                if (sWifi[ssidNames.ElementAt(x)] == 3)
                {
                    GameObject.Find("ID23").GetComponent<Renderer>().material.color = Color.black;
                    GameObject.Find("ID11").GetComponent<Renderer>().material.color = Color.white;
                    GameObject.Find("ID17").GetComponent<Renderer>().material.color = Color.white;
                }
                else if (sWifi[ssidNames.ElementAt(x)] == 2)
                {
                    GameObject.Find("ID17").GetComponent<Renderer>().material.color = Color.black;
                    GameObject.Find("ID23").GetComponent<Renderer>().material.color = Color.black;
                    GameObject.Find("ID11").GetComponent<Renderer>().material.color = Color.white;
                }
                else if (sWifi[ssidNames.ElementAt(x)] == 1)
                {
                    GameObject.Find("ID17").GetComponent<Renderer>().material.color = Color.black;
                    GameObject.Find("ID23").GetComponent<Renderer>().material.color = Color.black;
                    GameObject.Find("ID11").GetComponent<Renderer>().material.color = Color.black;
                }
                else if (sWifi[ssidNames.ElementAt(x)] == 0)
                {
                    GameObject.Find("Wifi-" + ssidNames.ElementAt(x)).GetComponent<Renderer>().material.color = Color.red;
                }
            }
            else
            {
                Debug.Log(ssidNames.ElementAt(x) + " wifi bar has not been created yet");
            }  
        }
        var lines = sWifi.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
     #if !UNITY_EDITOR
        Debug.Log("[" + string.Join(", ", lines) + "]");
#endif
    }

    public void CreatePanels()
    {
        

        Vector3 posC = new Vector3(.133f, .133f + .8f, .615f - 8.11f);
        Vector3 posW = new Vector3(.096f, .157f + .8f, .6096f - 8.11f);
        Vector3 posB = new Vector3(.097f, .113f + .8f, .6077f - 8.11f);
        Vector3 posDB = new Vector3(.129f, .128f + .8f, .6108f - 8.11f);
        Vector3 posCompass = new Vector3(.0843f,  .9496f, - 7.5046f);
        Vector3 scaleC = new Vector3(.01f, .05f, .11f);
        Vector3 scaleW = new Vector3(.01f, .01f, .01f);
        Vector3 scaleCompass = new Vector3(.001f, .001f, .001f);
        Vector3 scaleB = new Vector3(1e-05f, 1e-05f, 1e-05f);

        Vector3 rotC = transform.rotation.eulerAngles;
        Vector3 rotB = transform.rotation.eulerAngles;
        Vector3 rotCompass = transform.rotation.eulerAngles;
        Vector3 rotDummy = transform.rotation.eulerAngles;

        rotC.y = 90.397f;
        rotB.x = -.333f;
        rotDummy.x = 0;
        rotCompass.y = -180f;
        var decPos = -.0547f;//56


        Debug.Log("# of SSID Allocated: " + ssidNames.Count());
        Debug.Log("In Create: [" + string.Join(", ", ssidNames.ToArray()) + "]");

        for (int x = 0; x< ssidNames.Count(); x++)//
        {
            GameObject dummy = new GameObject(""+ ssidNames.ElementAt(x));//
            // Fix for new panels

           
            NameCount = ssidNames.Count();
            

            if (x > 0)
            {
                posC = (new Vector3(posC.x, posC.y - decPos, posC.z ));
                posCompass = (new Vector3(posCompass.x, posCompass.y - decPos, posCompass.z));
                posW = (new Vector3(posW.x, posW.y - decPos, posW.z));
                posB = (new Vector3(posB.x, posB.y - decPos, posB.z));
                posDB = (new Vector3(posDB.x, posDB.y - decPos, posDB.z));
            }
            
                
                // creates new panel
                var panel = GameObject.CreatePrimitive(PrimitiveType.Cube);
                panel.transform.position = new Vector3(posC.x, posC.y, posC.z);
                panel.transform.localScale = new Vector3(scaleC.x, scaleC.y, scaleC.z);
                panel.transform.rotation = Quaternion.Euler(rotC);
                panel.AddComponent<ColorChange>();
                panel.transform.parent = dummy.transform;
                // creates new compass location
               /*GameObject compass = new GameObject();//
            compass = Instantiate(compassPrefab);
            compass.name = "Compass-" + ssidNames.ElementAt(x);
                //compass.AddComponent<Compass>();
                // compass.GetComponent<Compass>().player = Camera.main.transform;
                //compass.GetComponent<Compass>().POALayer = GameObject.Find("Compass-" + ssidNames.ElementAt(x)).transform;
                //Debug.Log("Child count of Compass-" + GameObject.Find("instance_0").GetType());
                //compass.GetComponent<Compass>().POALocation = GameObject.Find("box-" + ssidNames.ElementAt(x)).transform;
            compass.transform.position = posCompass;
                compass.transform.localScale = scaleCompass;
                compass.transform.rotation = Quaternion.Euler(rotCompass);
                compass.transform.parent = dummy.transform;*/
           
            // creates Names of wifi
            GameObject Text = new GameObject("ssid");
                Text.AddComponent<TextMesh>();
                var textMesh = Text.GetComponent<TextMesh>();
                textMesh.color = Color.black;
            textMesh.text = "" + ssidNames.ElementAt(x);//;
                Text.transform.position = posW;
                Text.transform.localScale = scaleW;
                Text.transform.parent = dummy.transform;
                // creates wifi bars
                GameObject Bars = new GameObject();//
                Bars = Instantiate(prefab);
                Bars.name = "wifi-" + ssidNames.ElementAt(x);
                Bars.transform.position = posB;
                Bars.transform.localScale = scaleB;
                Bars.transform.rotation = Quaternion.Euler(rotB);
                Bars.transform.parent = dummy.transform;

                //Create DB value
                GameObject DBText = new GameObject();//
                DBText.AddComponent<TextMesh>();
                var DBtextMesh = DBText.GetComponent<TextMesh>();
                DBtextMesh.name = "DB-" + ssidNames.ElementAt(x);
                DBtextMesh.color = Color.black;


                if (dWifi.ContainsKey(ssidNames.ElementAt(x)) == true)
                {
                    DBtextMesh.text = "DB: " + dWifi[ssidNames.ElementAt(x)];
                }
                else
                    DBtextMesh.text = "DB: ";
               

                DBText.transform.position = posDB;
                DBText.transform.localScale = scaleW;
                DBText.transform.parent = dummy.transform;
            // sets the panels to fixed position on canvas

            dummy.transform.parent = canvas.transform;
            }

        DisplayWifiObj();

    }
  

    private Transform posXYZ(string name)
    {

        var posXYZ = GameObject.Find(name).transform;
        Debug.Log("Name= " + GameObject.Find(name).transform);
        return posXYZ;
    }

    

    void DisplayWifiObj()
    {
        float maxPos = 20.0f;
        float minPos = 9.0f;
        var max = 22;
        Vector3 rotBox = transform.rotation.eulerAngles;
        rotBox.y = 180f;
        rotBox.x = -90f;


        for (var i = 0; i < ssidNames.Count; i++)//
        {
            
            var theNewPos = new Vector3(Random.Range(minPos, maxPos), 0, Random.Range(minPos, maxPos));
            GameObject box = new GameObject();// 
            box = Instantiate(wifiBoxprefab);
            box.name = "box-" + ssidNames.ElementAt(i);
            box.transform.position = theNewPos;
            box.transform.rotation = Quaternion.Euler(rotBox);
            box.GetComponentInChildren<TextMesh>().text = ssidNames.ElementAt(i);// 

            //GameObject.Find("box-" + ssidNames.ElementAt(i)).AddComponent<Billboard>();//
           // GameObject.Find("box-" ).GetComponent<Billboard>();//+ ssidNames.ElementAt(i)

            //Compass pointer
            GameObject pointer = new GameObject("pointTO-" + ssidNames.ElementAt(i));//
            pointer = Instantiate(linePrefab);
            //pointer.AddComponent<Compass>();
            pointer.GetComponent<Compass>().origin = GameObject.Find(ssidNames.ElementAt(i)).transform;//.transform.GetChild(1).transform;//
            pointer.GetComponent<Compass>().destination = GameObject.Find("box-" + ssidNames.ElementAt(i)).transform;

            
        }

        
    }


}
