using FluentValidation;
using LojaGames.Model;

namespace LojaGames.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {

            RuleFor(p => p.tipo)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

        }
    }
}
