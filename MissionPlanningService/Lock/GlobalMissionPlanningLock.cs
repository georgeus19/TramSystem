using System.Runtime.InteropServices.ComTypes;
using MissionPlanningService.Lock.Operations;

namespace MissionPlanningService.Lock; 

/// <summary>
/// Lock that lets run only one mutation operation at one moment.
/// </summary>
public class GlobalMissionPlanningLock : MissionPlanningLock {
	private readonly GeneralLock _lock;

	public GlobalMissionPlanningLock(ILogger<GlobalMissionPlanningLock> logger) {
		_lock = new GeneralLock(logger);
	}

	public async Task<TOperationResult?> RunOperation<TOperationResult>(Operation<TOperationResult> operation) {
		if (operation.Query) {
			return await operation.OperationCode();
		}

		return await _lock.RunOperation(operation);
	}
}