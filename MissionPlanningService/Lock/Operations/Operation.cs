namespace MissionPlanningService.Lock.Operations; 

/// <summary>
/// Represents an operation that can be requested from the service. All code logic should be part of `OperationCode` of
/// some operation so that locking mechanisms can work correctly.
/// </summary>
public abstract class Operation<TOperationResult> {
	/// <summary>
	/// The full callable code of the operation.
	/// </summary>
	public Func<Task<TOperationResult>> OperationCode { get; }

	public Operation(Func<Task<TOperationResult>> operationCode) {
		OperationCode = operationCode;
	}

	public abstract bool Mutation { get; }
	public abstract bool Query { get; }
	public abstract string Name { get; }
}