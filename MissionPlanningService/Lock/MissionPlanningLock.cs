using MissionPlanningService.Lock.Operations;

namespace MissionPlanningService.Lock;

/// <summary>
/// Interface for implementing locks that say how in parallel can certain operations run.
/// </summary>
public interface MissionPlanningLock {
	public Task<TOperationResult?> RunOperation<TOperationResult>(Operation<TOperationResult> operation);
}