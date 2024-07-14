using FINSHARK.Dtos.Stock;
using FINSHARK.Helpers;
using FINSHARK.Models;

namespace FINSHARK.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock> UpdateAsync(int id , UpdateStockDto updateStockDto);
        Task<Stock> DeleteAsync(int id);
        Task<bool> StockExists(int  id);

    }
}
