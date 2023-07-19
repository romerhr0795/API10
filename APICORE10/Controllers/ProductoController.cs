using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using BLL;
using ENTIDADES;
using System.Collections.Generic;
using System.Collections;
using BLL.Interfaces;
using DAL;
using System.Data.Common;
using System.Diagnostics;
using System;

namespace APICORE10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly string Cadenasql;

        public ProductoController(IConfiguration config)
        {
            Cadenasql = config.GetConnectionString("cadenaSQL");
        }





        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {
            try
            {



                ProductoBL productosBL = new ProductoBL();
                List<producto> productos = productosBL.lista();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", Response = productos });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = new List<producto>() });

            }
        }




        [HttpGet]
        [Route("Obtener_Un_Producto/{idProducto:int}")]

        public IActionResult ListarProducto(int idProducto)
        {
            producto listar = new producto();

            SqlConnection con = new SqlConnection(Constantes.Conexion.Mece);

            try
            {


                return (IActionResult)listar;

            }
            catch (Exception ex)
            {
#pragma warning disable CA2200 // Rethrow to preserve stack details
                throw ex;
#pragma warning restore CA2200 // Rethrow to preserve stack details
            }
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] producto obje)
        {

            SqlConnection con = new SqlConnection(Constantes.Conexion.Mece);
            try
            {



                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });

            }
        }
                

            [HttpPut]
            [Route("Editar")]

            public IActionResult Editar([FromBody] producto obje)
        {
            SqlConnection conex = new SqlConnection(Constantes.Conexion.Mece);
            try
            {
                

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });

            }
            catch (Exception ex)
            {
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
            SqlConnection con = new SqlConnection(Constantes.Conexion.Mece);

            try
            {




                return StatusCode(StatusCodes.Status200OK, new { mensaje = " Producto eliminado" });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
            finally
            {
                con.Close();
            }
        }

    }
}




