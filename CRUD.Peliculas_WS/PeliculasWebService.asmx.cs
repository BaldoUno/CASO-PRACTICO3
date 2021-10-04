using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CRUD.Peliculas_WS.Models;

namespace CRUD.Peliculas_WS
{
    /// <summary>
    /// Descripción breve de PeliculasWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PeliculasWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string Saludo()
        {
            return "¡Hola a todos, Bienvenidos a Servicio Web CRUD Peliculas!";
        }

        #region CRUD de peliculas
        //Create Peliculas
        [WebMethod(Description = "Insertar pelicula a la tabla peliculas")]
        public bool CreatePelicula(string titulo, string fechaestreno, string genero) 
        {
            try
            {
                using (peliculaEntities conexion = new peliculaEntities())
                {
                    Pelicula nuevo = new Pelicula();
                    nuevo.id = Guid.NewGuid();
                    nuevo.Titulo = titulo;
                    nuevo.FechaEstreno = fechaestreno;
                    nuevo.Genero = genero;
                    conexion.Peliculas.Add(nuevo);
                    conexion.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Read
        [WebMethod(Description = "Obtener la lista de la tabla peliculas")]

        public List<Pelicula> GetPeliculas()
        {
            using (peliculaEntities conexion = new peliculaEntities())
            {
                var consulta = (from all in conexion.Peliculas select all);
                return consulta.ToList();
            }
        }
        //Update
        [WebMethod(Description = "Modifica pelicula de la tabla peliculas")]
        public bool UpdatePelicula(Guid Id, string titulo, string fechaestreno, string genero)
        {
            try
            {
                using (peliculaEntities conexion = new peliculaEntities())
                {
                    var consulta = (from up in conexion.Peliculas where up.id == Id select up).FirstOrDefault();
                    if (consulta != null)
                    {
                        consulta.Titulo = titulo;
                        consulta.FechaEstreno = fechaestreno;
                        consulta.Genero = genero;
                        conexion.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Delete
        [WebMethod(Description = "Elimina pelicula de la tabla peliculas")]
        public bool DeletePelicula(Guid Id)
        {
            try
            {
                using (peliculaEntities conexion = new peliculaEntities())
                {
                    var consulta = (from del in conexion.Peliculas where del.id == Id select del).FirstOrDefault();
                    if (consulta != null)
                    {
                        conexion.Peliculas.Remove(consulta);
                        conexion.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

    }
}
