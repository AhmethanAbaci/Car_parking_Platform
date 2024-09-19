using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class araba : MonoBehaviour
{
    public bool ilerle;
    public Transform parent;
    public GameObject[] tekerizi;
    public gameManager _gameManager;
    bool durusnoktasiDurumu = false;
    public GameObject parcpoint;
    bool platformYukselt;

    float yukselmeDeger;

    void Update()
    {
        if (!durusnoktasiDurumu)
        {
            transform.Translate(UnityEngine.Vector3.forward * 5f * Time.deltaTime);
        }

        if (ilerle)
        {
            transform.Translate(UnityEngine.Vector3.forward * 10f * Time.deltaTime);
        }
        if(platformYukselt)
        {

            if(yukselmeDeger>_gameManager.platform_1.transform.position.y)
            {
               _gameManager.platform_1.transform.position = UnityEngine.Vector3.Lerp(_gameManager.platform_1.transform.position,new UnityEngine.Vector3(_gameManager.platform_1.transform.position.x,
               _gameManager.platform_1.transform.position.y+1.3f,_gameManager.platform_1.transform.position.z),.010f); 
            }
            else
            {
                platformYukselt=false;
            }

            

            
        }
    }

    void OnCollisionEnter(Collision collision)
    {
         
        
        
         if (collision.gameObject.CompareTag("parking"))
        {
            ilerle = false; 
            tekerizi[0].SetActive(false); 
            tekerizi[1].SetActive(false); 
            transform.SetParent(parent); 
            GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionZ;
            GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeRotationX|RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezeRotationY;
            if(_gameManager.yukselecekPltform)
            {
                yukselmeDeger=_gameManager.platform_1.transform.position.y +1.3f;
                platformYukselt=true;
            }
           
            
            _gameManager.yeniArabaGetir(); 
            _gameManager.BasariliPark();

        }
        
        
        else if (collision.gameObject.CompareTag("araba"))
        {
            aracislem();
            _gameManager.sesler[3].Play();

            _gameManager.kaybettin();

        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("durusNoktasi"))
        {
            durusnoktasiDurumu = true;
            
        }
        else if (other.gameObject.CompareTag("elmas"))
        {
            _gameManager.elmasSayisi++;
            _gameManager.sesler[0].Play();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("anaKolon") )
        {
            Destroy(gameObject);
            aracislem();
            _gameManager.sesler[3].Play();

            _gameManager.kaybettin();
            
            

        }
         else if (other.gameObject.CompareTag("on_parking") )
        {
            other.gameObject.GetComponent<on_parking>().parking.SetActive(true);
        }
    }
    void aracislem()
    {
        _gameManager.carpmaEfekt.transform.position=parcpoint.transform.position;
        _gameManager.carpmaEfekt.Play();
        ilerle=false;
    }
}

    

