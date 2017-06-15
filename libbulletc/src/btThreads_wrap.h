#include "main.h"

#ifdef __cplusplus
extern "C" {
#endif
	EXPORT btITaskScheduler* btThreads_btGetSequentialTaskScheduler();
	EXPORT btITaskScheduler* btThreads_btGetOpenMPTaskScheduler();
	EXPORT btITaskScheduler* btThreads_btGetPPLTaskScheduler();
	EXPORT btITaskScheduler* btThreads_btGetTBBTaskScheduler();
	EXPORT void btThreads_btSetTaskScheduler(btITaskScheduler* ts);
#ifdef __cplusplus
}
#endif
