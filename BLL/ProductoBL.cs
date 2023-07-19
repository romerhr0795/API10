using System.Data.SqlClient;
using System.Data;
using DAL;
using ENTIDADES;
using BLL.Interfaces;
using System.Collections.Generic;

namespace BLL
{
    public class ProductoBL
    {
        public List<producto> lista()
        {

            ProductoDAL producto= new  ProductoDAL();
            return producto.Lista();

        }

        public producto ListarProducto(int idproducto)
        {
            producto listar = new producto();
            return listar;
        }

        public producto Guardar(producto obje)
        {
            return obje;
        }

        public producto Editar(producto obje)
        {
            return obje;    
        }

        public bool Eliminar(int idProducto)
        {
            return false;
        }

    }

}