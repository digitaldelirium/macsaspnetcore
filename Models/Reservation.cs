using System;

namespace macsaspnetcore.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        private int customerId;

        public enum ReservationType
        {
            RvSpace = 1,
            RvRental,
            Tent
        }
        public ReservationType SiteType { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool ReservationConfirmed { get; set; }

        public int CustomerId
        {
            get
            {
                return customerId;
            }

            set
            {
                customerId = value;
            }
        }
    }
}