namespace Arayuz.Response
{
    public class ViewOrtak
    {
        public string _zAciklama { get; set; }
        public IslemSonucu _zSonuc { get; set; }
    }

    public enum IslemSonucu
    {
        BASARILI = 1,
        HATALI = 2


    }
}