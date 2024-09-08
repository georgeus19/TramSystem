namespace MissionPlanningService.Lock.Operations; 

public sealed class GetMissionsOperation<TOperationResult> : Operation<TOperationResult> {
	public GetMissionsOperation(Func<Task<TOperationResult>> operationCode) : base(operationCode) {}
	public override bool Mutation => false;
	public override bool Query => !Mutation;
	public override string Name => "GetMissions";
}