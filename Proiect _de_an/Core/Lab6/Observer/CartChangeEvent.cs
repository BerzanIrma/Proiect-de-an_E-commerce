namespace Proiect__de_an.Core.Lab6.Observer;

/// <summary>Date trimise observatorilor când se modifică coșul.</summary>
public record CartChangeEvent(int TotalItems, string DeliveryType);
