using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface Iproducto
    {

        List<producto> Lista();
        producto ListarProducto();

        producto Guardar(producto obje);
         producto Editar(producto obje);

         bool Eliminar(int idProducto);


    }
}
