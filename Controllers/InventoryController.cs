using Ost_Inventory_b4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ost_Inventory_b4.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Dashboard()
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            List<BaseEquipment> equipments = baseEquipment.LstEquipment();

            ViewBag.Equipment = equipments;

            return View();
        }

        [HttpPost]
        public ActionResult Dashboard(FormCollection formCollection)
        {
            BaseEquipment baseEquipmentNew = new BaseEquipment();
            baseEquipmentNew.EquipmentName = formCollection["txtName"].ToString();
            baseEquipmentNew.Quantity = Convert.ToInt32(formCollection["txtQuantity"].ToString());
            baseEquipmentNew.EntryDate = DateTime.ParseExact(formCollection["txtEntryDate"].ToString(), "dd/MM/yyyy", null);
            baseEquipmentNew.ReceiveDate = DateTime.ParseExact(formCollection["txtReceiveDate"].ToString(), "dd/MM/yyyy", null);
            if (baseEquipmentNew.SaveEquipment())
            {
                ViewBag.Message = "Save Successfully";
            }
            else
            {
                ViewBag.Message = "Error Occured";
            }
            BaseEquipment baseEquipment = new BaseEquipment();
            List<BaseEquipment> equipments = baseEquipment.LstEquipment();

            ViewBag.Equipment = equipments;
            return View();
        }
    }
}