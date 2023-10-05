namespace Fleet.Parts;

public partial class StockOutLinePayload
{
    public decimal DecimalQuantity => Quantity;
    public decimal DecimalStockQty => StockQty;
    public bool CanApprove => DecimalQuantity <= DecimalStockQty;
}
