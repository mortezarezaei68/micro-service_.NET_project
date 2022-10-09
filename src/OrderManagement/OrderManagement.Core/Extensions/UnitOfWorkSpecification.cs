using MassTransit;
using MassTransit.Configuration;

namespace OrderManagement.Core.Extensions;

public class UnitOfWorkSpecification<TConsumer, TMessage> : IPipeSpecification<ConsumerConsumeContext<TConsumer, TMessage>>
    where TConsumer : class
    where TMessage : class
{
    public IEnumerable<ValidationResult> Validate()
    {
        return Enumerable.Empty<ValidationResult>();
    }

    public void Apply(IPipeBuilder<ConsumerConsumeContext<TConsumer, TMessage>> builder)
    {
        builder.AddFilter(new UnitOfWorkFilter<ConsumerConsumeContext<TConsumer, TMessage>, TMessage>());
    }
}