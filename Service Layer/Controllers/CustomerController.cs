using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infosys.PackXpreZ.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Infosys.PackXperZ.Services.Models;

namespace Infosys.PackXperZ.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : Controller
    {
        CustomerRepository repos;
        public CustomerController()
        {
            repos = new CustomerRepository();
        }


        [HttpPost]
        public bool Register(CustomerDetails obj)
        {
            bool status = false;
            try
            {
                PackXpreZ.DataAccessLayer.Models.CustomerDetails CustObj = new PackXpreZ.DataAccessLayer.Models.CustomerDetails();
                CustObj.EmailId = obj.EmailId;
                CustObj.CustomerName = obj.CustomerName;
                CustObj.Password = obj.Password;
                CustObj.ContactNo = obj.ContactNo;
                status = repos.RegisterUser(CustObj);
                PackXpreZ.DataAccessLayer.Models.Address addObj = new PackXpreZ.DataAccessLayer.Models.Address();
                addObj.EmailId = obj.EmailId;
                addObj.BuildingNo = obj.BuildingNo;
                addObj.StreetName = obj.StreetName;
                addObj.Locality = obj.Locality;
                addObj.Pincode = obj.Pincode;
                status = repos.AddAddress(addObj);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpPost]
        public bool ValidateUserCredentials(string Emailid, string Password)
        {
            bool status = false;
            try
            {
                status = repos.LoginUser(Emailid, Password);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        [HttpPut]
        public bool updateDetails(CustomerDetails obj)
        {
            bool status = false;
            try
            {
                PackXpreZ.DataAccessLayer.Models.CustomerDetails custObj = new PackXpreZ.DataAccessLayer.Models.CustomerDetails();
                custObj.EmailId = obj.EmailId;
                custObj.CustomerName = obj.CustomerName;
                custObj.Password = obj.Password;
                custObj.ContactNo = obj.ContactNo;
                status = repos.EditUserDetails(custObj);
            }
            catch (Exception ex)
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
                CustObj.SenderAddr = packObj.SenderAddr ;
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

        [HttpPost]
        public bool AddAddress(Address obj)
        {
            bool status = false;
            try
            {
                PackXpreZ.DataAccessLayer.Models.Address addObj = new PackXpreZ.DataAccessLayer.Models.Address();
                addObj.EmailId = obj.EmailId;
                addObj.BuildingNo = obj.BuildingNo;
                addObj.StreetName = obj.StreetName;
                addObj.Locality = obj.Locality;
                addObj.Pincode = obj.Pincode;
                status = repos.AddAddress(addObj);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        [HttpGet]
        public JsonResult GetHistory(string emailId)
        {
            List<shipmentHistory> packList = new List<shipmentHistory>();
            try
            {
                var history = repos.PackageHistory(emailId);
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

        [HttpPut]
        public bool changePass(CustomerDetails obj)
        {
            bool status = false;
            try
            {
                PackXpreZ.DataAccessLayer.Models.CustomerDetails custObj = new PackXpreZ.DataAccessLayer.Models.CustomerDetails();
                custObj.EmailId = obj.EmailId;
                custObj.CustomerName = obj.CustomerName;
                custObj.Password = obj.Password;
                custObj.ContactNo = obj.ContactNo;
                status = repos.changePassword(custObj);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        [HttpGet]
        public string tracking(int awbNumber)
        {
            string status="";
            try
            {
                status = repos.TrackShipmentStatus(awbNumber);
            }
            catch (Exception)
            {
                status=null;
            }
            return status;
        }

        [HttpPost]
        public bool feedback(FeedbackDetails feedObj)
        {
            bool status = false;
            try
            {
                PackXpreZ.DataAccessLayer.Models.FeedbackDetails CustObj = new PackXpreZ.DataAccessLayer.Models.FeedbackDetails();
                CustObj.EmailId = feedObj.EmailId;
                CustObj.FeedBackType = feedObj.FeedBackType;
                CustObj.FeedBackText = feedObj.FeedBackText;
                status = repos.CustomerFeedback(CustObj);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        [HttpDelete]
        public bool removeadd(int AddressId)
        {
            bool status = false;
            try
            {
                status = repos.removeaddress(AddressId);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        

        [HttpPost]
        public bool CheckServiceAvailability(decimal pincode1, decimal pincode2)
        {
            bool status = false;
            try
            {
                status = repos.CheckServiceAvailability(pincode1,pincode2);
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        [HttpGet]
        public JsonResult allAddress(string emailId)
        {
            List<Address> addSerList = new List<Address>();
            try
            {
                var addlist = repos.GetAllAddress(emailId);
                foreach (var item in addlist)
                {
                    PackXperZ.Services.Models.Address add = new PackXperZ.Services.Models.Address();
                    add.AddressId = item.AddressId;
                    add.BuildingNo = item.BuildingNo;
                    add.StreetName = item.StreetName;
                    add.Locality = item.Locality;
                    add.Pincode = item.Pincode;

                    addSerList.Add(add);
                }
                return Json(addSerList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
         
        }


        [HttpGet]
        public decimal getDistance(decimal pincode1, decimal pincode2)
        { 
            try
            {
                var distance = repos.Distance(pincode1,pincode2);
                
                return distance;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        [HttpGet]
        public JsonResult GetUserDetails(string emailId)
        {
            try
            {
                var details = repos.GetUserDetails(emailId);
                return Json(details);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }


        [HttpGet]
        public JsonResult GetAddressById(int AddressId)
        {
             //Address addr = new Address();
            try
            {
                var addr = repos.GetAddressById(AddressId);
                
                    PackXperZ.Services.Models.Address add = new PackXperZ.Services.Models.Address();
                    add.AddressId = addr.AddressId;
                    add.BuildingNo = addr.BuildingNo;
                    add.StreetName = addr.StreetName;
                    add.Locality = addr.Locality;
                    add.Pincode = addr.Pincode;

        
                
                return Json(add);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
    }
}