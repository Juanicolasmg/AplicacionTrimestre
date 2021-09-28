using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicaciónTrimestre.Models;
using System.IO;

namespace AplicaciónTrimestre.Controllers
{
    public class ProductoImagenController : Controller
    {
        [Authorize]
        // GET: ProductoImagen
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_imagen.ToList());
            }
        }

        public static string NombreProducto(int idProducto)
        {
            using (var db = new inventario2021Entities())
            {
                return db.producto.Find(idProducto).nombre;
            }
        }

        public ActionResult ListarProductos()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto_imagen imagen)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto_imagen.Add(imagen);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto_imagen.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_imagen editImagen = db.producto_imagen.Where(a => a.id == id).FirstOrDefault();
                    return View(editImagen);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto_imagen editImagen)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_imagen oldImagen = db.producto_imagen.Find(editImagen.id);

                    oldImagen.imagen = editImagen.imagen;
                    oldImagen.id_producto = editImagen.id_producto;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto_imagen producto_imagen = db.producto_imagen.Find(id);
                    db.producto_imagen.Remove(producto_imagen);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult CargarImagen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CargarImagen(int id_producto, HttpPostedFileBase imagen)
        {
            try
            {
                //string para guardar la ruta
                string filePath = string.Empty;
                string nameFile = "";

                //condicion para saber si el archivo llego
                if (imagen != null)
                {
                    //ruta de la carpeta que guardara el archivo
                    string path = Server.MapPath("~/Uploads/Imagenes/");

                    //condicion para saber si la carpeta uploads existe
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    nameFile = Path.GetFileName(imagen.FileName);

                    //obtener el nombre del archivo
                    filePath = path + Path.GetFileName(imagen.FileName);

                    //obtener la extension del archivo
                    string extension = Path.GetExtension(imagen.FileName);

                    //guardar el archivo
                    imagen.SaveAs(filePath);
                }

                using (var db = new inventario2021Entities())
                {
                    var imagenProducto = new producto_imagen();
                    imagenProducto.id_producto = id_producto;
                    imagenProducto.imagen = "/Uploads/Imagenes/" + nameFile;
                    db.producto_imagen.Add(imagenProducto);
                    db.SaveChanges();

                }

                return View();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }

        }
    }
}