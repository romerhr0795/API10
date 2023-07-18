using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using APICORE10.Models;
using System.Collections.Generic;
using System.Collections;

namespace APICORE10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly string Cadenasql;

        public ProductoController (IConfiguration config)
        {
            Cadenasql = config.GetConnectionString("cadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista() 
        
        { 
            List<Producto> lista = new List<Producto>();
            SqlConnection conn = new SqlConnection(Cadenasql);

            try
            {
                var cmd = new SqlCommand("sp_lista_productos", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using(var rd = cmd.ExecuteReader()) { 

                    while (rd.Read())
                    {

                        lista.Add(new Producto
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

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = lista });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = lista });

            }
            finally
            {
                conn.Close();
            }
          

        }
        [HttpGet]
        [Route("Obtener_Lista/{idproducto:int}")]


        public IActionResult Obtener_Lista(int idproducto)
        {

           Producto listar = new Producto();

            SqlConnection con = new SqlConnection(Cadenasql);

            try
            {
                con.Open();
                var cmd = new SqlCommand("ObtenerProductoPorId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("IdProducto", idproducto);


                using ( var rd = cmd.ExecuteReader() )
                {

                    while (rd.Read())
                    {

                        listar.IdProducto = Convert.ToInt32(rd["IdProducto"]);
                        listar.CodigoBarra = rd["CodigoBarra"].ToString();
                        listar.Nombre = rd["Nombre"].ToString();
                        listar.Marca = rd["Marca"].ToString();
                        listar.Categoria = rd["Categoria"].ToString() ;
                        listar.Precio = Convert.ToDecimal(rd["Precio"]);
                            

                    }
                }


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = listar });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = listar });
            }
            finally
            {
                con.Close();   
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody]  Producto obje)
        {

            SqlConnection conex = new SqlConnection(Cadenasql);

            try
            {
                {
                    conex.Open();
                    var cmd = new SqlCommand("sp_guardar_producto", conex);
                    cmd.Parameters.AddWithValue("codigoBarra", obje.CodigoBarra);
                    cmd.Parameters.AddWithValue("nombre", obje.Nombre);
                    cmd.Parameters.AddWithValue("marca", obje.Marca);
                    cmd.Parameters.AddWithValue("categoria", obje.Categoria);
                    cmd.Parameters.AddWithValue("precio", obje.Precio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new {mensaje = ex.Message});    

            }
            finally { conex?.Close(); } 
        }

        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Producto obje) {
            SqlConnection conex = new SqlConnection(Cadenasql);
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

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });

            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "error" });
            }
            finally
            {
                conex.Close();
            }

            
        }
        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]

        public IActionResult Eliminar(int idProducto)
        {
            SqlConnection con = new SqlConnection(Cadenasql);

            try
            {


                con.Open();
                var cmd = new SqlCommand("Eliminar_Producto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProducto", idProducto);
                cmd.ExecuteNonQuery();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = " Producto eliminado" });

            }catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new {mensaje = ex.Message});
            }
            finally
            {
                con.Close();
            }
        }

    }
}
