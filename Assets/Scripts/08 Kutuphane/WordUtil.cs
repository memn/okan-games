using System.Collections.Generic;

public class WordUtil
{
    private static readonly Dictionary<string, string> Second = new Dictionary<string, string>
    {
        {"KIBLEMİZ", "Kabe"},
        {"KURAN'IN İNDİRİLDİĞİ ŞEHİRLER", "Mekke Medine"},
        {"SÜT ANNE", "Bereket Halime BeniSad"},
        {"BUSRA", "EbuTalib RahipBahira Kervan Şam"},
        {"KUTLU DOĞUM", "İrhasat Nisan Pazartesi Abdulmuttalib Amine"},
    };

    private static readonly Dictionary<string, string> Third = new Dictionary<string, string>
    {
        {"İLAHİ KİTAP", "Kuran"},
        {"BİLAL-İ HABEŞİ", "Ezan Müezzin"},
        {"HZ ÖMER", "Faruk Halife Adalet"},
        {"HİRA", "Nur Oku Vahiy Cebrail"},
        {"İSLAMIN ŞARTLARI", "KelimeiŞehadet Namaz Oruç Zekat Hac"},
    };

    private static readonly Dictionary<string, string> Fourth = new Dictionary<string, string>
    {
        {"PEYGAMBER", "Nebi"},
        {"İLİM YERLERİ", "DarulErkam Suffa"},
        {"AKABE", "Biat Müslüman Medine"},
        {"YARDIM", "Sadaka Zekat Fitre"},
        {"NUR DAĞI", "Vahiy Hira Oku Cebrail Nübüvvet"},
    };

    private static readonly Dictionary<string, string> Fifth = new Dictionary<string, string>
    {
        {"İLK MESCİD", "Kuba"},
        {"PEYGAMBER TORUNLARI", "Hasan Hüseyin"},
        {"MİRAC", "İsra Kudüs Namaz"},
        {"İBADET", "Farz Sünnet Nafile Vacib"},
        {"HİCRET", "Habeşistan Sevr Medine Ensar Muhacir"},
    };

    public static Dictionary<string, string> FindDictionaryByLevel(int level)
    {
        switch (level)
        {
            case 2:  return Second;
            case 3:  return Third;
            case 4:  return Fourth;
            case 5:  return Fifth;
            default: return null;
        }
    }
}