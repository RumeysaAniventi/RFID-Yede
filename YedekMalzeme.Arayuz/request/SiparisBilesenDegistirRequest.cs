namespace YedekMalzeme.Arayuz.request
{
    public class SiparisBilesenDegistirRequest : OrtakRequest
    {
        public string zepc { get; set; }
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
        public string zsernr { get; set; }
        public string zaufnr { get; set; }
    }
}