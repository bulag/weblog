using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using weblog.Models;
using weblog.ViewModels;
using System.Data;

namespace weblog.Controllers
{
    public class AccountController : Controller
    {
        ExampleEntities1 db = new ExampleEntities1();
        ExampleEntities2 dc = new ExampleEntities2();
       

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {
            if(avm.account.Username.Equals("abc") && avm.account.Password.Equals("123")){
                Session["username"]=avm.account.Username;
                return View("Create");
            }
            else{
                ViewBag.Error="";
                return View("Index");

            }
            
        }
        public ActionResult Logout()
        {
            Session.Remove("username");
            return RedirectToAction("Index");
        }
        public ActionResult List()
        {
            return View(db.Kısıler.ToList());

        }
        
        //create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ExampleEntities1 db = new ExampleEntities1();
            //Users model = new Users();
            Kısıler model=new Kısıler();
            model.Ad = form["UserName"].Trim();    
            model.Soyad=form["UserSurName"].Trim();
            model.Telefon=form["Telefon"].Trim();
            db.Kısıler.Add(model);
            db.SaveChanges();



            return RedirectToAction("List");
        }
        public ActionResult Edit(int id=0)
        {
            return View(db.Kısıler.Find(id));
            
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(Kısıler k)
        {
            db.Entry(k).State = ExampleEntities1.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }
        //delete
        public ActionResult Delete(int id = 0)
        {
            return View(db.Kısıler.Find(id));
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult delete_conf(int id)
        {
            Kısıler k = db.Kısıler.Find(id);
            db.Kısıler.Remove(k);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Liste()
        {
            return View(dc.Odalar.ToList());
        
        }
         public ActionResult Created()
        {
            return View();
        }
         [HttpPost]
         public ActionResult Created(FormCollection form)
         {
            
             Odalar oda = new Odalar();
             oda.OdaAdı = form["OdaAdı"].Trim();
             oda.OdaSayısı =Convert.ToInt32( form["OdaSayısı"].Trim());
             oda.OdaKapasıtesı= Convert.ToInt32(form["OdaKapasıtesı"].Trim());
             dc.Odalar.Add(oda);
             dc.SaveChanges();



             return RedirectToAction("Liste");
         }
         public ActionResult Editt(int id = 0)
         {
             return View(dc.Odalar.Find(id));

         }
         [HttpPost, ValidateAntiForgeryToken]
         public ActionResult Editt(Odalar o)
         {
             db.Entry(o).State = ExampleEntities2.Modified;
             db.SaveChanges();
             return RedirectToAction("Liste");
         }
        
    }
}
