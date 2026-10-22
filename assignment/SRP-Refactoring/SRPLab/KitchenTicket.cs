
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrpLab;

/// <summary>
/// Manages kitchen order items and coordinates kitchen operations.
/// </summary>
public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

    private readonly AllergenDetector _allergenDetector = new();
    private readonly KitchenEtaCalculator _etaCalculator = new();
    private readonly ThermalTicketFormatter _ticketFormatter = new();
    private readonly ExpoLanePolicy _expoLanePolicy = new();

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((
            item,
            ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(),
            prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        var ingredients = _items.SelectMany(i => i.Ingredients);
        return _allergenDetector.Detect(ingredients);
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        var prepMinutes = _items.Select(i => i.PrepMinutes);
        var hasAllergens = DetectAllergens().Count > 0;

        return _etaCalculator.Calculate(prepMinutes, openStations, hasAllergens);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var items = _items.Select(
            i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)");

        var allergens = DetectAllergens();
        var eta = EstimatedReadyMinutes(2);

        return _ticketFormatter.Format(orderNumber, items, eta, allergens);
    }

    public string ExpoLaneHint()
    {
        var hasAllergens = DetectAllergens().Count > 0;
        var eta = EstimatedReadyMinutes(2);

        return _expoLanePolicy.GetLane(hasAllergens, eta);
    }
}
