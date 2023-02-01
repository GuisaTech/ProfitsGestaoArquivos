using ProfitsChallange.DataContext;
using ProfitsChallange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProfitsChallange.Controllers
{
    public class DocumentController : Controller
    {
        private Context _context;

        public DocumentController() { _context = new Context(); }

        public ActionResult MainPage()
        {
            var list = _context.Document.Where(e => !e.Removed).OrderByDescending(e=> e.ID).ToList();
            ViewBag.ArchivedDocuments = _context.Document.Where(e => e.Removed).OrderByDescending(e=> e.ID).ToList().Count();

            return View(list);
        }

        public ActionResult GetDocumentById(int id)
        {
            var doc = _context.Document.FirstOrDefault(e => !e.Removed && e.ID == id);
            return Json(doc, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SaveNewOrUpdateDocument(Document doc)
        {
            if(doc.ID != 0)
            {
                _context.Document.Attach(doc);
                _context.Entry(doc).Property(e => e.Code).IsModified = true;
                _context.Entry(doc).Property(e => e.Title).IsModified = true;
                _context.Entry(doc).Property(e => e.Review).IsModified = true;
                _context.Entry(doc).Property(e => e.Value).IsModified = true;
                _context.Entry(doc).Property(e => e.PlannedDate).IsModified = true;
            }   
            else
                _context.Document.Add(doc);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("DocumentParcialList");
            }
            catch (Exception e) { return Json(new { msg = "Erro ao tentar salvar os dados.", erro = true }, JsonRequestBehavior.AllowGet); }
        }

        public async Task<ActionResult> ArchiveDocument(int id)
        {
            if (id <= 0)
                return Json(false, JsonRequestBehavior.AllowGet);

            var doc = _context.Document
                .Where(e => e.ID == id && !e.Removed)
                .FirstOrDefault();

            if (doc != null)
            {
                doc.Removed = true;
                _context.Document.Attach(doc);
                _context.Entry(doc).Property(e => e.Removed).IsModified = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("DocumentParcialList");
                }
                catch (Exception e) { return Json(new { msg = "Erro ao tentar arquivar este documento, tente mais tarde.", erro = true }, JsonRequestBehavior.AllowGet); }
            }

            return Json(false, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> UnarchiveDocument(int id)
        {
            if (id <= 0)
                return Json(false, JsonRequestBehavior.AllowGet);

            var doc = _context.Document
                .Where(e => e.ID == id && e.Removed)
                .FirstOrDefault();

            if (doc != null)
            {
                doc.Removed = false;
                _context.Document.Attach(doc);
                _context.Entry(doc).Property(e => e.Removed).IsModified = true;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("DocumentParcialList", new { isArchived = true });
                }
                catch (Exception e) { return Json(new { msg = "Erro ao tentar arquivar este documento, tente mais tarde.", erro = true }, JsonRequestBehavior.AllowGet); }
            }

            return Json(false, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> RemovePermanentlyDocument(int id)
        {
            if(id <= 0)
                return Json(false);

            var doc = _context.Document
                .Where(e => e.ID == id && e.Removed)
                .FirstOrDefault();

            if (doc != null)
            {
                _context.Document.Remove(doc);

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("DocumentParcialList", new { isArchived = true });
                }
                catch (Exception e) { return Json(new { msg = "Erro ao tentar remover este documento, tente mais tarde.", erro = true }, JsonRequestBehavior.AllowGet); }
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocumentParcialList(bool isArchived = false)
        
        {
            return View(_context.Document.Where(e => e.Removed == isArchived).OrderByDescending(e => e.ID).ToList());
        }

    }
}