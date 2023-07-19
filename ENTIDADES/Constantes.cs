using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
public static  class Constantes
    {
        public static class ProcedimientoAlmacenado
        {
            public const string Lista = "sp_lista_productos";
            public const string ListarProducto = "ObtenerProductoPorId";
            //public const string _guardarProducto = "sp_guardar_producto";
            public static string GuardarProducto = "sp_guardar_producto";
            public static string EditarProducto = "Editar_producto";
            public static string EliminarProducto = "Eliminar_Producto";



        }
        public static class Conexion
        {

            public const string Mece = "Data Source=10.10.72.161\\SQL2016_1;Initial Catalog=APIHR;Integrated Security=True;";

        }

    }
}
