using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblzrfidorderlistreturn")]
    public class tblzrfidorderlistreturn: TabloObject
    {
        public tblzrfidorderlistreturn(Session session) : base(session) { }

        string _istekno = "";
        [Persistent("istekno")]
        [Size(255)]
        public string istekno
        {
            get { return _istekno; }
            set { SetPropertyValue<string>("istekno", ref _istekno, value); }
        }

        string _number = "";
        [Persistent("number")]
        [Size(255)]
        public string number
        {
            get { return _number; }
            set { SetPropertyValue<string>("number", ref _number, value); }
        }

        string _message = "";
        [Persistent("message")]
        [Size(255)]
        public string message
        {
            get { return _message; }
            set { SetPropertyValue<string>("message", ref _message, value); }
        }
        string _logno = "";
        [Persistent("logno")]
        [Size(255)]
        public string logno
        {
            get { return _logno; }
            set { SetPropertyValue<string>("logno", ref _logno, value); }
        }
        string _logmsgno = "";
        [Persistent("logmsgno")]
        [Size(255)]
        public string logmsgno
        {
            get { return _logmsgno; }
            set { SetPropertyValue<string>("logmsgno", ref _logmsgno, value); }
        }

        string _messagev1 = "";
        [Persistent("messagev1")]
        [Size(255)]
        public string messagev1
        {
            get { return _messagev1; }
            set { SetPropertyValue<string>("messagev1", ref _messagev1, value); }
        }

        string _messagev2 = "";
        [Persistent("messagev2")]
        [Size(255)]
        public string messagev2
        {
            get { return _messagev2; }
            set { SetPropertyValue<string>("messagev2", ref _messagev2, value); }
        }

        string _messagev3 = "";
        [Persistent("messagev3")]
        [Size(255)]
        public string messagev3
        {
            get { return _messagev3; }
            set { SetPropertyValue<string>("messagev3", ref _messagev3, value); }
        }

        string _messagev4 = "";
        [Persistent("messagev4")]
        [Size(255)]
        public string messagev4
        {
            get { return _messagev4; }
            set { SetPropertyValue<string>("messagev4", ref _messagev4, value); }
        }

        string _parameter = "";
        [Persistent("parameter")]
        [Size(255)]
        public string parameter
        {
            get { return _parameter; }
            set { SetPropertyValue<string>("parameter", ref _parameter, value); }
        }

        string _row = "";
        [Persistent("row")]
        [Size(255)]
        public string row
        {
            get { return _row; }
            set { SetPropertyValue<string>("row", ref _row, value); }
        }

        string _field = "";
        [Persistent("field")]
        [Size(255)]
        public string field
        {
            get { return _field; }
            set { SetPropertyValue<string>("field", ref _field, value); }
        }

        string _system = "";
        [Persistent("system")]
        [Size(255)]
        public string system
        {
            get { return _system; }
            set { SetPropertyValue<string>("system", ref _system, value); }
        }
        string _type = "";
        [Persistent("type")]
        [Size(255)]
        public string type
        {
            get { return _type; }
            set { SetPropertyValue<string>("type", ref _type, value); }
        }
    }
}
