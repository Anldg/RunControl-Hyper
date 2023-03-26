using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Anıl;

public class Ayarlar_Manager : MonoBehaviour
{
    public AudioSource ButonSes;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _VeriYonetim = new VeriYonetimi();
    public List<DilVerileriAnaObje> _DilVerileriAnaObje = new List<DilVerileriAnaObje>();
    List<DilVerileriAnaObje> _DilOkunanVeriler = new List<DilVerileriAnaObje>();
    public TextMeshProUGUI[] TextObjeleri;
    [Header("---------DİL TERCİHİ OBJELERİ")]
    public TextMeshProUGUI DilText;
    public Button[] DilButonlari;

    void Start()
    {
        ButonSes.volume = _BellekYonetim.VeriOku_f("MenuFx");
        MenuSes.value = _BellekYonetim.VeriOku_f("MenuSes");
        MenuFx.value = _BellekYonetim.VeriOku_f("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_f("OyunSes");       

        _VeriYonetim.Dil_Load();
        _DilOkunanVeriler = _VeriYonetim.DilVerileriListeyiAktar();
        _DilVerileriAnaObje.Add(_DilOkunanVeriler[4]);
        DilTercihiYonetimi();
        DilDurumunuKontrolEt();
    }

    void DilTercihiYonetimi()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_TR[i].Metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjeleri.Length; i++)
            {
                TextObjeleri[i].text = _DilVerileriAnaObje[0]._DilVerileri_EN[i].Metin;
            }
        }
    }
    public void SesAyarla(string HangiAyar)
    {
        switch (HangiAyar)
        {
            case "menuses":                
                 _BellekYonetim.VeriKaydet_float("MenuSes", MenuSes.value);
                break;

            case "menufx":                
                _BellekYonetim.VeriKaydet_float("MenuFx", MenuFx.value);
                break;

            case "oyunses":                
                _BellekYonetim.VeriKaydet_float("OyunSes", OyunSes.value);
                break;
        }
    }
    public void GeriDon()
    {
        ButonSes.Play();
        SceneManager.LoadScene(0);
    }    

    void DilDurumunuKontrolEt()
    {
        if (_BellekYonetim.VeriOku_s("Dil") == "TR")
        {
            DilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
        }
        else
        {
            DilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;
        }
    }
    public void DilDegistir(string Yon)
    {
        if (Yon == "ileri")
        {
            DilText.text = "ENGLISH";
            DilButonlari[1].interactable = false;
            DilButonlari[0].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "EN");
            DilTercihiYonetimi();
        }
        else
        {
            DilText.text = "TÜRKÇE";
            DilButonlari[0].interactable = false;
            DilButonlari[1].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "TR");
            DilTercihiYonetimi();
        }
        ButonSes.Play();
    }
}
