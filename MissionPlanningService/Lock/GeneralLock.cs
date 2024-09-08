using MissionPlanningService.Lock.Operations;

namespace MissionPlanningService.Lock; 

/// <summary>
/// General locking mechanism for allowing only one operation to be run at one time.
/// </summary>
public class GeneralLock : MissionPlanningLock {
	private static volatile int _lock;

	private const int Locked = 1;
	private const int Unlocked = 0;

	private readonly ILogger _logger;

	public GeneralLock(ILogger logger) {
		_lock = Unlocked;
		_logger = logger;
	}

	public async Task<TOperationResult?> RunOperation<TOperationResult>(Operation<TOperationResult> operation) {
		// Check if lock is unlocked (Unlocked is returned by Exchange) and lock it.
		if (Interlocked.Exchange(ref _lock, Locked) == Unlocked) {

			_logger.LogInformation($"Operation {operation.Name} acquires lock and is running.");
			// The lock must be released even if the operation fails --> include try-catch block.
			try {
				TOperationResult result = await operation.OperationCode();
				_logger.LogInformation($"Operation {operation.Name} releases the lock.");
				// Unlock the lock;
				Interlocked.Exchange(ref _lock, Unlocked);
				return result;
			} catch {
				_logger.LogInformation($"Operation {operation.Name} has thrown an exception. Lock is released.");
				// Unlock the lock;
				Interlocked.Exchange(ref _lock, Unlocked);
				throw;
			}
		} else {
			_logger.LogInformation($"Operation {operation.Name} does not acquire lock.");
			return default(TOperationResult?);
		}
	}
}