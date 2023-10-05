using Telerik.Maui.Controls.Compatibility.DataGrid;

namespace Fleet.Management.Converters;

public class StockOutLineAlertStyleSelector : DataGridStyleSelector
{
    public DataGridStyle NegativeCellTemplate { get; set; }
    public DataGridStyle PositiveCellTemplate { get; set; }
    public override DataGridStyle SelectStyle(object item, BindableObject container)
    {
        if (item is DataGridCellInfo cellInfo)
        {
            if ((cellInfo.Item is StockOutLinePayload outLinePayload) && !outLinePayload.CanApprove)
            {
                return NegativeCellTemplate;
            }
            else
            {
                return PositiveCellTemplate;
            }
        }
        return base.SelectStyle(item, container);
    }
}
