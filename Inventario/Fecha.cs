using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario
{
    class Fecha
    {
        public static void VerificarFecha()
        {

            var date1 = "30/03/2017";
            var date2 = "22/03/2017";

            DateTime dt1 = DateTime.Now;
            DateTime dt2 = dt1;
            var culture = CultureInfo.CreateSpecificCulture("es-MX");
            var styles = DateTimeStyles.None;


            bool fechaValida = DateTime.TryParse(date1, culture, styles, out dt1)
                               && DateTime.TryParse(date2, culture, styles, out dt2);

            Console.WriteLine(dt1 >= dt2);

            if (!fechaValida)
            {
                Console.WriteLine("Error en la fecha");
            }

            if (dt1 >= dt2)
            {
                Console.WriteLine("La Fecha de ingrese Tiene que ser menor a la Fecha Actual ..::Aviso del Sistema::..");
            }
        }
    }
}
