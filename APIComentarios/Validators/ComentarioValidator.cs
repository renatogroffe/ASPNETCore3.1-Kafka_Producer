using FluentValidation;
using APIComentarios.Models;

namespace APIComentarios.Validators
{
    public class ComentarioValidator : AbstractValidator<Comentario>
    {
        public ComentarioValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Preencha o campo 'Nome'")
                .MinimumLength(5).WithMessage("O campo 'Nome' deve possuir no mínimo 5 caracteres")
                .MaximumLength(50).WithMessage("O campo 'Nome' deve possuir no máximo 50 caracteres");

            RuleFor(c => c.Mensagem).NotEmpty().WithMessage("Preencha o campo 'Mensagem'")
                .MinimumLength(20).WithMessage("O campo 'Mensagem' deve possuir no mínimo 20 caracteres")
                .MaximumLength(1000).WithMessage("O campo 'Mensagem' deve possuir no máximo 1000 caracteres");
        }
    }
}