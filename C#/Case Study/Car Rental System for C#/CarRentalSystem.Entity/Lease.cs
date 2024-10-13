using System;

public class Lease
{
    public int LeaseID { get; set; }
    public int VehicleID { get; set; }
    public int CustomerID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Type { get; set; }

    // Constructor with optional leaseID (for creating new leases)
    public Lease(int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type)
    {
        this.VehicleID = vehicleID;
        this.CustomerID = customerID;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.Type = type;
    }

    // Constructor with leaseID (for existing leases)
    public Lease(int leaseID, int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type)
    {
        this.LeaseID = leaseID;
        this.VehicleID = vehicleID;
        this.CustomerID = customerID;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.Type = type;
    }
}
