using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infosys.PackXperZ.Services.Models;
using Infosys.PackXpreZ.DataAccessLayer;

namespace Infosys.PackXperZ.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BranchController : Controller
    {
        BranchRepository repos;
        public BranchController()
        {
            repos = new BranchRepository();
        }


        [HttpPost]
        public bool ValidateBranchCredentials(string Emailid, string Password)
        {
            bool status = false;
            try
            {
                status = repos.ValidateUser(Emailid, Password);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        [HttpPost]
        public bool ScheduleShipment(PackageDetails packObj)
        {
            bool status = false;
            try
            {
                PackXpreZ.DataAccessLayer.Models.PackageDetails CustObj = new PackXpreZ.DataAccessLayer.Models.PackageDetails();
                CustObj.EmailId = packObj.EmailId;
                CustObj.SenderAddr = packObj.SenderAddr;
                CustObj.ReceiverAddr = packObj.ReceiverAddr;
                CustObj.PackageCost = packObj.PackageCost;
                CustObj.PackagingRequired = packObj.PackagingRequired;
                CustObj.DeliveryStatus = packObj.DeliveryStatus;
                CustObj.Insurance = packObj.Insurance;
                status = repos.ScheduleShipment(CustObj);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;

        }


        [HttpGet]
        public JsonResult allFeedback()
        {
            List<FeedbackDetails> feed = new List<FeedbackDetails>();
            try
            {
                var feedList = repos.GetAllFeedBack();
                foreach (var item in feedList)
                {
                    PackXperZ.Services.Models.FeedbackDetails feed1 = new PackXperZ.Services.Models.FeedbackDetails();
                    feed1.EmailId = item.EmailId;
                    feed1.FeedBackType = item.FeedBackType;
                    feed1.FeedBackText = item.FeedBackText;

                    feed.Add(feed1);
                }
                return Json(feed);
            }
            catch (Exception ex)
            {
                feed = null;
                return Json(ex.Message);
            }

        }


        [HttpPut]
        public Boolean Pickedup(int TransitionId)
        {
            bool status = false;
            try
            {
                status = repos.pickedup(TransitionId);
            }
            catch (Exception) { status = false; }
            return status;
        }

        [HttpPut]
        public Boolean Pickupfailed(int TransitionId)
        {
            bool status = false;
            try
            {
                status = repos.pickupfailed(TransitionId);
            }
            catch (Exception) { status = false; }
            return status;
        }

        [HttpPut]
        public Boolean Delivered(int TransitionId)
        {
            bool status = false;
            try
            {
                status = repos.delivered(TransitionId);
            }
            catch (Exception) { status = false; }
            return status;
        }

        [HttpPut]
        public Boolean NotDelivered(int TransitionId)
        {
            bool status = false;
            try
            {
                status = repos  .notdelivered(TransitionId);
            }
            catch (Exception) { status = false; }
            return status;
        }

        [HttpGet]
        public JsonResult GetHistorybranch()
        {
            List<shipmentHistory> packList = new List<shipmentHistory>();
            try
            {
                var history = repos.PackageHistorybranch();
                foreach (var item in history)
                {
                    PackXperZ.Services.Models.shipmentHistory pack = new PackXperZ.Services.Models.shipmentHistory();
                    pack.TransitionId = item.TransitionId;
                    pack.Awbnumber = item.Awbnumber;
                    pack.SenderAddr = item.SenderAddr;
                    pack.ReceiverAddr = item.ReceiverAddr;
                    pack.DeliveryStatus = item.DeliveryStatus;
                    packList.Add(pack);
                }
                return Json(packList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
        
    }

}