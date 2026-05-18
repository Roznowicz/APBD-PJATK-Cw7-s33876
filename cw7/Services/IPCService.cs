using cw7.DTOs;

namespace cw7.Services;

public interface IPCService
{
    Task<List<PcGetDto>> GetPcs();

    Task<List<ComponentDto>?> GetComponents(int id);

    Task<int> Create(CreatePcDto dto);

    Task<bool> Update(int id, UpdatePcDto dto);

    Task<bool> Delete(int id);
}