using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelYukle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1); 
            PlayerPrefs.Save(); 
        }

        // Kayıtlı level değerini yükle
        int levelToLoad = PlayerPrefs.GetInt("level", 1); 
        SceneManager.LoadScene(levelToLoad); 
    }

    
}
