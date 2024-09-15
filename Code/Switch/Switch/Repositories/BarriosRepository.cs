using Switch.Models;
using Switch.Repositories.Interfaces;

namespace Switch.Repositories
{
    public class BarriosRepository : IBarriosRepository
    {
        private readonly SwitchContext context;
        public BarriosRepository(SwitchContext context)
        {
            this.context = context;
        }

        public async Task<List<Barrios>> GetBarrios()
        {
            var data = await context.Barrios.ToListAsync();
            return data;
        }

        public async Task<bool> PostBarrios(Barrios barrios)
        {
            await context.Barrios.AddAsync(barrios);
            await context.SaveAsync();
            return true;
        }
    }
}
