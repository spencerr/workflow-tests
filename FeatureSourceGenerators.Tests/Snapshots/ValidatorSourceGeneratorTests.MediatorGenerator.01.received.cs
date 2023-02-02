﻿//HintName: Create.g.cs
// <auto-generated />
using System.Collections.Generic;
using FluentValidation;


public static partial class Create
{

    internal sealed partial class CommandHandlerImpl : IRequestHandler<Command, Mediator.Unit>
    {
        private readonly ApplicationDbContext _context;,
        private readonly ILogger _logger;

        public CommandHandlerImpl(
            ApplicationDbContext context,
            ILogger logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async System.Threading.Tasks.Task<Mediator.Unit> Handle(Command request, CancellationToken cancellationToken) 
        {
            await CommandHandler(
                request,
                _context,
                _logger
            );
            return Unit.Value;
        }
    }

    public sealed partial record Command
    {
        internal sealed partial class Validator : AbstractValidator<Command>
        {
            public Validator() 
            {
                Command.AddValidation(this);
            }
        }
    }
}