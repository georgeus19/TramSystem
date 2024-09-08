namespace MissionPlanningService.Lock.Operations; 

public sealed class PlanMissionOperation<TOperationResult> : Operation<TOperationResult> {
	public PlanMissionOperation(Func<Task<TOperationResult>> operationCode) : base(operationCode) {}
	public override bool Mutation => true;
	public override bool Query => !Mutation;
	public override string Name => "PlanMission";
}