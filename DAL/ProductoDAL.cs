using ENTIDADES;
using System.Data;
using System.Data.SqlClient;




namespace DAL
{
    public class ProductoDAL
    {
        public List<producto> Lista()
        {


            List<producto> lista = new List<producto>();
            SqlConnection conn = new SqlConnection(Constantes.Conexion.Mece);


            try
            {
                var cmd = new SqlCommand(Constantes.ProcedimientoAlmacenado.Lista, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (var rd = cmd.ExecuteReader())
                {

                    while (rd.Read())
                    {

                        lista.Add(new producto
                        {
                            IdProducto = Convert.ToInt32(rd["IdProducto"]),
                            CodigoBarra = string.IsNullOrEmpty(rd["CodigoBarra"].ToString()) ? "null" : rd["CodigoBarra"].ToString(),
                            Nombre = string.IsNullOrEmpty(rd["Nombre"].ToString()) ? "Null" : rd["Nombre"].ToString(),
                            Marca = string.IsNullOrEmpty(rd["Marca"].ToString()) ? "null" : rd["Marca"].ToString(),
                            Categoria = string.IsNullOrEmpty(rd["Categoria"].ToString()) ? "Null" : rd["Categoria"].ToString(),
                            Precio = Convert.ToDecimal(rd["Precio"]),


                        });
                    }

                }

                return lista;

            }
            catch (Exception ex)
            {
                return lista;
            }

        }



        public producto Listar(int idproducto)
        {

            producto listar = new producto();
            ProductoDAL lista = new ProductoDAL();

            SqlConnection conn = new SqlConnection(Constantes.Conexion.Mece);

            try
            {
                conn.Open();
                var cmd = new SqlCommand(Constantes.ProcedimientoAlmacenado.ListarProducto, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdProducto", idproducto);


                using (var rd = cmd.ExecuteReader())
                {

                    while (rd.Read())
                    {

                        listar.IdProducto = Convert.ToInt32(rd["IdProducto"]);
                        listar.CodigoBarra = rd["CodigoBarra"].ToString();
                        listar.Nombre = rd["Nombre"].ToString();
                        listar.Marca = rd["Marca"].ToString();
                        listar.Categoria = rd["Categoria"].ToString();
                        listar.Precio = Convert.ToDecimal(rd["Precio"]);


                    }
                }


                return listar;


            }
            catch (Exception ex)
            {
                return listar;
            }

        }



        public producto Guardar(producto obje)
        {

            SqlConnection conex = new SqlConnection(Constantes.Conexion.Mece);

            try
            {
                {
                    conex.Open();
                    var cmd = new SqlCommand(Constantes.ProcedimientoAlmacenado.GuardarProducto, conex);
                    cmd.Parameters.AddWithValue("codigoBarra", obje.CodigoBarra);
                    cmd.Parameters.AddWithValue("nombre", obje.Nombre);
                    cmd.Parameters.AddWithValue("marca", obje.Marca);
                    cmd.Parameters.AddWithValue("categoria", obje.Categoria);
                    cmd.Parameters.AddWithValue("precio", obje.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }


                return obje;


            }
            catch (Exception ex)
            {
                throw ex;

            }
        }




        public producto Editar(producto obje)
        {
            SqlConnection conex = new SqlConnection(Constantes.Conexion.Mece);
            try
            {
                {
                    conex.Open();
                    var cmd = new SqlCommand("Editar_producto", conex);
                    cmd.Parameters.AddWithValue("idProducto", obje.IdProducto == 0 ? DBNull.Value : obje.IdProducto);
                    cmd.Parameters.AddWithValue("codigoBarra", obje.CodigoBarra is null ? DBNull.Value : obje.CodigoBarra);
                    cmd.Parameters.AddWithValue("nombre", obje.Nombre is null ? DBNull.Value : obje.Nombre);
                    cmd.Parameters.AddWithValue("marca", obje.Marca is null ? DBNull.Value : obje.Marca);
                    cmd.Parameters.AddWithValue("categoria", obje.Categoria is null ? DBNull.Value : obje.Categoria);
                    cmd.Parameters.AddWithValue("Precio", obje.Precio == 0 ? DBNull.Value : obje.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return obje;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }






        public bool Eliminar(int idProducto)
        {
            bool respuesta = false;
            SqlConnection con = new SqlConnection(Constantes.Conexion.Mece);



            try
            {

                con.Open();
                var cmd = new SqlCommand(Constantes.ProcedimientoAlmacenado.EliminarProducto, con);
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.CommandType = CommandType.StoredProcedure;


                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}


    