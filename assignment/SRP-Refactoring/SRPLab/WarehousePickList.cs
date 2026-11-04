using System;
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

/// <summary>
/// Warehouse pick list: stores picking requirements and coordinates operations.
/// </summary>
public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    private readonly StockAllocator _stockAllocator = new();
    private readonly WarehousePathPlanner _pathPlanner = new();
    private readonly PickerScriptFormatter _scriptFormatter = new();
    private readonly WmsXmlFormatter _xmlFormatter = new();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _stockAllocator.Allocate(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _pathPlanner.Plan(_lines);
    }

    public string PickerScript()
    {
        var shortfalls = Allocate()
            .Where(allocation =>
            {
                var need = _lines.First(line => line.Sku == allocation.Sku).QtyNeeded;
                return allocation.Allocated < need;
            })
            .Select(allocation => allocation.Sku);

        return _scriptFormatter.Format(WalkingOrder(), shortfalls);
    }

    public string WmsXmlBatch(string batchId)
    {
        return _xmlFormatter.Format(batchId, Allocate());
    }
}