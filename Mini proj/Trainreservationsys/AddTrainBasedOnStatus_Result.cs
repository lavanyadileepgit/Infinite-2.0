//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trainreservationsys
{
    using System;
    
    public partial class AddTrainBasedOnStatus_Result
    {
        public int trainid { get; set; }
        public string TrainName { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public Nullable<decimal> FirstClassFare { get; set; }
        public Nullable<decimal> SecondClassFare { get; set; }
        public Nullable<decimal> SleeperClassFare { get; set; }
        public Nullable<int> totalberths { get; set; }
        public Nullable<int> availableberths { get; set; }
        public string Status { get; set; }
    }
}
