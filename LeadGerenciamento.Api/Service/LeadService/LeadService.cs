
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

public class LeadService : ILeadInterface
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public LeadService(ApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<ServiceResponse<Lead>> AcceptLead(int id)
    {
        ServiceResponse<Lead> response = new ServiceResponse<Lead>();
        try
        {
            Lead lead = await _context.Leads.FirstOrDefaultAsync(lead => lead.ID == id);
            if (lead == null)
            {
                response.Sucesso = false;
                response.Dados = null;
                response.Mensagem = "Lead não encontrada.";
                return response;
            }
            lead.Status = StatusEnum.ACEITO;
            if (lead.Price > 500)
            {
                lead.Price = lead.Price * 0.9m;
            }
            await _emailService.SendAcceptanceEmail(lead);
            _context.Leads.Update(lead);
            await _context.SaveChangesAsync();
            response.Dados = lead;
        }
        catch (Exception ex)
        {
            response.Sucesso = false;
            response.Mensagem = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<Lead>> RejectedLead(int id)
    {
        ServiceResponse<Lead> response = new ServiceResponse<Lead>();
        try
        {
            Lead lead = await _context.Leads.FirstOrDefaultAsync(lead => lead.ID == id);
            if (lead == null)
            {
                response.Sucesso = false;
                response.Dados = null;
                response.Mensagem = "Lead não encontrada.";
                return response;
            }
            lead.Status = StatusEnum.RECUSADO;
            _context.Leads.Update(lead);
            await _context.SaveChangesAsync();
            response.Dados = lead;
        }
        catch (Exception ex)
        {
            response.Sucesso = false;
            response.Mensagem = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<Lead>> CreateLead(Lead novaLead)
    {
        ServiceResponse<Lead> response = new ServiceResponse<Lead>();
        try
        {
            if (novaLead == null)
            {
                response.Sucesso = false;
                response.Dados = null;
                response.Mensagem = "Por favor, informe os dados da lead.";
                return response;
            }
            novaLead.Status = StatusEnum.CONVIDADO;
            _context.Leads.Add(novaLead);
            await _context.SaveChangesAsync();
            response.Dados = novaLead;
        }
        catch (Exception ex)
        {
            response.Sucesso = false;
            response.Mensagem = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<Lead>> DeleteLead(int id)
    {
        ServiceResponse<Lead> response = new ServiceResponse<Lead>();
        try
        {
            Lead lead = await _context.Leads.FirstOrDefaultAsync(lead => lead.ID == id);
            if (lead == null)
            {
                response.Sucesso = false;
                response.Dados = null;
                response.Mensagem = "Lead não encontrada.";
                return response;
            }
            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();
            response.Dados = lead;
        }
        catch (Exception ex)
        {
            response.Sucesso = false;
            response.Mensagem = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<List<Lead>>> GetAllLeads(StatusEnum? status)
    {
        ServiceResponse<List<Lead>> response = new ServiceResponse<List<Lead>>();
        try
        {
            IQueryable<Lead> query = _context.Leads.AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(lead => lead.Status == status.Value);
            }
            response.Dados = await query.ToListAsync();
            if (response.Dados.Count == 0)
            {
                response.Mensagem = "Nenhum lead encontrado.";
            }
        }
        catch (Exception ex)
        {
            response.Sucesso = false;
            response.Mensagem = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<Lead>> GetLeadById(int id)
    {
        ServiceResponse<Lead> response = new ServiceResponse<Lead>();
        try
        {
            Lead lead = await _context.Leads.FirstOrDefaultAsync(lead => lead.ID == id);
            if (lead == null)
            {
                response.Dados = null;
                response.Sucesso = false;
                response.Mensagem = "Lead não encontrada.";
                return response;
            }
            response.Dados = lead;
        }
        catch (Exception ex)
        {
            response.Sucesso = false;
            response.Mensagem = ex.Message;
        }
        return response;
    }

    public async Task<ServiceResponse<Lead>> UpdateLead(Lead leadAtualizada)
    {
        ServiceResponse<Lead> response = new ServiceResponse<Lead>();
        try
        {
            Lead lead = await _context.Leads.AsNoTracking().FirstOrDefaultAsync(lead => lead.ID == leadAtualizada.ID);
            if (lead == null)
            {
                response.Sucesso = false;
                response.Dados = null;
                response.Mensagem = "Lead não encontrada.";
                return response;
            }
            _context.Leads.Update(leadAtualizada);
            await _context.SaveChangesAsync();
            response.Dados = leadAtualizada;
        }
        catch (Exception ex)
        {
            response.Sucesso = false;
            response.Mensagem = ex.Message;
        }
        return response;
    }
}