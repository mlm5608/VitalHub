using WebAPI.Contexts;
using WebAPI.Domains;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        VitalContext ctx = new VitalContext();
        
        public Especialidade BuscarPorNome(string esp)
        {
            return ctx.Especialidades!.FirstOrDefault(x=> x.Especialidade1==esp)!;
        }

        public void Cadastrar(Especialidade especialidade)
        {
            ctx.Especialidades.Add(especialidade);
            ctx.SaveChanges();
        }

        public List<Especialidade> Listar()
        {
            return ctx.Especialidades.ToList();
        }
    }
}
