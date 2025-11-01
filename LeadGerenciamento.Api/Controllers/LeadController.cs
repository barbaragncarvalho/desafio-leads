using Microsoft.AspNetCore.Mvc;

namespace LeadGerenciamento.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LeadController : ControllerBase
{
    private readonly ILeadInterface _leadInterface;
    public LeadController(ILeadInterface leadInterface)
    {
        _leadInterface = leadInterface;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Lead>>>> GetAllLeads([FromQuery] StatusEnum? status)
    {
        return Ok(await _leadInterface.GetAllLeads(status));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Lead>>> GetLeadById(int id)
    {
        ServiceResponse<Lead> response = await _leadInterface.GetLeadById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Lead>>> CreateLead(Lead novaLead)
    {
        return Ok(await _leadInterface.CreateLead(novaLead));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Lead>>> UpdateLead(Lead leadAtualizada)
    {
        ServiceResponse<Lead> response = await _leadInterface.UpdateLead(leadAtualizada);
        return Ok(response);
    }

    [HttpPut("accept/{id}")]
    public async Task<ActionResult<ServiceResponse<Lead>>> AcceptLead(int id)
    {
        ServiceResponse<Lead> response = await _leadInterface.AcceptLead(id);
        return Ok(response);
    }

    [HttpPut("rejected/{id}")]
    public async Task<ActionResult<ServiceResponse<Lead>>> RejectedLead(int id)
    {
        ServiceResponse<Lead> response = await _leadInterface.RejectedLead(id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<Lead>>> DeleteLead(int id)
    {
        ServiceResponse<Lead> response = await _leadInterface.DeleteLead(id);
        return Ok(response);
    }
}