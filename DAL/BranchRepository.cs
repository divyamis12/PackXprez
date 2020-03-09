using Infosys.PackXpreZ.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infosys.PackXpreZ.DataAccessLayer
{
    public class BranchRepository
    {
        private PackXprezDB1Context Context { get; set; }

        public BranchRepository()
        {
            Context = new PackXprezDB1Context();
        }

        public bool ValidateUser(string emailId, string password)
        {
            bool status = false;

            try
            {
                var officerObj = (from officer in Context.OfficerDetails
                                  where officer.OfficerEmail == emailId && officer.Password == password
                                  select officer).FirstOrDefault<OfficerDetails>();
                if(officerObj != null)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                status = false;
            }

            return status;
        }

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


        public List<FeedbackDetails> GetAllFeedBack()
        {
            List<FeedbackDetails> feedback = new List<FeedbackDetails>();
            try
            {
                feedback = (from feed in Context.FeedbackDetails select feed).ToList();
            }
            catch (Exception)
            {
                feedback = null;
            }
            return feedback;
        }

        public bool pickedup(int Transaction)
        {

            bool flag = false;
            int trans;
            trans = (from usr in Context.PackageDetails
                     where usr.TransitionId == Transaction
                     select usr.TransitionId).FirstOrDefault();

            if (trans != 0)
            {
               
                PackageDetails det = Context.PackageDetails.Find(trans);
                det.DeliveryStatus = "Pickup";
                det.Awbnumber = det.TransitionId + 1000001;
                Context.SaveChanges();
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;


        }

        public bool pickupfailed(int Transaction)
        {

            bool flag = false;
            int trans;
            trans = (from usr in Context.PackageDetails
                     where usr.TransitionId == Transaction
                     select usr.TransitionId).FirstOrDefault();

            if (trans != 0)
            {
                PackageDetails det = Context.PackageDetails.Find(trans);
                det.DeliveryStatus = "Pickup Failed";
                det.Awbnumber = null;
                Context.SaveChanges();
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;


        }

        public bool delivered(int Transaction)
        {

            bool flag = false;
            int trans;
            trans = (from usr in Context.PackageDetails
                     where usr.TransitionId == Transaction
                     select usr.TransitionId).FirstOrDefault();

            if (trans != 0)
            {
                DateTime date = DateTime.Now.Date;
                PackageDetails det = Context.PackageDetails.Find(trans);
                det.DeliveryStatus = "Delivered";
                det.DeliveredDate = date;
                Context.SaveChanges();
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;


        }

        public bool notdelivered(int Transaction)
        {

            bool flag = true;
            int trans;
            trans = (from usr in Context.PackageDetails
                     where usr.TransitionId == Transaction
                     select usr.TransitionId).FirstOrDefault();

            if (trans != 0)
            {
                PackageDetails det = Context.PackageDetails.Find(trans);
                det.DeliveryStatus = "Not Delivered";
                Context.SaveChanges();
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;


        }

        public List<PackageDetails> PackageHistorybranch()
        {
            List<PackageDetails> historyList = new List<PackageDetails>();
            try
            {
                historyList = (from package in Context.PackageDetails select package).ToList();

            }
            catch (Exception)
            {
                historyList = null;
            }
            return historyList;
        }
    }

}
