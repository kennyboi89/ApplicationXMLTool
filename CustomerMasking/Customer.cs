

public class Customer
{
    public int PersonID { get; set; }
    public string SocialSecurity { get; set; }
    public string FirstName { get; set; }
    public string MiddleNames { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int AddressID { get; set; }
    public string Line1 { get; set; }
    public string Line3 { get; set; }
    public string Postcode { get; set; }
    public int UserID { get; set; }
    public int CustomerUserHistoryID { get; set; }
    public int VersionID { get; set; }
    public string CustomerApplicationXML { get; set; }
    public bool Exported { get; set; }
    public DateTime? ExportedDate { get; set; }
}
