using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

using Base.Core.Entities;

[Index(nameof(EmailAddress), IsUnique = true)]
public class Customer : EntityObject
{
    [Display(Name = "Vorname")]
    public string? FirstName { get; set; }

    [Display(Name = "Nachname")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Kreditkartennummer")]
    [CreditCard(ErrorMessage = "Kreditkartennummer ist ungültig")]
    public string CreditCardNumber { get; set; } = string.Empty;

    [Display(Name = "Email-Adresse")]
    [EmailAddress(ErrorMessage = "Email-Adresse ist ungültig")]
    public string EmailAddress { get; set; } = string.Empty;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public string FullName => $"{LastName} {FirstName}";

    public override string ToString()
    {
        return $"{LastName} {FirstName} (Email: {EmailAddress} CC: {CreditCardNumber})";
    }
}