using Microsoft.EntityFrameworkCore;
using Moq;

namespace LeadGerenciamento.Api.Tests;

public class LeadServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly LeadService _leadService;

    public LeadServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);

        _emailServiceMock = new Mock<IEmailService>();

        _emailServiceMock.Setup(s => s.SendAcceptanceEmail(It.IsAny<Lead>()))
            .Returns(Task.CompletedTask);

        _leadService = new LeadService(_context, _emailServiceMock.Object);
    }

    [Fact]
    public async Task AcceptLead_ShouldApply10PercentDiscount_WhenPriceIsOver500()
    {
        var leadId = 1;
        var leadComPrecoAlto = new Lead
        {
            ID = leadId,
            Price = 600.00m,
            Status = StatusEnum.CONVIDADO,
            ContactFirstName = "Teste",
            ContactFullName = "Teste",
            ContactEmail = "a@a.com",
            ContactPhoneNumber = "123",
            Category = "cat",
            Description = "desc",
            Suburb = "sub"
        };

        await _context.Leads.AddAsync(leadComPrecoAlto);
        await _context.SaveChangesAsync();

        var result = await _leadService.AcceptLead(leadId);

        var precoEsperado = 540.00m;

        Assert.True(result.Sucesso);
        Assert.NotNull(result.Dados);
        Assert.Equal(precoEsperado, result.Dados.Price);
        Assert.Equal(StatusEnum.ACEITO, result.Dados.Status);
        _emailServiceMock.Verify(s => s.SendAcceptanceEmail(It.IsAny<Lead>()), Times.Once);
    }

    [Fact]
    public async Task AcceptLead_ShouldNotApplyDiscount_WhenPriceIs500OrLess()
    {
        var leadId = 2;
        var leadComPrecoBaixo = new Lead
        {
            ID = leadId,
            Price = 500.00m,
            Status = StatusEnum.CONVIDADO,
            ContactFirstName = "Teste2",
            ContactFullName = "Teste2",
            ContactEmail = "b@b.com",
            ContactPhoneNumber = "123",
            Category = "cat",
            Description = "desc",
            Suburb = "sub"
        };

        await _context.Leads.AddAsync(leadComPrecoBaixo);
        await _context.SaveChangesAsync();

        var result = await _leadService.AcceptLead(leadId);

        Assert.True(result.Sucesso);

        Assert.Equal(500.00m, result.Dados.Price);
        Assert.Equal(StatusEnum.ACEITO, result.Dados.Status);
        _emailServiceMock.Verify(s => s.SendAcceptanceEmail(It.IsAny<Lead>()), Times.Once);
    }
}