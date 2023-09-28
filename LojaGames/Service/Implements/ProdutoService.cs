using LojaGames.Data;
using LojaGames.Model;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace LojaGames.Service.Implements
{
    public class ProdutoService : IProdutoService
    {

        private readonly AppDbContext _context;

        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Produto?> GetById(long id)
        {
            try
            {
                var ProdutoUpdate = await _context.Produtos
                    .Include(p => p.Categoria)
                    .FirstAsync(i => i.id == id);

                return ProdutoUpdate;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Produto>> GetByNomeCon(string nome, string console)
        {
            
            var Produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.nome.Contains(nome) || p.console.Contains(console)).ToListAsync();
                
            return Produtos;
        }

        public async Task<IEnumerable<Produto>> GetByPreco(decimal numero1, decimal numero2)
        {
            var Produtos = await _context.Produtos
                .Where(p => p.preco >= numero1 && p.preco <= numero2)
                .Include(p => p.Categoria)
                .ToListAsync();

                return Produtos;          
        }


        public async Task<IEnumerable<Produto>>GetByConsole(string console)
        {
            var Produtos = await _context.Produtos
               .Include(p => p.Categoria)
               .Where(p => p.console.Contains(console)).ToListAsync();
            return Produtos;
        }

        public async Task<Produto?> Create(Produto Produto)
        {
            if (Produto.Categoria is not null)
            {
                var BuscaCategoria = await _context.Categorias.FindAsync(Produto.Categoria.id);

                if (BuscaCategoria is null)
                    return null;
            }
            Produto.Categoria = Produto.Categoria is not null ? _context.Categorias.FirstOrDefault(t => t.id == Produto.Categoria.id) : null;

            await _context.Produtos.AddAsync(Produto);
            await _context.SaveChangesAsync();

            return Produto;
        }

        public async Task<Produto?> Update(Produto Produto)
        {
            var ProdutoUpdate = await _context.Produtos.FindAsync(Produto.id);

            if (ProdutoUpdate is null)
                return null;

            Produto.Categoria = Produto.Categoria is not null ? _context.Categorias.FirstOrDefault(t => t.id == Produto.Categoria.id) : null;

            _context.Entry(ProdutoUpdate).State = EntityState.Detached;
            _context.Entry(Produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Remove(produto);
            await _context.SaveChangesAsync();
        }

        
    }
}
