using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestETL.Infrastructure.DTO
{
    public record class CsvDTO
    {
        [Name("VendorID")]
        public string VendorID { get; init; }

        [Name("tpep_pickup_datetime")]
        public string PickupDateTime { get; init; }

        [Name("tpep_dropoff_datetime")]
        public string DropoffDateTime { get; init; }

        [Name("passenger_count")]
        public int? PassengerCount { get; init; }

        [Name("trip_distance")]
        public double TripDistance { get; init; }

        [Name("RatecodeID")]
        public int? RatecodeID { get; init; }

        [Name("store_and_fwd_flag")]
        public string StoreAndForwardFlag { get; init; }

        [Name("PULocationID")]
        public int? PULocationID { get; init; }

        [Name("DOLocationID")]
        public int? DOLocationID { get; init; }

        [Name("payment_type")]
        public int? PaymentType { get; init; }

        [Name("fare_amount")]
        public decimal FareAmount { get; init; }

        [Name("extra")]
        public decimal Extra { get; init; }

        [Name("mta_tax")]
        public decimal MtaTax { get; init; }

        [Name("tip_amount")]
        public decimal TipAmount { get; init; }

        [Name("tolls_amount")]
        public decimal TollsAmount { get; init; }

        [Name("improvement_surcharge")]
        public decimal ImprovementSurcharge { get; init; }

        [Name("total_amount")]
        public decimal TotalAmount { get; init; }

        [Name("congestion_surcharge")]
        public decimal CongestionSurcharge { get; init; }
    }
}
