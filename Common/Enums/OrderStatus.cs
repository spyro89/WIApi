namespace Common.Enums
{
    public enum OrderStatus
    {
        Created = 1,
        Canceled = 2,
        Finished = 3
    }

    public enum OrderChangeStatusResult
    {
        Ok,
        OrderNotFound,
        StatusChangeNotAllowed
    }

    public enum OrderAddResult
    {
        Ok,
        ProductQuantityValidationFailed,
        ProductDoesNotExist
    }
}
