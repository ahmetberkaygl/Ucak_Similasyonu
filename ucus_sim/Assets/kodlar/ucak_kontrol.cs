using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UcakKontrol : MonoBehaviour
{
    // Uçuş parametreleri
    public float limitHiz = 100f;  // Uçağın başlangıç hızı
    public float maxLimitHiz = 250f;  // Uçağın maksimum hızı
    public float minLimitHiz = 50f;  // Uçağın minimum hızı
    public float yukariAsagiHassasiyeti = 2f;  // Yukarı ve aşağı hareket hassasiyeti
    public float yatayHassasiyet = 2f;  // Yatay hareket (yaw) hassasiyeti
    public float rollHassasiyeti = 2f;  // Yatış (roll) hassasiyeti
    public float hizlanmaOrani = 10f;  // Hızlanma ve yavaşlama oranı

    public Text hizGosterge;  // Uçağın hızını gösterecek Text referansı
    public Text maxHizMesaji;  // Maksimum hız mesajını gösterecek Text referansı

    void Update()
    {
        // Kullanıcı girdi kontrolleri
        float yukariAsagi = 0f;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            yukariAsagi = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            yukariAsagi = -1f;
        }

        // Hız kontrolü
        if (Input.GetKey(KeyCode.W))
        {
            limitHiz += hizlanmaOrani * Time.deltaTime;
            limitHiz = Mathf.Clamp(limitHiz, minLimitHiz, maxLimitHiz); // Hızı min ve max limitler arasında tutar
        }
        else if (Input.GetKey(KeyCode.S))
        {
            limitHiz -= hizlanmaOrani * Time.deltaTime;
            limitHiz = Mathf.Clamp(limitHiz, minLimitHiz, maxLimitHiz); // Hızı min ve max limitler arasında tutar
        }

        // Yaw (sağa ve sola dönüş) kontrolleri
        float sagaSolaDonus = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            sagaSolaDonus = -1f; // Sola dön
        }
        else if (Input.GetKey(KeyCode.D))
        {
            sagaSolaDonus = 1f; // Sağa dön
        }

        // Roll (sağa ve sola yatma) kontrolleri
        float sagaSolaYatis = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sagaSolaYatis = 1f; // Sağa yat
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            sagaSolaYatis = -1f; // Sola yat
        }

        // Uçağın ileri hareketi
        transform.Translate(Vector3.forward * limitHiz * Time.deltaTime);

        // Uçağın yukarı ve aşağı hareketi
        transform.Rotate(Vector3.right * yukariAsagiHassasiyeti * yukariAsagi * Time.deltaTime);

        // Uçağın yatay (yaw) hareketi
        transform.Rotate(Vector3.up * yatayHassasiyet * sagaSolaDonus * Time.deltaTime);

        // Uçağın yatış (roll) hareketi
        transform.Rotate(Vector3.forward * -rollHassasiyeti * sagaSolaYatis * Time.deltaTime);

        // Hız göstergesini güncelle
        if (hizGosterge != null)
        {
            hizGosterge.text = "Hız: " + limitHiz.ToString("F1") + " km/h";
        }

        // Maksimum hız mesajını güncelle
        if (maxHizMesaji != null)
        {
            if (limitHiz >= maxLimitHiz)
            {
                maxHizMesaji.text = "Maksimum Hız Limitine Ulaşıldı!!!";
                maxHizMesaji.enabled = true;  // Mesajı göster
            }
            else
            {
                maxHizMesaji.enabled = false;  // Mesajı gizle
            }
        }
    }
}
