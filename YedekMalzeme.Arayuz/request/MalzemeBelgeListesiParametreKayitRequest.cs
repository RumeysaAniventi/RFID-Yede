namespace YedekMalzeme.Arayuz.request
{
    public class MalzemeBelgeListesiParametreKayitRequest : OrtakRequest
    {
        public string z_BodatLow { get; set; }
        public string z_BudatHigh { get; set; }
        public string z_Iwerk { get; set; }
        public string z_Aufnr { get; set; }

    }
}