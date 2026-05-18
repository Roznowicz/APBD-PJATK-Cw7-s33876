using cw7.Data;
using cw7.DTOs;
using cw7.Models;
using Microsoft.EntityFrameworkCore;

namespace cw7.Services;

public class PCService : IPCService
{
    private readonly AppDbContext _context;

    public PCService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcGetDto>> GetPcs()
    {
        return await _context.Pcs
            .Include(x => x.PcComponents)
            .ThenInclude(x => x.Component)
            .Select(x => new PcGetDto
            {
                Id = x.Id,
                Name = x.Name,
                Weight = x.Weight,
                Warranty = x.Warranty,
                CreatedAt = x.CreatedAt,

                Components = x.PcComponents
                    .Select(pc => new ComponentDto
                    {
                        Code = pc.Component.Code,
                        Name = pc.Component.Name,
                        Amount = pc.Amount
                    }).ToList()
            })
            .ToListAsync();
    }
    public async Task<List<ComponentDto>?> GetComponents(int id)
    {
        var pc = await _context.Pcs
            .Include(x => x.PcComponents)
            .ThenInclude(x => x.Component)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (pc == null)
            return null;

        return pc.PcComponents
            .Select(x => new ComponentDto
            {
                Code = x.Component.Code,
                Name = x.Component.Name,
                Amount = x.Amount
            }).ToList();
    }
    public async Task<int> Create(CreatePcDto dto)
    {
        var pc = new Pc
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.Pcs.Add(pc);

        await _context.SaveChangesAsync();

        return pc.Id;
    }
    public async Task<bool> Update(int id, UpdatePcDto dto)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc == null)
            return false;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<bool> Delete(int id)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc == null)
            return false;

        _context.Pcs.Remove(pc);

        await _context.SaveChangesAsync();

        return true;
    }
}