using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ApiMongoDb.Data.Collections
{
    public class Infectado
    {
        public Infectado(DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Localizacao = new GeoJson2DGeographicCoordinates(latitude, longitude);
        }

        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}
