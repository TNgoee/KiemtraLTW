using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kiemtra.Models
{
    public interface IBadmintonRepository
    {
        // GiaiDau
        Task<IEnumerable<GiaiDau>> GetAllGiaiDauAsync();
        Task<GiaiDau> GetGiaiDauByIdAsync(string id);
        Task AddGiaiDauAsync(GiaiDau entity);
        Task UpdateGiaiDauAsync(GiaiDau entity);
        Task DeleteGiaiDauAsync(string id);

        // Có thể bổ sung các phương thức cho các entity khác tương tự
    }
}