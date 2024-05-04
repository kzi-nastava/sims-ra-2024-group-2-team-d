using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class MainService
    {
        private static MainService instance;

        private static readonly object LockObject = new object();

        //public static MainService Instance { get => Instance; set => Instance = value; }

        public TourInstanceService TourInstanceService { get; set; }
        public TourService TourService { get; set; }
        public TourReservationService TourReservationService { get; set; }
        public GiftCardService GiftCardService { get; set; }

        public MainService()
        {
            TourInstanceService = new TourInstanceService();
            TourService = new TourService();
            TourReservationService = new TourReservationService();
            GiftCardService = new GiftCardService();
        }

        public static MainService GetInstance()
        {
            lock(LockObject)
            {
                if (instance == null)
                {
                    instance = new MainService();
                }
            }    

            return instance;
        }
    }
}
