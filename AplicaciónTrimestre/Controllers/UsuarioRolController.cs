using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicaciónTrimestre.Models;

namespace AplicaciónTrimestre.Controllers
{
    public class UsuarioRolController : Controller
    {
        // GET: UsuarioRol
        public ActionResult Index()
        {
            using (var db = new inventario2021Entities())
            {
                return View(db.usuariorol.ToList());
            }
        }

        public static string NombreUsuario(int idUsuario)
        {
            using (var db = new inventario2021Entities())
            {
                return db.usuario.Find(idUsuario).nombre;
            }
        }

        public static string DescripcionRol(int idRoles)
        {
            using (var db = new inventario2021Entities())
            {
                return db.roles.Find(idRoles).descripcion;
            }
        }

        public ActionResult ListarUsuarios()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.usuario.ToList());
            }
        }

        public ActionResult ListarRoles()
        {
            using (var db = new inventario2021Entities())
            {
                return PartialView(db.roles.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuariorol usuarioRol)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new inventario2021Entities())
                {
                    db.usuariorol.Add(usuarioRol);
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
                return View(db.usuariorol.Find(id));
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuariorol usuarioRolEdit = db.usuariorol.Where(a => a.id == id).FirstOrDefault();
                    return View(usuarioRolEdit);
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
        public ActionResult Edit(usuariorol usuarioRolEdit)
        {
            try
            {
                using (var db = new inventario2021Entities())
                {
                    usuariorol oldUsuariorol = db.usuariorol.Find(usuarioRolEdit.id);

                    oldUsuariorol.idUsuario = usuarioRolEdit.idUsuario;
                    oldUsuariorol.idRol = usuarioRolEdit.idRol;

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
                    usuariorol usuariorol = db.usuariorol.Find(id);
                    db.usuariorol.Remove(usuariorol);
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
    }
}