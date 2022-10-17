using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MozaeekCore.Domain.Pricing;
using MozaeekCore.Persistense.EF;

public class SubjectPriceRepository : ISubjectPriceRepository
{
    private readonly CoreDomainContext _context;

    public SubjectPriceRepository(CoreDomainContext context)
    {
        _context = context;
    }

    public async Task CreateSubjectPrice(SubjectPriceHeader subjectPrice)
    {
        await _context.SubjectPriceHeaders.AddAsync(subjectPrice);
    }

    public Task<SubjectPriceHeader> Find(long id)
    {
        return _context.SubjectPriceHeaders.Where(m => m.Id == id)
            .Include(m => m.PriceDetails)
            .FirstOrDefaultAsync();
    }

    public void Update(SubjectPriceHeader subjectPrice)
    {
        _context.SubjectPriceHeaders.Update(subjectPrice);
    }

    public void Delete(SubjectPriceHeader subjectPrice)
    {
        _context.SubjectPriceHeaders.Remove(subjectPrice);
    }
}