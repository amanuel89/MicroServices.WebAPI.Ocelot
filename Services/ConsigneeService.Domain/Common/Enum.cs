namespace ConsigneeService.Domain.Common;
public enum RecordStatus
{
    InActive = 1,
    Active = 2,
    Deleted = 3
}


public enum CustomFieldTypes
{
    Text = 1,
    Int = 2,
    ComboBox = 3
}


public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded,
    Cancelled
}
public enum AddressType
{
    Country = 1,
    Region = 2,
    City,
    SubCity,
    Woreda,
    Kebele,
}



public enum IssueType
{
    In=1,
    Out
}
public enum IssueReason
{
    Order=1,
    InventoryCount,
    Other, 
    StockAdjustment, 
    StockTransfer,
    Beginning
}

public enum QuotationType
{
    Sales,
    Purchase
}

public enum DiscountType
{
    NoDiscount,
    Percent,
    Constant
}

public enum SortProductBy
{
    Default=1,
    Review,
    Date,
    Price
}

public enum DefaultFilter
{
    Daily = 0,
    Weekly = 1,
    Monthly = 2,
    Anualy = 3,
    EndoftheDay = 4,
    Custom = 5,
    All = 6
}

public enum SortDirection
{
    ASC,
    DESC
}

public enum StaffRoles
{
    Admin,
    Dispatcher
}

public enum DriverStatus
{
    PENDING,
    APPROVED,
    BLOCKED,
    DELETED
}

public enum Gender
{
    Male,
    Female
}

public enum StaffStatus
{
    Active,
    Blocked
}

public enum TransactionStatus
{
    PENDING,
    COMPLETED,
    FAILED,
    CANCELLED
}

public enum IMAGECATEGORY
{
DRIVERSPHOTO,
CUSTOMERPHOTO,
LIBRE,
LICENSE,
INSURANCE,
VEHICLES,
DOCUMENTS,
OTHER
}


public enum RideStatus
{
    Pending,
    Accepted,
    InProgress,
    Completed,
    Cancelled
}
