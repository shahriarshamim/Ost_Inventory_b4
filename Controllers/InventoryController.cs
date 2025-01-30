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
            if (Session["Message"] == null)
                Session["Message"] = "";
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
                Session["Message"] = "Save Successfully";
            }
            else
            {
                Session["Message"] = "Error Occured";
            }
            BaseEquipment baseEquipment = new BaseEquipment();
            List<BaseEquipment> equipments = baseEquipment.LstEquipment();

            ViewBag.Equipment = equipments;

            return View();
        }        
       
        public ActionResult CustomerAssignment()
        {
            if (Session["Message"] == null)
                Session["Message"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult CustomerAssignment(FormCollection frmColl)
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            int ddlCustomer = Convert.ToInt32(frmColl["ddlCustomer"].ToString());
            int ddlEquipment = Convert.ToInt32(frmColl["ddlEquipment"].ToString());
            int txtEquiCount = Convert.ToInt32(frmColl["txtEquiCount"].ToString());

            List<BaseEquipment> LstEquipment = baseEquipment.LstEquipment();
            ///var instock = (from a in LstEquipment where a.EquipmentId == ddlCustomer select a.Stock).FirstOrDefault();
            var instock = (from a in LstEquipment where a.EquipmentId == ddlEquipment select a.Stock).FirstOrDefault();
            bool status = true;
            if (instock != null)
            {
                if (txtEquiCount > Convert.ToInt32(instock))
                {
                    status = false;
                }
            }
            if (status)
            {
                baseEquipment.SaveCustomerEquipmentAssignment(frmColl);
                Session["Message"] = "Save Successfully";
                return RedirectToAction("Dashboard", "Inventory");
            }
            else
            {
                Session["Message"] = "Requested Quantity not Available";
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddCustomer(FormCollection formCollection)
        {
           
            BaseCustomer baseCustomerNew = new BaseCustomer();
            baseCustomerNew.CustomerName = formCollection["txtCustomerName"].ToString();
            baseCustomerNew.CustomerMobile = formCollection["txtCustomerMobile"].ToString();
            baseCustomerNew.CustAddress = formCollection["txtCustomerAddress"].ToString();

            if (baseCustomerNew.SaveCustomer())
            {
                Session["Message"] = "Save Successfully";
            }
            else
            {
                Session["Message"] = "Error Occured";
            }
            return RedirectToAction("Dashboard", "Inventory");
        }


        public ActionResult CustomerAssignmentReturn()
        {
            if (Session["Message"] == null)
                Session["Message"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult CustomerAssignmentReturn(FormCollection frmColl)
        {
            BaseEquipment baseEquipment = new BaseEquipment();
            int ddlCustomer = Convert.ToInt32(frmColl["ddlCustomer"].ToString());
            int ddlEquipment = Convert.ToInt32(frmColl["ddlEquipment"].ToString());
            int txtEquiCount = Convert.ToInt32(frmColl["txtEquiCount"].ToString());

            ////List<BaseEquipment> LstEquipment = baseEquipment.LstEquipment();
            int inhand = baseEquipment.GetEquipCountByCust(ddlCustomer,ddlEquipment);
            ///var instock = (from a in LstEquipment where a.EquipmentId == ddlCustomer select a.Stock).FirstOrDefault();
            ////var instock = (from a in LstEquipment where a.EquipmentId == ddlEquipment select a.Stock).FirstOrDefault();
            bool status = true;
            if (inhand != 0)
            {
                if (txtEquiCount >inhand)
                {
                    status = false;
                }
            }
            if (status)
            {
                baseEquipment.SaveCustomerEquipmentAssignmentReturn(frmColl);
                Session["Message"] = "Update Successfully";
                return RedirectToAction("Dashboard", "Inventory");
            }
            else
            {
                Session["Message"] = "Requested Quantity not Available";
                return View();
            }
        }
    }
}