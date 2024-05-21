namespace ConsigneService.Domain.Common;
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

public enum CustomFieldValidation
{
    Optional=1,
    Mandatory
}