public class EmailService : IEmailService
{
    public async Task SendAcceptanceEmail(Lead acceptedLead)
    {
        Console.WriteLine("---- EMAIL ----");
        Console.WriteLine($"Para: vendas@test.com");
        Console.WriteLine($"Assunto: Lead Aceito! (ID: {acceptedLead.ID})");
        Console.WriteLine($"O lead '{acceptedLead.ContactFullName}' foi aceito.");
        Console.WriteLine($"Preço final (com desconto, se aplicável): ${acceptedLead.Price:F2}");
        Console.WriteLine("--------------------");

        await Task.CompletedTask;
    }
}