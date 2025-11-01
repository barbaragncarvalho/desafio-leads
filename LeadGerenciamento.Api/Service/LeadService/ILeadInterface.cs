public interface ILeadInterface
{
    Task<ServiceResponse<List<Lead>>> GetAllLeads(StatusEnum? status);
    Task<ServiceResponse<Lead>> CreateLead(Lead novaLead);
    Task<ServiceResponse<Lead>> GetLeadById(int id);
    Task<ServiceResponse<Lead>> UpdateLead(Lead leadAtualizada);
    Task<ServiceResponse<Lead>> DeleteLead(int id);
    Task<ServiceResponse<Lead>> AcceptLead(int id);
    Task<ServiceResponse<Lead>> RejectedLead(int id);
}