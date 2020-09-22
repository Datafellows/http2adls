using System;

namespace DataFellows.HttpConsumer
{
    public class AdlsConfiguration
    {
        private string _filename;
        public string ConnectionString { get; set; }

        public string Container { get; set; }
        public string Filename 
        { 
            get
            {
                return _filename
                    .Replace("{year}", DateTime.UtcNow.Year.ToString("0000"))
                    .Replace("{month}", DateTime.UtcNow.Month.ToString("00"))
                    .Replace("{day}", DateTime.UtcNow.Day.ToString("00"))
                    .Replace("{hour}", DateTime.UtcNow.Hour.ToString("00"))
                    .Replace("{minute}", DateTime.UtcNow.ToString("00"));
            }
            set
            {
                _filename = value;
            }
        }
    }
}