using Microsoft.EntityFrameworkCore;
using TEST_DEV_DAAR_03062025.Data;

namespace TEST_DEV_DAAR_03062025.Services;
public interface IPersonaService
{
    Task<bool> ExistePersonaConRFC(string rfc);
}

public class PersonaService(ApplicationDbContext context) : IPersonaService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> ExistePersonaConRFC(string rfc)
    {
        return await _context.PersonasFisicas.AnyAsync(p => p.RFC == rfc);
    }
}
