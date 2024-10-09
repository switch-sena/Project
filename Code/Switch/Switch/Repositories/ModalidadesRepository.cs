using SwitchBack.Models;
using SwitchBack.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SwitchBack.Repositories
{
    public class ModalidadesRepository : IModalidadesRepository
    {
        private readonly SwitchContext context;
        public ModalidadesRepository(SwitchContext context)
        {
            this.context = context;
        }

        public async Task<List<Modalidades>> GetModalidades()
        {
            var data = await context.Modalidades.ToListAsync();
            return data;
        }

        public async Task<bool> PostModalidades(Modalidades modalidades)
        {
            await context.Modalidades.AddAsync(modalidades);
            await context.SaveAsync();
            return true;
        }
    }
}
