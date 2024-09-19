using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [Header("---ARABA AYARLARI")]
    public GameObject[] arabalar;
    public int kacAraba;
    int kalanAracSayisiDegeri;
    int aktifAracindex=0;
    
   

    [Header("---PLATFORM AYARLARI")]
    public GameObject platform_1;
    public GameObject platform_2;
    public float[] DonusHizlari;
    public bool donusvarmi;


    [Header("---CANVAS AYARLARI")]
    public GameObject[] arabaCanvasGorselleri;
    public Sprite parkbasariliGorsel;
    public Sprite parkbasarisizGorsel;
    public TextMeshProUGUI KalanAracSayisi;
    public TextMeshProUGUI[] textler;
    public GameObject[] panellerim;
    public GameObject[] TapToButonlar;


    [Header("---LEVEL AYARLARI")]
    public GameObject[] elmaslar;
    public int elmasSayisi;
    public AudioSource[] sesler;
    public ParticleSystem carpmaEfekt;
    public ParticleSystem winEfekt;
    public bool yukselecekPltform;

    bool dokunmaKilidi;



   
    void Start()
    {  
        dokunmaKilidi=true;
        donusvarmi=true;
        varsayilanDegerleriKontrolEt();
        kalanAracSayisiDegeri=kacAraba;
       /* KalanAracSayisi.text=kalanAracSayisiDegeri.ToString();
       for (int i = 0;i<kacAraba;i++)
        {
            arabaCanvasGorselleri[i].SetActive(true);
        }
        */
        
    }
    public void yeniArabaGetir()
    {
        kalanAracSayisiDegeri--;
        if (aktifAracindex < kacAraba)
        {
            arabalar[aktifAracindex].SetActive(true);
        }
        else
        {
            kazandin();
            
            
        }
        
    }
    public void BasariliPark()
    {
        arabaCanvasGorselleri[aktifAracindex-1].GetComponent<UnityEngine.UI.Image>().sprite=parkbasariliGorsel;
    }
     public void BasarisizPark()
    {
        arabaCanvasGorselleri[aktifAracindex-1].GetComponent<UnityEngine.UI.Image>().sprite=parkbasarisizGorsel;
    }

    // Update is called once per frame
    void Update()
    {   
       if (UnityEngine.Input.touchCount == 1)
{
    Touch touch = UnityEngine.Input.GetTouch(0);
    if (touch.phase == TouchPhase.Began)
    {
        Debug.Log("Dokunma algılandı.");
        if (dokunmaKilidi == true)
        {
            Debug.Log("Dokunma kilidi aktif. Paneli kapatıyorum.");
            panellerim[0].SetActive(false);
            dokunmaKilidi = false;
        }
        else
        {
            Debug.Log("Araba ilerlemeye başladı.");
            arabalar[aktifAracindex].GetComponent<araba>().ilerle = true;
            aktifAracindex++;
        }
    }
}
        if(donusvarmi==true)
        {
          platform_1.transform.Rotate(new Vector3(0, 0,DonusHizlari[0]),Space.Self); 
          platform_2.transform.Rotate(new Vector3(0, 0,-DonusHizlari[1]),Space.Self); 

        }
        
        

        

        
    }

    void kazandin()
    {
    
    PlayerPrefs.SetInt("elmas", PlayerPrefs.GetInt("elmas") + elmasSayisi);
    PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
    PlayerPrefs.Save();
    textler[2].text = PlayerPrefs.GetInt("elmas").ToString();
    textler[3].text = SceneManager.GetActiveScene().name;
    textler[4].text = (kacAraba - kalanAracSayisiDegeri).ToString();
    textler[5].text = elmasSayisi.ToString();
    sesler[2].Play();
    donusvarmi=false;
    panellerim[2].SetActive(true);
    Invoke("winShowbutton", 2f);
    winEfekt.Play();

    }
    public void kaybettin()
    { 

        panellerim[1].SetActive(true);
        textler[6].text=PlayerPrefs.GetInt("elmas").ToString();
        textler[7].text=SceneManager.GetActiveScene().name;
        textler[8].text=(kacAraba-kalanAracSayisiDegeri).ToString();
        textler[9].text=elmasSayisi.ToString();
        donusvarmi=false;
        sesler[1].Play();
        Invoke("loseShowbutton",2f);
              
    }
    void loseShowbutton()
    {
        TapToButonlar[0].SetActive(true);
    }
    void winShowbutton()
    {
        TapToButonlar[1].SetActive(true);
    }

    //bellekyönetim
    void varsayilanDegerleriKontrolEt()
    {
    
    textler[0].text = PlayerPrefs.GetInt("elmas").ToString();
    textler[1].text = SceneManager.GetActiveScene().name;

    }

    public void devam()
    {
                    panellerim[0].SetActive(false);
    }
    public void izleveDahaFazlaKazan()
    {
        //
    }
    public void sonrakiLevel()
   {
    int yeniLevel = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("level", yeniLevel);
        PlayerPrefs.Save();  

        SceneManager.LoadScene(yeniLevel);
   }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}