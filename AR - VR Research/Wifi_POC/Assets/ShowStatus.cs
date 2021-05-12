using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using Assets;





public class ShowStatus : MonoBehaviour {
    // KeywordRecognizer object.
    KeywordRecognizer keywordRecognizer;
    // Defines which function to call when a keyword is recognized.
    delegate void KeywordAction(PhraseRecognizedEventArgs args);
    Dictionary<string, KeywordAction> keywordCollection;
    public GameObject prefab;
    public Canvas canvas;
    // Vector postions for new panel
    Vector3 posCube = new Vector3(.017f ,0,26.77f );//+.01f +.99f -16.791f
    Vector3 posName = new Vector3(-.691f , .345f , 26.77f );
    Vector3 posBars = new Vector3(0, -.04599f,26.6f);
    Vector3 posDB = new Vector3(.243f,.344f,26.77f);
    Vector3 posMAC = new Vector3(-.384f,-.183f,26.77f);
    Vector3 scaleCube = new Vector3(.1f, .7570211f, 1.549753f);
    Vector3 scaleWords = new Vector3(.1f, .1f,.1f);
    Vector3 scaleBars = new Vector3(0.0002335939f, .0001100214f, 0.0001f);
#if !UNITY_EDITOR
     //private IWiFiAdapter<wifis> WiFiAdapter;
#endif

    // Use this for initialization
    void Start () {
        keywordCollection = new Dictionary<string, KeywordAction>();

        // Add keyword to start manipulation.
        keywordCollection.Add("Show Status", ShowStats);
        keywordCollection.Add("Close", Kill);
        // Initialize KeywordRecognizer with the previously added keywords.
        keywordRecognizer = new KeywordRecognizer(keywordCollection.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();

    }
	
	// Update is called once per frame
	void Update () {
        //var cam = posXYZ("Main Camera");
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        KeywordAction keywordAction;

        if (keywordCollection.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(args);
        }
    }
    private void Kill(PhraseRecognizedEventArgs args) {

        ShowPanels();

        var objects = GameObject.Find("Panel");
        Destroy(objects);
    }
    private void HidePanels() {

        foreach(Transform child in canvas.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    private void ShowPanels()
    {

        foreach (Transform child in canvas.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    private void ShowStats(PhraseRecognizedEventArgs args)
    {
        
        GameObject dummy = new GameObject("Panel");

        //WiFiAdapter = new UniversalWiFi();
        //var list = WiFiAdapter.getNetworks();
        // rotate the cube and bars prefabs
        Vector3 rotC = transform.rotation.eulerAngles;
        Vector3 rotB = transform.rotation.eulerAngles;
        rotC.y = 90.397f;
        rotB.x = -.333f;
        
        // creates new panel
        var panel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        panel.transform.position = new Vector3(posCube.x, posCube.y, posCube.z);
        panel.transform.localScale = new Vector3(scaleCube.x, scaleCube.y, scaleCube.z);
        panel.transform.rotation = Quaternion.Euler(rotC);
        panel.AddComponent<ColorChange>();
        panel.transform.parent = dummy.transform;
        // creates Names of wifi
        GameObject Text = new GameObject();
        Text.AddComponent<TextMesh>();
        var textMesh = Text.GetComponent<TextMesh>();
        textMesh.color = Color.black;
       // textMesh.text = "Name: " + list.Count;
        Text.transform.position = posName;
        Text.transform.localScale = scaleWords;
        Text.transform.parent = dummy.transform;
        // creates wifi bars
        GameObject Bars = new GameObject();
        Bars = Instantiate(prefab);
        Bars.transform.position = posBars;
        Bars.transform.localScale = scaleBars;
        Bars.transform.rotation = Quaternion.Euler(rotB);
        Bars.transform.parent = dummy.transform;
        //Create DB value
        GameObject DBText = new GameObject();
        DBText.AddComponent<TextMesh>();
        var DBtextMesh = DBText.GetComponent<TextMesh>();
        DBtextMesh.color = Color.black;
        DBtextMesh.text = "DBs ";
        DBText.transform.position = posDB;
        DBText.transform.localScale = scaleWords;
        DBText.transform.parent = dummy.transform;
        //Create MAC address
        GameObject MACText = new GameObject();
        MACText.AddComponent<TextMesh>();
        var MACtextMesh = MACText.GetComponent<TextMesh>();
        MACtextMesh.color = Color.black;
        MACtextMesh.text = "MAC Address";
        MACText.transform.position = posMAC;
        MACText.transform.localScale = scaleWords;
        MACText.transform.parent = dummy.transform;

        dummy.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y  - 1f, Camera.main.transform.position.z - 16.7f);

        HidePanels();
    }

    private Vector3 posXYZ(string name)
    {
        var posXYZ = GameObject.Find(name).transform.position;
        return posXYZ;
    }

    private void Follow (GameObject x)
    {
        
    }
}
