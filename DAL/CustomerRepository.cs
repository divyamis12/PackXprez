using Infosys.PackXpreZ.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infosys.PackXpreZ.DataAccessLayer
{
    public class CustomerRepository
    {

        private PackXprezDB1Context Context { get; set; }

        public CustomerRepository()
        {
            Context = new PackXprezDB1Context();
        }

        public bool RegisterUser(CustomerDetails customer)
        {
            bool status = false;

            try
            {
                Context.CustomerDetails.Add(customer);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }

        public bool AddAddress(Address addr)
        {
            bool status = false;

            try
            {
                Context.Address.Add(addr);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool LoginUser(string emailId,string password)
        {
            bool status=false;
            try
            {
                var userObj = (from usr in Context.CustomerDetails
                               where usr.EmailId == emailId && usr.Password == password
                               select usr).FirstOrDefault<CustomerDetails>();

                if (userObj != null)
                {
                    status = true;
                }
                else
                {
                    status =false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool EditUserDetails(CustomerDetails Obj)
        {
            bool status = false;
            try
            {
                var custObj = Context.CustomerDetails.Find(Obj.EmailId);
                if (custObj != null)
                {
                    custObj.CustomerName = Obj.CustomerName;
                    custObj.ContactNo = Obj.ContactNo;
                    Context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool changePassword(CustomerDetails Obj)
        {
            bool status = false;
            try
            {
                var custObj = Context.CustomerDetails.Find(Obj.EmailId);
                if (custObj != null)
                {
                    custObj.Password= Obj.Password;
                    Context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        //public bool ScheduleShipment(string emailId, string receiverAddr, int packageCost, bool packagingRequired, bool insurance,string deliveryStatus)
        //{
        //    bool status = false;

        //    try
        //    {
        //        var addrId = (from addr in Context.Address where addr.EmailId == emailId select addr.AddressId).FirstOrDefault();
        //        PackageDetails package = new PackageDetails();
        //        package.EmailId = emailId;
        //        package.SenderAddr = ;
        //        package.ReceiverAddr = receiverAddr;
        //        package.PackageCost = packageCost;
        //        package.PackagingRequired = packagingRequired;
        //        package.DeliveryStatus = deliveryStatus;
        //        package.Insurance = insurance;

        //        Context.PackageDetails.Add(package);
        //        Context.SaveChanges();
        //        status = true;
        //    }
        //    catch (Exception)
        //    {
        //        status = false;
        //    }
        //    return status;

        //}


        public bool ScheduleShipment(PackageDetails package)
        {
            bool status = false;

            try
            {
                Context.PackageDetails.Add(package);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        public List<PackageDetails> PackageHistory(string emailId)
        {
            List<PackageDetails> historyList = new List<PackageDetails>();
            try
            {
                historyList = (from package in Context.PackageDetails where package.EmailId == emailId select package).ToList();

            }
            catch (Exception)
            {
                historyList = null;
            }
            return historyList;
        }



        public string TrackShipmentStatus(int awbNumber)
        {
            string status = "";
            try
            {
                status = (from package in Context.PackageDetails where package.Awbnumber == awbNumber select package.DeliveryStatus).FirstOrDefault();
            }
            catch (Exception)
            {
                status = null;
            }
            return status;
        }


        public bool CustomerFeedback(FeedbackDetails feedback)
        {
            bool status = false;
            try
            {
                Context.FeedbackDetails.Add(feedback);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }


        public bool CheckServiceAvailability(decimal pincode1,decimal pincode2)
        {
            bool status = false;
            try
            {
                var code1 = Context.PincodeDetails.Find(pincode1);
                var code2 = Context.PincodeDetails.Find(pincode2);
                if (code1 != null && code2!=null)
                {
                    status = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                status = false;
            }
            return status;
        }


        public List<Address> GetAllAddress(string emailId)
        {
            List<Address> custAddr = new List<Address>();
            try
            {
                custAddr = (from addr in Context.Address where addr.EmailId == emailId select addr).ToList();
            }
            catch (Exception)
            {
                custAddr = null;
            }
            return custAddr; 
        }

        public Address GetAddressById(int addrId)
        {
            Address addr = new Address();
            try
            {
                addr = Context.Address.Find(addrId);
      
            }
            catch (Exception)
            {
                addr = null;
            }
            return addr;
        }

        public decimal Distance(decimal pincode1,decimal pincode2)
        {
            decimal dist = 0;
            try
            {
                dist = (from d in Context.Distance where d.Pincode1 == pincode1 && d.Pincode2 == pincode2 select d.Distance1).FirstOrDefault();
            }
            catch (Exception)
            {
                dist = 0;
            }
            return dist;
        }

        public CustomerDetails GetUserDetails(string emailId)
        {
            CustomerDetails custObj;
            try
            {
                custObj = (from cust in Context.CustomerDetails where cust.EmailId == emailId select cust).FirstOrDefault();
            }
            catch (Exception)
            {
                custObj = null;
            }
            return custObj;
        }
        public bool removeaddress(int AddressId)
        {
            bool status = false;
            var temp = (from address in Context.Address where address.AddressId == AddressId select address).FirstOrDefault();
            try
            {
                Context.Address.Remove(temp);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        
    }
}
