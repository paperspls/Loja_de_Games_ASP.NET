using LojaGames.Model;

namespace LojaGames.Service
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAll();

        Task<Produto?> GetById(long id);

        Task<IEnumerable<Produto>> GetByNomeCon(string nome, string console);

        Task<IEnumerable<Produto>> GetByPreco(decimal numero1, decimal numero2);

        Task<Produto?> Create(Produto produto);

        Task<Produto?> Update(Produto produto);

        Task Delete(Produto produto);
    }
}
