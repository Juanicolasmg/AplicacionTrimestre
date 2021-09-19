using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicaciónTrimestre.Models;
using Rotativa;
using System.IO;

namespace AplicaciónTrimestre.Controllers
{
    public class ProductoController : Controller
    {
        [Authorize]
        // GET: Producto
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.producto.ToList());
            }
        } 

        public static string NombreProveedor(int idproveedor)
        {
            using (var db = new inventario2021Entities())
            {
                return db.proveedor.Find(idproveedor).nombre;
            }
        }

        public ActionResult ListarProveedores()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.proveedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.producto.Add(producto);
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
                return View(db.producto.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto productEdit = db.producto.Where(a => a.id == id).FirstOrDefault();
                    return View(productEdit);
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
        public ActionResult Edit(producto productoEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    producto oldProduct = db.producto.Find(productoEdit.id);

                    oldProduct.nombre = productoEdit.nombre;
                    oldProduct.percio_unitario = productoEdit.percio_unitario;
                    oldProduct.descripcion = productoEdit.descripcion;
                    oldProduct.cantidad = productoEdit.cantidad;
                    oldProduct.id_proveedor = productoEdit.id_proveedor;

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
                    producto producto = db.producto.Find(id);
                    db.producto.Remove(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Reporte()
        {
            try
            {
                var db = new inventario2021Entities();
                var query = from tabProveedor in db.proveedor
                            join tabProducto in db.producto on tabProveedor.id equals tabProducto.id_proveedor
                            select new Reporte
                            {
                                nombreProveedor = tabProveedor.nombre,
                                telefonoProveedor = tabProveedor.telefono,
                                direccionProveedor = tabProveedor.direccion,
                                nombreProducto = tabProducto.nombre,
                                precioProducto = tabProducto.percio_unitario
                            };
                return View(query);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error" + ex);
                return View();
            }
        }

        public ActionResult pdfReporte()
        {
            return new ActionAsPdf("Reporte") { FileName = "reporte.pdf" };
        }

        public ActionResult uploadCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadCSV(HttpPostedFileBase fileForm)
        {
            try
            {
                //string para guardar la ruta del archivo
                string filePath = string.Empty;

                //Condición para saber si el archivo llego
                if (fileForm != null)
                {
                    //Ruta de la carpeta que guardara el archivo
                    string path = Server.MapPath("~/uploads/");

                    //Condición para verificar si la rura existe
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    //Obtener el nombre del archivo
                    filePath = path + Path.GetFileName(fileForm.FileName);

                    //Obtener la extensión del archivo
                    string extension = Path.GetExtension(fileForm.FileName);

                    //Guardar el archivo
                    fileForm.SaveAs(filePath);

                    string csvData = System.IO.File.ReadAllText(filePath);

                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            var newProducto = new producto
                            {
                                nombre = row.Split(';')[0],
                                percio_unitario = Convert.ToInt32(row.Split(';')[1]),
                                descripcion = row.Split(';')[2],
                                cantidad = Convert.ToInt32(row.Split(';')[3]),
                                id_proveedor = Convert.ToInt32(row.Split(';')[4])
                            };

                            using (var db = new inventario2021Entities())
                            {
                                db.producto.Add(newProducto);
                                db.SaveChanges();
                            }
                        }      
                    }
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