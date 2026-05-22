namespace OrderPipeline;

public class Pipeline<TIn, TOut>
{
    private readonly Func<TIn, TOut> action;

    internal Pipeline(Func<TIn, TOut> action) => this.action = action;

    public Pipeline<TIn, TNext> Then<TNext>(Step<TOut, TNext> step)
        => new(input => step.Execute(action(input)));

    public TOut Run(TIn input) => action(input);
}

public static class Pipeline
{
    public static Pipeline<TIn, TOut> Start<TIn, TOut>(Step<TIn, TOut> step)
        => new(step.Execute);
}
