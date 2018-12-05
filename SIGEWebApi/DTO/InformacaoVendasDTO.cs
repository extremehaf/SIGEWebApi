using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.DTO
{
    public class InformacaoVendasDTO
    {
        public int idVenda { get; set; }
        public int idFuncionario { get; set; }
        public int quantidade { get; set; }
        public int qualidade { get; set; }
        public int avaliacao { get; set; }
        public DateTime data { get; set; }
        public string Cliente { get; set; }

        public string MesCorrente
        {
            get
            {
                return ConvertDataToString(this.data.Month);
            }
        }

        private string ConvertDataToString(int mes)
        {
            switch (mes)
            {

                case 1:
                    return "janeiro";

                case 2:
                    return "fevereiro";

                case 3:
                    return "março";

                case 4:
                    return "abril";

                case 5:
                    return "maio";

                case 6:
                    return "junho";

                case 7:
                    return "julho";

                case 8:
                    return "agosto";

                case 9:
                    return "setembro";

                case 10:
                    return "outubro";

                case 11:
                    return "novembro";

                case 12: return "dezembro";

                default: return "indefinido";
            }
        }
    }
}